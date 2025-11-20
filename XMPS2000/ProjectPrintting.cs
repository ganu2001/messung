using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using XMPS2000.Core;
using XMPS2000.Core.App;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.LadderLogic;
using XMPS2000.Core.Types;
using XMPS2000.LadderLogic;
using static iTextSharp.text.Font;

namespace XMPS2000
{
    public static class ProjectPrintting
    {
        private static XMPS xm;

        /// <summary>
        /// Variable for Page Setup Printting 
        /// </summary>
        public static string pagesize { get; set; }
        public static bool IsLandscape { get; set; }
        public static float LeftMargin { get; set; }
        public static float RightMargin { get; set; }
        public static float TopMargin { get; set; }
        public static float BottomMargin { get; set; }
        public static string Header { get; set; }
        public static string Footer { get; set; }

        public static string TitleHeader { get; set; }
        public static string ProjectName { get; set; }

        public static string CustomerName { get; set; }
        public static string TitleDate { get; set; }

        public static string Profile { get; set; }
        /// <summary>
        /// Function to Generate PDF Document from given parameteres and in given sequence
        /// </summary>
        /// <param name="filepath"></param> path of the File
        public static void GeneratePdfDocument(string filepath)
        {
            xm = XMPS.Instance;
            float topmargin = 0, leftmargin = 0, rightmargin = 0, bottommargin = 0;

            bool fileError = false;
            if (LeftMargin == 0)
            {
                LeftMargin = 25;
                RightMargin = 25;
                TopMargin = 25;
                BottomMargin = 25;
                pagesize = "A4";
                IsLandscape = true;
                Header = "Main Application Program";
                TitleHeader = "Project Documentation";
                CustomerName = "Name of Customer :";
                TitleDate = DateTime.Now.Day.ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month).ToString() + "-" + DateTime.Now.Year.ToString(); ;
                ProjectName = xm.LoadedProject.ProjectPath.Split(Path.DirectorySeparatorChar).Last();
                Footer = "Project Name : " + ProjectName.ToString() + " Date : " + TitleDate.ToString() + " Page No. @";
            }
            if (filepath != null)
            {
                if (File.Exists(filepath))
                {
                    try
                    {
                        File.Delete(filepath);
                    }
                    catch (IOException ex)
                    {
                        fileError = true;
                        MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //("It wasn't possible to write the data to the disk." + ex.Message)
                        return;
                    }
                }
                if (!fileError)
                {
                    using (FileStream stream = new FileStream(filepath, FileMode.Create))
                    {

                        leftmargin = LeftMargin;
                        rightmargin = RightMargin;
                        topmargin = TopMargin;
                        bottommargin = BottomMargin;
                        iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document((IsLandscape ? PageSize.A4.Rotate() : PageSize.A4), leftmargin, rightmargin, topmargin, bottommargin);
                        try
                        {
                            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, stream);
                            pdfWriter.PageEvent = new ITextEvents(Header, Footer);

                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                        pdfDoc.Open();
                        //use as header space  

                        DataTable dt = new DataTable();
                        dt.Columns.Add("Title");
                        //Create a single column table
                        var t = new PdfPTable(1);
                        //Tell it to fill the page horizontally
                        t.WidthPercentage = 100;
                        //Create a single cell
                        var c = new PdfPCell();
                        //Tell the cell to vertically align in the middle
                        c.VerticalAlignment = Element.ALIGN_MIDDLE;
                        //Tell the cell to fill the page vertically
                        c.MinimumHeight = pdfDoc.PageSize.Height - (pdfDoc.BottomMargin + pdfDoc.TopMargin);
                        c.PaddingLeft = IsLandscape ? 500 : 350;
                        //Create a test paragraph
                        //Font font = Font(FontFamily.GenericSerif, "25", Font.BOLD);
                        Font font = new Font(FontFamily.HELVETICA, 25, Font.BOLD);
                        var p = new Paragraph(TitleHeader, font);
                        //Add it a couple of times
                        c.AddElement(p);
                        p = new Paragraph(ProjectName, font);
                        //Add it a couple of times
                        c.AddElement(p);
                        p = new Paragraph(CustomerName, font);
                        //Add it a couple of times
                        c.AddElement(p);
                        p = new Paragraph(TitleDate, font);
                        //Add it a couple of times
                        c.AddElement(p);
                        p = new Paragraph(Profile, font);
                        //Add it a couple of times
                        c.AddElement(p);
                        //Add it a couple of times
                        //Add the cell to the paragraph
                        t.AddCell(c);
                        //Add the table to the document
                        pdfDoc.Add(t);
                        pdfDoc.NewPage();
                        p = new Paragraph("IO CONFIGURATION", font);
                        pdfDoc.Add(p);
                        p = new Paragraph(" ", font);
                        pdfDoc.Add(p);
                        pdfDoc.Add(Exporttopdf(getConfigIOData()));
                        pdfDoc.Add(Exporttopdf(ProjectHelper.getIO_ConfigurationDataForPrintting()));
                        pdfDoc.NewPage();
                        p = new Paragraph("COM settings", font);
                        pdfDoc.Add(p);
                        p = new Paragraph(" ", font);
                        pdfDoc.Add(p);
                        pdfDoc.Add(Exporttopdf(getcomrecords()));
                        pdfDoc.NewPage();
                        p = new Paragraph("Ethernet settings", font);
                        pdfDoc.Add(p);
                        p = new Paragraph(" ", font);
                        pdfDoc.Add(p);
                        pdfDoc.Add(Exporttopdf(getEthernetrecords()));
                        pdfDoc.NewPage();
                        dt = ProjectHelper.ToDataTable(LoadRetentiveAddressApplicationrecords());
                        if (dt.Rows.Count > 0)
                        {
                            p = new Paragraph("Retentive Address List", font);
                            pdfDoc.Add(p);
                            p = new Paragraph(" ", font);
                            pdfDoc.Add(p);
                            pdfDoc.Add((Exporttopdf(ProjectHelper.ToDataTable(LoadRetentiveAddressApplicationrecords().Select(d => new { d.LogicalAddress, d.RetentiveAddress, d.Tag }).ToList()))));
                            pdfDoc.NewPage();
                        }
                        dt = ProjectHelper.ToDataTable(LoadAddressInitialrecords());
                        if (dt.Rows.Count > 0)
                        {
                            p = new Paragraph("Initial Address List", font);
                            pdfDoc.Add(p);
                            p = new Paragraph(" ", font);
                            pdfDoc.Add(p);
                            pdfDoc.Add((Exporttopdf(ProjectHelper.ToDataTable(LoadAddressInitialrecords().Select(d => new { d.LogicalAddress, d.InitialValue, d.Tag }).ToList()))));
                            pdfDoc.NewPage();
                        }
                        dt = ProjectHelper.ToDataTable(LoadMemoryAddressApplicationrecords());
                        if (dt.Rows.Count > 0)
                        {
                            p = new Paragraph("Memory Address List", font);
                            pdfDoc.Add(p);
                            p = new Paragraph(" ", font);
                            pdfDoc.Add(p);
                            pdfDoc.Add((Exporttopdf(ProjectHelper.ToDataTable(LoadMemoryAddressApplicationrecords().Select(d => new { d.LogicalAddress, d.Tag }).ToList()))));
                            pdfDoc.NewPage();
                        }
                        dt = LoadMainLadderBlocks();
                        if (dt.Rows.Count > 0)
                        {
                            p = new Paragraph("Blocks Added in Main Logic", font);
                            pdfDoc.Add(p);
                            p = new Paragraph(" ", font);
                            pdfDoc.Add(p);
                            pdfDoc.Add((Exporttopdf(LoadMainLadderBlocks())));
                            pdfDoc.NewPage();
                        }
                        dt = ProjectHelper.BeforeSaveToDataTable(LoadApplicationrecords());
                        if (dt.Rows.Count > 0)
                        {
                            p = new Paragraph("Application Logic", font);
                            pdfDoc.Add(p);
                            p = new Paragraph(" ", font);
                            pdfDoc.Add(p);
                            foreach (Block BlockName in xm.LoadedProject.Blocks)
                            {
                                p = new Paragraph(BlockName.Name.ToString(), font);
                                pdfDoc.Add(p);
                                p = new Paragraph(" ", font);
                                pdfDoc.Add(p);
                                pdfDoc.Add(ExportImagestopdf(LoadApplicationrecordsImages(BlockName.Name.ToString())));
                                pdfDoc.NewPage();
                                pdfDoc.NewPage();
                            }

                        }
                        if (LoadModBusRTUMasterrecords().Count() > 0)
                        {
                            p = new Paragraph("Modbus RTU Master", font);
                            pdfDoc.Add(p);
                            p = new Paragraph(" ", font);
                            pdfDoc.Add(p);
                            pdfDoc.Add(Exporttopdf(ProjectHelper.ToDataTable(LoadModBusRTUMasterrecords())));
                            pdfDoc.NewPage();
                        }
                        if (LoadModBusTCPServerrecords().Count() > 0)
                        {
                            p = new Paragraph("Modbus TCP Server", font);
                            pdfDoc.Add(p);
                            p = new Paragraph(" ", font);
                            pdfDoc.Add(p);
                            pdfDoc.Add(Exporttopdf(ProjectHelper.ToDataTable(LoadModBusTCPServerrecords())));
                            pdfDoc.NewPage();
                        }
                        if (LoadModBusTCPClientrecords().Count() > 0)
                        {
                            p = new Paragraph("Modbus TCP Client", font);
                            pdfDoc.Add(p);
                            p = new Paragraph(" ", font);
                            pdfDoc.Add(p);
                            pdfDoc.Add(Exporttopdf(ProjectHelper.ToDataTable(LoadModBusTCPClientrecords().Select(M => new { M.Name, M.ServerIPAddress, M.Port, M.Polling, M.DeviceId, M.Address, M.Length, M.Variable, M.Functioncode }).ToList())) ?? Exporttopdf(dt));
                        }
                        pdfDoc.Close();
                        stream.Close();
                    }
                }
            }
        }

        private static PdfPTable ExportImagestopdf(DataGridView dataGridView1)
        {
            ///https://www.aspsnippets.com/questions/121587/Export-DataGridView-with-Images-to-PDF-using-C-and-VBNet-in-Windows-Application/
            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 99;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable.DefaultCell.BorderWidth = 1;
            //Adding Header row
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }
            //https://stackoverflow.com/questions/9208482/how-to-set-the-cell-width-in-itextsharp-pdf-creation
            int[] intTblWidth = { 5, 95 };
            pdfTable.SetWidths(intTblWidth);
            //Adding DataRow
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string id = (row.Index + 1).ToString(); //row.Cells[0].FormattedValue.ToString()
                    pdfTable.AddCell(id);
                    //https://stackoverflow.com/questions/6597676/bitmapimage-to-byte
                    byte[] data;
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    System.Drawing.Bitmap bitmapimg = (System.Drawing.Bitmap)row.Cells[1].Value;
                    BitmapImage bitmapImage = ConvertBitmap(bitmapimg);
                    encoder.Frames.Add((BitmapFrame.Create(bitmapImage)));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        encoder.Save(ms);
                        data = ms.ToArray();
                    }
                    byte[] imageByte = data; // (byte[])row.Cells[1].Value;
                    iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(imageByte);
                    pdfTable.AddCell(myImage);
                }
            }
            return pdfTable;
        }

        public static BitmapImage ConvertBitmap(System.Drawing.Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();

            return image;
        }

        private static DataGridView LoadApplicationrecordsImages(string BlockName)
        {
            DataGridView dataBlocksGridView = new DataGridView();
            dataBlocksGridView.Columns.Clear();             //clear data
            dataBlocksGridView.ColumnCount = 1;
            dataBlocksGridView.BackgroundColor = System.Drawing.Color.White;
            dataBlocksGridView.Columns[0].Name = "Rung no";
            // Grid define image column
            DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
            iconColumn.Name = "Function Block";                                                 //--todo--Change name
            iconColumn.HeaderText = "Blocks";
            dataBlocksGridView.Columns.Add(iconColumn);
            dataBlocksGridView.Update();
            List<ApplicationRung> ApRec = xm.LoadedProject.LogicRungs.Where(d => d.WindowName.EndsWith(BlockName.ToString())).OrderBy(d => d.LineNumber).ToList();
            foreach (ApplicationRung applicationRung in ApRec)
            {
                FunctionBlock functionBlock = new FunctionBlock(applicationRung);
                System.Drawing.Bitmap bitmap1 = new System.Drawing.Bitmap(functionBlock.Size.Width, functionBlock.Size.Height);
                functionBlock.DrawToBitmap(bitmap1, new System.Drawing.Rectangle(0, 0, functionBlock.Size.Width, functionBlock.Size.Height));
                object[] row = { $"", bitmap1 };
                dataBlocksGridView.Columns[1].Width = functionBlock.Size.Width;
                dataBlocksGridView.Rows.Add(row);
                //dataTable.Rows.Add(row);
                dataBlocksGridView.Update();
                dataBlocksGridView.Refresh();
                functionBlock.Dispose();
            }
            return dataBlocksGridView;
        }

        private static DataTable LoadMainLadderBlocks()
        {
            DataTable MainBlockList = new DataTable();
            MainBlockList.Columns.Add("Sequence and Block Name Added");
            foreach (string AddedBlock in xm.LoadedProject.MainLadderLogic)
            {
                DataRow Dr = MainBlockList.NewRow();
                Dr["Sequence and Block Name Added"] = AddedBlock;
                MainBlockList.Rows.Add(Dr);
            }
            return MainBlockList;
        }

        private static List<MODBUSTCPClient_Slave> LoadModBusTCPClientrecords()
        {
            var modBUSTCPClient = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
            return modBUSTCPClient.Slaves;
        }

        private static List<MODBUSTCPServer_Request> LoadModBusTCPServerrecords()
        {
            var modBUSTCPServer = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
            return modBUSTCPServer.Requests;
        }

        private static List<MODBUSRTUMaster_Slave> LoadModBusRTUMasterrecords()
        {
            var modBUSRTUMaster = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
            return modBUSRTUMaster.Slaves;
        }

        private static List<ApplicationRung> LoadApplicationrecords()
        {
            return xm.LoadedProject.LogicRungs.ToList();
        }

        private static List<XMIOConfig> LoadMemoryAddressApplicationrecords()
        {
            List<XMIOConfig> MTags = xm.LoadedProject.Tags.Where(d => d.Model == "").ToList();
            return MTags;
        }

        private static List<XMIOConfig> LoadAddressInitialrecords()
        {
            List<XMIOConfig> ITags = xm.LoadedProject.Tags.Where(d => d.InitialValue != null && d.InitialValue != "").ToList();
            return ITags;
        }

        private static List<XMIOConfig> LoadRetentiveAddressApplicationrecords()
        {
            List<XMIOConfig> RTags = xm.LoadedProject.Tags.Where(d => d.RetentiveAddress != null && d.RetentiveAddress != "").ToList();
            return RTags;
        }

        private static DataTable getEthernetrecords()
        {
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Use DHCP");
            Dt.Columns.Add("IP Address");
            Dt.Columns.Add("Subnet");
            Dt.Columns.Add("Gateway");
            Dt.Columns.Add("Port Number");
            DataRow Dr1 = Dt.NewRow();
            var ethernetset = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
            //Dr1["ConfigType"] = "Ethernet Settings";
            Dr1["Use DHCP"] = Convert.ToByte(ethernetset.UseDHCPServer);
            Dr1["IP Address"] = ethernetset.EthernetIPAddress;
            Dr1["Subnet"] = ethernetset.EthernetSubNet;
            Dr1["Gateway"] = ethernetset.EthernetGetWay;
            Dr1["Port Number"] = ethernetset.Port;
            Dt.Rows.Add(Dr1);
            return Dt;
        }

        private static DataTable getcomrecords()
        {
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Baud Rate");
            Dt.Columns.Add("Data Length");
            Dt.Columns.Add("Stop Bit");
            Dt.Columns.Add("Parity");
            Dt.Columns.Add("SendDelay");
            Dt.Columns.Add("MinimumInterface");
            Dt.Columns.Add("CommunicationTimeout");
            Dt.Columns.Add("NoOfRetries");
            var comsetting = (COMDevice)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "COMDevice").FirstOrDefault();
            DataRow Dr = Dt.NewRow();
            //Dr["ConfigType"] = "COM Settings";
            Dr["Baud Rate"] = ((int)Enum.Parse(typeof(COMBaudRate), "_" + comsetting.BaudRate.ToString())) - 1;
            Dr["Data Length"] = ((int)Enum.Parse(typeof(COMDataLength), "_" + comsetting.DataLength.ToString())) - 1;  //comsetting.DataLength;
            Dr["Stop Bit"] = ((int)Enum.Parse(typeof(COMStopBit), "_" + comsetting.StopBit.ToString())) - 1;  //comsetting.StopBit;
            Dr["Parity"] = ((int)Enum.Parse(typeof(COMParity), comsetting.Parity.ToString())) - 1;  //comsetting.Parity;
            Dr["SendDelay"] = comsetting.SendDelay;
            Dr["MinimumInterface"] = comsetting.MinimumInterface;
            Dr["CommunicationTimeout"] = comsetting.CommunicationTimeout;
            Dr["NoOfRetries"] = comsetting.NumberOfRetries;
            Dt.Rows.Add(Dr);
            return Dt;
        }


        private static DataTable getConfigIOData()
        {
            // IO Config data

            DataTable Dt = new DataTable();

            Dt.Columns.Add("ConfigType");
            Dt.Columns.Add("On-Board IO");
            Dt.Columns.Add("Remote IO");
            Dt.Columns.Add("Expansion");
            Dt.Columns.Add("Total");

            DataRow DrDI = Dt.NewRow();
            DrDI["ConfigType"] = "Digital Input";
            DrDI["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.DigitalInput && d.IoList == IOListType.OnBoardIO).Count();
            DrDI["Remote IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.DigitalInput && d.IoList == IOListType.RemoteIO).Count();
            DrDI["Expansion"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.DigitalInput && d.IoList == IOListType.ExpansionIO).Count();
            DrDI["Total"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.DigitalInput).Count();
            Dt.Rows.Add(DrDI);
            DataRow DrDO = Dt.NewRow();
            DrDO["ConfigType"] = "Digital Output";
            DrDO["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.DigitalOutput && d.IoList == IOListType.OnBoardIO).Count();
            DrDO["Remote IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.DigitalOutput && d.IoList == IOListType.RemoteIO).Count();
            DrDO["Expansion"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.DigitalOutput && d.IoList == IOListType.ExpansionIO).Count();
            DrDO["Total"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.DigitalOutput).Count();
            Dt.Rows.Add(DrDO);
            DataRow DrAI = Dt.NewRow();
            DrAI["ConfigType"] = "Analog Input";
            DrAI["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.AnalogInput && d.IoList == IOListType.OnBoardIO).Count();
            DrAI["Remote IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.AnalogInput && d.IoList == IOListType.RemoteIO).Count();
            DrAI["Expansion"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.AnalogInput && d.IoList == IOListType.ExpansionIO).Count();
            DrAI["Total"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.AnalogInput).Count();
            Dt.Rows.Add(DrAI);
            DataRow DrAO = Dt.NewRow();
            DrAO["ConfigType"] = "Analog Output";
            DrAO["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.AnalogOutput && d.IoList == IOListType.OnBoardIO).Count();
            DrAO["Remote IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.AnalogOutput && d.IoList == IOListType.RemoteIO).Count();
            DrAO["Expansion"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.AnalogOutput && d.IoList == IOListType.ExpansionIO).Count();
            DrAO["Total"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.AnalogOutput).Count();
            Dt.Rows.Add(DrAO);

            return Dt;
        }

        /// <summary>
        /// Export Data from Datatable to PDF Table
        /// </summary>
        /// <param name="dataTable"></param>Data Table to convert
        /// <returns></returns>converted PDF table
        public static PdfPTable Exporttopdf(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Columns.Contains("Key"))
                {
                    dataTable.Columns.Remove("Key");
                }
                try
                {
                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);
                    pdfTable.DefaultCell.Padding = 3;
                    pdfTable.WidthPercentage = 100;
                    pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                    pdfTable.HeaderRows = 1;
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName.ToString()));
                        pdfTable.AddCell(cell);
                    }

                    foreach (DataRow row in dataTable.Rows)
                    {
                        for (int col = 0; col < dataTable.Columns.Count; col++)
                        {
                            pdfTable.AddCell(row[col].ToString());
                        }
                    }


                    return (pdfTable);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }



    }
}
