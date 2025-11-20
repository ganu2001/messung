using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace XMPS2000.LadderLogic
{
    [Serializable]
    internal class TimerMaxLimitExceedException : Exception
    {
        static string errorMessage = "Maximum Number of Rungs of Exceeds for this instructions for this type: ";


        public TimerMaxLimitExceedException()
        {
        }

        public TimerMaxLimitExceedException(string message) : base(errorMessage + message)
        {

        }

        public TimerMaxLimitExceedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TimerMaxLimitExceedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
