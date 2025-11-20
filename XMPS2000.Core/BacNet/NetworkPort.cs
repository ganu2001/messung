using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS2000.Core.BacNet
{
    public class NetworkPort
    {
        public string ObjectIdentifier { get; set; }
        public string InstanceNumber { get; set; }
        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public bool IsEnable { get; set; }
        public string NetworkType { get; set; }
        public string NetworkNumber { get; set; }
        public string BacnetIPMode { get; set; }
        public string IPAddress { get; set; }
        public string BacnetIPUDPPort { get; set; }
        public string IPSubnetMask { get; set; }
        public string IPDefaultGateway { get; set; }
        public string IPDNSServer { get; set; }
        public string IPDHCPEnable { get; set; }
        public string FDBBMDIP { get; set; }
        public string FDBBMDPort { get; set; }
        public string FDSubscriptionLifetime { get; set; }

        public NetworkPort(string obj_identifier, string inst_number, string obj_type, string obj_name, string description, bool isEnable)
        {
            ObjectIdentifier = obj_identifier;
            InstanceNumber = inst_number;
            ObjectType = obj_type;
            ObjectName = obj_name;
            Description = description;
            IsEnable = isEnable;
        }

        public NetworkPort()
        {

        }
    }
}

