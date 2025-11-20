using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using XMPS2000.Core.LadderLogic;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.DBHelper;

namespace XMPS2000.LadderLogic
{
    internal class FunctionBlock : System.Windows.Forms.GroupBox
    {
        private String text = "";
        private int fbheight = 10;
        XMPS xm;
        //Create Properties  
        public String DisplayText
        {
            get { return text; }
            set { text = value; Invalidate(); }
        }
        public FunctionBlock(OnlineMonitor appdata)
        {
            if (appdata != null)
            {
                xm = XMPS.Instance;
                GroupBox groupBox = new GroupBox();
                string Header = text;
                //this.ForeColor = Color.Transparent;
                Label LHeader = new Label();
                LHeader.Text = appdata.OpCodeNm.ToString();
                LHeader.BackColor = Color.Aqua;
                LHeader.ForeColor = Color.Red;
                LHeader.BorderStyle = BorderStyle.FixedSingle;
                LHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                LHeader.Size = new System.Drawing.Size(47, 29);
                LHeader.Location = new System.Drawing.Point(25, 0);
                groupBox.Controls.Add(LHeader);
                fbheight = 10;
                Label En = new Label();
                En.Location = new System.Drawing.Point(2, fbheight - 5);
                En.Text = "EN";
                En.AutoSize = true;
                if (!appdata.Enable.ToString().Contains("-"))
                {
                    ClsContact Ce = new ClsContact(GetTagDetails(appdata.Enable.ToString()));
                    Ce.Location = new System.Drawing.Point(1, fbheight + 2);
                    Ce.Width = 298;
                    this.Controls.Add(Ce);
                }
                else
                {
                    if (appdata.Enable == "-")
                    {
                        ClsLine Le = new ClsLine(""); Le.Location = new System.Drawing.Point(1, fbheight + 2);
                        Le.Width = 298;
                        this.Controls.Add(Le);
                    }
                    else
                    {
                        ClsLine Le = new ClsLine(GetTagDetails(appdata.Enable.ToString()));
                        Le.Location = new System.Drawing.Point(1, fbheight + 2);
                        Le.Width = 298;
                        this.Controls.Add(Le);
                    }
                }
                groupBox.Controls.Add(En);
                fbheight = fbheight + 28;
                //LinkLabel Eq = new LinkLabel();
                //Eq.Location = new System.Drawing.Point(221, 16);
                //Eq.Text = "ENO";
                //Eq.AutoSize = true;
                if (appdata.Input1.ToString() != "-")
                {
                    ClsLineWithLabel L1 = new ClsLineWithLabel(GetTagDetails(appdata.Input1.ToString()));
                    L1.Location = new System.Drawing.Point(0, fbheight + 10);
                    L1.Width = 300;
                    this.Controls.Add(L1);
                    Label In1 = new Label();
                    In1.Location = new System.Drawing.Point(6, fbheight);
                    In1.Text = "IN1";
                    if(appdata.TC_Name != "-" )
                    {
                        In1.Text = In1.Text + "     " + appdata.TC_Name.ToString(); 
                    }
                    In1.AutoSize = true;
                    groupBox.Controls.Add(In1);
                }

                if (appdata.Output1.ToString() != "-")
                {
                    if (appdata.Output1.ToString().Contains(".") || appdata.Output1.ToString().StartsWith("F2") || appdata.Output1.ToString().StartsWith("~F2"))
                    {
                        ClsCoil Lo = new ClsCoil(GetTagDetails(appdata.Output1.ToString()));
                        Lo.Location = new System.Drawing.Point(400, fbheight + 10);
                        Lo.Width = 200;
                        this.Controls.Add(Lo);
                    }
                    else
                    {
                        ClsLineWithLabel Lio = new ClsLineWithLabel(GetTagDetails(appdata.Output1.ToString()), false);
                        Lio.Location = new System.Drawing.Point(400, fbheight + 10);
                        Lio.Width = 200;
                        this.Controls.Add(Lio);

                    }
                    Label lblOut = new Label();
                    lblOut.Text = "OP";
                    lblOut.AutoSize = true;
                    lblOut.Location = new System.Drawing.Point(75, fbheight);
                    groupBox.Controls.Add(lblOut);
                    fbheight = fbheight + 18;
                }

                if (appdata.Input2.ToString() != "-")
                {

                    ClsLineWithLabel L2 = new ClsLineWithLabel(GetTagDetails(appdata.Input2.ToString()));
                    L2.Location = new System.Drawing.Point(0, fbheight + 10);
                    L2.Width = 300;
                    this.Controls.Add(L2);
                    Label In2 = new Label();
                    In2.AutoSize = true;
                    In2.Location = new System.Drawing.Point(6, fbheight);
                    In2.Text = "IN2";
                    groupBox.Controls.Add(In2);
                    fbheight = fbheight + 18;
                }

                if (appdata.Output2.ToString() != "-")
                {
                    if (appdata.Output2.ToString().Contains(".") || appdata.Output2.ToString().StartsWith("F2") || appdata.Output2.ToString().StartsWith("~F2"))
                    {
                        ClsCoil Lo1 = new ClsCoil(GetTagDetails(appdata.Output2.ToString()));
                        Lo1.Location = new System.Drawing.Point(400, fbheight + 5);
                        Lo1.Width = 200;
                        this.Controls.Add(Lo1);
                    }
                    else
                    {
                        ClsLineWithLabel Lio1 = new ClsLineWithLabel(GetTagDetails(appdata.Output2.ToString()), false);
                        Lio1.Location = new System.Drawing.Point(400, fbheight + 5);
                        Lio1.Width = 200;
                        this.Controls.Add(Lio1);
                    }
                    Label lblOutFM = new Label();
                    lblOutFM.Text = "OP2";
                    lblOutFM.AutoSize = true;
                    lblOutFM.Location = new System.Drawing.Point(70, fbheight);
                    groupBox.Controls.Add(lblOutFM);

                    if (appdata.Input3.ToString() == "-") fbheight = fbheight + 18;

                }
                // Output 3 ----->>
                if (appdata.Output3.ToString() != "-")
                {
                    if (appdata.Output3.ToString().Contains(".") || appdata.Output3.ToString().StartsWith("F2") || appdata.Output3.ToString().StartsWith("~F2"))
                    {
                        ClsCoil Lo2 = new ClsCoil(GetTagDetails(appdata.Output3.ToString()));
                        Lo2.Location = new System.Drawing.Point(400, fbheight + 5);
                        Lo2.Width = 200;
                        this.Controls.Add(Lo2);
                    }
                    else
                    {
                        ClsLineWithLabel Lio2 = new ClsLineWithLabel(GetTagDetails(appdata.Output3.ToString()), false);
                        Lio2.Location = new System.Drawing.Point(400, fbheight + 5);
                        Lio2.Width = 200;
                        this.Controls.Add(Lio2);
                    }
                    Label lblOutFM = new Label();
                    lblOutFM.Text = "OP3";
                    lblOutFM.AutoSize = true;
                    lblOutFM.Location = new System.Drawing.Point(70, fbheight);
                    groupBox.Controls.Add(lblOutFM);

                    if (appdata.Input4.ToString() == "-") fbheight = fbheight + 18;

                }

                if (appdata.Input3.ToString() != "-")
                {

                    ClsLineWithLabel L3 = new ClsLineWithLabel(GetTagDetails(appdata.Input3.ToString()));
                    L3.Location = new System.Drawing.Point(0, fbheight + 10);
                    L3.Width = 300;
                    this.Controls.Add(L3);
                    Label In3 = new Label();
                    In3.AutoSize = true;
                    In3.Location = new System.Drawing.Point(6, fbheight);
                    In3.Text = "IN3";
                    groupBox.Controls.Add(In3);
                    fbheight = fbheight + 18;
                }

                if (appdata.Input4.ToString() != "-")
                {

                    ClsLineWithLabel L4 = new ClsLineWithLabel(GetTagDetails(appdata.Input4.ToString()));
                    L4.Location = new System.Drawing.Point(0, fbheight + 10);
                    //L4.AutoSize = true;
                    L4.TextAlign = ContentAlignment.TopRight;
                    L4.Width = 300;
                    this.Controls.Add(L4);
                    Label In4 = new Label();
                    In4.AutoSize = true;
                    In4.Location = new System.Drawing.Point(6, fbheight);
                    In4.Text = "IN4";
                    In4.TextAlign = ContentAlignment.TopRight;
                    groupBox.Controls.Add(In4);
                    fbheight = fbheight + 18;
                }
                if (appdata.Input5.ToString() != "-")                                                   //Input 5 Later we have to add 3 Output
                {

                    ClsLineWithLabel L5 = new ClsLineWithLabel(GetTagDetails(appdata.Input5.ToString()));
                    L5.Location = new System.Drawing.Point(0, fbheight + 10);
                    //L4.AutoSize = true;
                    L5.TextAlign = ContentAlignment.TopRight;
                    L5.Width = 300;
                    this.Controls.Add(L5);
                    Label In5 = new Label();
                    In5.AutoSize = true;
                    In5.Location = new System.Drawing.Point(6, fbheight);
                    In5.Text = "IN5";
                    In5.TextAlign = ContentAlignment.TopRight;
                    groupBox.Controls.Add(In5);
                    fbheight = fbheight + 18;
                }
                //groupBox.Controls.Add(Eq);
                groupBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                groupBox.Location = new System.Drawing.Point(300, 10);
                groupBox.Width = 100;
                groupBox.Height = fbheight;
                //groupBox.BorderStyle = BorderStyle.FixedSingle;  
                //selectedgroupbox = groupBox;
                this.Controls.Add(groupBox);
                if (appdata.Comments.Length > 0)
                {
                    Label Comment = new Label();
                    Comment.ForeColor = Color.Green;
                    Comment.Text = " Comment : " + appdata.Comments.ToString();
                    Comment.Location = new System.Drawing.Point(455, 10);
                    Comment.Width = 300;
                    //Comment.AutoSize = true;
                    //Comment.Height *= 2;
                    //Comment.BorderStyle = BorderStyle.FixedSingle; 
                    this.Controls.Add(Comment);
                }
                this.DisplayText = "";
                this.Width = 850;
                this.Height = fbheight + 20;
            }
        }
        public FunctionBlock(ApplicationRung appdata)
        {
            xm = XMPS.Instance;
            GroupBox groupBox = new GroupBox();
            string Header = text;
            //this.ForeColor = Color.Transparent;
            Label LHeader = new Label();
            LHeader.Text = appdata.OpCodeNm.ToString();
            LHeader.BackColor = Color.Aqua;
            LHeader.ForeColor = Color.Red;
            LHeader.BorderStyle = BorderStyle.FixedSingle;
            LHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            LHeader.Size = new System.Drawing.Size(47, 29);
            LHeader.Location = new System.Drawing.Point(25, 0);
            groupBox.Controls.Add(LHeader);
            fbheight = 10;
            Label En = new Label();
            En.Location = new System.Drawing.Point(2, fbheight - 5);
            En.Text = "EN";
            En.AutoSize = true;
            if (!appdata.Enable.ToString().Contains("-"))
            {
                ClsContact Ce = new ClsContact(GetTagDetails(appdata.Enable.ToString()));
                Ce.Location = new System.Drawing.Point(1, fbheight + 2);
                Ce.Width = 298;
                this.Controls.Add(Ce);
            }
            else
            {
                if (appdata.Enable == "-")
                {
                    ClsLine Le = new ClsLine(""); Le.Location = new System.Drawing.Point(1, fbheight + 2);
                    Le.Width = 298;
                    this.Controls.Add(Le);
                }
                else
                {
                    ClsLine Le = new ClsLine(GetTagDetails(appdata.Enable.ToString()));
                    Le.Location = new System.Drawing.Point(1, fbheight + 2);
                    Le.Width = 298;
                    this.Controls.Add(Le);
                }
            }
            groupBox.Controls.Add(En);
            fbheight = fbheight + 28;
            //LinkLabel Eq = new LinkLabel();
            //Eq.Location = new System.Drawing.Point(221, 16);
            //Eq.Text = "ENO";
            //Eq.AutoSize = true;
            if (appdata.Inputs["Input1"].ToString() != "-")
            {
                ClsLineWithLabel L1 = new ClsLineWithLabel(GetTagDetails(appdata.Inputs["Input1"].ToString()));
                L1.Location = new System.Drawing.Point(0, fbheight + 10);
                L1.Width = 300;
                this.Controls.Add(L1);
                Label In1 = new Label();
                In1.Location = new System.Drawing.Point(6, fbheight);
                In1.Text = "IN1";
                if (appdata.TC_Name != "-")
                {
                    In1.Text = In1.Text + "     " + appdata.TC_Name.ToString();
                }
                In1.AutoSize = true;
                groupBox.Controls.Add(In1);
            }

            if (appdata.Inputs["Output1"].ToString() != "-")
            {
                if (appdata.Inputs["Output1"].ToString().Contains(".") || appdata.Inputs["Output1"].ToString().StartsWith("F2") || appdata.Inputs["Output1"].ToString().StartsWith("~F2"))
                {
                    ClsCoil Lo = new ClsCoil(GetTagDetails(appdata.Inputs["Output1"].ToString()));
                    Lo.Location = new System.Drawing.Point(400, fbheight + 10);
                    Lo.Width = 200;
                    this.Controls.Add(Lo);
                }
                else
                {
                    ClsLineWithLabel Lio = new ClsLineWithLabel(GetTagDetails(appdata.Inputs["Output1"].ToString()), false);
                    Lio.Location = new System.Drawing.Point(400, fbheight + 10);
                    Lio.Width = 200;
                    this.Controls.Add(Lio);

                }
                Label lblOut = new Label();
                lblOut.Text = "OP";
                lblOut.AutoSize = true;
                lblOut.Location = new System.Drawing.Point(75, fbheight);
                groupBox.Controls.Add(lblOut);
                fbheight = fbheight + 18;
            }

            if (appdata.Inputs["Input2"].ToString() != "-")
            {

                ClsLineWithLabel L2 = new ClsLineWithLabel(GetTagDetails(appdata.Inputs["Input2"].ToString()));
                L2.Location = new System.Drawing.Point(0, fbheight + 10);
                L2.Width = 300;
                this.Controls.Add(L2);
                Label In2 = new Label();
                In2.AutoSize = true;
                In2.Location = new System.Drawing.Point(6, fbheight);
                In2.Text = "IN2";
                groupBox.Controls.Add(In2);
                fbheight = fbheight + 18;
            }

            if (appdata.Inputs["Output2"].ToString() != "-")
            {
                if (appdata.Inputs["Output2"].ToString().Contains(".") || appdata.Inputs["Output2"].ToString().StartsWith("F2") || appdata.Inputs["Output2"].ToString().StartsWith("~F2"))
                {
                    ClsCoil Lo1 = new ClsCoil(GetTagDetails(appdata.Inputs["Output2"].ToString()));
                    Lo1.Location = new System.Drawing.Point(400, fbheight + 5);
                    Lo1.Width = 200;
                    this.Controls.Add(Lo1);
                }
                else
                {
                    ClsLineWithLabel Lio1 = new ClsLineWithLabel(GetTagDetails(appdata.Inputs["Output2"].ToString()), false);
                    Lio1.Location = new System.Drawing.Point(400, fbheight + 5);
                    Lio1.Width = 200;
                    this.Controls.Add(Lio1);
                }
                Label lblOutFM = new Label();
                lblOutFM.Text = "OP2";
                lblOutFM.AutoSize = true;
                lblOutFM.Location = new System.Drawing.Point(70, fbheight);
                groupBox.Controls.Add(lblOutFM);

                if (appdata.Inputs["input3"].ToString() == "-") fbheight = fbheight + 18;

            }
            //Output 3 ------------>
            if (appdata.Inputs["Output3"].ToString() != "-")
            {
                if (appdata.Inputs["Output3"].ToString().Contains(".") || appdata.Inputs["Output3"].ToString().StartsWith("F2") || appdata.Inputs["Output3"].ToString().StartsWith("~F2"))
                {
                    ClsCoil Lo2 = new ClsCoil(GetTagDetails(appdata.Inputs["Output3"].ToString()));
                    Lo2.Location = new System.Drawing.Point(400, fbheight + 5);
                    Lo2.Width = 200;
                    this.Controls.Add(Lo2);
                }
                else
                {
                    ClsLineWithLabel Lio2 = new ClsLineWithLabel(GetTagDetails(appdata.Inputs["Output3"].ToString()), false);
                    Lio2.Location = new System.Drawing.Point(400, fbheight + 5);
                    Lio2.Width = 200;
                    this.Controls.Add(Lio2);
                }
                Label lblOutFM = new Label();
                lblOutFM.Text = "OP3";
                lblOutFM.AutoSize = true;
                lblOutFM.Location = new System.Drawing.Point(70, fbheight);
                groupBox.Controls.Add(lblOutFM);

                if (appdata.Inputs["Input3"].ToString() == "-") fbheight = fbheight + 18;

            }

            if (appdata.Inputs["Input3"].ToString() != "-")
            {

                ClsLineWithLabel L3 = new ClsLineWithLabel(GetTagDetails(appdata.Inputs["Input3"].ToString()));
                L3.Location = new System.Drawing.Point(0, fbheight + 10);
                L3.Width = 300;
                this.Controls.Add(L3);
                Label In3 = new Label();
                In3.AutoSize = true;
                In3.Location = new System.Drawing.Point(6, fbheight);
                In3.Text = "IN3";
                groupBox.Controls.Add(In3);
                fbheight = fbheight + 18;
            }

            if (appdata.Inputs["Input4"].ToString() != "-")
            {

                ClsLineWithLabel L4 = new ClsLineWithLabel(GetTagDetails(appdata.Inputs["Input4"].ToString()));
                L4.Location = new System.Drawing.Point(0, fbheight + 10);
                //L4.AutoSize = true;
                L4.TextAlign = ContentAlignment.TopRight;
                L4.Width = 300;
                this.Controls.Add(L4);
                Label In4 = new Label();
                In4.AutoSize = true;
                In4.Location = new System.Drawing.Point(6, fbheight);
                In4.Text = "IN4";
                In4.TextAlign = ContentAlignment.TopRight;
                groupBox.Controls.Add(In4);
                fbheight = fbheight + 18;
            }
            if (appdata.Inputs["Input5"].ToString() != "-")                                                                       //Added new input to function Block
            {

                ClsLineWithLabel L5 = new ClsLineWithLabel(GetTagDetails(appdata.Inputs["Input5"].ToString()));
                L5.Location = new System.Drawing.Point(0, fbheight + 10);
                //L4.AutoSize = true;
                L5.TextAlign = ContentAlignment.TopRight;
                L5.Width = 300;
                this.Controls.Add(L5);
                Label In5 = new Label();
                In5.AutoSize = true;
                In5.Location = new System.Drawing.Point(6, fbheight);
                In5.Text = "IN5";
                In5.TextAlign = ContentAlignment.TopRight;
                groupBox.Controls.Add(In5);
                fbheight = fbheight + 18;
            }
            //groupBox.Controls.Add(Eq);
            groupBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            groupBox.Location = new System.Drawing.Point(300, 10);
            groupBox.Width = 100;
            groupBox.Height = fbheight;
            //groupBox.BorderStyle = BorderStyle.FixedSingle;  
            //selectedgroupbox = groupBox;
            this.Controls.Add(groupBox);
            if (appdata.Comments.Length > 0)
            {
                Label Comment = new Label();
                Comment.ForeColor = Color.Green;
                Comment.Text = " Comment : " + appdata.Comments.ToString();
                Comment.Location = new System.Drawing.Point(455, 10);
                Comment.Width = 300;
                //Comment.AutoSize = true;
                //Comment.Height *= 2;
                //Comment.BorderStyle = BorderStyle.FixedSingle; 
                this.Controls.Add(Comment);
            }
            this.DisplayText = "";
            this.Width = 850;
            this.Height = fbheight + 20;
        }

        private string GetTagDetails(string LogicalAddress)
        {
            string VariableName = "";
            string CheckName = "";
            if(LogicalAddress.LastIndexOf(':') > 3)
            {
                CheckName = LogicalAddress.Substring(0,LogicalAddress.LastIndexOf(':') - 1);
            }
            else
            {
                CheckName = LogicalAddress;
            }
            
            var TagInfo = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.LogicalAddress == CheckName.Replace("~", "") ).FirstOrDefault();
            if (TagInfo != null)
            {
                if (TagInfo.Tag != null && TagInfo.Tag != "")
                {
                    VariableName = TagInfo.Tag + " ( " + LogicalAddress + " )";
                }
                else
                {
                    VariableName = LogicalAddress;
                }
                //TagInfo.tag 
            }
            else
            {
                VariableName = LogicalAddress;
            }
            if (VariableName.Length < 25)
            {
                while (VariableName.Length < 45)
                {
                    VariableName = " " + VariableName;
                }
            }
            return VariableName;
        }

        private void Out_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("OutPut is clicked", "Click Event");
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //this.Text = text;
        //    //this.AutoSize = false;
        //    //draw a string of text label  
        //    //e.Graphics.DrawString(this.Text, this.Font, b, new Point(0, 0));
        //    //Pen p1 = new Pen(Color.Black, 3);
        //    //e.Graphics.DrawLine(p1, new Point(selectedgroupbox.Location.X , selectedgroupbox.Location.Y), new Point(selectedgroupbox.Location.X , selectedgroupbox.Width));
        //    //e.Graphics.DrawLine(p1, new Point(0, 100), new Point(200, 100));
        //    //e.Graphics.DrawLine(p1, new Point(0, 140), new Point(200, 140));
        //    //e.Graphics.DrawLine(p1, new Point(0, 180), new Point(200, 180));
        //    //e.Graphics.DrawLine(p1, new Point(0, 16), new Point(200, 16));
        //    //e.Graphics.DrawLine(p1, new Point(500, 58), new Point(1500, 58));
        //}
    }
}
