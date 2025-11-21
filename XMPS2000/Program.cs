using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core;
//using K5;

namespace XMPS2000
{
    internal static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Diagnostics.Trace.WriteLine("Msg, WParam, LParam, Num9, Num10");
            Application.Run(new SplashForm());
        }

        

    }

}
