using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInterpreter.Helpers
{
    public class TestAddress
    {
        public static void TestAddressHexValue(List<string> addresses)
        {
            // sample
            //addresses = new List<string> { "I1:010", "I1:000.04", "F2:004", "Q0:000.05" };
            
            for (var i = 0; i < addresses.Count; i++)
            {
                Console.WriteLine(addresses[i] + ": " + ParseExp.GetAddressHex(addresses[i]));
            }
        }
    }
}
