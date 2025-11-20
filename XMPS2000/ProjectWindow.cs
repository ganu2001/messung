using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using System.IO;
using System.Drawing;
using Image = System.Drawing.Image;

namespace XMPS2000
{

    public class ProjectWindow : Form
    {
        private PictureBox pictureBox1;
        private Point lineStart;
        private Point lineEnd;
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;
        private List<string> imagePath = new List<string>();
        private FlowLayoutPanel expansionPanel;
        XMPS xm;
        public ProjectWindow()
        {
            xm = XMPS.Instance;
            InitializeComponent();
            if (xm.LoadedProject != null)
            {
                string _modelName = xm.PlcModel.ToString();
                if (_modelName == null) return;
                var imagePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\ProjectTemplates\" + _modelName + "\\" + _modelName + ".jpg";
                if (File.Exists(imagePath))
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            lineStart = new Point(pictureBox1.Right, pictureBox1.Bottom / 2);
            lineEnd = new Point(lineStart.X + 100, lineStart.Y);
        }
        //Draw Line Between picture boxes
        private void ProjectWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //ControlPaint.DrawBorder(g, pictureBox1.Bounds, Color.Black, ButtonBorderStyle.Solid);
            //ControlPaint.DrawBorder(g, expansionPanel.Bounds, Color.Black, ButtonBorderStyle.Solid);
            g.DrawLine(Pens.Black, lineStart, lineEnd);
            DrawArrowhead(g, Pens.Black, lineStart, lineEnd, 20, 10);
            AddPictureBoxes();
        }

        //Draw ArrowHead to drwan line Between picture boxes
        private void DrawArrowhead(Graphics g, Pen pen, Point start, Point end, int width, int height)
        {
            float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            PointF[] arrowheadPoints = new PointF[3];
            arrowheadPoints[0] = end;
            arrowheadPoints[1] = new PointF(end.X - width, end.Y - height / 2.0f);
            arrowheadPoints[2] = new PointF(end.X - width, end.Y + height / 2.0f);
            g.FillPolygon(Brushes.Black, arrowheadPoints);
        }
        
        //Creating PB to add added Expansion Devices....
        private void AddPictureBoxes()
        {
            int pictureBoxWidth = 429;
            int pictureBoxHeight = 298;
            string imagePath1;
            List<string> addedDevices = xm.LoadedProject.Tags.Where(T => T.IoList == Core.Types.IOListType.ExpansionIO).Select(T => T.Model).Distinct().ToList();
            foreach (string device in addedDevices)
            {
                string baseModel = device.Contains('_') ? device.Substring(0, device.LastIndexOf('_')) : device;
                imagePath1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\Devices\\" + baseModel + ".png";
                if (File.Exists(imagePath1))
                {
                    PictureBox pictureBox = new PictureBox
                    {
                        Size = new Size(pictureBoxWidth - 150, pictureBoxHeight - 25),
                        Image = Image.FromFile(imagePath1),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        ImageLocation = imagePath1
                    };
                    expansionPanel.Controls.Add(pictureBox);
                }
            }
            for (int i = 0; i < imagePath.Count; i++)
            {
                bool imageAlreadyExists = false;
                foreach (Control control in expansionPanel.Controls)
                {
                    if (control is PictureBox existingPictureBox)
                    {
                        if (existingPictureBox.ImageLocation == imagePath[i])
                        {
                            imageAlreadyExists = true;
                            break;
                        }
                    }
                }

                if (!imageAlreadyExists)
                {
                    PictureBox pictureBox = new PictureBox
                    {
                        Size = new Size(pictureBoxWidth - 150, pictureBoxHeight - 25),
                        Image = Image.FromFile(imagePath[i]),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        ImageLocation = imagePath[i] // Store the image path for comparison
                    };

                    expansionPanel.Controls.Add(pictureBox);
                }
                //PictureBox pictureBox = new PictureBox();
                //pictureBox.Size = new Size(pictureBoxWidth-150, pictureBoxHeight -25);
                //pictureBox.Image = Image.FromFile(imagePath[i]);
                //pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                //expansionPanel.Controls.Add(pictureBox);
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.expansionPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(329, 298);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // expansionPanel
            // 
            this.expansionPanel.AutoScroll = true;
            this.expansionPanel.AutoScrollMinSize = new System.Drawing.Size(2, 0);
            this.expansionPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.expansionPanel.Location = new System.Drawing.Point(441, 34);
            this.expansionPanel.Name = "expansionPanel";
            this.expansionPanel.Size = new System.Drawing.Size(605, 298);
            this.expansionPanel.TabIndex = 1;
            this.expansionPanel.WrapContents = false;
            // 
            // ProjectWindow
            // 
            this.ClientSize = new System.Drawing.Size(1056, 353);
            this.Controls.Add(this.expansionPanel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectWindow";
            this.ShowIcon = false;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ProjectWindow_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
