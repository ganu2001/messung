using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS1000.LadderLogic
{
    internal class ApplicationRecs
    {
        public List<ApplicationData> ApplicationRungs { get; set; }

        public ApplicationRecs()
        {
            ApplicationRungs = new List<ApplicationData>();
        }

        public bool AddRungs(ApplicationData applicationData)
        {
            bool _success = false;

            //ApplicationRungs.RemoveAll(s => s.LineNumber == applicationData.LineNumber);
            //var obj = new ApplicationData(applicationData);
            ApplicationRungs.Add(applicationData);

            return _success;
        }

        internal void UpdateApplicationTimerCounterDetails(int linenumber, string tC_Name)
        {
            throw new NotImplementedException();
        }

        internal bool CheckIfRetentiveAddressExists(string text)
        {
            return true;
            //throw new NotImplementedException();
        }

        internal string ReturnLogicalAddressOfRetetentiveAddress(string text)
        {
            return "";
            //throw new NotImplementedException();
        }
    }
}
