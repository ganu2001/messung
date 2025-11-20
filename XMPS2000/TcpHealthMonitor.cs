using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.IO;

public class TcpHealthMonitor
{
    private TcpClient tcpClient;
    private NetworkStream stream;
    private Timer healthCheckTimer;
    private bool isMonitoring;
    private TcpConnectionHealth lastHealth;

    public event EventHandler<TcpHealthEventArgs> HealthChanged;
    public event EventHandler<string> HealthAlert;

    public TcpHealthMonitor(TcpClient client)
    {
        tcpClient = client;
        stream = client.GetStream();
        lastHealth = new TcpConnectionHealth();
    }

    // Start continuous health monitoring
    public void StartMonitoring(int intervalMs = 1000)
    {
        if (isMonitoring) return;

        isMonitoring = true;
        healthCheckTimer = new Timer(CheckHealthCallback, null, 0, intervalMs);
        Console.WriteLine($"Health monitoring started (interval: {intervalMs}ms)");
    }

    public void StopMonitoring()
    {
        isMonitoring = false;
        healthCheckTimer?.Dispose();
        Console.WriteLine("Health monitoring stopped");
    }

    // Comprehensive health check
    public TcpConnectionHealth CheckHealth()
    {
        var health = new TcpConnectionHealth
        {
            Timestamp = DateTime.Now,
            IsConnected = IsSocketConnected(),
            CanRead = CanRead(),
            CanWrite = CanWrite(),
            HasErrors = HasSocketErrors(),
            DataAvailable = stream?.DataAvailable ?? false,
            BufferInfo = GetBufferInfo(),
            NetworkStats = GetNetworkStats()
        };

        // Detect specific issues
        health.HasZeroWindow = DetectZeroWindow();
        health.IsStalled = DetectConnectionStall(health);
        health.HasHighLatency = DetectHighLatency();

        // Calculate overall health score (0-100)
        health.HealthScore = CalculateHealthScore(health);
        health.Status = DetermineConnectionStatus(health);

        return health;
    }

    private bool IsSocketConnected()
    {
        try
        {
            if (tcpClient?.Client == null) return false;

            Socket socket = tcpClient.Client;
            return socket.Connected &&
                   !socket.Poll(1, SelectMode.SelectError) &&
                   !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
        }
        catch
        {
            return false;
        }
    }

    private bool CanRead()
    {
        try
        {
            return tcpClient?.Client?.Poll(0, SelectMode.SelectRead) ?? false;
        }
        catch
        {
            return false;
        }
    }

    private bool CanWrite()
    {
        try
        {
            return tcpClient?.Client?.Poll(0, SelectMode.SelectWrite) ?? false;
        }
        catch
        {
            return false;
        }
    }

    private bool HasSocketErrors()
    {
        try
        {
            return tcpClient?.Client?.Poll(0, SelectMode.SelectError) ?? true;
        }
        catch
        {
            return true;
        }
    }

    private BufferInfo GetBufferInfo()
    {
        try
        {
            var socket = tcpClient?.Client;
            if (socket == null) return new BufferInfo();

            return new BufferInfo
            {
                SendBufferSize = socket.SendBufferSize,
                ReceiveBufferSize = socket.ReceiveBufferSize,
                SendBufferFree = GetSendBufferFreeSpace(),
                ReceiveBufferUsed = socket.Available,
                StreamDataAvailable = stream?.DataAvailable ?? false
            };
        }
        catch
        {
            return new BufferInfo();
        }
    }

    private int GetSendBufferFreeSpace()
    {
        try
        {
            // This is an approximation - actual implementation would need platform-specific calls
            var socket = tcpClient.Client;
            return socket.SendBufferSize; // Simplified
        }
        catch
        {
            return 0;
        }
    }

    private NetworkStats GetNetworkStats()
    {
        try
        {
            // Get basic TCP statistics that are actually available
            var tcpStats = IPGlobalProperties.GetIPGlobalProperties().GetTcpIPv4Statistics();

            return new NetworkStats
            {
                BytesSent = 0, // Per-connection tracking not available in standard API
                BytesReceived = 0,
                PacketsRetransmitted = 0, // Not available in .NET TcpStatistics
                ConnectionsReset = (long)tcpStats.ResetConnections,
                CurrentConnections = (int)tcpStats.CurrentConnections
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting network stats: {ex.Message}");
            return new NetworkStats
            {
                BytesSent = 0,
                BytesReceived = 0,
                PacketsRetransmitted = 0,
                ConnectionsReset = 0,
                CurrentConnections = 0
            };
        }
    }

    // Zero window detection
    private bool DetectZeroWindow()
    {
        try
        {
            // Method 1: Try to write a single byte with very short timeout
            if (stream == null) return false;

            var originalTimeout = stream.WriteTimeout;
            stream.WriteTimeout = 10; // 10ms timeout

            try
            {
                // Create a test byte array
                byte[] testData = { 0x00 };

                // Try to write - if it times out immediately, likely zero window
                var writeTask = Task.Run(() => stream.Write(testData, 0, 1));

                if (!writeTask.Wait(50)) // Wait max 50ms
                {
                    return true; // Likely zero window
                }

                return false;
            }
            finally
            {
                stream.WriteTimeout = originalTimeout;
            }
        }
        catch (IOException ex) when (ex.Message.Contains("timeout") ||
                                   ex.Message.Contains("would block"))
        {
            return true; // Definite zero window
        }
        catch
        {
            return false; // Other error, not zero window
        }
    }

    // Connection stall detection
    private bool DetectConnectionStall(TcpConnectionHealth currentHealth)
    {
        if (lastHealth == null) return false;

        // Check if no data movement for extended period
        var timeDiff = currentHealth.Timestamp - lastHealth.Timestamp;

        return timeDiff.TotalSeconds > 30 && // More than 30 seconds
               currentHealth.BufferInfo.ReceiveBufferUsed == lastHealth.BufferInfo.ReceiveBufferUsed &&
               !currentHealth.CanWrite;
    }

    // High latency detection using ping
    private bool DetectHighLatency()
    {
        try
        {
            var endpoint = tcpClient?.Client?.RemoteEndPoint as System.Net.IPEndPoint;
            if (endpoint == null) return false;

            using (var ping = new Ping())
            {
                var reply = ping.Send(endpoint.Address, 1000);
                return reply.Status == IPStatus.Success && reply.RoundtripTime > 500; // >500ms is high
            }
        }
        catch
        {
            return false;
        }
    }

    // Calculate overall health score
    private int CalculateHealthScore(TcpConnectionHealth health)
    {
        int score = 100;

        if (!health.IsConnected) score -= 100;
        if (health.HasErrors) score -= 30;
        if (health.HasZeroWindow) score -= 25;
        if (health.IsStalled) score -= 20;
        if (health.HasHighLatency) score -= 15;
        if (!health.CanWrite) score -= 20;
        if (!health.CanRead && health.DataAvailable) score -= 10;

        return Math.Max(0, score);
    }

    private ConnectionStatus DetermineConnectionStatus(TcpConnectionHealth health)
    {
        if (!health.IsConnected) return ConnectionStatus.Disconnected;
        if (health.HealthScore >= 80) return ConnectionStatus.Healthy;
        if (health.HealthScore >= 60) return ConnectionStatus.Degraded;
        if (health.HealthScore >= 30) return ConnectionStatus.Poor;
        return ConnectionStatus.Critical;
    }

    // Timer callback for continuous monitoring
    private void CheckHealthCallback(object state)
    {
        if (!isMonitoring) return;

        try
        {
            var currentHealth = CheckHealth();

            // Check for status changes
            if (lastHealth?.Status != currentHealth.Status)
            {
                HealthChanged?.Invoke(this, new TcpHealthEventArgs(currentHealth, lastHealth));

                if (currentHealth.Status == ConnectionStatus.Critical ||
                    currentHealth.Status == ConnectionStatus.Disconnected)
                {
                    HealthAlert?.Invoke(this, $"Connection status: {currentHealth.Status}");
                }
            }

            // Check for specific issues
            if (currentHealth.HasZeroWindow && !lastHealth?.HasZeroWindow == true)
            {
                HealthAlert?.Invoke(this, "Zero window condition detected!");
            }

            if (currentHealth.IsStalled && !lastHealth?.IsStalled == true)
            {
                HealthAlert?.Invoke(this, "Connection stall detected!");
            }

            // Log health info
            LogHealthInfo(currentHealth);

            lastHealth = currentHealth;
        }
        catch (Exception ex)
        {
            HealthAlert?.Invoke(this, $"Health check error: {ex.Message}");
        }
    }

    private void LogHealthInfo(TcpConnectionHealth health)
    {
        Console.WriteLine($"[{health.Timestamp:HH:mm:ss}] Health: {health.Status} " +
                         $"(Score: {health.HealthScore}) " +
                         $"Connected: {health.IsConnected}, " +
                         $"CanWrite: {health.CanWrite}, " +
                         $"ZeroWindow: {health.HasZeroWindow}");
    }

    public void Dispose()
    {
        StopMonitoring();
    }
}

// Supporting classes
public class TcpConnectionHealth
{
    public DateTime Timestamp { get; set; }
    public bool IsConnected { get; set; }
    public bool CanRead { get; set; }
    public bool CanWrite { get; set; }
    public bool HasErrors { get; set; }
    public bool DataAvailable { get; set; }
    public bool HasZeroWindow { get; set; }
    public bool IsStalled { get; set; }
    public bool HasHighLatency { get; set; }
    public int HealthScore { get; set; }
    public ConnectionStatus Status { get; set; }
    public BufferInfo BufferInfo { get; set; }
    public NetworkStats NetworkStats { get; set; }
}

public class BufferInfo
{
    public int SendBufferSize { get; set; }
    public int ReceiveBufferSize { get; set; }
    public int SendBufferFree { get; set; }
    public int ReceiveBufferUsed { get; set; }
    public bool StreamDataAvailable { get; set; }
}

public class NetworkStats
{
    public long BytesSent { get; set; }
    public long BytesReceived { get; set; }
    public long PacketsRetransmitted { get; set; }
    public long ConnectionsReset { get; set; }
    public int CurrentConnections { get; set; }
}

public enum ConnectionStatus
{
    Healthy,
    Degraded,
    Poor,
    Critical,
    Disconnected
}

public class TcpHealthEventArgs : EventArgs
{
    public TcpConnectionHealth CurrentHealth { get; }
    public TcpConnectionHealth PreviousHealth { get; }

    public TcpHealthEventArgs(TcpConnectionHealth current, TcpConnectionHealth previous)
    {
        CurrentHealth = current;
        PreviousHealth = previous;
    }
}

