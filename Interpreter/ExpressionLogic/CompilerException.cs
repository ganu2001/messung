using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.ExpressionLogic
{
    public class CompilerException : Exception
    {

        //Constructors. It is recommended that at least all the
        //constructors of
        //base class Exception are implemented
      //  List<string> exceptionsOcuured = new List<string>();
        public CompilerException() : base() { }
        public CompilerException(string message) : base(message) { }
        public CompilerException(string message, Exception e) : base(message, e) { }



    }
  
}
