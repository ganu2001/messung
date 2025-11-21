using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XMPS2000.Core.Base.Helpers;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.Core.App
{
    public class MqttTextGeneration
    {
        private static readonly XMPS xm = XMPS.Instance;
        public static List<long> _mqttlistForCRC = new List<long>();
        public MqttTextGeneration() { }

        // Create list of DataTypes for fetching DataType ID
        public List<KeyValuePair<int, string>> IDTextList = new List<KeyValuePair<int, string>>()
        {
            new KeyValuePair<int, string>(0, "Bool"),
            new KeyValuePair<int, string>(1, "Byte"),
            new KeyValuePair<int, string>(2, "Word"),
            new KeyValuePair<int, string>(3, "Double Word"),
            new KeyValuePair<int, string>(4, "Int"),
            new KeyValuePair<int, string>(5, "Real"),
            new KeyValuePair<int, string>(13, "DINT"),
            new KeyValuePair<int, string>(14, "UDINT")
        };

        //Fetch Mqtt Data and stored in List to write
        public void FetchDataToText()
        {
            MQTTForm mqttForm = (MQTTForm)xm.LoadedProject.Devices.FirstOrDefault(d => d.GetType().Name == "MQTTForm");
            var publish = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList();
            var Listpublish = publish.OrderBy(p => p.keyvalue).ToList();
            var Subscribe = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
            var ListSubscribe = Subscribe.OrderBy(s => s.key).ToList();
            List<string> dataToWrite = new List<string>();
            string licenseData = string.Empty;
            if (mqttForm != null)
            {
                //Converting HostName(string) to long and add into the _mqttlistForCRC
                long hostNameData = XMProBaseValidator.ConvertStringToLong(mqttForm.hostname);
                _mqttlistForCRC.Add(hostNameData);

                //Converting port(string) to long and add into the _mqttlistForCRC
                long portData = XMProBaseValidator.ConvertStringToLong(mqttForm.port.ToString());
                _mqttlistForCRC.Add(portData);

                //Converting userName(string) to long and add into the _mqttlistForCRC
                if (mqttForm.username != null && mqttForm.username != "")
                {
                    long userName = XMProBaseValidator.ConvertStringToLong(mqttForm.username);
                    _mqttlistForCRC.Add(userName);
                }

                //Converting Password(string) to long and add into the _mqttlistForCRC
                if (mqttForm.password != null && mqttForm.password != "")
                {
                    long password = XMProBaseValidator.ConvertStringToLong(mqttForm.password);
                    _mqttlistForCRC.Add(password);
                }

                //Converting cleanSession(string) to long and add into the _mqttlistForCRC
                if (mqttForm.cleanSession != null && mqttForm.cleanSession != "")
                {
                    long cleanSession = XMProBaseValidator.ConvertStringToLong(mqttForm.cleanSession);
                    _mqttlistForCRC.Add(cleanSession);
                }

                //Converting keepAlive(string) to long and add into the _mqttlistForCRC
                if (mqttForm.keepAlive > 0)
                {
                    long keepAlive = XMProBaseValidator.ConvertStringToLong(mqttForm.keepAlive.ToString());
                    _mqttlistForCRC.Add(keepAlive);
                }
                string ca3Data = "0";
                if (!string.IsNullOrEmpty(mqttForm.ca3certificate) && File.Exists(mqttForm.ca3certificate))
                {
                    // Read file content if the file path is valid and the file exists
                    ca3Data = File.ReadAllText(mqttForm.ca3certificate);
                }
                //ca3Data = File.ReadAllText(mqttForm.ca3certificate);

                //Converting ca3Data(string) to long and add into the _mqttlistForCRC
                long ca3NameData = XMProBaseValidator.ConvertStringToLong(mqttForm.ca3certificate);
                _mqttlistForCRC.Add(ca3NameData);


                string clientData = "0";
                if (!string.IsNullOrEmpty(mqttForm.clientCertificate) && File.Exists(mqttForm.clientCertificate))
                {
                    // Read file content if the file path is valid and the file exists
                    clientData = File.ReadAllText(mqttForm.clientCertificate);
                }
                //string clientData = File.ReadAllText(mqttForm.clientCertificate).TrimEnd();

                //Converting clientData(string) to long and add into the _mqttlistForCRC
                long clientNameData = XMProBaseValidator.ConvertStringToLong(mqttForm.clientCertificate);
                _mqttlistForCRC.Add(clientNameData);

                string key = "0";
                if (!string.IsNullOrEmpty(mqttForm.clientKey) && File.Exists(mqttForm.clientKey))
                {
                    // Read file content if the file path is valid and the file exists
                    key = File.ReadAllText(mqttForm.clientKey);
                }
                //string key = File.ReadAllText(mqttForm.clientKey).TrimEnd();

                //Converting keyData(string) to long and add into the _mqttlistForCRC
                long keyData = XMProBaseValidator.ConvertStringToLong(mqttForm.clientKey);
                _mqttlistForCRC.Add(keyData);

                long _licenceData = XMProBaseValidator.ConvertStringToLong(mqttForm.license);
                
                licenseData = string.IsNullOrEmpty(mqttForm.license) ? "0" : File.ReadAllText(mqttForm.license).TrimEnd();

                dataToWrite.AddRange(new[]
                {
                    "8611",
                    mqttForm.hostname.Trim()  ?? "0",
                    "8612",
                    mqttForm.port.ToString() ?? "0",
                    "8613",
                    string.IsNullOrEmpty(mqttForm.username) ? "0" : mqttForm.username,
                    "8614",
                    string.IsNullOrEmpty(mqttForm.password) ? "0" : mqttForm.password,
                    "8615",
                    ca3Data,
                    "8616",
                    clientData,
                    "8617",
                    key,
                    "8618",
                    string.IsNullOrEmpty(mqttForm.cleanSession) ? "0" : mqttForm.cleanSession,
                    "8619",
                    mqttForm.keepAlive.ToString() ?? "0",
                    "8620",
                    Listpublish.Count.ToString() ?? "0"
                });
                long clientId = XMProBaseValidator.ConvertStringToLong(mqttForm.clientId ?? string.Empty);
                _mqttlistForCRC.Add(clientId);
            }
            else
            {
                for (int i = 8611; i <= 8620; i++)
                {
                    dataToWrite.Add(i.ToString());
                    dataToWrite.Add("0");
                }
                dataToWrite.Add(Listpublish.Count.ToString() ?? "0");
            }

            //Fetch Publish Block Data
            for (int i = 0; i < Listpublish.Count; i++)
            {
                int TopicNo = i + 1;
                int reqCount = Listpublish[i].PubRequest.Count;
                dataToWrite.AddRange(new[]
                {
                    TopicNo.ToString(),
                    Listpublish[i].topic,
                    "{" ,
                });
                //Converting Qos(string) to long and add into the _mqttlistForCRC
                long topicNameData = XMProBaseValidator.ConvertStringToLong(Listpublish[i].topic);
                _mqttlistForCRC.Add(topicNameData);

                //Converting Qos(string) to long and add into the _mqttlistForCRC
                long qosData = XMProBaseValidator.ConvertStringToLong(Listpublish[i].qos);
                _mqttlistForCRC.Add(qosData);

                //Converting RetainFlag(string) to long and add into the _mqttlistForCRC
                long retainFlagData = XMProBaseValidator.ConvertStringToLong(Listpublish[i].retainflag);
                _mqttlistForCRC.Add(retainFlagData);

                _mqttlistForCRC.Add(TopicNo);
                for (int j = 0; j < reqCount; j++)
                {
                    //Converting KeyName(string) to long and add into the _mqttlistForCRC
                    long keyNameData = XMProBaseValidator.ConvertStringToLong(Listpublish[i].PubRequest[j].req);
                    _mqttlistForCRC.Add(keyNameData);
                    var tag = Listpublish[i].PubRequest[j].Tag;
                    string logicalAddress = string.Empty;
                    if (tag.Contains(":"))
                    {
                        logicalAddress = tag;
                    }
                    else
                        logicalAddress = xm.LoadedProject.Tags.Where(t => t.Tag == tag).Select(t => t.LogicalAddress).FirstOrDefault();
                    string _address = uint.Parse(XMPS.Instance.GetHexAddress(logicalAddress), System.Globalization.NumberStyles.HexNumber).ToString();
                    //uint integerValue = uint.Parse(_address, System.Globalization.NumberStyles.HexNumber);
                    string dataType = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == logicalAddress).Select(t => (logicalAddress.StartsWith("I") || logicalAddress.StartsWith("Q")) ? t.Type.ToString() : t.Label).FirstOrDefault();
                    string mode = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == logicalAddress && t.Type.ToString().StartsWith("Universal")).Select(t => t.Mode).FirstOrDefault();

                    _mqttlistForCRC.Add(Convert.ToInt64(_address));
                    string id = "";
                    foreach (var datatype in IDTextList)
                    {
                        if (datatype.Value == dataType)
                        {
                            id = datatype.Key.ToString();
                            break;
                        }
                    }
                    if (id == "")
                        id = (logicalAddress.Contains('.') || mode == "Digital") ? "0" : "2";

                    dataToWrite.AddRange(new[]
                    {
                      $"\"{Listpublish[i].PubRequest[j].req}\"" +"="+ _address+ ","+ id
                    });

                }

                dataToWrite.AddRange(new[]
                {
                    "}",
                    Listpublish[i].qos,
                    Listpublish[i].retainflag
                });
            }
            dataToWrite.AddRange(new[]
            {
               "8621",
               ListSubscribe.Count.ToString() ?? "0",

            });

            //Fetch Subscribe Block Data
            for (int i = 0; i < ListSubscribe.Count; i++)
            {
                int TopicNo = i + 1;
                int reqCount = ListSubscribe[i].SubRequest.Count;
                dataToWrite.AddRange(new[]
                {
                    TopicNo.ToString(),
                    ListSubscribe[i].topic,
                    "{" ,
                });
                //Converting Qos(string) to long and add into the _mqttlistForCRC
                long topicNameData = XMProBaseValidator.ConvertStringToLong(ListSubscribe[i].topic);
                _mqttlistForCRC.Add(topicNameData);

                //Converting Qos(string) to long and add into the _mqttlistForCRC
                long qosData = XMProBaseValidator.ConvertStringToLong(ListSubscribe[i].qos);
                _mqttlistForCRC.Add(qosData);

                _mqttlistForCRC.Add(TopicNo);
                for (int j = 0; j < reqCount; j++)
                {
                    //Converting KeyName(string) to long and add into the _mqttlistForCRC
                    long keyNameData = XMProBaseValidator.ConvertStringToLong(ListSubscribe[i].SubRequest[j].req);
                    _mqttlistForCRC.Add(keyNameData);

                    var tag = ListSubscribe[i].SubRequest[j].Tag;
                    string logicalAddress = string.Empty;
                    if (tag.Contains(":"))
                    {
                        logicalAddress = tag;
                    }
                    else
                        logicalAddress = tag.Contains(":") ? tag : xm.LoadedProject.Tags.Where(t => t.Tag == tag).Select(t => t.LogicalAddress).FirstOrDefault();
                    string _address = uint.Parse(XMPS.Instance.GetHexAddress(logicalAddress), System.Globalization.NumberStyles.HexNumber).ToString();
                    string dataType = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == logicalAddress).Select(t => (logicalAddress.StartsWith("I") || logicalAddress.StartsWith("Q")) ? t.Type.ToString() : t.Label).FirstOrDefault();
                    string mode = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == logicalAddress && t.Type.ToString().StartsWith("Universal")).Select(t => t.Mode).FirstOrDefault();

                    string id = "";
                    _mqttlistForCRC.Add(Convert.ToInt64(_address));
                    foreach (var datatype in IDTextList)
                    {
                        if (datatype.Value == dataType)
                        {
                            id = datatype.Key.ToString();
                            break;
                        }
                    }
                    if (id == "")
                        id = (logicalAddress.Contains('.') || mode == "Digital") ? "0" : "2";
                    dataToWrite.AddRange(new[]
                    {
                        $"\"{ListSubscribe[i].SubRequest[j].req}\"" +"="+ _address+ ","+ id
                    });
                }
                dataToWrite.AddRange(new[]
                {
                    "}",
                    ListSubscribe[i].qos
                });
            }
            dataToWrite.AddRange(new[]
            {
               "8622",
                string.IsNullOrEmpty(licenseData) ? "0" : licenseData 

            });

           
            dataToWrite.AddRange(new[]
            {
                "8623",
                  mqttForm != null && !string.IsNullOrEmpty(mqttForm.clientId) ? mqttForm.clientId.Trim() : "0"
            });
            notePadFile(dataToWrite);
        }

        //Create Text File And Write Data 
        public void notePadFile(List<string> dataToWrite)
        {
            string filepath = xm.CurrentProjectData.ProjectPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            string newfilePath = xm.CurrentProjectData.ProjectPath.Replace(filepath, "MQTT_CNF.txt");
            using (StreamWriter sw = new StreamWriter(newfilePath))
            {
                foreach (string line in dataToWrite)
                {
                    sw.WriteLine(line);
                }
            }
            PerformXOR();
        }
        
        private void PerformXOR()
        {
            xm.LoadedProject.MQTTCRC = 0;
            if (_mqttlistForCRC.Count == 0)
            {
                xm.LoadedProject.MQTTCRC = 0;
                return;
            }
            else
                xm.LoadedProject.MQTTCRC = _mqttlistForCRC[0];

            for (int i = 1; i < _mqttlistForCRC.Count; i++)
            {
                xm.LoadedProject.MQTTCRC ^= _mqttlistForCRC[i];
            }

            //long conctc = Convert.ToInt32(xm.LoadedProject.McodeCRC, 16) ^ Convert.ToInt32("97", 16);
            //xm.LoadedProject.McodeCRC = Convert.ToInt32(conctc).ToString("X");
            _mqttlistForCRC.Clear();
        }
    }
}
