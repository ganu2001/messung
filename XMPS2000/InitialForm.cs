using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.App;
using File = System.IO.File;

namespace XMPS2000
{
    public partial class InitialForm : Form, IXMForm
    {
        private XMPS xm;
        public InitialForm()
        {
            InitializeComponent();

        }

        private void InitialForm_Load(object sender, EventArgs e)
        {
            xm = XMPS.Instance;
        }

        private void picLogoPlacement()
        {
            Image img = picLogo.Image;
            // Store the PictureBox's relative position before resizing it.  
            int rightMargin = picLogo.Parent.ClientSize.Width - (picLogo.Left + picLogo.Width);
            int bottomMargin = picLogo.Parent.ClientSize.Height - (picLogo.Top + picLogo.Height);

            // You could resize the PictureBox to fit the Image like this.  
            // It depends on what you want.  

            if (img.Width > picLogo.Parent.ClientSize.Width)
                picLogo.Width = picLogo.Parent.ClientSize.Width;
            else
                picLogo.Width = img.Width + picLogo.Width - picLogo.ClientSize.Width;

            if (img.Height > picLogo.Parent.ClientSize.Height)
                picLogo.Height = picLogo.Parent.ClientSize.Height;
            else
                picLogo.Height = img.Height + picLogo.Height - picLogo.ClientSize.Height;

            // Now move the PictureBox to its new Location  
            picLogo.Left = 5;
            picLogo.Top = picLogo.Parent.ClientSize.Height - picLogo.Height - 10;
        }

        private void picHomePlacement()
        {
            Image img = picHome.Image;
            // Store the PictureBox's relative position before resizing it.  
            int left = 0;
            int top = 0;
            int width = picHome.Parent.ClientSize.Width - pnlMenu.Width;
            int height = picHome.Parent.ClientSize.Height;
            // Now move the PictureBox to its new Location  
            picHome.Left = left;
            picHome.Top = top;
            picHome.Height = height;
            picHome.Width = width;
        }

        private void LoadRecent()
        {
            xm = XMPS.Instance;
            var projects = xm.RecentProjects.Projects;
            string actPath = xm.DefaultPath;
            foreach (string chcekproject in Directory.GetDirectories(actPath))
            {
                RecentProject AlreadyAddedProjects = new RecentProject();
                AlreadyAddedProjects.ProjectName = chcekproject.Split('\\').Last().ToString() + ".xmprj";
                AlreadyAddedProjects.ProjectPath = System.IO.Path.Combine(chcekproject, AlreadyAddedProjects.ProjectName);
                if (projects.Where(P => P.ProjectName == AlreadyAddedProjects.ProjectName).Count() == 0)
                    projects.Add(AlreadyAddedProjects);
            }
            //File.Delete(project);
            int YPos = lblRecentProject1.Bounds.Y;

            // Datatable for recent project
            DataTable dataTable = new DataTable();

            DataColumn Name = new DataColumn("ProjectName");
            dataTable.Columns.Add(Name);
            DataColumn Path = new DataColumn("ProjectPath");
            dataTable.Columns.Add(Path);
            DataColumn TimeAccessed = new DataColumn("TimeAccessed");
            TimeAccessed.DataType = typeof(DateTime);
            dataTable.Columns.Add(TimeAccessed);

            // Populate datatable
            foreach (var project in projects)
            {
                bool isDuplicate = dataTable.AsEnumerable().Any(row => row.Field<string>("ProjectName") == project.ProjectName &&
                                                                row.Field<string>("ProjectPath") == project.ProjectPath);
                if (!isDuplicate)
                {
                    DataRow dr = dataTable.NewRow();
                    dr["ProjectName"] = project.ProjectName;
                    dr["ProjectPath"] = project.ProjectPath;
                    dr["TimeAccessed"] = System.IO.File.GetLastAccessTime(project.ProjectPath);
                    dataTable.Rows.Add(dr);
                }
            }

            // Sort Table
            dataTable.DefaultView.Sort = "TimeAccessed desc";
            dataTable = dataTable.DefaultView.ToTable();
            int i = 0;
            foreach (DataRow project in dataTable.Rows)
            {
                if (File.Exists(project.ItemArray[1].ToString()))
                {
                    if (i < 10)
                    {
                        Label lblRecentProj = new Label();
                        lblRecentProj.Font = lblRecentProject1.Font;
                        lblRecentProj.ForeColor = lblRecentProject1.ForeColor;
                        lblRecentProj.Text = project.ItemArray[0].ToString();
                        lblRecentProj.Cursor = lblRecentProject1.Cursor;
                        pnlMenu.Controls.Add(lblRecentProj);
                        YPos = YPos + lblRecentProject1.Bounds.Height + 5;
                        lblRecentProj.SetBounds(lblRecentProject1.Bounds.X, YPos,
                                                lblRecentProject1.Bounds.Width + 100, lblRecentProject1.Bounds.Height);
                        lblRecentProj.Click += LblRecentProj_Click;
                        lblRecentProj.Show();
                        i++;
                    }
                }
            }

        }

        private void loadProject(object sender)
        {
            var project = xm.RecentProjects.Projects.Where(p => p.ProjectName == ((Label)sender).Text).LastOrDefault();
            xm.SetCurrentProject(project);
            var parent = this.ParentForm as frmMain;
            parent.LoadCurrentProject();
            parent.AddSystemTags();
            Application.OpenForms[1].Text = $"XMPS 2000 {((Label)sender).Text.Replace(".xmprj", "")}";
        }

        private void LblRecentProj_Click(object sender, EventArgs e)
        {
            if (xm.IsProjectModified())
            {
                string message = "Do you want save current project?";
                string title = "Save Current Project";
                MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    xm.SaveCurrentProject();
                    loadProject(sender);
                }
                else if (result == DialogResult.No)
                {
                    xm.MarkProjectModified(false);
                    loadProject(sender);
                }
                this.DialogResult = result;
            }
            else
            {
                loadProject(sender);
            }
        }

        private void mnuPlacement()
        {
            int left = pnlMenu.Parent.ClientSize.Width - pnlMenu.Width;
            int top = 0;

            pnlMenu.Height = pnlMenu.Parent.ClientSize.Height;

            pnlMenu.Left = left;
            pnlMenu.Top = top;
        }

        private void picLogo1Placement()
        {
            Image img = picLogo1.Image;
            // Store the PictureBox's relative position before resizing it.  
            int left = picLogo1.Parent.ClientSize.Width - img.Width - 5;
            int top = picLogo1.Parent.ClientSize.Height - img.Height - 5;

            // Now move the PictureBox to its new Location  
            picLogo1.Left = left;
            picLogo1.Top = top;
        }

        private void InitialForm_Shown(object sender, EventArgs e)
        {
            OnShown();
        }

        public void OnShown()
        {
            picLogoPlacement();
            picHomePlacement();
            picLogo1Placement();
            mnuPlacement();
            LoadRecent();
        }

        private void lblNew_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as frmMain;
            DialogResult dr = parent.NewProjectDialog();

            if (dr == DialogResult.OK)
                this.Hide();
        }

        private void lblOpen_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as frmMain;
            parent.OpenProjectDialog();

        }

    }
}
