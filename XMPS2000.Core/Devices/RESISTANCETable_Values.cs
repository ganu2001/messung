using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XMPS2000.Core.Base;
using XMPS2000.Core.Types;

namespace XMPS2000.Core.Devices.Slaves
{
    public class RESISTANCETable_Values
    {
        private double _resistance;
        private string _name;
        private double _output;
        private bool _isDeleted = false;

        private Guid _id = Guid.NewGuid();
        [XmlElement("Id")]
        public Guid Id
        {
            get => _id;
            set => _id = value;
        }
        public RESISTANCETable_Values()
        {

        }

        public RESISTANCETable_Values(string name, double resistance, double output, bool isDeleted = false)
        {
            _name = name;
            _resistance = resistance;
            _output = output;
            _isDeleted = isDeleted;
        }

        [XmlElement("Resistance")]
        public double Resistance
        {
            get => _resistance;
            set => _resistance = value;
        }

        [XmlElement("Name")]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [DisplayName("Output Value")]
        [XmlElement("Output")]
        public double output
        {
            get => _output;
            set => _output = value;
        }

        [XmlElement("IsDeletedRequest")]
        public bool IsDeletedRequest
        {
            get => _isDeleted;
            set => _isDeleted = value;
        }

        // Optional: helper method to duplicate a record
        public RESISTANCETable_Values Clone()
        {
            return new RESISTANCETable_Values(Name, Resistance, output, IsDeletedRequest);
        }

        // Optional: helper to safely add a new entry to a list (returns success)
        public static bool AddValue(List<RESISTANCETable_Values> list, RESISTANCETable_Values newValue)
        {
            if (list == null) return false;

            // Prevent duplicates based on Resistance
            if (list.Exists(x => x.Resistance == newValue.Resistance))
                return false;

            list.Add(newValue);
            return true;
        }
    }
}
