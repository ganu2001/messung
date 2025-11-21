using LadderEditorLib.DInterpreter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using XMPS2000;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.Core.Types;

namespace LadderEditorLib.UserControls
{
    public partial class AddDevice : UserControl
    {
        public string DeviceAdded = "";
        public AddDevice(string devicetype)
        {
            InitializeComponent();
            string PlcModel = XMPS.Instance.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).Select(d => d.Model).FirstOrDefault();
            if (PlcModel.StartsWith("XBLD") && (devicetype == "Expansion I/O") /*devicetype == "RS485"*/)
                DDLAddDevice.Items.AddRange(RemoteModule.List.FindAll(x => x.Name.StartsWith("XBLD") && x.IOType.Equals(devicetype)).ToArray());
            else
                DDLAddDevice.Items.AddRange(RemoteModule.List.FindAll(x => !x.Name.StartsWith("XBLD") && x.IOType.Equals(devicetype)).ToArray());
        }
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            this.DeviceAdded = DDLAddDevice.Text.ToString();
            this.ParentForm.DialogResult = DialogResult.OK;
            this.ParentForm.Close();

        }

        public void AddRemoteExpansionIOs(RemoteModule model, IOListType ioListType, string newname = "", List<XMIOConfig> oldTag = null)
        {
            int _inputCounter = 0;
            int _outputCounter = 0;
            var OList = XMPS.Instance.LoadedProject.Tags.Where(d => d.Label != "" && d.Label.ToString().Length > 1 && d.Type.ToString().Contains("Out") && !d.Tag.EndsWith("_OR"));
            _outputCounter = OList.AsEnumerable().Select(d => Convert.ToInt32(d.LogicalAddress.ToString().Substring(3, 3))).Max();
            var IList = (XMPS.Instance.LoadedProject.Tags.Where(d => d.Label != "" && d.Label.ToString().Length > 1 && d.Type.ToString().Contains("In")));
            _inputCounter = IList.AsEnumerable().Select(d => Convert.ToInt32(d.LogicalAddress.ToString().Substring(3, 3))).Max();


            BacNetIP bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            if (bacNetIP == null) bacNetIP = new BacNetIP();

            //adding logic for Getting new Instance Number.
            string maxAnalogInstaceNumber = oldTag == null ? BacNetObjectHelper.GetMaxAnalogInstanceNumber(ref bacNetIP) : "0";
            string maxBinaryInstanceNumber = oldTag == null ? BacNetObjectHelper.GetMaxBinaryInstaceNumber(ref bacNetIP) : "0";
            maxAnalogInstaceNumber = maxAnalogInstaceNumber.Length == 1 ? maxAnalogInstaceNumber.PadLeft(3, '0') : maxAnalogInstaceNumber;
            maxBinaryInstanceNumber = maxBinaryInstanceNumber.Length == 1 ? maxBinaryInstanceNumber.PadLeft(3, '0') : maxBinaryInstanceNumber;
            int maxAnalog = int.TryParse(maxAnalogInstaceNumber.Length > 3 ? maxAnalogInstaceNumber.Substring(0, 2) : maxAnalogInstaceNumber.Substring(0, 1), out int analog) ? analog : 0;
            int maxBinary = int.TryParse(maxBinaryInstanceNumber.Length > 3 ? maxBinaryInstanceNumber.Substring(0, 2) : maxBinaryInstanceNumber.Substring(0, 1), out int binary) ? binary : 0;

            int newExpansionNo = Math.Max(maxAnalog, maxBinary) + 1;

            int binaryInputCounter = 0;
            int binaryOutputCounter = 0;
            int analogInputCounter = 0;
            int analogOutputCounter = 0;
            var existingBinaryAddresses = new HashSet<string>(bacNetIP.BinaryIOValues.Select(b => b.LogicalAddress));
            var existingAnalogAddresses = new HashSet<string>(bacNetIP.AnalogIOValues.Select(b => b.LogicalAddress));

            string Addedlogicaladdress = "";
            //for expansion Adding form frmMainCode
            int countTotal = 0;
            foreach (var ioType in model.SupportedTypesAndIOs)
            {

                string logicalAddressPrefix;
                int counterToUseToFormLogicalAddress;
                bool isDigitalIO = ioType.Type.Equals("Digital Input") || ioType.Type.Equals("Digital Output");

                if (ioType.Type.Equals("Digital Input") || ioType.Type.Equals("Analog Input") || ioType.Type.Equals("Universal Input"))
                {
                    logicalAddressPrefix = "I1:";
                    counterToUseToFormLogicalAddress = _inputCounter + 1;
                }
                else
                {
                    logicalAddressPrefix = "Q0:";
                    counterToUseToFormLogicalAddress = _outputCounter + 1;
                }

                for (int i = 0; i < ioType.Units; i++)
                {
                    XMIOConfig xMIOConfig = new XMIOConfig();
                    xMIOConfig.IoList = ioListType;
                    if (newname != "")
                    {
                        xMIOConfig.Model = newname;
                    }
                    else
                    {
                        xMIOConfig.Model = model.Name;
                    }
                    xMIOConfig.Type = ioType.Type == "Digital Output" ? IOType.DigitalOutput : ioType.Type == "Digital Input" ? IOType.DigitalInput : ioType.Type == "Analog Input" ? IOType.AnalogInput : ioType.Type == "Analog Output" ? IOType.AnalogOutput : ioType.Type == "Universal Input" ? IOType.UniversalInput : IOType.UniversalOutput;
                    xMIOConfig.Label = ioType.TypeText.Equals("Other")
                        ? ioType.LabelPrefix + (i + 1).ToString()
                        : ioType.LabelPrefix + i.ToString();

                    string logicalAddress = logicalAddressPrefix
                        + counterToUseToFormLogicalAddress.ToString("D3")
                        + (isDigitalIO ? "." + i.ToString("D2") : string.Empty);
                    var TagId = (XMPS.Instance.LoadedProject.Tags.Where(d => (!d.Tag.EndsWith("_OR") && !d.Tag.EndsWith("_OL")) && d.Label != "" && (d.IoList != IOListType.NIL) && d.Label.ToString().Length > 1 && d.Tag.ToString().Any(char.IsDigit) && d.Tag.ToString().Contains("_" + d.Label.ToString().Substring(0, 2)) && d.Label.ToString().Substring(0, 2) == xMIOConfig.Label.Substring(0, 2))).ToList();
                    if (TagId != null && TagId.Count() > 0)
                    {
                        var MaxTag = TagId.Max(T => Convert.ToInt64(T.Tag.ToString().Substring(T.Tag.ToString().IndexOf(xMIOConfig.Label.Substring(0, 2)), T.Tag.ToString().Length - (T.Tag.ToString().IndexOf(xMIOConfig.Label.Substring(0, 2)))).Replace(xMIOConfig.Label.Substring(0, 2), ""))) + 1;
                        xMIOConfig.Tag = oldTag != null ? oldTag[countTotal].Tag
                                         : xMIOConfig.Type + "_" + xMIOConfig.Label.Substring(0, 2) + MaxTag;
                    }
                    else
                    {
                        xMIOConfig.Tag = oldTag != null ? oldTag[countTotal].Tag
                                        : xMIOConfig.Type + "_" + xMIOConfig.Label.Substring(0, 2) + "0";
                    }
                    if (!isDigitalIO)
                        ++counterToUseToFormLogicalAddress;

                    xMIOConfig.LogicalAddress = logicalAddress;
                    Addedlogicaladdress = logicalAddress;
                    if (!xMIOConfig.LogicalAddress.Contains("."))
                    {
                        if (ioType.Type.Equals("Digital Input") || ioType.Type.Equals("Analog Input") || ioType.Type.Equals("Universal Input"))
                        {
                            ++_inputCounter;
                        }
                        else
                        {
                            ++_outputCounter;
                        }
                    }
                    xMIOConfig.InpuFilterValue = (xMIOConfig.Type == IOType.DigitalInput && XMPS.Instance.LoadedProject.IsEditableDigitalFilter && xMIOConfig.IoList == IOListType.ExpansionIO)
                                                 ? "10" : string.Empty;
                    xMIOConfig.IsEnableInputFilter = (xMIOConfig.Type == IOType.DigitalInput && XMPS.Instance.LoadedProject.IsEditableDigitalFilter && xMIOConfig.IoList == IOListType.ExpansionIO)
                                                 ? true : false;
                    xMIOConfig.Key = XMPS.Instance.LoadedProject.Tags.Max(K => K.Key) + 1;

                    //for additional old information adding after creating new tags.
                    xMIOConfig.ShowLogicalAddress = oldTag != null ?  oldTag[countTotal].ShowLogicalAddress : false;
                    xMIOConfig.Mode = oldTag != null ? oldTag[countTotal].Mode : string.Empty;
                    xMIOConfig.Retentive = oldTag != null ? oldTag[countTotal].Retentive : false;
                    xMIOConfig.RetentiveAddress = oldTag != null ? oldTag[countTotal].RetentiveAddress : string.Empty;
                    xMIOConfig.InitialValue = oldTag != null ? oldTag[countTotal].InitialValue : string.Empty;
                    countTotal++;
                    XMPS.Instance.LoadedProject.Tags.Add(xMIOConfig);
                    if(oldTag == null)
                    {
                        //Create an BacNet Object for Expansion and Remote IO's
                        if (logicalAddress.Contains("."))
                        {
                            BacNetObjectHelper.AddBinaryIOV(ref bacNetIP, logicalAddress, xMIOConfig.Tag.ToString(), existingBinaryAddresses, ref binaryInputCounter, ref binaryOutputCounter, newExpansionNo);
                        }
                        else if (xMIOConfig.Type == IOType.AnalogInput ||
                                 xMIOConfig.Type == IOType.AnalogOutput ||
                                 xMIOConfig.Type == IOType.UniversalInput ||
                                 xMIOConfig.Type == IOType.UniversalOutput)
                        {
                            BacNetObjectHelper.AddAnalogIOV(ref bacNetIP, logicalAddress, xMIOConfig.Tag.ToString(), existingAnalogAddresses, ref analogInputCounter, ref analogOutputCounter, xMIOConfig.Type.ToString(), newExpansionNo);
                        }

                        BacNetObjectHelper.IncrementCounters(xMIOConfig.Type, ref binaryInputCounter, ref binaryOutputCounter, ref analogInputCounter, ref analogOutputCounter);
                    }
                    XMPS.Instance.MarkProjectModified(true);
                }
                if (Addedlogicaladdress.Contains("."))
                {
                    if (ioType.Type.Equals("Digital Input") || ioType.Type.Equals("Analog Input") || ioType.Type.Equals("Universal Input"))
                    {
                        ++_inputCounter;
                    }
                    else
                    {
                        ++_outputCounter;
                    }
                }
            }

            if (XMPS.Instance.LoadedProject.DiagnosticParametersEnabled && (model.Name.Contains("XM-AI2-AO-2") || model.Name.Contains("XM-UI4-UO2") || model.Name.Contains("XBLD-UI4-UO2") || model.Name.Contains("XBLD-AI2-AO2")) && ioListType.ToString() == "ExpansionIO")
            {
                BacNetObjectHelper.AddDignosticTags(true, newname, model.SupportedTypesAndIOs[0].Type);
            }
        }


    }

}
