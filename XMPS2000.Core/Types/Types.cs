using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMPS2000.Core.Types
{
    public static class Types
    {
        public static string GetEnumDisplayName(this Enum enumType)
        {
            return enumType.GetType().GetMember(enumType.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .Name;
        }
    }
    public enum NodeType
    { 
        RootNode=1,
        ListNode=2,
        ProjectNode=3,
        CurrentProjectNode=4,
        BlockNode=5,
        DeviceNode=6,
        MainBlockNode=7,
        BackNetNode = 8

    }

    public enum COMBaudRate
    {
        [Display(Name = "9600")]
        _9600 = 1,
        [Display(Name = "19200")]
        _19200 = 2,
        [Display(Name = "38400")]
        _38400 = 3,
        [Display(Name = "57600")]
        _57600 = 4,
        [Display(Name = "115200")]
        _115200 =5
    }

    public enum COMDataLength
    {
        [Display(Name = "6")]
        _6 = 1,
        [Display(Name = "7")]
        _7 = 2,
        [Display(Name = "8")]
        _8 = 3
    }

    public enum COMStopBit
    {
        [Display(Name = "1")]
        _1 = 1,
        [Display(Name = "2")]
        _2 = 2
    }

    public enum COMParity
    {
        [Display(Name = "Even")]
        Even = 1,
        [Display(Name = "Odd")]
        Odd = 2,
        [Display(Name = "None")]
        None = 3
    }

    [Serializable]
    public enum FunctionCode
    {
        [EnumMember]
        ReadCoil,
        [EnumMember]
        ReadDescreateInput,
        [EnumMember]
        ReadHoldingRegisters,
        [EnumMember]
        ReadInputRegisters,
        [EnumMember]
        WriteSingleCoil,
        [EnumMember]
        WriteSingleHoldingRegister,
        [EnumMember]
        WriteMultipleCoils,
        [EnumMember]
        WriteMultipleHoldingRegisters
    }

    
    [Serializable]
    public enum IOListType
    {
        [EnumMember]
        OnBoardIO,
        [EnumMember]
        RemoteIO,
        [EnumMember]
        ExpansionIO,
        [EnumMember]
        NIL,
       [EnumMember]
        Default
    }

    [Serializable]
    public enum IOType
    {
        [EnumMember]
        DigitalInput,
        [EnumMember]
        DigitalOutput,
        [EnumMember]
        AnalogInput,
        [EnumMember]
        AnalogOutput,
        [EnumMember]
        UniversalInput,
        [EnumMember]
        UniversalOutput,
        [EnumMember]
        DataType
    }
}
