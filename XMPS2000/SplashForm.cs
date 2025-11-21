using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace XMPS2000
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //if (!Debugger.IsAttached)
            //{
            //    if (System.Deployment.Application.ApplicationDeployment.CurrentDeployment.IsFirstRun)
            //    {
            //        string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //        string appDataPath = Application.UserAppDataPath.ToString();
            //        if (!Directory.Exists(Path.Combine(dataPath, @"MessungSystems")))
            //            Directory.CreateDirectory(Path.Combine(dataPath, @"MessungSystems"));

            //        if (!Directory.Exists(Path.Combine(dataPath, @"MessungSystems\XMPS2000")))
            //            Directory.CreateDirectory(Path.Combine(dataPath, @"MessungSystems\XMPS2000"));

            //        if (!Directory.Exists(Path.Combine(dataPath, @"MessungSystems\XMPS2000\XM Projects")))
            //            Directory.CreateDirectory(Path.Combine(dataPath, @"MessungSystems\XMPS2000\XM Projects"));

            //        foreach (var file in Directory.GetFiles(appDataPath))
            //        {
            //            string destination = Path.Combine(dataPath.ToString(), @"MessungSystems\XMPS2000", Path.GetFileName(file));
            //            if (!File.Exists(destination))
            //                File.Copy(file, destination);
            //        }

            //        foreach (var file in Directory.GetFiles(Path.Combine(appDataPath, "Files")))
            //        {
            //            string detination = Path.Combine(dataPath.ToString(), @"MessungSystems\XMPS2000", Path.GetFileName(file));
            //            if (!File.Exists(detination))
            //                File.Copy(file, detination);
            //        }
            //        string templetPath = Path.Combine(dataPath, @"MessungSystems\XMPS2000\ProjectTemplates");
            //        if (!Directory.Exists(templetPath))
            //            Directory.CreateDirectory(templetPath);
            //        CloneDirectory(appDataPath.ToString() + "\\ProjectTemplates", templetPath);
            //        string defaultPath = templetPath.Replace("\\ProjectTemplates", "\\XM Projects");
            //        Directory.CreateDirectory(defaultPath);
            //    }
            //}
        }

        private static void CloneDirectory(string root, string dest)
        {
            foreach (var directory in Directory.GetDirectories(root))
            {
                string dirName = Path.GetFileName(directory);
                if (!Directory.Exists(Path.Combine(dest, dirName)))
                {
                    Directory.CreateDirectory(Path.Combine(dest, dirName));
                }
                CloneDirectory(directory, Path.Combine(dest, dirName));
            }

            foreach (var file in Directory.GetFiles(root))
            {
                if (!File.Exists(Path.Combine(dest, Path.GetFileName(file))))
                    File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
            }


        }

        private void SplashForm_Shown(object sender, EventArgs e)
        {
            tmr.Interval = 5000;

            //starts the timer

            tmr.Start();
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            //after 3 sec stop the timer

            tmr.Stop();

            //display mainform

            frmMain mf = new frmMain();

            mf.Show();

            //hide this form

            this.Hide();
            this.Cursor = Cursors.Default;
        }
    }
}
