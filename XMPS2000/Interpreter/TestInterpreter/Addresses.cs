using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInterpreter
{
    public class Addresses
    {
        private List<string> _variables;

        private List<string> _outputAddress;
        private List<string> _inputAddress;
        private List<string> _flags;
        private List<string> _status;
        private List<string> _intWord;
        private List<string> _floatPoint;
        private List<string> _timers;
        private List<string> _counters;
        private List<string> _reserved1;
        private List<string> _reserved2;
        private List<string> _autoMemoryFlags;
        
        public Addresses()
        {
            _outputAddress = getOutputAddress();
            _inputAddress = getInputAddress();
            _flags = getAddress("F2");
            _status = getAddress("S3");
            _intWord = getAddress("W4");
            _floatPoint = getAddress("P5");
            _timers = getAddress("T6");
            _counters = getAddress("C7");
            _reserved1 = getAddress("X8");
            _reserved2 = getAddress("Y9");
            _autoMemoryFlags = getAutoMemoryFlags();
            setVariables();
        }
        public List<string> Variables { get => _variables; }
        public List<string> OutputAddress { get => _outputAddress; }
        public List<string> InputAddress { get => _inputAddress; }
        public List<string> Flags { get => _flags; }
        public List<string> Status { get => _status; }
        public List<string> IntWord { get => _intWord; }
        public List<string> FloatPoint { get => _floatPoint; }
        public List<string> Timers { get => _timers; }
        public List<string> Counter { get => _counters; }
        public List<string> Reserved1 { get => _reserved1; }
        public List<string> Reserved2 { get => _reserved2; }
        public List<string> AutoMemoryFlags { get => _autoMemoryFlags; }

        private List<string> getOutputAddress()
        {
            List<string> oa = new List<string>();
            for (var i = 0; i < 256; i++)
            {
                var str = "Q0:" + i.ToString("000");
                oa.Add(str);
            }
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    var str = "Q0:" + i.ToString("000") + "." + j.ToString("00");
                    oa.Add(str);
                }
            }
            return oa;
        }

        private List<string> getInputAddress()
        {
            List<string> ia = new List<string>();
            for (var i = 0; i < 256; i++)
            {
                var str = "I1:" + i.ToString("000");
                ia.Add(str);
            }
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    var str = "I1:" + i.ToString("000") + "." + j.ToString("00");
                    ia.Add(str);
                }
            }
            return ia;
        }
        private List<string> getAddress(string prefix)
        {
            List<string> addresses = new List<string>();
            for (var i = 0; i < 256; i++)
            {
                var str = prefix + ":" + i.ToString("000");
                addresses.Add(str);
            }
            return addresses;
        }
        private List<string> getAutoMemoryFlags()
        {
            List<string> amf = new List<string>();
            for (var i = 0; i < 2049; i++)
            {
                var str = "D10:" + i.ToString("000");
                amf.Add(str);
            }
            return amf;
        }

        private void setVariables()
        {
            List<string> v = new List<string>();
            List<string> oa = _outputAddress;
            List<string> ia = _inputAddress;
            List<string> f = _flags;
            List<string> s = _status;
            List<string> iw = _intWord;
            List<string> fp = _floatPoint;
            List<string> t = _timers;
            List<string> c = _counters;
            List<string> r1 = _reserved1;
            List<string> r2 = _reserved2;
            v.AddRange(oa);
            v.AddRange(ia);
            v.AddRange(f);
            v.AddRange(s);
            v.AddRange(iw);
            v.AddRange(fp);
            v.AddRange(t);
            v.AddRange(c);
            v.AddRange(r1);
            v.AddRange(r2);
            _variables = v;
        }
    }
}
