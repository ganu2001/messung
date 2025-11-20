using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.MCodeConversion
{
    public class Addresses
    {
        private List<string> _variables;

        private List<string> _outputWordAddress;
        private List<string> _outputBitAddress;
        private List<string> _inputWordAddress;
        private List<string> _inputBitAddress;
        private List<string> _flags;
        private List<string> _status;
        private List<string> _intWord;
        private List<string> _floatPoint;
        private List<string> _timers;
        private List<string> _counters;
        private List<string> _reserved1;
        private List<string> _reserved2;
        private List<string> _autoMemoryFlags;
        
        private List<string> _bitAddresses;
        private List<string> _wordAddresses;

        public Addresses()
        {
            _outputWordAddress = getOutputWordAddress();
            _outputBitAddress = getOutputBitAddress();
            _inputWordAddress = getInputWordAddress();
            _inputBitAddress = getInputBitAddress();
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
            setBitAddress();
            setWordAddress();
        }
        public List<string> Variables { get => _variables; }
        public List<string> OutputWordAddress { get => _outputWordAddress; }
        public List<string> OutputBitAddress { get => _outputBitAddress; }
        public List<string> InputWordAddress { get => _inputWordAddress; }
        public List<string> InputBitAddress { get => _inputBitAddress; }
        public List<string> FlagsMemoryBitAddress { get => _flags; }
        public List<string> StatusWordAddress { get => _status; }
        public List<string> IntWordAddress { get => _intWord; } // memory word address
        public List<string> FloatPointRealAddress { get => _floatPoint; }
        public List<string> TimersWordAddress { get => _timers; }
        public List<string> CounterWordAddress { get => _counters; }
        public List<string> Reserved1WordAddress { get => _reserved1; }
        public List<string> Reserved2WordAddress { get => _reserved2; }
        public List<string> AutoMemoryFlagsBitAddress { get => _autoMemoryFlags; }
        public List<string> BitAddresses { get => _bitAddresses; }
        public List<string> WordAddresses { get => _wordAddresses; }

        private List<string> getOutputWordAddress()
        {
            List<string> oaWord = new List<string>();
            for (var i = 0; i < 256; i++)
            {
                var str = "Q0:" + i.ToString("000");
                oaWord.Add(str);
            }
            return oaWord;
        }
        private List<string> getOutputBitAddress()
        {
            List<string> oaBit = new List<string>();
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    var str = "Q0:" + i.ToString("000") + "." + j.ToString("00");
                    oaBit.Add(str);
                }
            }
            return oaBit;
        }

        private List<string> getInputWordAddress()
        {
            List<string> iaWord = new List<string>();
            for (var i = 0; i < 256; i++)
            {
                var str = "I1:" + i.ToString("000");
                iaWord.Add(str);
            }
            return iaWord;
        }
        private List<string> getInputBitAddress()
        {
            List<string> iaBit = new List<string>();
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    var str = "I1:" + i.ToString("000") + "." + j.ToString("00");
                    iaBit.Add(str);
                }
            }
            return iaBit;
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
            List<string> oaWord = _outputWordAddress;
            List<string> oaBit = _outputBitAddress;
            List<string> iaWord = _inputWordAddress;
            List<string> iaBit = _inputBitAddress;
            List<string> f = _flags;
            List<string> s = _status;
            List<string> iw = _intWord;
            List<string> fp = _floatPoint;
            List<string> t = _timers;
            List<string> c = _counters;
            List<string> r1 = _reserved1;
            List<string> r2 = _reserved2;
            v.AddRange(oaWord);
            v.AddRange(oaBit);
            v.AddRange(iaWord);
            v.AddRange(iaBit);
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
        private void setBitAddress()
        {
            List<string> bitAddress = new List<string>();
            List<string> oaBit = _outputBitAddress;
            List<string> iaBit = _inputBitAddress;
            List<string> f = _flags;
            List<string> amf = _autoMemoryFlags;
            bitAddress.AddRange(oaBit);
            bitAddress.AddRange(iaBit);
            bitAddress.AddRange(f);
            bitAddress.AddRange(amf);
            _bitAddresses = bitAddress;
        }
        private void setWordAddress()
        {
            List<string> wordAddress = new List<string>();
            List<string> oaWord = _outputWordAddress;
            List<string> iaWord = _inputWordAddress;
            List<string> s = _status;
            List<string> iw = _intWord;
            List<string> t = _timers;
            List<string> c = _counters;
            List<string> r1 = _reserved1;
            List<string> r2 = _reserved2;
            wordAddress.AddRange(oaWord);
            wordAddress.AddRange(iaWord);
            wordAddress.AddRange(s);
            wordAddress.AddRange(iw);
            wordAddress.AddRange(t);
            wordAddress.AddRange(c);
            wordAddress.AddRange(r1);
            wordAddress.AddRange(r2);
            _wordAddresses = wordAddress;
        }
    }
}
