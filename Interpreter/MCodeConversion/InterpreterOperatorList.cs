using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.MCodeConversion
{
    public class InterpreterOperatorList
    {
        private List<string> _logical;
        private List<string> _logicalBlock;
        private List<string> _arithmetic;
        private List<string> _bitShift;
        private List<string> _limit;
        private List<string> _compare;
        private List<string> _counter;
        private List<string> _timer;
        private List<string> _flipflop;
        
        public InterpreterOperatorList()
        {
            _logical = new List<string> { "AND", "OR" };
            _logicalBlock = new List<string> { "&", "||", "Not", "XOR" };
            _arithmetic = new List<string> { "+", "-", "*", "/", "MOD", "MOV" };
            _bitShift = new List<string> { "shl", "shr", "ror", "rol" };
            _limit = new List<string> { "LIMIT" };
            _compare = new List<string> { ">=", "<=", "<>", "==", ">", "<"};
            _counter = new List<string> { "CTU", "CTD" };
            _timer = new List<string> { "TON", "TOF", "TP" };
            _flipflop = new List<string> { "RS", "SR" };
        }

        public List<string> OperatorsList { get => getOperatorsList(); }

        public List<string> LogicalOperatorsList { get => _logical; }
        public List<string> LogicalBlockOperatorsList { get => _logicalBlock; }
        public List<string> ArithmeticOperatorsList { get => _arithmetic; }
        public List<string> BitShiftOperatorsList { get => _bitShift; }
        public List<string> LimitOperatorsList { get => _limit; }
        public List<string> CompareOperatorsList { get => _compare; }
        public List<string> CounterOperatorsList { get => _counter; }
        public List<string> TimerOperatorsList { get => _timer; }
        public List<string> FlipFlopOperatorsList { get => _flipflop; }

        private List<string> getOperatorsList()
        {
            List<string> operatorsList = new List<string>();
            
            List<string> logical = _logical;
            List<string> logicalBlock = _logicalBlock;
            List<string> arithmetic = _arithmetic;
            List<string> bitShift = _bitShift;
            List<string> limit = _limit;
            List<string> compare = _compare;
            List<string> counter = _counter;
            List<string> timer = _timer;
            List<string> flipflop = _flipflop;
            
            operatorsList.AddRange(logical);
            operatorsList.AddRange(logicalBlock);
            operatorsList.AddRange(arithmetic);
            operatorsList.AddRange(bitShift);
            operatorsList.AddRange(limit);
            operatorsList.AddRange(compare);
            operatorsList.AddRange(counter);
            operatorsList.AddRange(timer);
            operatorsList.AddRange(flipflop);
            
            return operatorsList;
        }
    }
}
