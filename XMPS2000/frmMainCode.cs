using iTextSharp.text.pdf;
using iTextSharp.xmp;
using LadderDrawing;
using LadderDrawing.UserControls;
using LadderEditorLib.DInterpreter;
using LadderEditorLib.MementoDesign;
using LadderEditorLib.UserControls;
using Microsoft.VisualBasic.ApplicationServices;
using RawPrint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using XMPS2000.Bacnet;
using XMPS2000.Configuration;
using XMPS2000.Core;
using XMPS2000.Core.App;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.LadderLogic;
using XMPS2000.Core.Types;
using XMPS2000.LadderLogic;
using XMPS2000.UndoRedoGridLayout;
using Application = System.Windows.Forms.Application;
using Cursors = System.Windows.Forms.Cursors;
using ProjectInfo = XMPS2000.Core.App.ProjectInfo;

namespace XMPS2000
{
    partial class frmMain
    {

        private TreeNode _listObjectTreeNode;
        private readonly PLCForceFunctionality forceFunctionality = PLCForceFunctionality.Instance;

        private int prevScreenIndex = -1;
        private int _zoomFactor = 100;
        private bool _LoggedIn = false;
        private string _devicetype = "";
        //saving old element by deepcloning
        private (LadderDesignMemento, string) currentUDFBElements;
        private List<string> currentUDFBBlockElements;
        private bool _isMemory = false;

        #region Initialize Form

        private void InitializeMenuBar()
        {

            //Project
            MenuProjectNew.Image = imgListToolbar.Images["NewProject"];
            MenuProjectOpen.Image = imgListToolbar.Images["OpenProject"];
            MenuProjectSave.Image = imgListToolbar.Images["SaveProject"];
            MenuProjectSaveAs.Image = imgListToolbar.Images["SaveProjectAs"];
            MenuProjectExit.Image = imgListToolbar.Images["Exit"];

            //Edit
            MenuEditUndo.Image = imgListToolbar.Images["Undo"];
            MenuEditRedo.Image = imgListToolbar.Images["Redo"];
            MenuEditUndo.Image = imgListToolbar.Images["Undo"];
            MenuEditCut.Image = imgListToolbar.Images["Cut"];
            MenuEditCopy.Image = imgListToolbar.Images["Copy"];
            MenuEditPaste.Image = imgListToolbar.Images["Paste"];
            MenuEditDelete.Image = imgListToolbar.Images["Delete"];
            MenuEditFindNReplace.Image = imgListToolbar.Images["Find"];

            //View

            MenuViewDInfo.Image = imgListToolbar.Images["Info"];
            MenuViewCompErr.Image = imgListToolbar.Images["Error"];
            MenuViewProject.Image = imgListToolbar.Images["ProjectView"];

            //Mode

            MenuModeLogin.Image = imgListToolbar.Images["Login"];
            MenuModeLogout.Image = imgListToolbar.Images["Logout"];
            MenuModeDnldProject.Image = imgListToolbar.Images["Download"];
            MenuModeUpldProject.Image = imgListToolbar.Images["Upload"];
            MenuModeOfflineSim.Image = imgListToolbar.Images["Simulation"];
            MenuModePLCStart.Image = imgListToolbar.Images["PLCStart"];
            MenuModePLCStop.Image = imgListToolbar.Images["PLCStop"];
            MenuModeCompile.Image = imgListToolbar.Images["Compile"];
        }

        private void InitializeToolbar()
        {
            tspMain.ImageList = imgListToolbar;
            strpBtnNewProject.ImageIndex = 0;
            strpBtnOpenProject.ImageIndex = 1;
            strpBtnSaveProject.ImageIndex = 2;
            strpBtnCloseProject.ImageIndex = 3;

            strpBtnUploadProject.ImageIndex = 5;
            strpBtnDownloadProject.ImageIndex = 4;

            //strpBtnZoomIn.ImageIndex = 6;
            //strpBtnZoomOut.ImageIndex = 7;

            strpBtnCompile.ImageIndex = 8;
            strpBtnLogin.ImageIndex = 9;
            strpBtnLogout.ImageIndex = 10;
            strpBtnOnlineMonitor.ImageIndex = 11;

            strpBtnCut.ImageIndex = 12;
            strpBtnCopy.ImageIndex = 13;
            strpBtnPaste.ImageIndex = 14;
            strpBtnSelect.ImageIndex = 15;

            strpBtnUndo.ImageIndex = 16;
            strpBtnRedo.ImageIndex = 17;
            strpBtnDelete.ImageIndex = 18;
            strpBtnPrvScreen.ImageIndex = 19;
            strpBtnNxtScreen.ImageIndex = 20;
            strpBtnFind.ImageIndex = 21;
            strpBtnHelp.ImageIndex = 22;


            strpBtnNewProject.Tag = "NEW";
            strpBtnOpenProject.Tag = "OPEN";
            strpBtnSaveProject.Tag = "SAVE";
            strpBtnCloseProject.Tag = "CLOSE";

            strpBtnUploadProject.Tag = "UPLOAD";
            strpBtnDownloadProject.Tag = "DOWNLOAD";

            //strpBtnZoomIn.Tag = "ZOOMIN";
            //strpBtnZoomOut.Tag = "ZOOMOUT";

            strpBtnCompile.Tag = "COMPILE";
            strpBtnLogin.Tag = "LOGIN";
            strpBtnLogout.Tag = "LOGOUT";
            strpBtnOnlineMonitor.Tag = "ONLINEMONITORING";

            strpBtnCut.Tag = "CUT";
            strpBtnCopy.Tag = "COPY";
            strpBtnPaste.Tag = "PASTE";
            strpBtnSelect.Tag = "SELECT";

            strpBtnUndo.Tag = "UNDO";
            strpBtnRedo.Tag = "REDO";
            strpBtnDelete.Tag = "DELETE";
            strpBtnPrvScreen.Tag = "NEXTSCREEN";
            strpBtnNxtScreen.Tag = "PREVSCREEN";
            strpBtnFind.Tag = "FIND";
            strpBtnHelp.Tag = "HELP";
            MQTTScreenName.Tag = "MQTTBLKName";

            strpBtnPrvScreen.Enabled = false;
            strpBtnNxtScreen.Enabled = false;
        }

        //void CloseApp()
        //{
        //    ExitApp(); 
        //}

        void ExitApp()
        {
            if (isExiting)
                return;

            DialogResult result = CheckandSaveApp();

            if (result == DialogResult.Cancel)
                return;

            if (xm != null && xm.Entries != null)
                xm.Entries.Clear();
            isExiting = true;

            Application.ExitThread();
        }

        private DialogResult CheckandSaveApp()
        {
            if (xm.IsProjectModified())
            {
                using (Form prompt = new Form())
                {
                    prompt.Width = 360;
                    prompt.Height = 160;
                    prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                    prompt.Text = "Save Current Project";
                    prompt.StartPosition = FormStartPosition.CenterScreen;
                    prompt.MaximizeBox = false;
                    prompt.MinimizeBox = false;
                    prompt.ShowIcon = false;

                    Label textLabel = new Label()
                    {
                        Left = 20,
                        Top = 20,
                        Width = 320,
                        Height = 40,
                        Text = "Do you want to save the current project?",
                        Font = new Font("Segoe UI", 11, FontStyle.Regular)
                    };

                    Button saveButton = new Button() { Text = "Save", Left = 30, Width = 80, Top = 80, DialogResult = DialogResult.Yes };
                    Button dontSaveButton = new Button() { Text = "Don't Save", Left = 130, Width = 100, Top = 80, DialogResult = DialogResult.No };
                    Button cancelButton = new Button() { Text = "Cancel", Left = 250, Width = 80, Top = 80, DialogResult = DialogResult.Cancel };

                    prompt.Controls.AddRange(new Control[] { textLabel, saveButton, dontSaveButton, cancelButton });
                    prompt.AcceptButton = saveButton;
                    prompt.CancelButton = cancelButton;

                    DialogResult result = prompt.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            SaveProject();
                            xm.MarkProjectModified(false); 
                            return DialogResult.Yes; 
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Project having some errors\n{ex.Message}\n\nPlease resolve the errors first.","XMPS 2000",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            return DialogResult.Cancel;
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        xm.MarkProjectModified(false);
                        return DialogResult.No;
                    }
                    else 
                    {
                        return DialogResult.Cancel;
                    }
                }
            }

            using (Form closeForm = new Form()
            {
                Width = 320,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Close Project",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowIcon = false
            })
            {
                Label label = new Label()
                {
                    Text = "Do you want to close the project?",
                    Location = new Point(20, 25),
                    Size = new Size(270, 40),
                    Font = new Font("Segoe UI", 10)
                };

                Button yesBtn = new Button() { Text = "Yes", DialogResult = DialogResult.Yes, Location = new Point(60, 75) };
                Button noBtn = new Button() { Text = "No", DialogResult = DialogResult.No, Location = new Point(160, 75) };

                closeForm.Controls.AddRange(new Control[] { label, yesBtn, noBtn });
                closeForm.AcceptButton = yesBtn;
                closeForm.CancelButton = noBtn;

                DialogResult result = closeForm.ShowDialog();
                return result == DialogResult.Yes ? DialogResult.Yes : DialogResult.Cancel;
            }
        }

        bool ChangeLoadedProject(bool isClosingProject = false)
        {
            if (xm.IsProjectModified())
            {
                string message = "Do you want save current project?";
                string title = "Save Current Project";
                DialogResult result;
                using (Form prompt = new Form())
                {
                    prompt.Width = 350;
                    prompt.Height = 150;
                    prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                    prompt.Text = title;
                    prompt.StartPosition = FormStartPosition.CenterScreen;
                    prompt.MaximizeBox = false;
                    prompt.MinimizeBox = false;

                    Label textLabel = new Label() { Left = 20, Top = 20, Width = 360, Height = 40, Text = message, Font = new Font("Segoe UI", 11, FontStyle.Regular) };
                    Button saveButton = new Button() { Text = "Save", Left = 30, Width = 80, Top = 70, DialogResult = DialogResult.Yes };
                    Button dontSaveButton = new Button() { Text = "Don't Save", Left = 130, Width = 80, Top = 70, DialogResult = DialogResult.No };
                    Button cancelButton = new Button() { Text = "Cancel", Left = 230, Width = 80, Top = 70, DialogResult = DialogResult.Cancel };

                    saveButton.Click += (sender, e) => { prompt.Close(); };
                    dontSaveButton.Click += (sender, e) => { prompt.Close(); };
                    cancelButton.Click += (sender, e) => { prompt.Close(); };

                    prompt.Controls.Add(textLabel);
                    prompt.Controls.Add(saveButton);
                    prompt.Controls.Add(dontSaveButton);
                    prompt.Controls.Add(cancelButton);
                    prompt.AcceptButton = saveButton;
                    prompt.CancelButton = cancelButton;

                    result = prompt.ShowDialog();
                }
                UpdateDefaultPath(xm.LoadedProject.ProjectPath);
                if (result == DialogResult.Yes)
                {
                    SaveProject();
                }
                else if (result == DialogResult.No)
                {
                    xm.MarkProjectModified(false);
                }
                else
                {
                    return false;
                }
                return true;
            }
            else if (isClosingProject)
            {
                using (var closeForm = new Form()
                {
                    Width = 300,Height = 150,FormBorderStyle = FormBorderStyle.FixedDialog, Text = "Close Project",StartPosition = FormStartPosition.CenterScreen,MaximizeBox = false,MinimizeBox = false,ControlBox = true 
                })
                {
                    var label = new Label()
                    {
                        Text = "Do you want to close the project?",
                        Location = new Point(20, 20),
                        Size = new Size(250, 40),
                        Font = new Font("Segoe UI", 9)
                    };

                    var yesBtn = new Button() { Text = "Yes", DialogResult = DialogResult.Yes, Location = new Point(60, 70) };
                    var noBtn = new Button() { Text = "No", DialogResult = DialogResult.No, Location = new Point(160, 70) };

                    yesBtn.Click += (s, e) => closeForm.Close();
                    noBtn.Click += (s, e) => closeForm.Close();

                    closeForm.Controls.AddRange(new Control[] { label, yesBtn, noBtn });
                    closeForm.AcceptButton = yesBtn;
                    closeForm.CancelButton = noBtn;

                    var result = closeForm.ShowDialog();
                    return (result == DialogResult.Yes);
                }
            }
            return true;
        }

        private int indexOf(Dictionary<string, Form> data, string value)
        {
            int index = -1;
            foreach (var obj in data)
            {
                index++;
                if (obj.Key.ToString() == value)
                    return index;
            }
            return index;
        }

        private void SaveProject()
        {
            InitializeTimerCounters();
            RefreshGridFormIfEditing("MODBUSRTUSlavesForm");
            xm.tcNamesCount.Clear();
            if (xm.isCompilied)
            {
                bool isSucess = ArrangeTC_NameOfFunctionBlock();
                if (!isSucess)
                    return;

            }
            // Check if LogicBlock was in loaded screen and save. Only the loaded screens data will get changed when saving, all other blocks which are not opened will remain same
            if (xm.LoadedProject != null)
            {
                bool errorInValidation = false;
                List<string> _errorCheck = new List<string>();
                xm.UDFBCompileTimeErrors.Clear();
                xm.MQTTCompileTimeErrors.Clear();
                xm.InstructionCheckErrors.Clear();
                TagsUserControl Tg = new TagsUserControl();
                Tg.BindResistancetable();
                foreach (var _block in xm.LoadedProject.Blocks)
                {
                    //Block not complie when it is commented
                    if (!(_block.Name.StartsWith("'")))
                    {
                        if (xm.LoadedScreens.ContainsKey($"LadderForm#{_block.Name}") || xm.LoadedScreens.ContainsKey($"UDFLadderForm#{_block.Name}"))
                        {

                            if (xm.LoadedScreens.ContainsKey($"LadderForm#{_block.Name}"))
                            {
                                LadderWindow _windowRef = (LadderWindow)xm.LoadedScreens[$"LadderForm#{_block.Name}"];
                                LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();
                                int _index = indexOf(xm.LoadedScreens, $"LadderForm#{_block.Name}");
                            }
                            else
                            {
                                LadderWindow _windowRef = (LadderWindow)xm.LoadedScreens[$"UDFLadderForm#{_block.Name}"];
                                LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();
                                int _index = indexOf(xm.LoadedScreens, $"UDFLadderForm#{_block.Name}");
                            }
                            // get expression data
                            (bool errorPresent, List<string> errorList, List<string> _curBlockRungs, List<string> _CommentEle) = DInterpreter.DesingToExpression(LadderDrawing.LadderDesign.Active);

                            //ChekingIF Used Instruciton is Present in Current instruction List
                            List<string> checkingInstruction = ValidatingInstruction(_block.Name, _curBlockRungs, _block);
                            if (checkingInstruction != null && checkingInstruction.Count > 0 && xm.isCompilied && _block.Type.Equals("LogicBlock"))
                            {
                                for (int i = 0; i < checkingInstruction.Count; i++)
                                {
                                    string line = checkingInstruction[i];
                                    if (line.Contains("Rung") && _block.Name != null)
                                    {
                                        string[] parts = line.Split(new[] { ": " }, StringSplitOptions.None);
                                        string rungPart = parts[0];
                                        string message = parts[1];

                                        string[] rungParts = rungPart.Split(' ');
                                        int rungNumber = int.Parse(rungParts[1]);

                                        if (!xm.InstructionCheckErrors.ContainsKey(_block.Name))
                                        {
                                            xm.InstructionCheckErrors[_block.Name] = new List<KeyValuePair<int, string>>();
                                        }
                                        xm.InstructionCheckErrors[_block.Name].Add(new KeyValuePair<int, string>(rungNumber, message));
                                    }
                                }
                            }
                            //checking UDFB errors.
                            List<string> validateUdfbRung = ValidateUDFBRung(_block.Name, _curBlockRungs);
                            if (validateUdfbRung != null && validateUdfbRung.Count > 0 && xm.isCompilied)
                            {
                                for (int i = 0; i < validateUdfbRung.Count; i++)
                                {
                                    string line = validateUdfbRung[i];
                                    if (line.Contains("Rung") && _block.Name != null)
                                    {
                                        string[] parts = line.Split(new[] { ": " }, StringSplitOptions.None);
                                        string rungPart = parts[0];
                                        string message = parts[1];

                                        string[] rungParts = rungPart.Split(' ');
                                        int rungNumber = int.Parse(rungParts[1]);

                                        if (!xm.UDFBCompileTimeErrors.ContainsKey(_block.Name))
                                        {
                                            xm.UDFBCompileTimeErrors[_block.Name] = new List<KeyValuePair<int, string>>();
                                        }
                                        xm.UDFBCompileTimeErrors[_block.Name].Add(new KeyValuePair<int, string>(rungNumber, message));
                                    }
                                }
                            }
                            //checking MQTT errors.
                            List<string> validateMQTT = ValidateMQTTRung(_block.Name, _curBlockRungs);
                            if (validateMQTT != null && validateMQTT.Count > 0 && xm.isCompilied)
                            {
                                for (int i = 0; i < validateMQTT.Count; i++)
                                {
                                    string line = validateMQTT[i];
                                    if (line.Contains("Rung") && _block.Name != null)
                                    {
                                        string[] parts = line.Split(new[] { ": " }, StringSplitOptions.None);
                                        string rungPart = parts[0];
                                        string message = parts[1];

                                        string[] rungParts = rungPart.Split(' ');
                                        int rungNumber = int.Parse(rungParts[1]);

                                        if (!xm.MQTTCompileTimeErrors.ContainsKey(_block.Name))
                                        {
                                            xm.MQTTCompileTimeErrors[_block.Name] = new List<KeyValuePair<int, string>>();
                                        }
                                        xm.MQTTCompileTimeErrors[_block.Name].Add(new KeyValuePair<int, string>(rungNumber, message));
                                    }
                                }
                            }

                            if (errorPresent)
                            {
                                _errorCheck.Add($"{_block.Name}");
                                _errorCheck.Add($"{string.Join(Environment.NewLine, errorList)}");
                                errorInValidation = true;
                                continue;
                            }

                            _block.ClearContent();
                            _block.AddContent(_curBlockRungs);
                            _block.ClearCommentContent();
                            _block.AddContentComment(_CommentEle);
                        }
                    }
                }
                if (XMPS.Instance.LoadedProject.isChanged)
                {
                    string activeFormName = XMPS.Instance.BacNetCurrentScreen.ToString();
                    Form activeFormInstance = null;

                    switch (activeFormName)
                    {
                        case "FormDevice":
                            activeFormInstance = Application.OpenForms.OfType<FormDevice>().LastOrDefault();
                            break;
                        case "FormDigitalBacNet":
                            activeFormInstance = Application.OpenForms.OfType<FormDigitalBacNet>().LastOrDefault();
                            break;

                        case "FormAnalogBacNet":
                            activeFormInstance = Application.OpenForms.OfType<FormAnalogBacNet>().LastOrDefault();
                            break;
                        case "FormMultiStateBacNet":
                            activeFormInstance = Application.OpenForms.OfType<FormMultiStateBacNet>().LastOrDefault();
                            break;
                        case "FormSchedule":
                            activeFormInstance = Application.OpenForms.OfType<FormSchedule>().LastOrDefault();
                            break;
                        case "FormNotification":
                            activeFormInstance = Application.OpenForms.OfType<FormNotification>().LastOrDefault();
                            break;
                        case "FormCalendar":
                            activeFormInstance = Application.OpenForms.OfType<FormCalendar>().LastOrDefault();
                            break;
                        case "FormNetworkPort":
                            activeFormInstance = Application.OpenForms.OfType<FormNetworkPort>().LastOrDefault();
                            break;
                        default:
                            break;
                    }
                    if (activeFormInstance != null)
                    {
                        // Retrieve the SaveChanges method using reflection
                        Type formType = activeFormInstance.GetType();
                        var saveChangesMethod = formType.GetMethod("SaveChanges", BindingFlags.Instance | BindingFlags.NonPublic);
                        if (saveChangesMethod != null)
                        {
                            // Invoke the SaveChanges method with null sender and EventArgs.Empty as parameters
                            bool saveResult = (bool)saveChangesMethod.Invoke(activeFormInstance, new object[] { null, EventArgs.Empty });
                            if (!saveResult)
                            {
                                errorInValidation = true;
                            }
                            else
                                XMPS.Instance.LoadedProject.isChanged = false;
                        }
                    }
                }
                if (errorInValidation && !xm.isCompilied)
                {
                    throw new Exception(String.Join(Environment.NewLine, _errorCheck));
                }

                try
                {
                    if (errorInValidation && xm.isCompilied)
                    {
                        throw new Exception(String.Join(Environment.NewLine, _errorCheck));
                    }
                    //checking MQTT Requests and Modbus requests
                    List<string> checkingInModbus = XMProValidator.ValidateModbusRequestMQTTSchedule();
                    if (checkingInModbus != null && checkingInModbus.Count > 0 && xm.isCompilied)
                    {
                        throw new Exception(String.Join(Environment.NewLine, checkingInModbus));
                    }

                    textBoxError.Clear();
                    //Removing UDFB logic block from MainLadderLogic
                    if (currentUDFBElements.Item2 != null)
                        xm.LoadedProject.MainLadderLogic.RemoveAll(t => t.Equals(currentUDFBElements.Item2));

                    xm.SaveCurrentProject();

                    if (xm.isCompilied)
                    {
                        if (ValidateProgrameMemory(out string _memoryError))
                        {
                            throw new Exception(String.Join(Environment.NewLine, _memoryError));
                        }
                        if (_isMemory)
                        {
                            textBoxError.Text = "";
                            splitContainer1.Panel2Collapsed = true;
                            _isMemory = false;
                            return;
                        }
                        // Message on successful compilation
                        splitContainer1.Panel2Collapsed = false;
                        groupBoxError.Text = "Output";
                        List<string> tempMsg = new List<string> { };
                        List<string> blocks = xm.LoadedProject.MainLadderLogic.Where(d => !(d.StartsWith("'"))).ToList();
                        foreach (string lb in blocks)
                        {
                            tempMsg.Add(lb.Contains("]") ? lb.Substring(lb.IndexOf("["), lb.Length - lb.IndexOf("[")).Replace("[", "").Replace("]", "") : lb);
                        }
                        textBoxError.Text = $"Compiled Successfully the blocks from MainLadderLogic : {Environment.NewLine} {string.Join(Environment.NewLine, tempMsg)}";
                    }
                }
                catch (Exception e)
                {
                    splitContainer1.Panel2Collapsed = false;
                    var existingForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "CompileErrors");
                    if (existingForm != null)
                    {
                        existingForm.Dispose(); // or existingForm.Close(), depending on intent
                    }
                    CompileErrors comperror = new CompileErrors();
                    string[] errorMessages = e.Message.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    comperror.ShowsErrorInListView(errorMessages, "Errors");
                    comperror.TopLevel = false;
                    comperror.Visible = true;
                    textBoxError.Controls.Add(comperror);
                    groupBoxError.Visible = true;
                    comperror.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    comperror.Dock = DockStyle.Fill;
                    comperror.Show();
                    buttonErrorClose.Visible = true;
                    groupBoxError.Text = "Output";
                    xm.isCompilied = false;
                }
                if (xm.isCompilied)
                {
                    //////////ShowingWarningMessage
                    List<string> warningMessages = new List<string>();
                    warningMessages = GetAllUnusedTags();
                    if (warningMessages.Count > 0)
                    {
                        var existingForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "CompileErrors");
                        if (existingForm != null)
                        {
                            existingForm.Dispose(); // or existingForm.Close(), depending on intent
                        }
                        CompileErrors compwarning = new CompileErrors();
                        compwarning.ShowsErrorInListView(warningMessages.ToArray(), "Warnings");
                        compwarning.TopLevel = false;
                        compwarning.Visible = true;
                        textBoxError.Controls.Add(compwarning);
                        groupBoxError.Visible = true;
                        compwarning.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        compwarning.Dock = DockStyle.Fill;
                        compwarning.Show();
                        buttonErrorClose.Visible = true;
                        groupBoxError.Text = "Output";
                    }
                }
                //////////END ShowingWarningMessage
                UpdateDefaultPath(xm.LoadedProject.ProjectPath);
                xm.MarkProjectDownloaded(false);
            }
            if (xm.LoadedProject == null)
            {
                MessageBox.Show("Select the Project before Saving", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private List<string> GetAllUnusedTags()
        {
            var tagDict = new Dictionary<string, TagInfo>();
            tagDict = CreateTagDictionary(xm.LoadedProject.Tags.Where(tag => tag.Model != null && (tag.Model == "User Defined Tags")).ToList());
            var resultLogicBlock = FindMissingTags(xm.LoadedProject.ProjectPath.Replace(".xmprj", "App.csv"), tagDict);
            var missingTagDict = CreateTagDictionary(xm.LoadedProject.Tags.Where(tags => tags.Model == "User Defined Tags" && resultLogicBlock.NotFoundTags.Any(mistag => mistag.LogicalAddress == tags.LogicalAddress)).ToList(), tagDict);
            foreach (UDFBInfo uDFBInfo in XMPS.Instance.LoadedProject.UDFBInfo)
            {
                var udfbTagDict = CreateTagDictionary(xm.LoadedProject.Tags.Where(tag => tag.Model != null && (tag.Model == uDFBInfo.UDFBName + " Tags")).ToList());
                var udfbresultLB = FindMissingTags(xm.LoadedProject.ProjectPath.Replace(".xmprj", "App.csv"), udfbTagDict);
                var missingUDFBTagDict = CreateTagDictionary(xm.LoadedProject.Tags.Where(tags => udfbresultLB.NotFoundTags.Any(mistag => mistag.LogicalAddress == tags.LogicalAddress)).ToList(), tagDict);
                missingTagDict = missingTagDict.Concat(missingUDFBTagDict).ToDictionary(x => x.Key, x => x.Value);
            }
            var finalResult = FindMissingTags(xm.LoadedProject.ProjectPath.Replace(".xmprj", "Config.csv"), missingTagDict);
            var taglist = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().SelectMany(publish => publish.PubRequest).Where(pubReq => pubReq != null && !string.IsNullOrEmpty(pubReq.Tag)).Select(pubReq => pubReq.Tag).Distinct().ToList();
            taglist.AddRange(xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().SelectMany(sublish => sublish.SubRequest).Where(subReq => subReq != null && !string.IsNullOrEmpty(subReq.Tag)).Select(subReq => subReq.Tag).Distinct().ToList());
            finalResult.NotFoundTags.RemoveAll(item => taglist.Contains(item.LogicalAddress));
            finalResult.NotFoundTags.RemoveAll(item => xm.LoadedProject.MainLadderLogic.SelectMany(input => Regex.Matches(input, @"\(([^)]+)\)").Cast<Match>().Select(m => m.Groups[1].Value.Replace("~", ""))).ToList().Contains(item.TagName));
            return finalResult.NotFoundTags.Select(t => t.ModelName == "User Defined Tags" ? "TagsForm : " + t.TagName + " is not used in logic and declared at row no-" + t.Index : "TagsForm@" + t.ModelName.Replace("Tags", "").Trim() + " : " + t.TagName + " is not used in logic and declared at row no-" + t.Index).ToList();
        }

        /// <summary>
        /// Searches for multiple tags and returns only the ones NOT found
        /// </summary>
        public static SearchResult FindMissingTags(string filePath, Dictionary<string, TagInfo> tagsToSearch)
        {
            var result = new SearchResult { TotalCount = tagsToSearch?.Count ?? 0 };

            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                    throw new FileNotFoundException("Invalid or missing file path");

                if (tagsToSearch == null || tagsToSearch.Count == 0)
                    throw new ArgumentException("No tags provided to search");

                // Read file once with fallback encoding
                string content = SafeReadFile(filePath);
                if (string.IsNullOrEmpty(content))
                {
                    result.NotFoundTags.AddRange(tagsToSearch.Values);
                    result.Success = true;
                    return result;
                }
                content = FilterContent(content);
                // Find all tags in one pass
                var foundTags = FindAllTags(content, tagsToSearch.Keys);

                // Return only missing tags
                result.NotFoundTags = tagsToSearch
                    .Where(kvp => !foundTags.Contains(kvp.Key))
                    .Select(kvp => kvp.Value)
                    .ToList();

                result.FoundCount = foundTags.Count;
                result.Success = true;

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.NotFoundTags.AddRange(tagsToSearch.Values);
                return result;
            }
        }
        /// <summary>
        /// Filters content to exclude rows starting with "IO_Mapping"
        /// </summary>
        private static string FilterContent(string content)
        {
            if (string.IsNullOrEmpty(content))
                return content;

            var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var filteredLines = lines.Where(line => !line.TrimStart().StartsWith("IO_Mapping", StringComparison.OrdinalIgnoreCase));

            return string.Join(Environment.NewLine, filteredLines);
        }
        /// <summary>
        /// Safe file reading with encoding fallback
        /// </summary>
        private static string SafeReadFile(string filePath)
        {
            try { return File.ReadAllText(filePath, Encoding.UTF8); }
            catch
            {
                try { return File.ReadAllText(filePath, Encoding.Default); }
                catch { return Encoding.UTF8.GetString(File.ReadAllBytes(filePath)); }
            }
        }

        /// <summary>
        /// Multi-strategy tag search in single pass
        /// </summary>
        private static List<string> FindAllTags(string content, IEnumerable<string> tagsToFind)
        {
            //var found = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var contentUpper = content.ToUpperInvariant();

            // Preprocess the content to extract possible tag tokens
            var contentTokens = Regex.Split(content, @"[\s,]+")
                .Select(token => token.Trim().ToUpperInvariant())
                .Where(token => !string.IsNullOrEmpty(token))
                .ToHashSet(); // O(N)

            var found = tagsToFind.Where(tag => contentTokens.Contains(tag.ToUpperInvariant())).ToList();

            //foreach (string tag in tagsToFind)
            //{
            //    string tagUpper = tag.ToUpperInvariant();

            //    // Strategy 1: Direct search
            //    if (contentUpper.Contains(tagUpper))
            //    {
            //        found.Add(tag);
            //        continue;
            //    }

            //    // Strategy 2: Regex word boundary + CSV field match
            //    try
            //    {
            //        string pattern = $@"\b{Regex.Escape(tag)}\b|(?:^|,)\s*{Regex.Escape(tag)}\s*(?:,|$)";
            //        if (Regex.IsMatch(content, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline))
            //        {
            //            found.Add(tag);
            //        }
            //    }
            //    catch { /* Skip regex if it fails */ }
            //}

            return found;
        }

        /// <summary>
        /// Creates TagInfo dictionary from tag list
        /// </summary>
        public static Dictionary<string, TagInfo> CreateTagDictionary(List<XMIOConfig> tags, Dictionary<string, TagInfo> maindict = null)
        {
            if (tags == null) return new Dictionary<string, TagInfo>();

            var dictionary = new Dictionary<string, TagInfo>(StringComparer.OrdinalIgnoreCase);
            int index = 0;

            foreach (XMIOConfig tag in tags)
            {
                dictionary[tag.LogicalAddress] = new TagInfo
                {
                    TagName = tag.Tag,
                    LogicalAddress = tag.LogicalAddress,
                    ModelName = tag.Model,
                    Index = maindict == null ? index : maindict.Keys.ToList().IndexOf(tag.LogicalAddress)
                };
                index++;
            }
            return dictionary;
        }


        private bool ValidateProgrameMemory(out string memoryError)
        {
            bool isInvalid = false;
            memoryError = "";
            if (xm.DeviceMemory.AvlblAddressMemory < CommonFunctions.CalculateTotalAddressMemory())
                memoryError = "Address memory of this project is crossing the available address memory limit";
            if (xm.DeviceMemory.AvlblRetentiveMemory < CommonFunctions.CalculateRetentiveMemory())
                memoryError = "Retentive memory of this project is crossing the available retentive memory limit";
            if (xm.DeviceMemory.AvlblProgMemory < CommonFunctions.CalculateProgramMemory())
                memoryError = "Program memory of this project is crossing the available program memory limit  \n  Current Program Memory            Available program memory \n              " + CommonFunctions.CalculateProgramMemory() + " bytes.                       " + xm.DeviceMemory.AvlblProgMemory + " bytes.";
            if (memoryError.Length > 0) isInvalid = true;
            return isInvalid;
        }

        private bool ArrangeTC_NameOfFunctionBlock()
        {
            Dictionary<string, Counter> tcNameDetails = new Dictionary<string, Counter>();
            tcNameDetails.Clear();
            foreach (var counter in Counters)
            {
                tcNameDetails[counter.Key] = counter.Value;
                xm.tcNamesCount[counter.Key] = counter.Value;
            }
            tcNameDetails["MES_PID"] = new Counter { Instruction = "MES_PID_", CurrentPosition = 1, Maximum = 49 };
            xm.tcNamesCount["MES_PID"] = new Counter { Instruction = "MES_PID_", CurrentPosition = 1, Maximum = 49 };
            //not considering the commented logic block and if contain contact then taking only logic block name.
            var normalLogicBlocks = xm.LoadedProject.MainLadderLogic.Where(t => !t.StartsWith("'"))
                                    .Select(t => t.Contains("[") && t.Contains("]")
                                    ? t.Substring(t.IndexOf("[") + 1, t.IndexOf("]") - t.IndexOf("[") - 1) : t).Distinct();
            foreach (string block in normalLogicBlocks)
            {
                string errorMessage = XMProValidator.ArrangeTheTCNameDetails(block, tcNameDetails);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    splitContainer1.Panel2Collapsed = false;
                    var existingForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "CompileErrors");
                    if (existingForm != null)
                    {
                        existingForm.Dispose(); // or existingForm.Close(), depending on intent
                    }
                    CompileErrors comperror = new CompileErrors();
                    string[] errorMessages = errorMessage.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    comperror.ShowsErrorInListView(errorMessages, "Errors");
                    comperror.Visible = true;
                    comperror.TopLevel = false;
                    textBoxError.Controls.Add(comperror);
                    groupBoxError.Visible = true;
                    comperror.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    comperror.Dock = DockStyle.Fill;
                    comperror.Show();
                    buttonErrorClose.Visible = true;
                    groupBoxError.Text = "Output";
                    xm.isCompilied = false;
                    return false;
                }
            }
            return true;
        }
        private List<string> ValidatingInstruction(string name, List<string> curBlockRungs, Block blockType)
        {
            List<string> checkInstructionRungs = new List<string>();
            if (blockType.Type.Equals("UDFB"))
            {
                return checkInstructionRungs;
            }
            else
                XMProValidator.ValidateInstructionWithInputsOutputs(curBlockRungs, ref checkInstructionRungs);
            return checkInstructionRungs;
        }

        private List<string> ValidateMQTTRung(string logicblk, List<string> curBlockRungs)
        {
            List<string> mqttErrors = new List<string>();
            foreach (string rung in curBlockRungs)
            {
                if (rung.StartsWith("'"))
                    continue;
                string patternpublish = @"MQTT@Publish";
                Regex regexpublish = new Regex(patternpublish);
                Match matchpublish = regexpublish.Match(rung);
                if (matchpublish.Success)
                {
                    try
                    {
                        int lastOpindex = rung.LastIndexOf("OP");
                        string topicId = rung.Substring(rung.LastIndexOf("OP"), 4).Split(':')[1];
                        var publst = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList();
                        var pubdata = publst.Where(t => t.keyvalue == Convert.ToInt32(topicId)).FirstOrDefault();
                        if (pubdata == null)
                        {
                            mqttErrors.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for output value publish topic not found.");
                        }
                    }
                    catch
                    {
                        mqttErrors.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for output value publish topic not found.");
                    }
                }
                string patternSubscribe = @"MQTT@Subscribe";
                Regex regexSubscribe = new Regex(patternSubscribe);
                Match matchSubscribe = regexSubscribe.Match(rung);
                if (matchSubscribe.Success)
                {
                    try
                    {
                        int lastOpindex = rung.LastIndexOf("OP");
                        string topicId = rung.Substring(rung.LastIndexOf("OP"), 4).Split(':')[1];
                        var sublst = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                        var subdata = sublst.Where(t => t.key == Convert.ToInt32(topicId)).FirstOrDefault();
                        if (subdata == null)
                        {
                            mqttErrors.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for output value subscribe topic not found.");
                        }
                    }
                    catch
                    {
                        mqttErrors.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for output value subscribe topic not found.");
                    }
                }
            }
            return mqttErrors;
        }

        private List<string> ValidateUDFBRung(string name, List<string> curBlockRungs)
        {
            List<string> udfbRungErr = new List<string>();
            foreach (string rung in curBlockRungs)
            {
                if (rung.Contains("OPC:9999") && !rung.StartsWith("'"))
                {
                    //////////Check if the UDFB is having enable before actual function block call//////////////////////////////////////////////////////
                    string chkAnding = rung.Substring(rung.IndexOf("="),(rung.IndexOf("FN:") - rung.IndexOf("=")));
                    if (! chkAnding.Contains("AND"))
                    {
                        udfbRungErr.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: enable parameter is missing in UDFB function block call, UDFB function block call requires enable");
                        continue;
                    }
                    ////END------------Check if the UDFB is having enable before actual function block call//////////////////////////////////////////////////////
                    int startIndexFN = rung.IndexOf("FN:");
                    int endIndexFN = rung.IndexOf(" ", startIndexFN + 3);
                    string instructionName = rung.Substring(startIndexFN, endIndexFN - startIndexFN);
                    UDFBInfo udfbInfo = xm.LoadedProject.UDFBInfo.FirstOrDefault(u => u.UDFBName == instructionName.Replace("FN:", ""));
                    if (udfbInfo == null)
                    {
                        udfbRungErr.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: {instructionName.Replace("FN:", "")} not found in current project for compilation");
                        continue;
                    }
                    List<string> udfbtexts = udfbInfo.UDFBlocks.Where(S => S.Type == "Input").Select(T => T.Text).ToList();

                    string strsplit = rung.Split(new[] { "DT" }, StringSplitOptions.None)[0];
                    int rungInputCount = strsplit.Split(new[] { "IN:" }, StringSplitOptions.None).Length - 1;
                    if (rungInputCount != (udfbtexts.Count))
                    {
                        udfbRungErr.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: Please check UDFB {instructionName.Replace("FN:", "")} configuration count not match for input.");
                        continue;
                    }
                    for (int i = 0; i < udfbtexts.Count; i++)
                    {
                        string operandValue = strsplit.Split(new[] { "IN:" }, StringSplitOptions.None)[i + 1];
                        string operandDataType = udfbInfo.UDFBlocks.FirstOrDefault(t => t.Text.Equals(udfbtexts[i])).DataType;
                        bool validatation = XMProValidator.ValidateUDFBOperad(operandValue.Trim().Replace("~", ""), operandDataType, "Input");
                        if (!validatation)
                        {
                            udfbRungErr.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for input no {i + 1} values not match with dataType");
                            continue;
                        }
                    }
                    //checking if output Count and output address present in rung are match or not.
                    int inputs = udfbtexts.Count;
                    udfbtexts.AddRange(udfbInfo.UDFBlocks.Where(S => S.Type == "Output").Select(T => T.Text).ToList());
                    strsplit = rung.Split(new[] { "OPTN" }, StringSplitOptions.None)[1];
                    int rungOutputCount = strsplit.Split(new[] { "OP:" }, StringSplitOptions.None).Length - 1;
                    if (rungOutputCount != (udfbtexts.Count - inputs))
                    {
                        udfbRungErr.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: Please check UDFB {instructionName.Replace("FN:", "")} configuration count not match for output.");
                        continue;
                    }

                    for (int i = 0; i < udfbtexts.Count - inputs; i++)
                    {
                        string operandValue = strsplit.Split(new[] { "OP:" }, StringSplitOptions.None)[i + 1].Replace("]);", "");
                        string operandDataType = udfbInfo.UDFBlocks.FirstOrDefault(t => t.Text.Equals(udfbtexts[inputs + i])).DataType;
                        bool validatation = XMProValidator.ValidateUDFBOperad(operandValue.Trim(), operandDataType, "Output");
                        if (!validatation)
                        {
                            udfbRungErr.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for output no {i + 1} values not match with dataType");
                            continue;
                        }
                    }


                    //Validating the UDFB Variables
                    (HashSet<string>, bool) usedVariables = XMProValidator.GetUDFBUsedVariables(instructionName.Replace("FN:", ""));
                    if (usedVariables.Item2)
                    {
                        if (usedVariables.Item1.Count > 0)
                        {
                            udfbRungErr.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: Please check UDFB {instructionName.Replace("FN:", "")} {usedVariables.Item1.First()}");
                        }
                        else
                        {
                            udfbRungErr.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: Please check UDFB {instructionName.Replace("FN:", "")} Logic Invalid Declaration for {instructionName.Replace("FN:", "")} instruction");
                        }
                        continue;
                    }
                    else
                    {
                        var differences = usedVariables.Item1.Where(variable => !udfbtexts.Contains(variable)).ToList();
                        if (differences.Any())
                        {
                            foreach (var variable in differences)
                            {
                                udfbRungErr.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: Please check UDFB {instructionName.Replace("FN:", "")} not found declaration for {variable}");
                                continue;
                            }
                        }
                    }

                    if (udfbInfo != null)
                    {
                        List<string> checkInstructionRungs = new List<string>();
                        XMProValidator.ValidateInstructionWithInputsOutputs(xm.LoadedProject.Blocks.FirstOrDefault(t => t.Type.Equals("UDFB") && t.Name.Equals(udfbInfo.UDFBName + " Logic")).Elements, ref checkInstructionRungs, udfbInfo);
                        if (checkInstructionRungs != null && checkInstructionRungs.Count > 0)
                        {
                            foreach (string udfbRungError in checkInstructionRungs)
                            {
                                udfbRungErr.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: Please check UDFB {instructionName.Replace("FN:", "")} Logic {udfbRungError.Split(':')[1]}");
                            }
                        }
                    }
                }
            }
            return udfbRungErr;
        }
        public bool UpdateDefaultPath(string newPath)
        {
            string sDefaultPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000";
            string docpath = Path.Combine(sDefaultPath, "ProjectsPaths.xml");

            try
            {
                // Ensure directory exists
                if (!Directory.Exists(sDefaultPath))
                {
                    Directory.CreateDirectory(sDefaultPath);
                }

                // Validate newPath parameter
                if (string.IsNullOrWhiteSpace(newPath))
                {
                    throw new ArgumentException("newPath cannot be null or empty", nameof(newPath));
                }

                XmlDocument doc = new XmlDocument();

                // Check if file exists and is valid
                if (!File.Exists(docpath) || !IsValidXmlFile(docpath))
                {
                    // Create new XML file with proper structure
                    CreateNewProjectPathsXml(docpath);
                    doc.Load(docpath);
                }
                else
                {
                    // Load existing file
                    doc.Load(docpath);
                }

                // Validate XML structure
                if (!ValidateXmlStructure(doc))
                {
                    // Recreate file if structure is invalid
                    CreateNewProjectPathsXml(docpath);
                    doc.Load(docpath);
                }

                // Select the ProjectPath node
                XmlNode projectPathNode = doc.SelectSingleNode("//DefaultPath");

                // This should not be null after validation, but double-check
                if (projectPathNode == null)
                {
                    throw new InvalidOperationException("DefaultPath node not found even after XML recreation");
                }

                // Validate and process the newPath
                string pathToSet = GetValidParentPath(newPath);

                if (!string.IsNullOrEmpty(pathToSet))
                {
                    projectPathNode.InnerText = pathToSet;

                    // Save with proper formatting
                    SaveXmlDocument(doc, docpath);
                    return true;
                }
                else
                {
                    throw new ArgumentException("Unable to determine valid parent path from newPath", nameof(newPath));
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied: {ex.Message}");
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Directory not found: {ex.Message}");
                return false;
            }
            catch (XmlException ex)
            {
                Console.WriteLine($"XML parsing error: {ex.Message}");
                // Try to recreate the file
                try
                {
                    CreateNewProjectPathsXml(docpath);
                    return UpdateDefaultPath(newPath); // Retry once
                }
                catch
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
        }

        private bool IsValidXmlFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;

                // Check if file is not empty
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Length == 0)
                    return false;

                // Try to load as XML
                XmlDocument testDoc = new XmlDocument();
                testDoc.Load(filePath);

                return true;
            }
            catch (XmlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ValidateXmlStructure(XmlDocument doc)
        {
            try
            {
                // Check if root element exists
                if (doc.DocumentElement == null)
                    return false;

                // Check if DefaultPath node exists
                XmlNode defaultPathNode = doc.SelectSingleNode("//DefaultPath");
                return defaultPathNode != null;
            }
            catch
            {
                return false;
            }
        }

        private void CreateNewProjectPathsXml(string filePath)
        {
            try
            {
                XmlDocument newDoc = new XmlDocument();

                // Create XML declaration
                XmlDeclaration xmlDeclaration = newDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                newDoc.InsertBefore(xmlDeclaration, newDoc.DocumentElement);

                // Create root element
                XmlElement root = newDoc.CreateElement("Configuration");
                newDoc.AppendChild(root);

                // Create DefaultPath element
                XmlElement defaultPath = newDoc.CreateElement("DefaultPath");
                defaultPath.InnerText = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                root.AppendChild(defaultPath);

                // Save the new file
                SaveXmlDocument(newDoc, filePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create new XML file: {ex.Message}", ex);
            }
        }

        private string GetValidParentPath(string newPath)
        {
            try
            {
                // Validate that the path is not null or empty
                if (string.IsNullOrWhiteSpace(newPath))
                    return null;

                // Normalize the path
                string normalizedPath = Path.GetFullPath(newPath);

                // Get first parent directory
                string firstParent = Path.GetDirectoryName(normalizedPath);
                if (string.IsNullOrEmpty(firstParent))
                    return null;

                // Get second parent directory (grandparent)
                string secondParent = Path.GetDirectoryName(firstParent);

                // Return the grandparent if it exists, otherwise return the parent
                if (!string.IsNullOrEmpty(secondParent))
                {
                    return secondParent;
                }
                else
                {
                    return firstParent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing path '{newPath}': {ex.Message}");
                return null;
            }
        }

        private void SaveXmlDocument(XmlDocument doc, string filePath)
        {
            try
            {
                // Create XmlWriterSettings for proper formatting
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineChars = Environment.NewLine,
                    NewLineHandling = NewLineHandling.Replace
                };

                // Save with formatting
                using (XmlWriter writer = XmlWriter.Create(filePath, settings))
                {
                    doc.Save(writer);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to save XML file: {ex.Message}", ex);
            }
        }
       
        private bool _isSaveAs = false;
        private bool SaveAsProject()
        {
            if (xm.LoadedProject == null)
            {
                MessageBox.Show("Please Select the File Before Saving");
                return false;
            }
            int currentScanTime = xm.LoadedProject._scanTime;
            int currentTimeRange = xm.LoadedProject._timeRange;
            bool currentIsEnable = xm.LoadedProject._isEnable;

            _isSaveAs = true;
            if (xm.LoadedProject != null)
            {
                if (NewProjectDialog(true) == DialogResult.None) return false;

                xm.LoadedProject._scanTime = currentScanTime;
                xm.LoadedProject._timeRange = currentTimeRange;
                xm.LoadedProject._isEnable = currentIsEnable;
            }
            if (xm.IsProjectModified())
            {
                string message = "Do you want save current project?";
                string title = "Save Current Project";
                MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    SaveProject();
                    return true;
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            _isSaveAs = false;
            return true;
        }
        #endregion

        #region Project Controling

        public DialogResult NewProjectDialog(bool isSaveAs = false)
        {
            //if (!isSaveAs)
            //{
            //    CheckandSaveApp();
            //}
            DialogResult dr = DialogResult.None;
            if (ChangeLoadedProject())
            {
                NewProjectForm newProjectForm = new NewProjectForm();
                if (isSaveAs && xm.LoadedProject != null) newProjectForm.SaveAsModel = xm.PlcModel.ToString();        //If Loaded  Project is Not Null
                newProjectForm.ShowDialog(this.ParentForm);
                if (newProjectForm.projectInfo.ProjectPath != string.Empty)
                    dr = DialogResult.OK;
                newProjectForm.Dispose();
                if (dr == DialogResult.OK)
                    CreateFilesForNewProject(newProjectForm, isSaveAs, newProjectForm.projectInfo);
            }
            return dr;
        }

        private void CreateFilesForNewProject(NewProjectForm newProjectForm, bool isSaveAs, ProjectInfo newProject)
        {
            var _projectname2 = newProject.ProjectPath.Split(new[] { "\\" }, StringSplitOptions.None);
            var _projectname = _projectname2[_projectname2.Length - 1];

            //Linq in recent project 
            var _remProj = xm.RecentProjects.Projects.Where(x => x.ProjectName == _projectname).Count();

            if (_remProj != 0)
            {
                for (int i = 0; i < _remProj; i++)
                {
                    var rem = xm.RecentProjects.Projects.Where(x => x.ProjectName == _projectname).First();
                    xm.RecentProjects.Projects.Remove(rem);
                }
            }
            if (isSaveAs)
            {
                Compile();
            }
            this.CreateNewProject(newProject, isSaveAs);
            if (xm.LoadedProject.PlcModel.StartsWith("XBLD"))
            {
                Ethernet newEthernet = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
                newEthernet.NetworkNo = 1;
                newEthernet.Port = 47808;
            }
            this.Text = "XMPS 2000 " + xm.CurrentProjectData.ProjectName.ToString().Replace(".xmprj", "");
            strpBtnCloseProject.Enabled = true;
            traceWindowToolStripMenuItem.Enabled = false;
            strpBtnSaveProject.Enabled = true;
            xm.MarkProjectDownloaded(false);
            tssStatusLabel_msg("Opened New Project", 5000, "DodgerBlue");
            if (!isSaveAs) AddDefaultTags();
            //Initializing the watchdog configuraiton
            bool isBacnet = xm.LoadedProject.ProjectName.StartsWith("XBLD", StringComparison.OrdinalIgnoreCase);
            xm._connectedIPAddress = "";
            if (isBacnet)
            {
                xm.LoadedProject._scanTime = 150;
                xm.LoadedProject._timeRange = 70;
            }
            else
            {
                xm.LoadedProject._scanTime = 20;
                xm.LoadedProject._timeRange = 10;
            }
            SaveProject();
            textBoxError.Controls.Clear();
        }

        public void OpenProjectDialog()
        {
            //CheckandSaveApp();
            if (!ChangeLoadedProject())
            {
                return;
            }
            string sDefaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MessungSystems\XMPS2000\XM Projects");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XM Project (.xmprj)|*.xmprj"; //XM Project (.xmprj)|*.xmprj|Backup XM Project (.xmprjbkp)|*.xmprjbkp
            openFileDialog.Title = "Browse XM Project Files";
            openFileDialog.InitialDirectory = sDefaultPath;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            DialogResult dr = openFileDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                var projectName = openFileDialog.FileName;
                ForceUserControl.setValueDic.Clear();
                RecentProject recentProject = new RecentProject();

                recentProject.ProjectPath = projectName;
                recentProject.ProjectName = Path.GetFileName(projectName).Replace("*", "");
                var projectPath = recentProject.ProjectPath;
                var backupPath = Path.ChangeExtension(projectPath, ".xmprjbkp");
                CreateProjectBackup(projectPath, backupPath);
                xm.SetCurrentProject(recentProject);
                this.LoadCurrentProject();

                // Save project path for loaded project
                xm.LoadedProject.ProjectPath = recentProject.ProjectPath;
                string _projectname = recentProject.ProjectPath;
                //Linq in recent project 
                var _remProj = xm.RecentProjects.Projects.Where(x => x.ProjectName == _projectname).Count();

                if (_remProj != 0)
                {
                    for (int i = 0; i < _remProj; i++)
                    {
                        var rem = xm.RecentProjects.Projects.Where(x => x.ProjectName == _projectname).First();
                        xm.RecentProjects.Projects.Remove(rem);
                    }
                }
                if (sDefaultPath != projectName.Replace("\\" + recentProject.ProjectName, ""))
                {
                    xm.UpdateRecentProjects(recentProject);
                }
                ButtonStatus(Save: true, Close: true, Upload: strpBtnUploadProject.Enabled, Download: strpBtnDownloadProject.Enabled,
                             Compile: true, Login: true, Logout: false, CVX: true, PLCAction: false, Find: true, Delete: true);
                traceWindowToolStripMenuItem.Enabled = false;
                AddSystemTags();
                xm.MarkProjectDownloaded(false);
                tssStatusLabel_msg($"Opened project {recentProject.ProjectName}", 3000, "DodgerBlue");
            }
        }

        private void CheckBacnetObjects()
        {
            var duplicateInstanceNumbers = xm.LoadedProject.BacNetIP.BinaryIOValues.GroupBy(b => new { b.InstanceNumber, b.ObjectType }).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            if (duplicateInstanceNumbers.Any())
            {
                foreach (var duplicateValue in duplicateInstanceNumbers)
                {
                    string instanceNo = duplicateValue.ToString().Split(',')[0].Split('=')[1].ToString().Trim();
                    string objectType = duplicateValue.ToString().Split(',')[1].Split('=')[1].ToString().Replace('}', ' ').Trim();
                    List<BinaryIOV> listOfDuplicates = xm.LoadedProject.BacNetIP.BinaryIOValues.Where(d => d.ObjectType == objectType && d.InstanceNumber == instanceNo).OrderBy(d => d.LogicalAddress).ToList();
                    for (int i = 1; i <= listOfDuplicates.Count - 1; i++)
                    {
                        string logicaladdress = listOfDuplicates[i].LogicalAddress;
                        BinaryIOV toChange = xm.LoadedProject.BacNetIP.BinaryIOValues.Where(d => d.LogicalAddress == logicaladdress).FirstOrDefault();
                        toChange.InstanceNumber = (Convert.ToInt32(toChange.InstanceNumber) + (i * 100)).ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Create backup of the project using the source and target path
        /// </summary>
        /// <param name="projectPath">source path : this will be existing project path</param>
        /// <param name="backupPath">Target path : this will be new project path</param>
        private void CreateProjectBackup(string projectPath, string backupPath)
        {
            try
            {
                if (File.Exists(backupPath))
                {
                    File.Delete(backupPath);
                }
                if (File.Exists(projectPath))
                {
                    var projectContent = File.ReadAllText(projectPath);
                    File.WriteAllText(backupPath, projectContent, Encoding.Unicode);
                }
                else
                {
                    MessageBox.Show("Original project file does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while creating the backup file: {ex.Message}");
            }
        }

        public void AddSystemTags()
        {
            try
            {
                // Validate that required objects exist
                if (xm == null || xm.LoadedProject == null || xm.LoadedProject.Tags == null)
                {
                    MessageBox.Show("Cannot update system tags: Project not properly loaded.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get system tag list with validation
                List<XMIOConfig> AllSystemTagList = null;
                try
                {
                    AllSystemTagList = CommonFunctions.GetSystemTagList(xm.PlcModel);

                    // Validate system tag list
                    if (AllSystemTagList == null || AllSystemTagList.Count == 0)
                    {
                        MessageBox.Show("Cannot update system tags: System tag template is empty or invalid.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to retrieve system tag list: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get current system tags
                List<XMIOConfig> currentSystemTags = xm.LoadedProject.Tags
                    .Where(t => t.LogicalAddress != null && t.LogicalAddress.StartsWith("S3"))
                    .ToList();

                // Create dictionaries for easier comparison
                var currentTagsDict = currentSystemTags.ToDictionary(t => t.LogicalAddress, StringComparer.OrdinalIgnoreCase);
                var allSystemTagsDict = AllSystemTagList.ToDictionary(t => t.LogicalAddress, StringComparer.OrdinalIgnoreCase);

                // Find differences
                var newTags = AllSystemTagList.Where(t => !currentTagsDict.ContainsKey(t.LogicalAddress)).ToList();
                var removedTags = currentSystemTags.Where(t => !allSystemTagsDict.ContainsKey(t.LogicalAddress)).ToList();

                // Find modified tags using the custom comparer
                var tagComparer = new TagComparer();
                var modifiedTags = AllSystemTagList.Where(newTag =>
                    currentTagsDict.TryGetValue(newTag.LogicalAddress, out var currentTag) &&
                    !tagComparer.Equals(newTag, currentTag)).ToList();

                // Check if there are any differences
                if (newTags.Count > 0 || removedTags.Count > 0 || modifiedTags.Count > 0)
                {
                    // Build a detailed message about the changes
                    try
                    {
                        DialogResult result = MessageBox.Show("Project will get updated to new version, do you want to take backup ? ", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            CreateProjectBackup(xm.LoadedProject.ProjectPath, xm.LoadedProject.ProjectPath.Replace(".xmprj", "_Backup_Before_Update_SystemTags.xmprj"));
                        }
                        // Remove all current S3 tags
                        xm.LoadedProject.Tags.RemoveAll(t =>
                            t.LogicalAddress != null && t.LogicalAddress.StartsWith("S3"));

                        // Add all system tags from the updated list
                        xm.LoadedProject.Tags.AddRange(AllSystemTagList);

                        // Validate the update was successful
                        XMProValidator.CheckSystemTagsInProject();
                        // Show detailed success message                        
                    }
                    catch (Exception ex)
                    {
                        // Show error and restore backup on failure
                        MessageBox.Show($"Error updating system tags: {ex.Message}\nRestoring previous configuration.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // No differences found
                    //MessageBox.Show("System tags are already up to date.",
                    //    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadCurrentProject()
        {
            HideForm();
            splitContainer1.Panel2Collapsed = true;
            xm.LoadCurrentProject();
            if (xm.LoadedProject.BacNetIP?.BinaryIOValues != null)
            {
                foreach (var bio in xm.LoadedProject.BacNetIP.BinaryIOValues)
                {
                    if (bio.LogicalAddress.StartsWith("I"))
                    {
                        bio.ObjectIdentifier = $"Binary Input:{bio.InstanceNumber}";
                    }
                    else if (bio.LogicalAddress.StartsWith("Q"))
                    {
                        bio.ObjectIdentifier = $"Binary Output:{bio.InstanceNumber}";
                    }
                }
            }
            //for adding Calibration tags.
            if (!xm.LoadedProject.HasDiagnosticTags && xm.LoadedProject.DiagnosticParametersEnabled)
            {
                if (!BacNetObjectHelper.DiagnosticTagsAlreadyExist(xm.LoadedProject.PlcModel, false))
                {
                    BacNetObjectHelper.AddDignosticTags(false, xm.LoadedProject.PlcModel, "Analog Input");
                }
            }
            if (xm.LoadedProject.BacNetIP != null && xm.LoadedProject.BacNetIP.AnalogIOValues != null)
            {
                var p5Tags = xm.LoadedProject.BacNetIP.AnalogIOValues.Where(a => a.LogicalAddress?.StartsWith("P5") ?? false).OrderBy(a => int.Parse(a.InstanceNumber)).ToList();
                // Reassign instance numbers sequentially for P5 tags
                for (int i = 0; i < p5Tags.Count; i++)
                {
                    p5Tags[i].InstanceNumber = i.ToString();
                }
                var w4TagsInAnalog = xm.LoadedProject.BacNetIP.AnalogIOValues.Where(a => a.LogicalAddress?.StartsWith("W4") ?? false).ToList();
                // Get starting instance number for Multistate
                int multistateStartIndex = xm.LoadedProject.BacNetIP.MultistateValues.Where(t => t.ObjectType == "19:Multistate Value").Select(t => int.Parse(t.InstanceNumber)).DefaultIfEmpty(-1).Max() + 1;

                foreach (var w4Tag in w4TagsInAnalog)
                {
                    var mainTag = xm.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress == w4Tag.LogicalAddress);

                    if (mainTag != null)
                    {
                        string label = mainTag.Label;
                        bool isSpecialType = label.Equals("Double Word", StringComparison.OrdinalIgnoreCase) ||
                                           label.Equals("DINT", StringComparison.OrdinalIgnoreCase) ||
                                           label.Equals("UDINT", StringComparison.OrdinalIgnoreCase) ||
                                           label.Equals("Byte", StringComparison.OrdinalIgnoreCase) ||
                                           label.Equals("INT", StringComparison.OrdinalIgnoreCase);

                        xm.LoadedProject.BacNetIP.AnalogIOValues.Remove(w4Tag);

                        if (!isSpecialType &&
                            !xm.LoadedProject.BacNetIP.MultistateValues.Any(m => m.LogicalAddress == w4Tag.LogicalAddress))
                        {
                            xm.LoadedProject.BacNetIP.MultistateValues.Add(new MultistateIOV(
                                $"Multistate Value:{multistateStartIndex}",
                                multistateStartIndex.ToString(),
                                "19:Multistate Value",
                                w4Tag.ObjectName,
                                w4Tag.LogicalAddress,
                                0
                            ));
                            multistateStartIndex++;
                        }
                    }
                }
                if (w4TagsInAnalog.Count > 0 || p5Tags.Count > 0)
                {
                    xm.MarkProjectModified(true);
                }
            }
            if (xm.LoadedProject.ProjectPath != xm.CurrentProjectData.ProjectPath)
                xm.LoadedProject.ProjectPath = xm.CurrentProjectData.ProjectPath;
            var modBUSRTUSlaves = (MODBUSRTUSlaves)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").FirstOrDefault();
            if (modBUSRTUSlaves == null)
            {
                MODBUSRTUSlaves _RtuSlave = new MODBUSRTUSlaves();
                xm.LoadedProject.Devices.Add(_RtuSlave);
            }
            //adding check for the adding Model Name for tags which are created on LadderForm (Logical Blocks).
            xm.LoadedProject.Tags.Where(T => (T.Model == "" && !T.LogicalAddress.StartsWith("S3"))).ToList().ForEach(T => T.Model = "User Defined Tags");
            xm.PlcModel = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).Select(d => d.Model).FirstOrDefault();
            // Loading networkport object for old projects
            if (xm.LoadedProject.BacNetIP != null)
            {
                if (xm.LoadedProject.BacNetIP.NetworkPort == null || string.IsNullOrWhiteSpace(xm.LoadedProject.BacNetIP.NetworkPort.ObjectName))
                {
                    xm.LoadedProject.BacNetIP.NetworkPort = new NetworkPort
                    {
                        InstanceNumber = "0",
                        ObjectIdentifier = "Network Port:0",
                        ObjectType = "56:Network Port",
                        ObjectName = "Network_Port",
                        Description = "Network Port",
                        IsEnable = true
                    };
                }
            }

            BindCurreProject();
            xm.MarkProjectDownloaded(false);
            ForceUserControl.setValueDic.Clear();
            ButtonStatus(Save: true, Close: true, Upload: strpBtnUploadProject.Enabled, Download: true,
                             Compile: true, Login: true, Logout: strpBtnLogout.Enabled, CVX: true, PLCAction: false, Find: true, Delete: true);  //Setting For Updating Rtc Time & Date
            if (xm.LoadedProject.Blocks.Count == 0)
            {
                AddNewDefaultLogicBlock();
            }
            if (xm.LoadedProject.PlcModel.StartsWith("XBLD"))
            {
                Ethernet newEthernet = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
                newEthernet.NetworkNo = 1;
                newEthernet.Port = 47808;
            }
            ShowDefaultLogicalBlocks();
            LoadCurrentBlock(xm.CurrentScreen);
            splitContainer1.Panel2Collapsed = true;
            MenuModeDnldProject.Enabled = true;
            traceWindowToolStripMenuItem.Enabled = false;
            MenuModeDnldSourceCode.Enabled = true;
            rTCSettingToolStripMenuItem.Enabled = true;
            strpBtnCompile.Enabled = true;
            forceUnforceMenu.Enabled = true;
            xm._connectedIPAddress = "";
            //Change for hidding splInnerPanel(Instruction Tree View) at the time of creating new Project
            splcInner.SplitterDistance = splcInner.Width + 1087;
            GetMemoryAddresses(xm.LoadedProject.PlcModel);
            //adding logic to is isEditableDigitalFilter is disable then remove the filter data from tags.
            CheckingOldProjectForDigitalFilter();
            UpdateEnumValuesForSystemTags();
            xm.Entries?.Clear();
            if (!BacNetObjectHelper.DiagnosticTagsAlreadyExist(xm.LoadedProject.PlcModel, false)
                && xm.LoadedProject.DiagnosticParametersEnabled && !xm.LoadedProject.HasDiagnosticTags)
            {
                List<XMIOConfig> beforeAddingCalibration = new List<XMIOConfig>(xm.LoadedProject.Tags);

                //Adding calibration tags for OnBoardTags
                BacNetObjectHelper.AddDignosticTags(false, xm.LoadedProject.PlcModel, "Analog Input");
                //Expansion And Remote Tags Before Adding Calibration tags
                List<XMIOConfig> beforeAddingRemoteExpansion = new List<XMIOConfig>(xm.LoadedProject.Tags.Where(t => t.IoList == IOListType.ExpansionIO || t.IoList == IOListType.RemoteIO));
                //Combine all ExpansionIO and RemoteIO tags, ordered by their Key
                var allIoTags = xm.LoadedProject.Tags
                    .Where(t => t.IoList == IOListType.ExpansionIO || t.IoList == IOListType.RemoteIO)
                    .OrderBy(t => t.Key)
                    .ToList();

                //Create a list of unique models along with their IoList type, preserving order
                var uniqueAllModels = allIoTags
                    .GroupBy(t => t.Model)
                    .Select(g => new
                    {
                        Model = g.Key,
                        IoList = g.First().IoList
                    })
                    .ToList();

                //Clearing the old Remote and Expansion Tags.
                xm.LoadedProject.Tags.RemoveAll(t => t.IoList == IOListType.ExpansionIO);
                xm.LoadedProject.Tags.RemoveAll(t => t.IoList == IOListType.RemoteIO);

                foreach (var modelInfo in uniqueAllModels)
                {
                    if (modelInfo.IoList == IOListType.ExpansionIO)
                    {
                        var model = RemoteModule.List.Find(x => x.Name.Equals(modelInfo.Model.Split('_')[0]));
                        AddDevice ad = new AddDevice(modelInfo.Model);
                        List<XMIOConfig> oldTags = beforeAddingRemoteExpansion.Where(t => t.Model == modelInfo.Model).OrderBy(t => t.Key).ToList();
                        ad.AddRemoteExpansionIOs(model, IOListType.ExpansionIO, modelInfo.Model, oldTags);
                    }
                    else if (modelInfo.IoList == IOListType.RemoteIO)
                    {
                        var model = RemoteModule.List.Find(x => x.Name.Equals(modelInfo.Model.Split('_')[0]));
                        AddDevice ad = new AddDevice(modelInfo.Model);
                        List<XMIOConfig> oldTags = beforeAddingRemoteExpansion.Where(t => t.Model == modelInfo.Model).OrderBy(t => t.Key).ToList();
                        ad.AddRemoteExpansionIOs(model, IOListType.RemoteIO, modelInfo.Model, oldTags);
                    }

                }
                var changedTagsWithAddresses = xm.LoadedProject.Tags.Where(modified =>
                    beforeAddingCalibration.Any(original => original.Tag == modified.Tag && original.LogicalAddress != modified.LogicalAddress))
                    .Select(modified =>
                    {
                        var original = beforeAddingCalibration.First(o => o.Tag == modified.Tag);
                        return new TagAddressChange
                        {
                            Tag = modified.Tag,
                            OldAddress = original.LogicalAddress,
                            NewAddress = modified.LogicalAddress
                        };
                    }).ToList();

                if (changedTagsWithAddresses.Count > 0)
                {
                    foreach (var currentTag in changedTagsWithAddresses)
                    {
                        //Replace in Contact and Coil Elements 
                        XMProValidator.ReplaceInContactAndCoilElements(currentTag.OldAddress, currentTag.NewAddress);
                    }
                    //Replace in All Function Block In Ladder Window
                    XMProValidator.ReplaceInFunctionBlocks(changedTagsWithAddresses);
                    //Replace in all modbus requests, BacNet Schedule, HSIO function blocks.
                    XMProValidator.ReplaceTagInAllModbusReqSlave(changedTagsWithAddresses);
                }
            }
            UpdateMaxCount();
            if (xm.PlcModel.ToString().Contains("XBLD"))
                CheckAndUpdateObjects();
        }

        private void CheckAndUpdateObjects()
        {
            if (xm.LoadedProject.BacNetIP == null) return;
            xm.LoadedProject.BacNetIP.BinaryIOValues.Where(b => b.DeviceType.ToString().Contains(':')).AsParallel()
    .ForAll(b => { var currentValue = b.DeviceType.ToString(); b.DeviceType = currentValue.Substring(0, currentValue.IndexOf(':')); });

            // Step 1: Preprocess logical addresses into a HashSet for fast lookup (O(n))
            var existingAddresses = new HashSet<string>(
                xm.LoadedProject.BacNetIP.MultistateValues.Select(m => m.LogicalAddress)
            );
            // Step 2: Iterate through relevant tags and insert if not already present (O(m))
            foreach (var wordtag in xm.LoadedProject.Tags.Where(t => t.Label == "Word"))
            {
                if (!existingAddresses.Contains(wordtag.LogicalAddress))
                {
                    BacNetTagFactory.AddTagtoBacNetObject(
                        wordtag.Tag,
                        wordtag.LogicalAddress,
                        wordtag.Label,
                        wordtag.Type,
                        wordtag.Mode,
                        true
                    );
                }
            }
            CheckBacnetObjects();
        }

        private void UpdateMaxCount()
        {
            DeviceModel systemConfiguration = GetParticularTemplatesDetails();
            DeviceModel oldConfiguration = xm.LoadedProject.SystemConfiguration.Devices.FirstOrDefault();
            if (systemConfiguration != null && oldConfiguration != null)
            {
                for (int i = 0; i < systemConfiguration.Templates.Count; i++)
                {
                    var sysTemplate = systemConfiguration.Templates[i];
                    var oldTemplate = oldConfiguration.Templates.ElementAtOrDefault(i);
                    if (oldTemplate == null) continue;
                    // Get all devices from Ethernet and RS 485
                    var sysDevices = new List<DeviceDetials>();
                    var oldDevices = new List<DeviceDetials>();
                    if (sysTemplate.Ethernet?.TreeNodes != null)
                        sysDevices.AddRange(sysTemplate.Ethernet.TreeNodes.SelectMany(tn => tn.Devices));
                    if (sysTemplate.RS485?.TreeNodes != null)
                        sysDevices.AddRange(sysTemplate.RS485.TreeNodes.SelectMany(tn => tn.Devices));
                    if (oldTemplate.Ethernet?.TreeNodes != null)
                        oldDevices.AddRange(oldTemplate.Ethernet.TreeNodes.SelectMany(tn => tn.Devices));
                    if (oldTemplate.RS485?.TreeNodes != null)
                        oldDevices.AddRange(oldTemplate.RS485.TreeNodes.SelectMany(tn => tn.Devices));
                    foreach (var sysDevice in sysDevices)
                    {
                        var oldDevice = oldDevices.FirstOrDefault(d => d.Name == sysDevice.Name);
                        if (oldDevice != null)
                        {
                            oldDevice.MaxCount = sysDevice.MaxCount;
                            // Update nested properties if they exist
                            if (sysDevice.Properties != null && oldDevice.Properties != null)
                            {
                                foreach (var sysProp in sysDevice.Properties)
                                {
                                    var oldProp = oldDevice.Properties.FirstOrDefault(p => p.Name == sysProp.Name);
                                    if (oldProp != null)
                                    {
                                        oldProp.MaxCount = sysProp.MaxCount;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void CheckingOldProjectForDigitalFilter()
        {
            bool projectModified = false;
            //set to IsEnableInputFilter to false and set InputFilterValue to string.empty.
            xm.LoadedProject.Tags.Where(T => T.Type != IOType.DigitalInput && T.Type != IOType.UniversalInput).ToList()
            .ForEach(T =>
            {
                T.IsEnableInputFilter = false;
                T.InpuFilterValue = string.Empty;
            });

            //Checking for old project if save with empty value and false then assigning value at the time of opening.
            if (xm.LoadedProject.IsEditableDigitalFilter && !xm.LoadedProject.IsDigitalFilterApply)
            {
                xm.LoadedProject.Tags.Where(T => T.Type == IOType.DigitalInput && !T.IsEnableInputFilter && (T.IoList == IOListType.ExpansionIO || T.IoList == IOListType.OnBoardIO)).ToList()
                .ForEach(T =>
                {
                    T.IsEnableInputFilter = true;
                    T.InpuFilterValue = "10";
                });
                xm.LoadedProject.Tags.Where(T => T.Type == IOType.UniversalInput && !string.IsNullOrEmpty(T.Mode) && T.Mode.Equals("Digital") && !T.IsEnableInputFilter
                && T.IoList == IOListType.ExpansionIO && !T.Label.Contains("_OR") && !T.Label.Contains("_OL")).ToList()
                .ForEach(T =>
                {
                    T.IsEnableInputFilter = true;
                    T.InpuFilterValue = "10";
                });
                xm.LoadedProject.IsDigitalFilterApply = true;
            }

            if (xm.LoadedProject.IsEditableDigitalFilter && (xm.LoadedProject.PlcModel == "XM-14-DT-HIO" || xm.LoadedProject.PlcModel == "XM-14-DT-HIO-E"))
            {
                var di5di7Tags = xm.LoadedProject.Tags.Where(T => T.Type == IOType.DigitalInput && T.IoList == IOListType.OnBoardIO && (T.Label == "DI5" || T.Label == "DI7")).ToList();
                foreach (var tag in di5di7Tags)
                {
                    bool shouldApplyDefaults = !xm.LoadedProject.IsDigitalFilterApply || (!tag.IsEnableInputFilter && string.IsNullOrEmpty(tag.InpuFilterValue));
                    if (shouldApplyDefaults)
                    {
                        tag.IsEnableInputFilter = true;
                        tag.InpuFilterValue = "10";
                        projectModified = true;
                    }
                    if (string.IsNullOrEmpty(tag.Mode))
                    {
                        tag.Mode = "Digital Input";
                        projectModified = true;
                    }
                }

                xm.LoadedProject.Tags.Where(T => T.Type == IOType.DigitalInput && T.IoList == IOListType.OnBoardIO
                && T.Mode != null
                && !T.Mode.Equals("Digital Input")).ToList()
               .ForEach(T =>
               {
                   T.IsEnableInputFilter = false;
                   T.InpuFilterValue = string.Empty;
               });
            }
            if (projectModified)
            {
                xm.MarkProjectModified(true);
            }
        }

        private void UpdateEnumValuesForSystemTags()
        {
            DataSet ds = new DataSet();

            List<XMIOConfig> systemtags = CommonFunctions.GetSystemTagList(xm.LoadedProject.PlcModel);
            foreach (XMIOConfig systag in systemtags)
            {
                XMIOConfig Oldsystag = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == systag.LogicalAddress.ToString()).FirstOrDefault();
                if (systag != null && Oldsystag != null)
                {
                    Oldsystag.EnumValues.Clear();
                    Oldsystag.EnumValues.AddRange(systag.EnumValues);
                }
            }
        }

        private void GetMemoryAddresses(string plcModel)
        {
            string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\ProjectTemplates\" + xm.PlcModel + "\\" + xm.PlcModel + ".plc");
            // Load XML file
            XDocument xdoc = XDocument.Load(filePath);
            xm.MemoryAllocation.Clear();
            // Find the ErrorTags element, no matter where it is in the document
            XElement errorTagsElement = xdoc.Descendants("ErrorTags").FirstOrDefault();
            // Check if ErrorTags element exists
            if (errorTagsElement != null)
            {
                // Extract the String elements under ErrorTags
                List<string> errorTags = errorTagsElement
                    .Elements("TagAddress")
                    .Select(e => e.Value)
                    .ToList();
                xm.LoadedProject.ErrorStatusTags = errorTags;
            }
            // Find the PLCStatusTag element, no matter where it is in the document
            XElement plcStatusElement = xdoc.Descendants("PLCStatusTag").FirstOrDefault();
            // Check if PLCStatusTag element exists
            if (plcStatusElement != null)
            {
                // Extract the value directly from the node
                xm.LoadedProject.PLCStatusTag = plcStatusElement.Value;
            }
            // Find the PLCStatusTag element, no matter where it is in the document
            XElement cpuDataTypeElement = xdoc.Descendants("CPUDatatype").FirstOrDefault();
            // Check if PLCStatusTag element exists
            if (cpuDataTypeElement != null)
            {
                // Extract the value directly from the node
                xm.LoadedProject.CPUDatatype = cpuDataTypeElement.Value.ToString()?.Trim().Trim('"').Trim();
            }

            // Find the PLCStatusTag element, no matter where it is in the document
            XElement expansionErrorType = xdoc.Descendants("ExpansionErrorType").FirstOrDefault();
            // Check if PLCStatusTag element exists
            if (expansionErrorType != null)
            {
                // Extract the value directly from the node
                xm.LoadedProject.ExpansionErrorType = expansionErrorType.Value.ToString()?.Trim().Trim('"').Trim();
            }

            //xm.LoadedProject.CPUDatatype = xm.LoadedProject.PlcModel.EndsWith("E") ? "Real" : "Word";

            // Iterate through root elements
            foreach (var memoryobjects in xdoc.Root.Elements())
            {
                foreach (var instruction in memoryobjects.Elements("AddressDetails"))
                {
                    MemoryAllocation memoryAllocation = new MemoryAllocation();
                    memoryAllocation.Initial = instruction.Attribute("Initial").Value.ToString();
                    memoryAllocation.StartAddress = Convert.ToInt16(instruction.Attribute("StartAddress").Value.ToString());
                    memoryAllocation.Limit = Convert.ToInt16(instruction.Attribute("Limit").Value.ToString());
                    memoryAllocation.AddLength = Convert.ToInt16(instruction.Attribute("AddLength").Value.ToString());
                    memoryAllocation.StartHexAddress = instruction.Attribute("StartHexAddress").Value.ToString();
                    memoryAllocation.IsBit = instruction.Attribute("IsBit").Value.ToString() == "true" ? true : false;
                    memoryAllocation.BitValue = Convert.ToInt16(instruction.Attribute("BitValue").Value.ToString());
                    memoryAllocation.OMStartHexAddress = instruction.Attribute("OMStartHexAddress").Value.ToString();
                    memoryAllocation.OMAddLength = Convert.ToInt16(instruction.Attribute("OMAddLength").Value.ToString());
                    memoryAllocation.BytesRequired = Convert.ToDouble(instruction.Attribute("BytesRequired").Value.ToString());
                    xm.MemoryAllocation.Add(memoryAllocation);
                }
            }
            var deviceMemoryElement = xdoc.Root.Element("DeviceMemory");
            if (deviceMemoryElement != null)
            {
                DeviceMemory deviceMemory = new DeviceMemory();
                deviceMemory.TotalAvlblMemory = Convert.ToDouble(deviceMemoryElement.Attribute("TotalAvlblMemory")?.Value.ToString());
                deviceMemory.AvlblProgMemory = Convert.ToDouble(deviceMemoryElement.Attribute("AvlblProgMemory")?.Value);
                deviceMemory.AvlblAddressMemory = Convert.ToDouble(deviceMemoryElement.Attribute("AvlblAddressMemory")?.Value);
                deviceMemory.AvlblRetentiveMemory = Convert.ToDouble(deviceMemoryElement.Attribute("AvlblRetentiveMemory")?.Value);
                xm.DeviceMemory = deviceMemory;
            }
            xm.LoadedProject.IsEditableDigitalFilter = xdoc.Root?.Element("IsEditableDigitalFilter")?.Value == "1";
            xm.LoadedProject.DiagnosticParametersEnabled = xdoc.Root?.Element("DiagnosticParametersEnabled")?.Value == "1";
        }

        private void ShowDefaultLogicalBlocks()
        {
            Block B = xm.LoadedProject.Blocks.Where(T => T.Type.Equals("LogicBlock")).First();
            //Creating List of Names of all the Logical Blocks form Current Project
            List<string> logicBlocks = xm.LoadedProject.Blocks.Where(T => T.Type.Equals("LogicBlock")
                                                                    || T.Type.Equals("InterruptLogicBlock")
                                                                    || T.Type.Equals("UDFB")).Select(T => T.Name).ToList();
            //Creating List of TreeNodes for the intialising all the Logical blocks at the time of Opening Project
            List<TreeNode> logicalBlocks = new List<TreeNode>();

            TreeNode Logic = null;
            foreach (TreeNode thisNode in tvProjects.Nodes)
            {
                foreach (TreeNode InnerNode in thisNode.Nodes)
                {
                    foreach (TreeNode MainNode in InnerNode.Nodes)
                    {
                        foreach (TreeNode SubNode in MainNode.Nodes)
                        {
                            //for UDFB Logic Blocks.
                            if (SubNode.Text.Equals("UDFB"))
                            {
                                //getting main UDFB 
                                foreach (TreeNode Udfb in SubNode.Nodes)
                                {
                                    //UDFB logic blocks.
                                    foreach (TreeNode UdfbLogicBlk in Udfb.Nodes)
                                    {
                                        if (logicBlocks.Contains(UdfbLogicBlk.Text) && ((NodeInfo)UdfbLogicBlk.Tag).Info.Equals("UDFLadder"))
                                        {
                                            logicalBlocks.Add(UdfbLogicBlk);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //for Normal Logical Blocks.
                                foreach (TreeNode LogicNode in SubNode.Nodes)
                                {
                                    if (LogicNode.Text == B.Name)
                                    {
                                        Logic = LogicNode;
                                        Logic.Parent.Expand();
                                        SubNode.Parent.Expand();
                                        MainNode.Parent.Expand();
                                    }
                                    //saving only Logical block TreeNode in the TreeNode List
                                    if (logicBlocks.Contains(LogicNode.Text) && ((NodeInfo)LogicNode.Tag).Info.Equals("Ladder"))
                                    {
                                        logicalBlocks.Add(LogicNode);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (Logic != null)
            {
                TreeNode firstLogicBlock = logicalBlocks.Where(T => T.Text == B.Name).FirstOrDefault();
                PerformTreeNodeActions((NodeInfo)firstLogicBlock.Tag, firstLogicBlock, firstLogicBlock.Text);
                //adding for the Intialize all the Logical Block at the time of Open Projects
                logicalBlocks.Remove(firstLogicBlock);
                foreach (TreeNode treeNode in logicalBlocks)
                {
                    LadderWindow frmLadder = new LadderWindow(treeNode.Text);
                    frmLadder.MdiParent = this;
                    frmLadder.TopLevel = false;
                    frmLadder.Dock = DockStyle.Fill;
                    splitContainer1.Panel1.Controls.Add(frmLadder);
                    if (((NodeInfo)(treeNode.Tag)).Info == "UDFLadder")
                    {
                        AddToLoadedForms("UDFLadderForm#" + treeNode.Text, frmLadder);
                        LoadCurrentBlock("UDFLadderForm#" + treeNode.Text);
                    }
                    else
                    {
                        AddToLoadedForms("LadderForm#" + treeNode.Text, frmLadder);
                        LoadCurrentBlock("LadderForm#" + treeNode.Text);
                    }
                }
            }
        }

        public void LoadCurrentBlock(string windowname)
        {
            int index1 = indexOf(xm.LoadedScreens, windowname);
            LadderWindow window = (LadderWindow)xm.LoadedScreens[windowname];
            LadderDrawing.LadderDesign.Active = window.getLadderEditor().getCanvas().getDesignView();
            LadderDesign.ClickedElement = null;
            LadderDrawing.Global.ClearActive();
            if (!LadderDrawing.LadderDesign.Active.Elements.Any() && !xm.ScreensToNavigate.Contains(windowname))
            {
                LadderDrawing.LadderDesign ladderDesign = LadderDesign.Active;
                int _blockIndex = xm.LoadedProject.Blocks.FindIndex(d => d.Name == $"{windowname.Split('#')[1]}");
                if (xm.LoadedProject.Blocks[_blockIndex].Elements != null)
                {
                    ladderDesign = DesignDraw.GetDesign(ref ladderDesign, xm.LoadedProject.Blocks[_blockIndex].Elements, xm.LoadedProject.Blocks[_blockIndex].Comments);
                    window.getLadderEditor().getCanvas().setDesignView(ladderDesign);
                    window.getLadderEditor().ReScale(true);
                }
            }
            ToolStrip curWindowControl = window.getLadderEditorToolStrip();
            if (_LoggedIn)
            {
                curWindowControl.Enabled = false;
                //add udfb in mainLadderLogic name at the time of online monitoring.
                if (currentUDFBElements.Item1 != null && !xm.LoadedProject.MainLadderLogic.Contains(currentUDFBElements.Item2))
                {
                    xm.LoadedProject.MainLadderLogic.Add(currentUDFBElements.Item2);
                }
                if (xm.LoadedProject.MainLadderLogic.Where(e => e.Contains(xm.CurrentScreen.Split('#')[1]) && !e.StartsWith("'")).Count() > 0)
                {
                    OnlineMonitoringHelper omh = OnlineMonitoringHelper.Instance;
                    omh.PopulateTagToAddress();
                    omh.PopulateCurBlockData();
                    LadderDesign.Active.GetVisibleRungs(window.getLadderEditor().DisplayRectangle.Top);
                    omh.SendActiveRungAddress();
                    OnlineMonitoringStatus.isOnlineMonitoring = true;
                    omh.SetCurrentCanvas(window.getLadderEditor().getCanvas());
                    OnlineMonitorTimer.Start();
                    xm.presentInMain = true;
                }
                else
                {
                    //those block not in MainLadderLogic
                    OnlineMonitorTimer.Start();
                }
            }
            else
            {
                curWindowControl.Enabled = true;
                xm.presentInMain = false;
            }
            //window.getLadderEditor().ReScale(true);
            window.getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
            window.getLadderEditor().ReScale(true);
        }

        private void AddNewDefaultLogicBlock()
        {
            foreach (TreeNode thisNode in tvProjects.Nodes)
            {
                foreach (TreeNode InnerNode in thisNode.Nodes)
                {
                    foreach (TreeNode MainNode in InnerNode.Nodes)
                    {
                        foreach (TreeNode SubNode in MainNode.Nodes)
                        {
                            if (SubNode.Text.Equals("Logic Blocks"))
                            {
                                AddNewLogicBlock(SubNode);
                            }
                            //Checking and Adding Default two interrupt Logical Blocks
                            var PlcModel = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).Select(d => d.Model).FirstOrDefault();
                            if (SubNode.Text.Equals("Interrupt Logic Blocks") && (PlcModel == "XM-14-DT-HIO" || PlcModel == "XM-14-DT-HIO-E"))
                            {
                                var InterruptlogicBlocks = (List<Block>)xm.LoadedProject.Blocks.Where(b => b.Type == "InterruptLogicBlock").ToList();
                                if (InterruptlogicBlocks.Count == 0)
                                {
                                    AddInterruptLogicBlocks(4);
                                }
                            }

                        }
                    }
                }
            }
        }


        //Added Intrupt Logic Blocks
        private void AddInterruptLogicBlocks(int numberOfBlocks)
        {
            for (int i = 1; i <= numberOfBlocks; i++)
            {
                Block interruptBlock = new Block
                {
                    Name = $"Interrupt_Logic_Block{i:00}",
                    Type = "InterruptLogicBlock"
                };
                xm.LoadedProject.Blocks.Add(interruptBlock);
            }
        }
        public void CreateNewProject(ProjectInfo newProject, bool isSaveAs)
        {
            ModeUI.ClearResistanceTables();
            xm.CreateNewProject(newProject, isSaveAs);
            splcInner.Panel2Collapsed = true;
            splitContainer1.Panel2Collapsed = true;
            LoadCurrentProject();
            RemoveRS485DevicesIfAny();

        }
        private void RemoveRS485DevicesIfAny()
        {
            if (_isSaveAs) return;
            var masters = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").ToList();
            foreach (var master in masters)
            {
                if (master is MODBUSRTUMaster modbusMaster)
                {
                    modbusMaster.Slaves.Clear();
                }
                xm.LoadedProject.Devices.Remove(master);
            }
            var slaves = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").ToList();
            foreach (var slave in slaves)
            {
                xm.LoadedProject.Devices.Remove(slave);
            }
            xm.LoadedProject.RS485Mode = null;
            xm.LoadedProject.SlaveID = 0;
            foreach (TreeNode node in tvProjects.Nodes.Cast<TreeNode>().ToList())
            {
                if (node.Text == "MODBUS RTU Master" || node.Text == "MODBUS RTU Slaves")
                {
                    tvProjects.Nodes.Remove(node);
                }
            }
        }
        public void CreateAndEditLocalCopy(LadderElement ladderElement, string originalName, string localCopyName)
        {
            try
            {
                TreeNode logicNode = this.tvProjects.SelectedNode;
                if (logicNode == null || logicNode.Parent == null || logicNode.Parent.Parent == null)
                {
                    MessageBox.Show("Unable to locate the UDFB node in TreeView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int _blockIndex = xm.LoadedProject.Blocks.FindIndex(d => d.Name == logicNode.Parent.Text + " Logic");
                List<string> rungs = xm.LoadedProject.Blocks[_blockIndex].Elements;
                XMPS.Instance.LoadedProject.Blocks.RemoveAll(t => (t.Type.Equals("UDFB") || t.Type.Equals("UserFunctionBlock")) && t.Name.Equals(logicNode.Parent.Text + " Logic"));

                UDFBInfo uDFBInfo = xm.LoadedProject.UDFBInfo.FirstOrDefault(t => t.UDFBName.Equals(originalName));
                uDFBInfo.UDFBName = localCopyName;

                TreeNode udfbNode = logicNode.Parent;
                TreeNode udfbParentNode = udfbNode.Parent;

                udfbParentNode.Nodes.Remove(udfbNode);

                TreeNode tnUDFLocalBlock = new TreeNode(localCopyName);
                NodeInfo nitnUDFLocalBlock = new NodeInfo();
                nitnUDFLocalBlock.NodeType = NodeType.BlockNode;
                nitnUDFLocalBlock.Info = "UDFB";
                tnUDFLocalBlock.Tag = nitnUDFLocalBlock;

                TreeNode tnUDFTag = new TreeNode(localCopyName + " Tags");
                NodeInfo nitnUDFTag = new NodeInfo();
                nitnUDFTag.NodeType = NodeType.ListNode;
                nitnUDFTag.Info = "UDFTags";
                tnUDFTag.Tag = nitnUDFTag;
                tnUDFLocalBlock.Nodes.Add(tnUDFTag);

                TreeNode tnUDFLogic = new TreeNode(localCopyName + " Logic");
                NodeInfo nitnUDFLogic = new NodeInfo();
                nitnUDFLogic.NodeType = NodeType.BlockNode;
                nitnUDFLogic.Info = "UDFLadder";
                tnUDFLogic.Tag = nitnUDFLogic;
                tnUDFLocalBlock.Nodes.Add(tnUDFLogic);

                udfbParentNode.Nodes.Add(tnUDFLocalBlock);
                XMPS.Instance.LoadedProject.Blocks.RemoveAll(t => t.Type.Equals("UDFB") && t.Name.Equals(logicNode.Parent.Text + " Logic"));
                XMPS.Instance.LoadedProject.Blocks.RemoveAll(t => (t.Type.Equals("UDFB") || t.Type.Equals("UserFunctionBlock")) && t.Name.Equals(logicNode.Parent.Text));

                Block blk = new Block();
                blk.Name = localCopyName;
                blk.Type = "UserFunctionBlock";
                XMPS.Instance.LoadedProject.Blocks.Add(blk);
                ///////////////
                Block blk1 = new Block();
                blk1.Name = localCopyName + " Logic";
                blk1.Type = "UDFB";
                XMPS.Instance.LoadedProject.Blocks.Add(blk1);
                int _blockIndexNew = xm.LoadedProject.Blocks.FindIndex(d => d.Name == localCopyName + " Logic");
                xm.LoadedProject.Blocks[_blockIndexNew].Elements.Clear();
                xm.LoadedProject.Blocks[_blockIndexNew].Elements.AddRange(rungs);

                tvProjects.SelectedNode = tnUDFLogic;
                tnUDFLocalBlock.Expand();

                // Manually call PerformTreeNodeActions to open the logic form
                NodeInfo nodeInfo = (NodeInfo)tnUDFLogic.Tag;
                PerformTreeNodeActions(nodeInfo, tnUDFLogic, tnUDFLogic.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating and editing local copy: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindCurreProject()
        {
            if (xm.LoadedProject is null) return;
            LoadInstructionInformationFromFile();
            XMProject mProject = xm.LoadedProject;
            UpdateProjectFileOpenInfo();
            RenderBaseTreeNodes();
            //Initalizing the Publish and Subcribe Undo Redo Manager Object
            publishManager = new PublishManager(XMPS.Instance.LoadedProject.Devices
                            .Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList());

            subscribeManager = new SubscribeManager(XMPS.Instance.LoadedProject.Devices
                             .Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList());
            modbusRTUSlaveManager = new ModbusRTUSlaveManager(((MODBUSRTUSlaves)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").FirstOrDefault()).Slaves);

            var masterDevice = XMPS.Instance.LoadedProject?.Devices?
                .FirstOrDefault(d => d.GetType().Name == "MODBUSRTUMaster") as MODBUSRTUMaster;

            if (masterDevice != null)
            {
                modbusRTUMasterManager = new ModbusRTUMasterManager(masterDevice.Slaves);
            }
            modbusTCPClientManager = new ModbusTCPClientManager(((MODBUSTCPClient)XMPS.Instance.LoadedProject.Devices
                                    .Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault()).Slaves);

            modbusTCPServerManager = new MODBUSTCPServerManager(((MODBUSTCPServer)XMPS.Instance.LoadedProject.Devices
                                    .Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault()).Requests);

            var curProjectNode = tvProjects.Nodes.Find("curProject", true).FirstOrDefault();
            curProjectNode.NodeFont = new Font(tvProjects.Font, FontStyle.Bold);
            curProjectNode.Text = PROJECT_NODE_DEFAULT_NAME + String.Format("({0})", mProject.ProjectName);

            //Added PLC Model to Project 
            if (xm.LoadedProject.PlcModel == null)
            {
                xm.LoadedProject.PlcModel = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).Select(d => d.Model).FirstOrDefault();
            }
            if (frmGridLayout.copiedTag != null)
                frmGridLayout.copiedTag = null;
            if (frmGridLayout.cutTag != null)
                frmGridLayout.cutTag = null;
            //clearing the copied rungs after opening new project
            if (DataCVX.CurrentRung != null)
            {
                DataCVX.CurrentRung = null;
                DataCVX.CopyPresent = false;
                DataCVX.CurrentRungComment = null;
            }

            // Clear Previous Child Nodes
            curProjectNode.Nodes.Clear();

            // Create Base Structure of Tree Before Loading
            TreeNode tnPowerUP = new TreeNode("Powerup Routine");
            NodeInfo niPowerUP = new NodeInfo();
            niPowerUP.NodeType = NodeType.BlockNode;
            niPowerUP.Info = "PURLadder";
            tnPowerUP.Tag = niPowerUP;

            TreeNode tnMain = new TreeNode("Main");
            NodeInfo niMain = new NodeInfo();
            niMain.NodeType = NodeType.MainBlockNode;
            niMain.Info = "Main";
            tnMain.Tag = niMain;

            TreeNode tnLogicBlocks = new TreeNode("Logic Blocks");
            NodeInfo niLogicBlocks = new NodeInfo();
            niLogicBlocks.NodeType = NodeType.BlockNode;
            niLogicBlocks.Info = "LogicBlock";
            tnLogicBlocks.Tag = niLogicBlocks;

            var logicBlocks = (List<Block>)xm.LoadedProject.Blocks.Where(b => b.Type == "LogicBlock").ToList();
            if (logicBlocks != null)
            {
                foreach (var block in logicBlocks)
                {
                    TreeNode tnLogicBlock = new TreeNode(block.Name);
                    NodeInfo nitnLogicBlock = new NodeInfo();
                    nitnLogicBlock.NodeType = NodeType.BlockNode;
                    nitnLogicBlock.Info = "Ladder";
                    tnLogicBlock.Tag = nitnLogicBlock;
                    tnLogicBlocks.Nodes.Add(tnLogicBlock);
                }
            }

            TreeNode tnHWInterrupt = new TreeNode("Hardware Interrupt");
            NodeInfo niHWInterrupt = new NodeInfo();
            niHWInterrupt.NodeType = NodeType.BlockNode;
            niHWInterrupt.Info = "HIBlock";
            tnHWInterrupt.Tag = niHWInterrupt;

            var hiBlocks = (List<Block>)xm.LoadedProject.Blocks.Where(b => b.Type == "HIBlock").ToList();
            if (hiBlocks != null)
            {
                foreach (var block in hiBlocks)
                {
                    TreeNode tnHIBlock = new TreeNode(block.Name);
                    NodeInfo nitnHIBlock = new NodeInfo();
                    nitnHIBlock.NodeType = NodeType.BlockNode;
                    nitnHIBlock.Info = "HILadder";
                    tnHIBlock.Tag = nitnHIBlock;
                    tnHWInterrupt.Nodes.Add(tnHIBlock);
                }
            }

            TreeNode tnUDFB = new TreeNode("UDFB");
            NodeInfo niUDFB = new NodeInfo();
            niUDFB.NodeType = NodeType.BlockNode;
            niUDFB.Info = "UserFunctionBlock";
            tnUDFB.Tag = niUDFB;

            var udfLocalBlocks = (List<Block>)xm.LoadedProject.Blocks.Where(b => b.Type == "UserFunctionBlock").ToList();         
            if (udfLocalBlocks != null)
            {
                foreach (var block in udfLocalBlocks)
                {
                    TreeNode tnUDFLocalBlock = new TreeNode(block.Name);
                    NodeInfo nitnUDFLocalBlock = new NodeInfo();
                    nitnUDFLocalBlock.NodeType = NodeType.BlockNode;
                    nitnUDFLocalBlock.Info = "UDFB";
                    tnUDFLocalBlock.Tag = nitnUDFLocalBlock;
                    tnUDFB.Nodes.Add(tnUDFLocalBlock);

                    TreeNode tnUDFTag = new TreeNode(block.Name + " Tags");
                    NodeInfo nitnUDFTag = new NodeInfo();
                    nitnUDFTag.NodeType = NodeType.ListNode;
                    nitnUDFTag.Info = "UDFTags";
                    tnUDFTag.Tag = nitnUDFTag;
                    tnUDFLocalBlock.Nodes.Add(tnUDFTag);

                    TreeNode tnUDFLogic = new TreeNode(block.Name + " Logic");
                    NodeInfo nitnUDFLogic = new NodeInfo();
                    nitnUDFLogic.NodeType = NodeType.BlockNode;
                    nitnUDFLogic.Info = "UDFLadder";
                    tnUDFLogic.Tag = nitnUDFLogic;
                    tnUDFLocalBlock.Nodes.Add(tnUDFLogic);

                }
            }

            TreeNode tnioconfig = new TreeNode("IO Configuration");
            NodeInfo tntioconfig = new NodeInfo();
            tntioconfig.NodeType = NodeType.ListNode;
            tntioconfig.Info = "IOConfig";
            tnioconfig.Tag = tntioconfig;


            var ModelNm = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).Select(d => d.Model).FirstOrDefault();

            TreeNode tniobase = new TreeNode("Base (" + ModelNm + ")");
            NodeInfo niiobase = new NodeInfo();
            niiobase.NodeType = NodeType.ListNode;
            niiobase.Info = "OnBoardIO";
            tniobase.Tag = niiobase;
            //Adding subfolder HSIO Configuration under Base IOConfiguration
            if (ModelNm == "XM-14-DT-HIO" || ModelNm == "XM-14-DT-HIO-E")
            {
                TreeNode HSIO = new TreeNode("HSIO Configuration");
                NodeInfo HSIOconfig = new NodeInfo();
                HSIOconfig.NodeType = NodeType.ListNode;
                HSIOconfig.Info = "HSIOConfig";
                HSIO.Tag = HSIOconfig;
                tniobase.Nodes.Add(HSIO);
            }
            xm.MarkProjectModified(true);

            TreeNode tnioremote = new TreeNode("Remote I/O");
            NodeInfo niioremote = new NodeInfo();
            niioremote.NodeType = NodeType.ListNode;
            niioremote.Info = "RemoteIO";
            tnioremote.Tag = niioremote;

            var remoteIOs = (List<XMIOConfig>)xm.LoadedProject.Tags.Where(t => t.IoList.ToString() == "RemoteIO").OrderBy(t => t.Key).ToList();
            if (remoteIOs != null)
            {
                var uniqueRemoteIOs = remoteIOs.Select(rio => rio.Model).Distinct();
                foreach (var remoteIO in uniqueRemoteIOs)
                {
                    TreeNode tnRemoteIO = new TreeNode(remoteIO);
                    NodeInfo nitnRemoteIO = new NodeInfo();
                    nitnRemoteIO.NodeType = NodeType.ListNode;
                    nitnRemoteIO.Info = remoteIO;
                    tnRemoteIO.Tag = nitnRemoteIO;
                    tnioremote.Nodes.Add(tnRemoteIO);
                }
            }          
            TreeNode tniolookup = new TreeNode("Resistance Lookup Table");
            NodeInfo niiolookup = new NodeInfo
            {
                NodeType = NodeType.ListNode,
                Info = "LookUpTbl"
            };
            tniolookup.Tag = niiolookup;
            if (xm.LoadedProject.ResistanceTables != null && xm.LoadedProject.ResistanceTables.Any())
            {
                foreach (var tbl in xm.LoadedProject.ResistanceTables)
                {
                    TreeNode tnTable = new TreeNode(tbl.Name);
                    NodeInfo niTable = new NodeInfo
                    {
                        NodeType = NodeType.ListNode,
                        Info = tbl.Name
                    };
                    tnTable.Tag = niTable;
                    tniolookup.Nodes.Add(tnTable);
                }
            }

            TreeNode tnioexp = new TreeNode("Expansion I/O");
            NodeInfo niioexp = new NodeInfo();
            niioexp.NodeType = NodeType.ListNode;
            niioexp.Info = "ExpansionIO";
            tnioexp.Tag = niioexp;

            var expansionIOs = (List<XMIOConfig>)xm.LoadedProject.Tags.Where(t => t.IoList.ToString() == "ExpansionIO").OrderBy(t => t.Key).ToList();
            if (expansionIOs != null)
            {
                var uniqueExpansionIOs = expansionIOs.Select(rio => rio.Model).Distinct();
                foreach (var expansionIO in uniqueExpansionIOs)
                {
                    TreeNode tnExpansionIO = new TreeNode(expansionIO);
                    NodeInfo nitnExapansionIO = new NodeInfo();
                    nitnExapansionIO.NodeType = NodeType.ListNode;
                    nitnExapansionIO.Info = expansionIO;
                    tnExpansionIO.Tag = nitnExapansionIO;
                    tnioexp.Nodes.Add(tnExpansionIO);
                }
            }
            //Create New Treenode And Adding User Defined Tag & System Tag
            TreeNode tnTagsNode = new TreeNode("Tags");

            TreeNode tnTags = new TreeNode("System Tags");
            NodeInfo niTags = new NodeInfo();
            niTags.NodeType = NodeType.ListNode;
            niTags.Info = "System Tags";
            tnTags.Tag = niTags;

            //Creating New Child for Tags
            TreeNode tnTags2 = new TreeNode("User Defined Tags");
            NodeInfo niTags2 = new NodeInfo();
            niTags2.NodeType = NodeType.ListNode;
            niTags2.Info = "User Defined Tags";
            tnTags2.Tag = niTags2;
            TreeNode tnErrorTags = new TreeNode("Error Diagnostic Tags");
            TreeNode tnSystemConfig = new TreeNode("System Configuration");

            //creating Task Config under current project
            TreeNode tnTaskConfig = new TreeNode("Task Configuration");
            NodeInfo tnttaskconfig = new NodeInfo();
            tnttaskconfig.NodeType = NodeType.ListNode;
            tnttaskconfig.Info = "TaskConfig";
            tnTaskConfig.Tag = tnttaskconfig;


            TreeNode tnEthernet = new TreeNode("Ethernet");
            NodeInfo niEthernet = new NodeInfo();
            niEthernet.NodeType = NodeType.DeviceNode;
            niEthernet.Info = "Ethernet";
            tnEthernet.Tag = niEthernet;

            TreeNode systemConfiguratioTreeView = GetSystemConfigurationTreeNode();


            //CheckingEthenetSettings
            if (systemConfiguratioTreeView.Nodes[0].Nodes.Count > 0)
            {
                foreach (TreeNode childNode in systemConfiguratioTreeView.Nodes[0].Nodes)
                {
                    //forMQTT
                    if (childNode.Text.Equals("MQTT"))
                    {
                        //creating new child for Ethernet
                        TreeNode mqtt = new TreeNode("MQTT");
                        NodeInfo niMqtt = new NodeInfo();
                        niMqtt.NodeType = NodeType.DeviceNode;
                        niMqtt.Info = "MQTT client";
                        mqtt.Tag = niMqtt;

                        TreeNode mqttP = new TreeNode("MQTT Publish");
                        NodeInfo niMqttP = new NodeInfo();
                        niMqttP.NodeType = NodeType.DeviceNode;
                        niMqttP.Info = "MQTT Publish";
                        mqttP.Tag = niMqttP;


                        TreeNode mqttS = new TreeNode("MQTT Subscribe");
                        NodeInfo niMqttS = new NodeInfo();
                        niMqttS.NodeType = NodeType.DeviceNode;
                        niMqttS.Info = "MQTT Subscribe";
                        mqttS.Tag = niMqttS;
                        ////Add Child to MQTT
                        mqtt.Nodes.Add(mqttP);
                        mqtt.Nodes.Add(mqttS);
                        tnEthernet.Nodes.Add(mqtt);
                    }
                    else if (childNode.Text.Equals("BacNet"))
                    {
                        #region Bacnet Data
                        /////Create treee nodes to show BacNet configuration details//////////////////////////////////////////
                        TreeNode tnBacnnet = new TreeNode("BACNET IP");
                        NodeInfo nitnBacnnet = new NodeInfo();
                        nitnBacnnet.NodeType = NodeType.BackNetNode;
                        nitnBacnnet.Info = "BacNetIP";
                        tnBacnnet.Tag = nitnBacnnet;

                        TreeNode bnttd = new TreeNode("Device");
                        NodeInfo niBnttd = new NodeInfo();
                        niBnttd.NodeType = NodeType.BackNetNode;
                        niBnttd.Info = "Device";
                        bnttd.Tag = niBnttd;
                        tnBacnnet.Nodes.Add(bnttd);

                        TreeNode bntthw = new TreeNode("Hardware IO's");
                        NodeInfo niBnttHw = new NodeInfo();
                        niBnttHw.NodeType = NodeType.BackNetNode;
                        niBnttHw.Info = "Hardware IO's";
                        bntthw.Tag = niBnttHw;
                        tnBacnnet.Nodes.Add(bntthw);


                        TreeNode bnttbv = new TreeNode("Binary Value");
                        NodeInfo niBnttbv = new NodeInfo();
                        niBnttbv.NodeType = NodeType.BackNetNode;
                        niBnttbv.Info = "Binary Value";
                        bnttbv.Tag = niBnttbv;
                        tnBacnnet.Nodes.Add(bnttbv);


                        TreeNode bnttav = new TreeNode("Analog Value");
                        NodeInfo niBnttav = new NodeInfo();
                        niBnttav.NodeType = NodeType.BackNetNode;
                        niBnttav.Info = "Analog Value";
                        bnttav.Tag = niBnttav;
                        tnBacnnet.Nodes.Add(bnttav);


                        TreeNode bnttmv = new TreeNode("Multistate Value");
                        NodeInfo niBnttmv = new NodeInfo();
                        niBnttmv.NodeType = NodeType.BackNetNode;
                        niBnttmv.Info = "Multistate Value";
                        bnttmv.Tag = niBnttmv;
                        tnBacnnet.Nodes.Add(bnttmv);


                        TreeNode bnttcal = new TreeNode("Calendar");
                        NodeInfo niBnttcal = new NodeInfo();
                        niBnttcal.NodeType = NodeType.BackNetNode;
                        niBnttcal.Info = "Calendar";
                        bnttcal.Tag = niBnttcal;
                        tnBacnnet.Nodes.Add(bnttcal);

                        TreeNode bnttshed = new TreeNode("Schedule");
                        NodeInfo niBnttshed = new NodeInfo();
                        niBnttshed.NodeType = NodeType.BackNetNode;
                        niBnttshed.Info = "Schedule";
                        bnttshed.Tag = niBnttshed;
                        tnBacnnet.Nodes.Add(bnttshed);

                        TreeNode bnttnotf = new TreeNode("Notification Class");
                        NodeInfo niBnttnotf = new NodeInfo();
                        niBnttnotf.NodeType = NodeType.BackNetNode;
                        niBnttnotf.Info = "Notification Class";
                        bnttnotf.Tag = niBnttnotf;
                        tnBacnnet.Nodes.Add(bnttnotf);

                        TreeNode bnttnetport = new TreeNode("Network Port");
                        NodeInfo niBnttnetport = new NodeInfo();
                        niBnttnetport.NodeType = NodeType.BackNetNode;
                        niBnttnetport.Info = "Network Port";   // identify it uniquely
                        bnttnetport.Tag = niBnttnetport;
                        tnBacnnet.Nodes.Add(bnttnetport);

                        TreeNode bnttfile = new TreeNode("File");
                        NodeInfo niBnttfile = new NodeInfo();
                        niBnttfile.NodeType = NodeType.BackNetNode;
                        niBnttfile.Info = "File";
                        bnttfile.Tag = niBnttfile;
                        tnBacnnet.Nodes.Add(bnttfile);

                        tnEthernet.Nodes.Add(tnBacnnet);
                        //check if only for if old project is open in new XBLD-Pro10E version 
                        DeviceModel systemConfiguration = XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault();
                        var ethernetDevices = systemConfiguration.Templates
                            ?.Where(template => template.Ethernet != null)
                            .ToList();
                        var bacNetDevice = ethernetDevices?.SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices)
                                           .FirstOrDefault(device => device.Name == "BacNet");
                        if (bacNetDevice != null && XMPS.Instance.LoadedProject.BacNetIP != null)
                        {
                            var propertyMappings = new Dictionary<string, (IEnumerable<dynamic> Collection, string ObjectType)>
                                                 {{ "BinaryInput", (XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues, "3") },
                                                  { "BinaryOutput", (XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues, "4") },
                                                  { "BinaryValue", (XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues, "5") },
                                                  { "AnalogInput", (XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues, "0") },
                                                  { "AnalogOutput", (XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues, "1") },
                                                  { "AnalogValue", (XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues, "2") },
                                                  { "MultiStateValue", (XMPS.Instance.LoadedProject.BacNetIP.MultistateValues, "19") },
                                                  { "Calendar", (XMPS.Instance.LoadedProject.BacNetIP.Calendars, "6") },
                                                  { "Schedule", (XMPS.Instance.LoadedProject.BacNetIP.Schedules, "17") }};

                            foreach (var mapping in propertyMappings)
                            {
                                var property = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == mapping.Key);
                                var (collection, objectType) = mapping.Value;

                                if (collection.Any(t => t.IsEnable && t.ObjectType.Split(':')[0] == objectType) && property?.CurrentCount == 0)
                                {
                                    property.CurrentCount = collection.Count(t => t.IsEnable && t.ObjectType.Split(':')[0] == objectType);
                                }
                            }
                            var notificationProperty = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "Notification");
                            var notifications = XMPS.Instance.LoadedProject.BacNetIP.Notifications;

                            if (notifications.Any(t => t.IsEnable) && notificationProperty?.CurrentCount == 0)
                            {
                                notificationProperty.CurrentCount = notifications.Count(t => t.IsEnable);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        string treeNodeText = childNode.Text.Equals("MODBUSTCPClient") ? "MODBUS TCP Client" : "MODBUS TCP Server";
                        TreeNode tnTCPServer = new TreeNode(treeNodeText);
                        NodeInfo niTCPServer = new NodeInfo();
                        niTCPServer.NodeType = NodeType.DeviceNode;
                        niTCPServer.Info = childNode.Text;
                        tnTCPServer.Tag = niTCPServer;
                        tnEthernet.Nodes.Add(tnTCPServer);
                    }

                }
            }

            //forRS485 settings
            TreeNode tnRS485 = new TreeNode("RS485");
            NodeInfo niRS845 = new NodeInfo();
            niRS845.NodeType = NodeType.DeviceNode;
            niRS845.Info = "COMDevice";
            tnRS485.Tag = niRS845;
            if (xm.LoadedProject != null)
            {
                var modbusMaster = xm.LoadedProject.Devices.FirstOrDefault(d => d.GetType().Name == "MODBUSRTUMaster") as MODBUSRTUMaster;
                if (modbusMaster != null && xm.LoadedProject.RS485Mode != "Master" && modbusMaster.Slaves.Count == 0)
                {
                    xm.LoadedProject.Devices.Remove(modbusMaster);
                    modbusMaster = null;
                }
                bool isMasterMode = xm.LoadedProject.RS485Mode == "Master";
                bool isSlaveMode = xm.LoadedProject.RS485Mode == "Slave";
                bool masterHasSlaves = modbusMaster != null && modbusMaster.Slaves.Count > 0;
                if (isMasterMode || masterHasSlaves)
                {
                    TreeNode tnMaster = new TreeNode("MODBUS RTU Master");
                    NodeInfo niMaster = new NodeInfo();
                    niMaster.NodeType = NodeType.DeviceNode;
                    niMaster.Info = "MODBUSRTUMaster";
                    tnMaster.Tag = niMaster;
                    tnRS485.Nodes.Add(tnMaster);
                }
                if (isSlaveMode)
                {
                    TreeNode tnSlave = new TreeNode("MODBUS RTU Slaves");
                    NodeInfo niSlave = new NodeInfo();
                    niSlave.NodeType = NodeType.DeviceNode;
                    niSlave.Info = "MODBUSRTUSlaves";
                    tnSlave.Tag = niSlave;
                    tnRS485.Nodes.Add(tnSlave);
                }
            }

            tnMain.Nodes.Add(tnLogicBlocks);
            ///<>
            ///Creating New Tree Node for the Interrupt Logic Blocks
            var PlcModel = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).Select(d => d.Model).FirstOrDefault();
            if (PlcModel == "XM-14-DT-HIO" || PlcModel == "XM-14-DT-HIO-E")
            {
                TreeNode InterrupttnLogicBlocks = new TreeNode("Interrupt Logic Blocks");
                NodeInfo InterruptniLogicBlocks = new NodeInfo();
                InterruptniLogicBlocks.NodeType = NodeType.BlockNode;
                InterruptniLogicBlocks.Info = "InterruptLogicBlock";
                InterrupttnLogicBlocks.Tag = InterruptniLogicBlocks;

                TreeNode InterrupttnLogicBlocks1 = new TreeNode("Interrupt_Logic_Block01");
                NodeInfo InterruptniLogicBlocks1 = new NodeInfo();
                InterruptniLogicBlocks1.NodeType = NodeType.BlockNode;
                InterruptniLogicBlocks1.Info = "Ladder";
                InterrupttnLogicBlocks1.Tag = InterruptniLogicBlocks1;

                TreeNode InterrupttnLogicBlocks2 = new TreeNode("Interrupt_Logic_Block02");
                NodeInfo InterruptniLogicBlocks2 = new NodeInfo();
                InterruptniLogicBlocks2.NodeType = NodeType.BlockNode;
                InterruptniLogicBlocks2.Info = "Ladder";
                InterrupttnLogicBlocks2.Tag = InterruptniLogicBlocks2;

                TreeNode InterrupttnLogicBlocks3 = new TreeNode("Interrupt_Logic_Block03");
                NodeInfo InterruptniLogicBlocks3 = new NodeInfo();
                InterruptniLogicBlocks3.NodeType = NodeType.BlockNode;
                InterruptniLogicBlocks3.Info = "Ladder";
                InterrupttnLogicBlocks3.Tag = InterruptniLogicBlocks3;

                TreeNode InterrupttnLogicBlocks4 = new TreeNode("Interrupt_Logic_Block04");
                NodeInfo InterruptniLogicBlocks4 = new NodeInfo();
                InterruptniLogicBlocks4.NodeType = NodeType.BlockNode;
                InterruptniLogicBlocks4.Info = "Ladder";
                InterrupttnLogicBlocks4.Tag = InterruptniLogicBlocks4;

                InterrupttnLogicBlocks.Nodes.Add(InterrupttnLogicBlocks1);
                InterrupttnLogicBlocks.Nodes.Add(InterrupttnLogicBlocks2);
                InterrupttnLogicBlocks.Nodes.Add(InterrupttnLogicBlocks3);
                InterrupttnLogicBlocks.Nodes.Add(InterrupttnLogicBlocks4);
                //adding Interrupt Logical Blocks node into the Main Node
                tnMain.Nodes.Add(InterrupttnLogicBlocks);
            }

            //Do not show Hardware Intrupt and UDFB Tags as they are not functional yet
            tnMain.Nodes.Add(tnUDFB);

            // Add Child Nodes to IO Node
            tnioconfig.Nodes.Add(tniobase);
            tnioconfig.Nodes.Add(tnioexp);
            tnioconfig.Nodes.Add(tnioremote);
            if (XMPS.Instance.PlcModel == "XBLD-14E" || XMPS.Instance.PlcModel == "XBLD-17E")
            {
                tnioconfig.Nodes.Add(tniolookup);
            }
            // Add base nodes to Current Project Node
            curProjectNode.Nodes.Add(tnMain);

            curProjectNode.Nodes.Add(tnioconfig);
            curProjectNode.Nodes.Add(tnTagsNode);

            //Adding Child For Tag Node

            tnTagsNode.Nodes.Add(tnTags);
            tnTagsNode.Nodes.Add(tnTags2);

            // Add ChildNodes to SystemConfig
            tnSystemConfig.Nodes.Add(tnEthernet);
            tnSystemConfig.Nodes.Add(tnRS485);

            curProjectNode.Nodes.Add(tnSystemConfig);
            curProjectNode.Nodes.Add(tnTaskConfig); // Add Task Configuration node under current project
            curProjectNode.Expand();
            tvProjects.SelectedNode = curProjectNode;

            xm.ScreensToNavigate.Clear();
            strpBtnPrvScreen.Enabled = false;
            strpBtnNxtScreen.Enabled = false;
            _zoomFactor = 100;
            // strpBtnZoomIn.Text = _zoomFactor.ToString() + "%";
        }

        private TreeNode GetSystemConfigurationTreeNode()
        {
            TreeNode rootNode = new TreeNode("System Configuration");
            DeviceModel systemConfiguration = new DeviceModel();
            if (xm.LoadedProject.SystemConfiguration.Devices != null && xm.LoadedProject.SystemConfiguration.Devices.Count() > 0)
            {
                systemConfiguration = xm.LoadedProject.SystemConfiguration.Devices.FirstOrDefault();
            }
            else
            {
                xm.LoadedProject.SystemConfiguration.Devices = new List<DeviceModel>();
                systemConfiguration = GetParticularTemplatesDetails();
                xm.LoadedProject.SystemConfiguration.Devices.Add(systemConfiguration);
            }
            // Filter Ethernet devices
            var ethernetDevices = systemConfiguration.Templates
                ?.Where(template => template.Ethernet != null)
                .ToList();

            // Filter RS485 devices
            var rs485Devices = systemConfiguration.Templates
                ?.Where(template => template.RS485 != null)
                .ToList();
            TreeNode ethernetTreeNode = new TreeNode("Ethernet");
            if (ethernetDevices != null && ethernetDevices.Any())
            {
                foreach (var device in ethernetDevices)
                {
                    foreach (var treeNodeText in device.Ethernet.TreeNodes)
                    {
                        foreach (var deviceDetail in treeNodeText.Devices)
                        {
                            TreeNode detailNode = new TreeNode($"{deviceDetail.Name}");
                            ethernetTreeNode.Nodes.Add(detailNode);
                        }
                    }
                }
            }
            rootNode.Nodes.Add(ethernetTreeNode);

            // RS485 Tree Nodes
            TreeNode rs485TreeNode = new TreeNode("RS485");
            if (rs485Devices != null && rs485Devices.Any())
            {
                //foreach (var device in rs485Devices)
                //{
                //    foreach (var treeNodeText in device.RS485.TreeNodes)
                //    {
                //        foreach (var deviceDetail in treeNodeText.Devices)
                //        {
                //            TreeNode detailNode = new TreeNode($"{deviceDetail.Name}");
                //            rs485TreeNode.Nodes.Add(detailNode);
                //        }
                //    }
                //}
            }
            rootNode.Nodes.Add(rs485TreeNode);
            return rootNode;
        }

        private DeviceModel GetParticularTemplatesDetails()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                           @"MessungSystems\XMPS2000\ProjectTemplates\SystemConfiguration.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(SystemConfiguration));

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                var config = (SystemConfiguration)serializer.Deserialize(fs);
                return config.Devices.FirstOrDefault(d => d.DeviceType == xm.LoadedProject.PlcModel);
            }
        }

        private void LoadInstructionInformationFromFile()
        {
            try
            {
                string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\NewInstructionFormat.xml");
                // Load XML file
                XDocument xdoc = XDocument.Load(filePath);
                xm.instructionsList.Clear();
                xm.instructionTreeNodes.Nodes.Clear();

                // Load XML file
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                XmlNode root = xmlDoc.DocumentElement;

                if (root != null)
                {
                    foreach (XmlNode categoryNode in root.ChildNodes) // Iterate over main categories (Logical, Arithmetic, etc.)
                    {
                        XmlAttribute categoryNameAttr = categoryNode.Attributes["name"];
                        string categoryName = categoryNameAttr != null ? categoryNameAttr.Value : categoryNode.Name;

                        // Check if the category already exists in the TreeView main list
                        if (xm.instructionTreeNodes.Nodes.Cast<TreeNode>().Any(node => node.Text == categoryName))
                        {
                            xm.instructionsList.Clear();
                            xm.instructionTreeNodes.Nodes.Clear();
                            MessageBox.Show($"Duplicate instruction type found: {categoryName}. please check instruction file once", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        TreeNode mainNode = new TreeNode(categoryName);

                        foreach (XmlNode instructionNode in categoryNode.ChildNodes)
                        {
                            XmlNode textNode = instructionNode.SelectSingleNode("Text");
                            if (textNode != null)
                            {
                                // Check if this instruction sub instructions already exists
                                bool duplicateSubNode = xm.instructionTreeNodes.Nodes
                                    .Cast<TreeNode>()
                                    .SelectMany(node => node.Nodes.Cast<TreeNode>())
                                    .Any(subNode => subNode.Text == textNode.InnerText);

                                if (duplicateSubNode)
                                {
                                    xm.instructionsList.Clear();
                                    xm.instructionTreeNodes.Nodes.Clear();
                                    MessageBox.Show($"Duplicate instruction '{textNode.InnerText}' found. please check instruction file", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                mainNode.Nodes.Add(new TreeNode(textNode.InnerText)); // Add instruction as a child node
                                string instructionText = textNode.InnerText.ToString().Equals("ANY to Dword") ? "ANY to DWORD" : textNode.InnerText.ToString();
                                //Removing the PID instruction number from the function_name Attributes.
                                instructionText = instructionText.StartsWith("MES_PID_") ? "MES_PID" : instructionText;
                                InstructionTypeDeserializer instructionData = GetInformationInstruction(instructionText);

                                List<string> validatingInstruction = new List<string>();
                                XMProValidator.ValidateInstructionData(instructionData, ref validatingInstruction);
                                if (validatingInstruction != null && validatingInstruction.Count > 0)
                                {
                                    MessageBox.Show($"{string.Join(Environment.NewLine, validatingInstruction)}\n please check instruction file once", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    xm.instructionsList.Clear();
                                    xm.instructionTreeNodes.Nodes.Clear();
                                    return;
                                }
                                xm.instructionsList.Add(instructionData);
                            }
                        }
                        xm.instructionTreeNodes.Nodes.Add(mainNode);
                    }
                }
                if (xm.instructionsList != null)
                {
                    if (string.IsNullOrEmpty(xm.LoadedProject.PlcModel))
                    {
                        xm.LoadedProject.PlcModel = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).Select(d => d.Model).FirstOrDefault();
                    }
                    if (!xm.LoadedProject.PlcModel.Equals("XBLD-14E") && !xm.LoadedProject.PlcModel.Equals("XBLD-17E"))
                    {
                        xm.instructionsList.Remove(xm.instructionsList.FirstOrDefault(t => t.Text.Equals("ISNULL")));

                        xm.instructionTreeNodes.Nodes.Remove(xm.instructionTreeNodes.Nodes
                                    .Cast<TreeNode>()
                                    .SelectMany(node => node.Nodes.Cast<TreeNode>())
                                    .FirstOrDefault(subNode => subNode.Text == "ISNULL"));

                        xm.instructionsList.Remove(xm.instructionsList.FirstOrDefault(t => t.Text.Equals("NULL")));

                        xm.instructionTreeNodes.Nodes.Remove(xm.instructionTreeNodes.Nodes
                                    .Cast<TreeNode>()
                                    .SelectMany(node => node.Nodes.Cast<TreeNode>())
                                    .FirstOrDefault(subNode => subNode.Text == "NULL"));

                        xm.instructionsList.RemoveAll(t => t.InstructionType.Equals("ReadProperty"));
                        xm.instructionTreeNodes.Nodes.Remove(xm.instructionTreeNodes.Nodes
                                    .Cast<TreeNode>()
                                    .FirstOrDefault(subNode => subNode.Text == "ReadProperty"));
                        xm.instructionsList.RemoveAll(t => t.InstructionType.Equals("Write_Read_PV"));
                        if (xm?.instructionTreeNodes?.Nodes != null)
                        {
                            var nodeToRemove = xm.instructionTreeNodes.Nodes
                                .Cast<TreeNode>()
                                .FirstOrDefault(subNode => subNode.Text == "Write_Read_PV");

                            if (nodeToRemove != null)
                            {
                                xm.instructionTreeNodes.Nodes.Remove(nodeToRemove);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Please check instruciton file once \n" + ex.Message);
            }
        }

        private InstructionTypeDeserializer GetInformationInstruction(string instructionText)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                           @"MessungSystems\XMPS2000\NewInstructionFormat.xml");

            // Deserialize XML file into InstructionsRoot
            XmlSerializer serializer = new XmlSerializer(typeof(InstructionsRoot));
            InstructionsRoot instructionsRoot;

            using (var reader = new StreamReader(filePath))
            {
                instructionsRoot = (InstructionsRoot)serializer.Deserialize(reader);
            }

            return instructionsRoot?.InstrctionTypes?
                .SelectMany(c => c.Instructions)
                .FirstOrDefault(inst => inst.Text == instructionText);
        }

        private void UpdateProjectFileOpenInfo()
        {
            // Extract the directory path from the project path
            string projectPath = Path.GetDirectoryName(xm.LoadedProject.ProjectPath);

            //Creating log file and combine it with the directory path
            string fileName = $"{xm.LoadedProject.ProjectName}_LogInfo.txt";
            string filePath = Path.Combine(projectPath, fileName);
            string lastLine = string.Empty;
            bool addentry = false;
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    lastLine = File.ReadLines(filePath).Last();
                }
            }
            //Get the current version of setup
            AboutBox aboutus = new AboutBox();
            string currentRevisionNo = aboutus.GetRevisionNumber();
            if (!string.IsNullOrEmpty(lastLine))
            {
                string oldrevisionNo = lastLine.Split(' ')[lastLine.Split(' ').Length - 1];
                string currentRevision = currentRevisionNo.Split(' ')[2];
                addentry = !oldrevisionNo.Equals(currentRevision);
            }
            if (File.Exists(filePath) && addentry)
            {
                string dataToAppend = $"Date: [{DateTime.Now}] Setup version no: {XMPS.Instance.UtilityVersion} {currentRevisionNo}";

                File.AppendAllText(filePath, dataToAppend + Environment.NewLine);
            }
            else
            {
                if (File.Exists(filePath))
                {
                    // Check if the file has zero lines
                    var lineCount = File.ReadAllLines(filePath).Length;

                    if (lineCount == 0)
                    {
                        string dataToAppend = string.IsNullOrEmpty(lastLine)
                            ? $"Date: [{DateTime.Now}] Setup version no: {XMPS.Instance.UtilityVersion} {currentRevisionNo}"
                            : lastLine;

                        File.AppendAllText(filePath, dataToAppend + Environment.NewLine);
                    }
                }
                else
                {
                    string dataToAppend = string.IsNullOrEmpty(lastLine)
                        ? $"Date: [{DateTime.Now}] Setup version no: {XMPS.Instance.UtilityVersion} {currentRevisionNo}"
                        : lastLine;

                    File.AppendAllText(filePath.Replace("*", ""), dataToAppend + Environment.NewLine);
                }
            }
        }

        #endregion

        #region Child Form Controling

        private void ShowOrActivateForm(string formName, TreeNode treeNode = null, NodeInfo nodeInfo = null, string nodeName = "", bool isPrevOrNextClicked = false) 
        {
             xm.SelectedNode= nodeInfo;
            this.prevScreenIndex = xm.ScreensToNavigate.IndexOf(xm.CurrentScreen) != -1 ? xm.ScreensToNavigate.IndexOf(xm.CurrentScreen) : xm.ScreensToNavigate.Count;
            this.MQTTScreenName.Visible = false;

            bool proceed = BacNetValidator.CheckAndPromptSaveChanges();
            if (!proceed)
            {
                return;
            }
            switch (formName)
            {
                case "InitialForm":
                    {
                        HideLadderFunctionTree();
                        if (!CurrentForm(formName))
                        {
                            InitialForm frmInitial = new InitialForm();
                            frmInitial.MdiParent = this;
                            frmInitial.TopLevel = false;
                            frmInitial.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmInitial);
                            frmInitial.Show();
                            AddToLoadedForms(formName, frmInitial);
                            ActivateForm(formName);
                        }
                        else
                        {
                            ActivateForm(formName);
                        }
                        if (!isPrevOrNextClicked)
                        {
                            AddToScreenNavigation(formName, treeNode);
                        }
                        break;
                    }

                case "COMDeviceForm":
                    {
                        HideLadderFunctionTree();
                        if (!CurrentForm(formName))
                        {
                            frmGridLayout frmCOMGrid = new frmGridLayout(formName);
                            frmCOMGrid.MdiParent = this;
                            frmCOMGrid.TopLevel = false;
                            frmCOMGrid.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmCOMGrid);

                            frmCOMGrid.Show();
                            AddToLoadedForms(formName, frmCOMGrid);
                            ActivateForm(formName);
                        }
                        else
                        {
                            ActivateForm(formName);
                        }
                        // when form is being accessed by clicking on tree node
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName, treeNode);
                        }
                        break;
                    }

                case "EthernetForm":
                    {
                        HideLadderFunctionTree();
                        if (!CurrentForm(formName))
                        {
                            frmGridLayout frmETHGrid = new frmGridLayout(formName);
                            frmETHGrid.MdiParent = this;
                            frmETHGrid.TopLevel = false;
                            frmETHGrid.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmETHGrid);

                            frmETHGrid.Show();
                            AddToLoadedForms(formName, frmETHGrid);
                            ActivateForm(formName);
                        }
                        else
                        {
                            ActivateForm(formName);
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName, treeNode);
                        }
                        break;
                    }
                case "LookUpTbl":
                    {
                        HideLadderFunctionTree();
                        if (!CurrentForm(formName + "#" + nodeName))
                        {
                            frmGridLayout frmMODBUSTCPServerGrid = new frmGridLayout(formName + "#" + nodeName);
                            frmMODBUSTCPServerGrid.MdiParent = this;
                            frmMODBUSTCPServerGrid.TopLevel = false;
                            frmMODBUSTCPServerGrid.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmMODBUSTCPServerGrid);
                            frmMODBUSTCPServerGrid.Show();

                            AddToLoadedForms(formName + "#" + nodeName, frmMODBUSTCPServerGrid);
                            ActivateForm(formName + "#" + nodeName);
                        }
                        else
                        {
                            ActivateForm(formName + "#" + nodeName);
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        break;
                    }
                case "ResistanceValue":
                    {
                        HideLadderFunctionTree();
                        if (!CurrentForm(formName + "#" + nodeName))
                        {
                            frmGridLayout frmMODBUSTCPServerGrid1 = new frmGridLayout(formName + "#" + nodeName);
                            frmMODBUSTCPServerGrid1.MdiParent = this;
                            frmMODBUSTCPServerGrid1.TopLevel = false;
                            frmMODBUSTCPServerGrid1.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmMODBUSTCPServerGrid1);
                            frmMODBUSTCPServerGrid1.Show();

                            AddToLoadedForms(formName + "#" + nodeName, frmMODBUSTCPServerGrid1);
                            ActivateForm(formName + "#" + nodeName);
                        }
                        else
                        {
                            ActivateForm(formName + "#" + nodeName);
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        break;
                    }

                case "MODBUSTCPServerForm":
                case "ModbusRequestForm":
                    {
                        HideLadderFunctionTree();
                        if (!CurrentForm(formName + "#" + nodeName))
                        {
                            frmGridLayout frmMODBUSTCPServerGrid = new frmGridLayout(formName + "#" + nodeName);
                            frmMODBUSTCPServerGrid.MdiParent = this;
                            frmMODBUSTCPServerGrid.TopLevel = false;
                            frmMODBUSTCPServerGrid.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmMODBUSTCPServerGrid);
                            frmMODBUSTCPServerGrid.Show();

                            AddToLoadedForms(formName + "#" + nodeName, frmMODBUSTCPServerGrid);
                            ActivateForm(formName + "#" + nodeName);
                        }
                        else
                        {
                            ActivateForm(formName + "#" + nodeName);
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        break;
                    }
                case "MODBUSRTUSlavesForm":
                    {
                        HideLadderFunctionTree();
                        if (!CurrentForm(formName + "#" + nodeName))
                        {
                            frmGridLayout frmMODBUSTCPServerGrid = new frmGridLayout(formName + "#" + nodeName);
                            frmMODBUSTCPServerGrid.MdiParent = this;
                            frmMODBUSTCPServerGrid.TopLevel = false;
                            frmMODBUSTCPServerGrid.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmMODBUSTCPServerGrid);
                            frmMODBUSTCPServerGrid.Show();

                            AddToLoadedForms(formName + "#" + nodeName, frmMODBUSTCPServerGrid);
                            ActivateForm(formName + "#" + nodeName);
                        }
                        else
                        {
                            ActivateForm(formName + "#" + nodeName);
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        break;
                    }
                case "MODBUSTCPClientForm":
                case "ModbusTCPSlaveForm":
                    {
                        HideLadderFunctionTree();
                        if (!CurrentForm(formName + "#" + nodeName))
                        {
                            frmGridLayout frmMODBUSTCPSlaveGrid = new frmGridLayout(formName + "#" + nodeName);
                            frmMODBUSTCPSlaveGrid.MdiParent = this;
                            frmMODBUSTCPSlaveGrid.TopLevel = false;
                            frmMODBUSTCPSlaveGrid.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmMODBUSTCPSlaveGrid);
                            frmMODBUSTCPSlaveGrid.Show();

                            AddToLoadedForms(formName + "#" + nodeName, frmMODBUSTCPSlaveGrid);
                            ActivateForm(formName + "#" + nodeName);
                        }
                        else
                        {
                            ActivateForm(formName + "#" + nodeName);
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        break;
                    }
                case "MODBUSRTUMasterForm":
                case "ModbusRTUSlaveForm":
                    {
                        HideLadderFunctionTree();
                        if (!CurrentForm(formName + "#" + nodeName))
                        {

                            frmGridLayout frmMODBUSRTUMasterGrid = new frmGridLayout(formName + "#" + nodeName);
                            frmMODBUSRTUMasterGrid.MdiParent = this;
                            frmMODBUSRTUMasterGrid.TopLevel = false;
                            frmMODBUSRTUMasterGrid.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmMODBUSRTUMasterGrid);
                            frmMODBUSRTUMasterGrid.Show();

                            AddToLoadedForms(formName + "#" + nodeName, frmMODBUSRTUMasterGrid);
                            ActivateForm(formName + "#" + nodeName);
                        }
                        else
                        {
                            ActivateForm(formName + "#" + nodeName);
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        break;
                    }
                case "System Tags":
                    {
                        HideLadderFunctionTree();
                        string strnodeinfo = "";
                        if (nodeInfo == null)
                            strnodeinfo = "System Tags";
                        else strnodeinfo = nodeInfo.Info.ToString();
                        if (!CurrentForm(formName + "#" + strnodeinfo))
                        {
                            frmGridLayout frmTagsGrid = new frmGridLayout(formName + "#" + strnodeinfo.ToString());
                            frmTagsGrid.MdiParent = this;
                            frmTagsGrid.TopLevel = false;
                            frmTagsGrid.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmTagsGrid);
                            frmTagsGrid.Show();
                            AddToLoadedForms(formName + "#" + strnodeinfo.ToString(), frmTagsGrid);
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        else
                        {
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        break;
                    }
                //System Tag ---> Active the Form For System Tag
                case "TagsForm":
                    {
                        HideLadderFunctionTree();
                        if (treeNode != null)
                        {
                            if (treeNode.Parent.Parent.Text == "UDFB")
                            {
                                formName = treeNode.Text;
                            }
                        }
                        //adding extra logic for checking if prev or next screen UDFTags.
                        if (isPrevOrNextClicked && nodeName.EndsWith("UDFTags"))
                        {
                            formName = nodeName.Split('#')[0].Trim();
                            nodeName = nodeName.Split('#')[1].Trim();
                        }
                        string strnodeinfo = "";
                        if (nodeInfo == null && !isPrevOrNextClicked)
                            strnodeinfo = "User Defined Tags";
                        else if (nodeInfo == null && isPrevOrNextClicked)
                            strnodeinfo = nodeName;
                        else strnodeinfo = nodeInfo.Info.ToString();
                        if (!CurrentForm(formName + "#" + strnodeinfo))
                        {
                            frmGridLayout frmTagsGrid = new frmGridLayout(formName + "#" + strnodeinfo.ToString());
                            frmTagsGrid.MdiParent = this;
                            frmTagsGrid.TopLevel = false;
                            frmTagsGrid.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmTagsGrid);
                            frmTagsGrid.Show();
                            AddToLoadedForms(formName + "#" + strnodeinfo.ToString(), frmTagsGrid);
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        else
                        {
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + strnodeinfo, treeNode);
                        }
                        break;
                    }
                case "MainForm":
                    {
                        HideLadderFunctionTree();
                        if (!CurrentForm(formName + "#" + nodeName))
                        {
                            MainLadderForm frmMain = new MainLadderForm();
                            frmMain.MdiParent = this;
                            frmMain.TopLevel = false;
                            frmMain.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmMain);
                            frmMain.Show();
                            AddToLoadedForms(formName + "#" + nodeName, frmMain);
                            ActivateForm(formName + "#" + nodeName);
                        }
                        else
                        {
                            var lw = (MainLadderForm)xm.LoadedScreens[formName + "#" + nodeName];
                            HideForm();
                            ActivateForm(formName + "#" + nodeName);
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        if (_LoggedIn)
                            ShowOnlineMonitoringMainForm();
                        xm.MarkProjectModified(true);
                        break;
                    }
                case "Mqtt Form":
                    {
                        string strnodeinfo = "";
                        if (!CurrentForm(formName + "#" + strnodeinfo))
                        {
                            frmGridLayout frmMQTT = new frmGridLayout(formName + "#" + nodeName);
                            frmMQTT.MdiParent = this;
                            frmMQTT.TopLevel = false;
                            frmMQTT.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmMQTT);
                            frmMQTT.Show();
                            AddToLoadedForms(formName + "#" + nodeName, frmMQTT);
                            ActivateForm(formName + "#" + nodeName);
                        }
                        else
                        {
                            ActivateForm(formName + "#" + nodeName);
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        break;
                    }
                case "MQTT Publish":
                case "MQTT PublishForm":
                case "MQTT SubscribeForm":
                    {
                        if (!CurrentForm(formName + "#" + nodeName))
                        {
                            frmGridLayout publish = new frmGridLayout(formName + "#" + nodeName);
                            publish.MdiParent = this;
                            publish.TopLevel = false;
                            publish.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(publish);
                            publish.Show();
                            AddToLoadedForms(formName + "#" + treeNode.Text, publish);
                            ActivateForm(formName + "#" + treeNode.Text);
                        }
                        else
                        {
                            ActivateForm(formName + "#" + nodeName);
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        this.MQTTScreenName.Visible = true;
                        this.MQTTScreenName.Text = formName.Equals("MQTT SubscribeForm") ? "MQTT Subscribe" : "MQTT Publish";
                        break;

                    }

                case "LadderForm":
                case "PURLadderForm":
                case "HILadderForm":
                case "UDFLadderForm":
                case "InterruptLogicBlock01Form":
                case "InterruptLogicBlock02Form":
                    {
                        ShowLadderFunctionTree();

                        if (!CurrentForm(formName + "#" + nodeName))
                        {
                            LadderWindow frmLadder = new LadderWindow(nodeName);
                            frmLadder.MdiParent = this;
                            frmLadder.TopLevel = false;
                            frmLadder.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmLadder);
                            frmLadder.Show();
                            AddToLoadedForms(formName + "#" + nodeName, frmLadder);
                            ActivateForm(formName + "#" + nodeName);
                            LoadCurrentBlock(formName + "#" + nodeName);
                        }
                        else
                        {
                            var lw = (LadderWindow)xm.LoadedScreens[formName + "#" + nodeName];
                            HideForm();
                            ActivateForm(formName + "#" + nodeName);
                            LoadCurrentBlock(formName + "#" + nodeName);
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        xm.MarkProjectModified(true);
                        break;
                    }

                case "IOConfigForm":
                    {
                        HideLadderFunctionTree();
                        string strnodeinfo = "";
                        if (nodeInfo == null)
                            strnodeinfo = "Configuration";
                        else strnodeinfo = nodeInfo.Info.ToString();
                        if (!CurrentForm(formName + "#" + strnodeinfo))
                        {
                            frmGridLayout frmTagsGrid = new frmGridLayout(formName + "#" + strnodeinfo.ToString());
                            frmTagsGrid.MdiParent = this;
                            frmTagsGrid.TopLevel = false;
                            frmTagsGrid.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmTagsGrid);
                            frmTagsGrid.Show();
                            AddToLoadedForms(formName + "#" + strnodeinfo.ToString(), frmTagsGrid);
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        else
                        {
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + "Configuration", treeNode);
                        }
                        break;
                    }
                case "HSIOConfigForm":
                    {
                        string strnodeinfo = "";
                        if (!CurrentForm(formName + "#" + strnodeinfo))
                        {
                            frmHSIOConfigeration frmHSIOConfig = new frmHSIOConfigeration();
                            frmHSIOConfig.MdiParent = this;
                            frmHSIOConfig.TopLevel = false;
                            frmHSIOConfig.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmHSIOConfig);
                            frmHSIOConfig.Show();
                            AddToLoadedForms(formName + "#" + strnodeinfo.ToString(), frmHSIOConfig);
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        else
                        {
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        break;
                    }
                case "Device":
                case "BacNetIP":
                case "Binary Value":
                case "Hardware IO's":
                case "Analog Value":
                case "Multistate Value":
                case "Calendar":
                case "Schedule":
                case "Notification Class":
                case "Network Port":
                    {
                        HideLadderFunctionTree();
                        string strnodeinfo = "";
                        if (!CurrentForm(formName + "#" + strnodeinfo))
                        {
                            FormBacNet formBacNet = new FormBacNet(treeNode.Text);
                            formBacNet.MdiParent = this;
                            formBacNet.TopLevel = false;
                            formBacNet.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(formBacNet);
                            formBacNet.Show();
                            AddToLoadedForms(formName + "#" + strnodeinfo.ToString(), formBacNet);
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        else
                        {
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        break;
                    }
                case "TaskConfigForm": //Adding TaskConfiguration Form
                    {
                        HideLadderFunctionTree();
                        string strnodeinfo = "";
                        if (!CurrentForm(formName + "#" + strnodeinfo))
                        {
                            frmTaskConfiguration frmTaskConfiguration = new frmTaskConfiguration();
                            frmTaskConfiguration.MdiParent = this;
                            frmTaskConfiguration.TopLevel = false;
                            frmTaskConfiguration.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(frmTaskConfiguration);
                            frmTaskConfiguration.Show();
                            AddToLoadedForms(formName + "#" + strnodeinfo.ToString(), frmTaskConfiguration);
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        else
                        {
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        if (!isPrevOrNextClicked && treeNode != null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        break;
                    }
                case "TraceWindow":
                    {
                        HideLadderFunctionTree();
                        string strnodeinfo = "";
                        if (!CurrentForm(formName + "#" + strnodeinfo))
                        {
                            TraceWindow traceWindow = new TraceWindow();
                            traceWindow.MdiParent = this;
                            traceWindow.TopLevel = false;
                            traceWindow.Dock = DockStyle.Fill;
                            splitContainer1.Panel1.Controls.Add(traceWindow);
                            traceWindow.Show();
                            AddToLoadedForms(formName + "#" + strnodeinfo.ToString(), traceWindow);
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        else
                        {
                            ActivateForm(formName + "#" + strnodeinfo.ToString());
                        }
                        if (!isPrevOrNextClicked && treeNode == null)
                        {
                            AddToScreenNavigation(formName + "#" + nodeName, treeNode);
                        }
                        break;
                    }
                default:
                    {
                        HideLadderFunctionTree();
                        HideForm();
                        break;
                    }
            }
        }

        private void ShowOnlineMonitoringMainForm()
        {
            string windowname = xm.CurrentScreen;
            MainLadderForm window = (MainLadderForm)xm.LoadedScreens[windowname];
            OnlineMonitoringHelper omh = OnlineMonitoringHelper.Instance;
            omh.PopulateTagToAddress();
            List<string> addressList = new List<string>();
            foreach (string row in xm.LoadedProject.MainLadderLogic.Where(d => !(d.StartsWith("'"))))
            {
                var tagnames = row.Split(')');
                if (tagnames.Count() > 0)
                {
                    List<string> tags = new List<string>();
                    for (int i = 0; i < tagnames.Count(); i++)
                    {
                        if (tagnames[i].Contains("("))
                            tags.Add(tagnames[i].Replace("(", "").Replace("AND", "").Replace("~", "").Trim());
                    }
                    addressList.AddRange(tags);
                }
            }
            string tagName = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == XMPS.Instance.LoadedProject.PLCStatusTag).Select(t => t.Tag).FirstOrDefault();
            if (tagName != null)
            {
                addressList.Add(tagName);
            }
            ///Send the error status address in every cycle to check status of CPU
            foreach (string errorTagAddress in XMPS.Instance.LoadedProject.ErrorStatusTags)
            {
                XMIOConfig errTagDtl = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == errorTagAddress).FirstOrDefault();
                if (Enum.GetNames(typeof(AddressDataTypes)).Any(name => name.Equals(errTagDtl.Label, StringComparison.OrdinalIgnoreCase)))
                {
                    addressList.Add(errTagDtl.Tag);
                }
            }
            omh.SendActiveRungAddress(addressList);
            OnlineMonitoringStatus.isOnlineMonitoring = true;
            omh.SetCurrentCanvas(window.getLadderEditor().getCanvas());
            OnlineMonitorTimer.Start();

            xm.presentInMain = true;

            ToolStrip curWindowControl = window.getLadderEditorToolStrip();
            curWindowControl.Enabled = false;
        }

        private void ShowLadderFunctionTree()
        {
            splcInner.Panel2Collapsed = false;
            tvBlocks.Nodes.Clear();
            AddBlocksData();
        }

        private void HideLadderFunctionTree()
        {
            splcInner.Panel2Collapsed = true;
            splitContainer1.Panel2Collapsed = true;
        }

        private void HideForm(string formName = "")
        {
            if (formName == string.Empty)
            {
                formName = xm.CurrentScreen;
            }
            if (formName != string.Empty && xm.LoadedScreens.TryGetValue(formName, out Form val))
            {
                Form frmTemp = xm.LoadedScreens[formName];
                frmTemp.Hide();
            }
        }

        private void ActivateForm(string formName)
        {
            HideForm();
            if (formName.StartsWith("Mqtt Form#"))
            {
                if (!xm.LoadedScreens.TryGetValue(formName, out _))
                    formName = xm.LoadedScreens.Where(v => v.Key.StartsWith("Mqtt Form#")).Select(v => v.Key).FirstOrDefault().ToString();

            }
            Form frmTemp = xm.LoadedScreens[formName];
            frmTemp.Show();
            xm.CurrentScreen = formName;
            ((IXMForm)frmTemp).OnShown();
        }

        private bool CurrentForm(string formName)
        {
            return xm.LoadedScreens.ContainsKey(formName);
        }

        private void AddToLoadedForms(string formName, Form frmToAdd)
        {
            if (!xm.LoadedScreens.ContainsKey(formName))
            {
                xm.LoadedScreens.Add(formName, frmToAdd);
            }
        }

        // adding/modifying screen navigation list only when form is activated through clicking tree node
        // not keeping InitialForm in screen navigation
        private void AddToScreenNavigation(string formName, TreeNode tn)
        {
            if (prevScreenIndex != -1)
            {
                for (var i = prevScreenIndex + 1; i < xm.ScreensToNavigate.Count;)
                {
                    xm.ScreensToNavigate.RemoveAt(i);
                }
            }
            var screenToRemove = xm.ScreensToNavigate.SingleOrDefault(s => s == formName);
            if (screenToRemove != null)
                xm.ScreensToNavigate.Remove(screenToRemove);
            HashSet<string> bacNetForms = new HashSet<string>() { "Device", "BacNetIP", "Binary Value","ResistanceValue",
                                                                  "Hardware IO's", "Analog Value", "Multistate Value",
                                                                  "Calendar", "Schedule", "Notification", "File" , "Mqtt Form", "HSIOConfigForm"};
            HashSet<string> mainTreeNodes = new HashSet<string>() { "EthernetForm", "COMDeviceForm" };
            bool isform = formName.Contains('#') ? (!string.IsNullOrEmpty(formName.Split('#')[1]) ? true
                : false) :
                          mainTreeNodes.Contains(formName) ? true : false;
            bool isBacNet = !string.IsNullOrEmpty(formName.Split('#')[0]) ? bacNetForms.Contains(formName.Split('#')[0].Trim()) : false;
            if (formName != "InitialForm" && (isform || isBacNet))
            {
                xm.ScreensToNavigate.Add(formName);
                strpBtnPrvScreen.Enabled = xm.ScreensToNavigate.Count > 1 && !(xm.PlcStatus == "LogIn");
            }
            else
            {
                strpBtnPrvScreen.Enabled = xm.ScreensToNavigate.Count > 0 && !(xm.PlcStatus == "LogIn");
            }
            strpBtnNxtScreen.Enabled = false;
            xm.ScreensTreeNode.Remove(formName);
            xm.ScreensTreeNode.Add(formName, tn);
        }

        private void NavigateToPrevious()
        {
            var currentScreenIndex = xm.ScreensToNavigate.IndexOf(xm.CurrentScreen);
            if (currentScreenIndex != 0)
            {
                // if current screen hasn't been kept for navigation
                if (currentScreenIndex == -1)
                {
                    currentScreenIndex = xm.ScreensToNavigate.Count;
                }
                string prevScreen = xm.ScreensToNavigate[currentScreenIndex - 1];
                TreeNode tn = xm.ScreensTreeNode[prevScreen];
                if (prevScreen.Contains('#'))
                {
                    var values = prevScreen.Split('#');
                    string prevScreenFormName = values[0];
                    string prevScreenNodeName = values[1];
                    //for UDFB Tags window.
                    bool isUDFBTagsForm = prevScreenNodeName.Equals("UDFTags");
                    prevScreenNodeName = isUDFBTagsForm ? $"{prevScreenFormName}#{prevScreenNodeName}" : prevScreenNodeName;
                    prevScreenFormName = isUDFBTagsForm ? "TagsForm" : prevScreenFormName;
                    ShowOrActivateForm(formName: prevScreenFormName, nodeName: prevScreenNodeName, isPrevOrNextClicked: true);
                }
                else
                {
                    ShowOrActivateForm(prevScreen, isPrevOrNextClicked: true);
                }
                this.prevScreenIndex = currentScreenIndex;
                if (currentScreenIndex == 1)
                {
                    strpBtnPrvScreen.Enabled = false;
                }
                strpBtnNxtScreen.Enabled = (!(currentScreenIndex == xm.ScreensToNavigate.Count) && !(xm.PlcStatus == "LogIn"));
                tvProjects.SelectedNode = tn;
            }
        }

        private void NavigateToNext()
        {
            var currentScreenIndex = xm.ScreensToNavigate.IndexOf(xm.CurrentScreen);
            if (currentScreenIndex != -1 && currentScreenIndex != (xm.ScreensToNavigate.Count - 1))
            {
                string nextScreen = xm.ScreensToNavigate[currentScreenIndex + 1];
                TreeNode tn = xm.ScreensTreeNode[nextScreen];
                if (nextScreen.Contains('#'))
                {
                    var values = nextScreen.Split('#');
                    string nextScreenFormName = values[0];
                    string nextScreenNodeName = values[1];
                    //for UDFB Tags window.
                    bool isUDFBTagsForm = nextScreenNodeName.Equals("UDFTags");
                    nextScreenNodeName = isUDFBTagsForm ? $"{nextScreenFormName}#{nextScreenNodeName}" : nextScreenNodeName;
                    nextScreenFormName = isUDFBTagsForm ? "TagsForm" : nextScreenFormName;
                    ShowOrActivateForm(formName: nextScreenFormName, nodeName: nextScreenNodeName, isPrevOrNextClicked: true);
                }
                else
                {
                    ShowOrActivateForm(nextScreen, isPrevOrNextClicked: true);
                }
                this.prevScreenIndex = currentScreenIndex;
                strpBtnPrvScreen.Enabled = true;
                tvProjects.SelectedNode = tn;
            }
            if (currentScreenIndex == -1 || currentScreenIndex == (xm.ScreensToNavigate.Count - 2) || xm.PlcStatus == "LogIn")
            {
                strpBtnNxtScreen.Enabled = false;
            }
        }

        #endregion

        #region Treeview
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeInfo"></param>
        /// <param name="treeNode"></param>
        /// <param name="nodeName"></param>
        private void PerformTreeNodeActions(NodeInfo nodeInfo, TreeNode treeNode, string nodeName = "")
        {
            if (nodeInfo.Info == "File")
            {
                return;
            }
            var nodeType = (int)nodeInfo.NodeType;
            string Hsio = "";
            if (treeNode.Text == "Base (XM-14-DT-HIO)")
            {
                Hsio = (string)treeNode.FirstNode.Text;
            }
            if (!_LoggedIn)
            {
                //set to currentRungScroll position to 0 after change the logic block.
                LadderDesign.currentRungScroll = 0;
                //assigning old desing for udfb.
                if (currentUDFBElements.Item1 != null)
                {
                    ResetUDFBOrigional();
                }
                switch (nodeType)
                {

                    case 1: // RootNode
                        {
                            ShowOrActivateForm("InitialForm");
                            break;
                        }

                    case 2: // ListNode
                        {
                            xm.LoadedProject.ClearFilter = true;
                            if (nodeInfo.Info == "IOConfig")
                            {
                                ShowOrActivateForm("IOConfigForm", treeNode: treeNode, nodeInfo);
                            }
                            else if (nodeInfo.Info == "HSIOConfig")
                            {
                                ShowOrActivateForm("HSIOConfigForm", treeNode: treeNode, nodeInfo);
                            }
                            else if (nodeInfo.Info == "TaskConfig")
                            {
                                ShowOrActivateForm("TaskConfigForm", treeNode: treeNode, nodeInfo);
                            }
                            else if (nodeInfo.Info == "LookUpTbl")
                            {
                                ShowOrActivateForm("LookUpTbl", treeNode: treeNode, nodeInfo);
                            }
                            else if (nodeInfo.Info == "ResistanceValue" || (treeNode.Parent != null && treeNode.Parent.Text == "Resistance Lookup Table"))
                            {
                                ShowOrActivateForm("ResistanceValue", treeNode: treeNode, nodeInfo);
                            }
                            else
                            {
                                ShowOrActivateForm("TagsForm", treeNode: treeNode, nodeInfo);
                            }

                            break;
                        }

                    case 3: // ProjectNode
                        {
                            //ShowRecentProject(nodeInfo);
                            break;
                        }

                    case 4: // CurrentProjectNode
                        {
                            break;
                        }

                    case 5: // BlockNode
                    case 6: // DeviceNode
                    case 7: // MainBlockNode
                        {
                            ShowOrActivateForm(nodeInfo.Info + "Form", treeNode: treeNode, nodeInfo, nodeName.Trim());
                            string pubInfo = nodeInfo.Info.ToString();
                            if (nodeInfo.Info == "User Defined Tags")
                            {
                                ShowOrActivateForm("User Defined Tags", treeNode: treeNode, nodeInfo);
                            }
                            //else if (nodeInfo.Info == "ResistanceValue")
                            //{
                            //    ShowOrActivateForm("ResistanceValue", treeNode: treeNode, nodeInfo);
                            //}
                            else if (nodeInfo.Info == "System Tags")
                            {
                                ShowOrActivateForm("System Tags", treeNode: treeNode, nodeInfo);
                            }
                            else if (nodeInfo.Info == "MQTT client")
                            {
                                ShowOrActivateForm("Mqtt Form", treeNode: treeNode, nodeInfo);
                            }
                            else if (treeNode.Parent.Text == "MQTT Publish")
                            {
                                ShowOrActivateForm(treeNode.Parent.Text, treeNode: treeNode, nodeInfo);

                            }
                            break;
                        }
                    case 8:
                        {
                            ShowOrActivateForm(nodeInfo.Info, treeNode: treeNode, nodeInfo);
                        }
                        break;
                    default:
                        {
                            break;
                        }
                }
            }
            else if (_LoggedIn && (nodeType == 5 || nodeName == "Main"))
            {
                OnlineMonitorTimer.Stop();
                OnlineMonitoringStatus.isOnlineMonitoring = false;
                if (nodeInfo.Info == "UDFLadder" || nodeInfo.Info == "Ladder")
                {
                    UDFBInfo udfbinfo = (UDFBInfo)XMPS.Instance.LoadedProject.UDFBInfo.Where(u => u.UDFBName == treeNode.Text.Replace(" Logic", "")).FirstOrDefault();
                    if (udfbinfo != null)
                    {
                        if (currentUDFBElements.Item1 != null)
                        {
                            LadderWindow udfbWindow = (LadderWindow)xm.LoadedScreens[$"UDFLadderForm#{currentUDFBElements.Item2}"];
                            udfbWindow.getLadderEditor().getCanvas().getDesignView().Elements = currentUDFBElements.Item1.Elements;
                            int _blockIndex = xm.LoadedProject.Blocks.FindIndex(d => d.Name == currentUDFBElements.Item2);
                            xm.LoadedProject.Blocks[_blockIndex].Elements.Clear();
                            xm.LoadedProject.Blocks[_blockIndex].Elements.AddRange(currentUDFBBlockElements);
                            xm.LoadedProject.MainLadderLogic.RemoveAll(t => t.Equals(currentUDFBElements.Item2));
                            currentUDFBElements.Item1 = null;
                            currentUDFBElements.Item2 = string.Empty;
                        }

                        List<string> getudfbrungs = GetUDFBUsedRungs(udfbinfo.UDFBName, true);
                        if (getudfbrungs.Count > 0)
                        {
                            ShowUDFBSuggestion udfbSuggest = new ShowUDFBSuggestion(getudfbrungs);
                            int centerX = this.Left + (this.Width - 150) / 2;
                            int centerY = this.Top + (this.Height - udfbSuggest.Height + 25) / 2;

                            udfbSuggest.StartPosition = FormStartPosition.Manual;
                            udfbSuggest.Location = new System.Drawing.Point(centerX, centerY);

                            udfbSuggest.ShowDialog();
                            currentUDFBElements.Item1 = udfbSuggest.OldUDFBDesing();
                            currentUDFBElements.Item2 = treeNode.Text;
                            currentUDFBBlockElements = udfbSuggest.OldBlockElements();
                        }
                        else
                            return;
                    }
                    else
                    {
                        if (currentUDFBElements.Item1 != null)
                        {
                            LadderWindow udfbWindow = (LadderWindow)xm.LoadedScreens[$"UDFLadderForm#{currentUDFBElements.Item2}"];
                            udfbWindow.getLadderEditor().getCanvas().getDesignView().Elements = currentUDFBElements.Item1.Elements;
                            int _blockIndex = xm.LoadedProject.Blocks.FindIndex(d => d.Name == $"{currentUDFBElements.Item2}");
                            xm.LoadedProject.Blocks[_blockIndex].Elements.Clear();
                            xm.LoadedProject.Blocks[_blockIndex].Elements.AddRange(currentUDFBBlockElements);
                            xm.LoadedProject.MainLadderLogic.RemoveAll(t => t.Equals(currentUDFBElements.Item2));
                            currentUDFBElements.Item1 = null;
                            currentUDFBElements.Item2 = string.Empty;
                        }
                    }
                }
                ShowOrActivateForm(nodeInfo.Info + "Form", treeNode: treeNode, nodeInfo, nodeName);
            }
            //Enabling the System Tag & User Defined Tag Window In Online Monitroring Mode
            else if (_LoggedIn && (nodeType == 6 || nodeType == 2 || nodeType == 8))
            {
                OnlineMonitorTimer.Stop();
                OnlineMonitoringStatus.isOnlineMonitoring = false;
                var formMappings = new Dictionary<string, string>
                {
                    { "MODBUSTCPClient", "MODBUSTCPClientForm" },
                    { "MODBUSTCPServer", "MODBUSTCPServerForm" },
                    { "MODBUSRTUMaster", "MODBUSRTUMasterForm" },
                    { "MODBUSRTUSlaves", "MODBUSRTUSlavesForm" },
                    { "UDFTags", "TagsForm" },
                    { "OnBoardIO", "TagsForm"},
                    { "ExpansionIO", "TagsForm"},
                    { "RemoteIO", "TagsForm"},
                    { "HSIOConfig", "HSIOConfigForm" },
                    { "IOConfig", "IOConfigForm"},
                    { "System Tags", "System Tags"},
                    { "User Defined Tags", "System Tags"},
                    { "BacNetIP", "BacNetIP"},
                    { "Device", "Device" },
                    { "Hardware IO's", "Hardware IO's" },
                    { "Binary Value", "Binary Value" },
                    { "Analog Value", "Analog Value" },
                    { "Multistate Value", "Multistate Value" },
                    { "Calendar", "Calendar" },
                    { "Schedule", "Schedule" },
                    { "Notification Class", "Notification Class" },
                    { "File", "File" },
                    { "MQTT client", "Mqtt Form" },
                    { "MQTT Publish", "MQTT Publish" },
                    { "MQTT Subscribe", "MQTT SubscribeForm" },
                     { "Network Port", "Network Port" },
                     { "Ethernet", "EthernetForm" },
                    { "LookUpTbl", "LookUpTbl" }
                };
                if (xm.LoadedProject.ResistanceTables.Any(a=>a.Name== nodeInfo.Info))
                {
                    formMappings[nodeName] = "ResistanceValue";
                }
                if (formMappings.TryGetValue(nodeInfo.Info, out string formName) || treeNode.Parent.Text == "Expansion I/O" || treeNode.Parent.Text == "Remote I/O")
                {
                    ShowOrActivateForm((treeNode.Parent.Text == "Expansion I/O" || treeNode.Parent.Text == "Remote I/O") ? "TagsForm" : formName, treeNode: treeNode, nodeInfo, nodeName);
                }
            }
        }

        private void ResetUDFBOrigional()
        {
            LadderWindow udfbWindow = (LadderWindow)xm.LoadedScreens[$"UDFLadderForm#{currentUDFBElements.Item2}"];
            udfbWindow.getLadderEditor().getCanvas().getDesignView().Elements = currentUDFBElements.Item1.Elements;
            udfbWindow.getLadderEditor().getCanvas().getDesignView().parallelElementsDictionary = currentUDFBElements.Item1.ParallelElementsDictionary;
            xm.LoadedProject.MainLadderLogic.RemoveAll(t => t.Equals(currentUDFBElements.Item2));
            int _blockIndex = xm.LoadedProject.Blocks.FindIndex(d => d.Name == currentUDFBElements.Item2);
            xm.LoadedProject.Blocks[_blockIndex].Elements.Clear();
            xm.LoadedProject.Blocks[_blockIndex].Elements.AddRange(currentUDFBBlockElements);
            currentUDFBElements.Item1 = null;
            currentUDFBElements.Item2 = string.Empty;
        }

        /// <summary>
        /// Catch the double click on the project thee node so that selected project can get opended
        /// </summary>
        /// <param name="nodeInfo"></param> Selected node information 
        /// <param name="treeNode"></param> selected tree node
        /// <param name="nodeName"></param> selected node name
        private void PerformTreeNodeDoubleClickActions(NodeInfo nodeInfo, TreeNode treeNode, string nodeName = "")
        {
            var nodeType = (int)nodeInfo.NodeType;
            if (!_LoggedIn)
            {
                switch (nodeType)
                {

                    case 1: // RootNode
                        {
                            break;
                        }

                    case 2: // ListNode
                        {
                            break;
                        }

                    case 3: // ProjectNode
                        {
                            if (ChangeLoadedProject())
                            {
                                ShowRecentProject(nodeInfo);
                                MenuModeLogin.Enabled = true;
                                strpBtnLogin.Enabled = true;
                                MenuModePLCStop.Enabled = false;
                                MenuModeLogout.Enabled = false;
                                traceWindowToolStripMenuItem.Enabled = false;
                                strpBtnLogout.Enabled = false;
                                MenuModePLCStart.Enabled = false;
                                MenuModePLCResetOrigin.Enabled = false;
                                MenuModePLCResetCold.Enabled = false;
                                MenuModePLCResetwarm.Enabled = false;
                                strpBtnCloseProject.Enabled = true;
                                strpBtnDownloadProject.Enabled = true;
                                forceUnforceMenu.Enabled = false;
                                tssStatusLabel_msg($"Opened project {nodeInfo.Info}", 3000, "DodgerBlue");
                            }
                            break;
                        }

                    case 4: // CurrentProjectNode
                        {
                            break;
                        }

                    case 5: // BlockNode
                    case 6: // DeviceNode
                    case 7: // MainBlockNode
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// Load Project on which the user has clicked on
        /// </summary>
        /// <param name="nodeInfo"></param> Selected node info, this will have the path and name of the project to be loaded
        private void ShowRecentProject(NodeInfo nodeInfo)
        {
            var project = xm.RecentProjects.Projects.Where(p => p.ProjectName == (nodeInfo.Info.ToString())).LastOrDefault();
            xm.SetCurrentProject(project);
            LoadCurrentProject();
            AddSystemTags();

            //AddDefaultTags();
            //UpdateSystemTags(xm.PlcModel);
        }
        /// <summary>
        /// Show all tree nodes as per the project selected by the user
        /// </summary>
        private void RenderBaseTreeNodes()
        {
            TreeNode nodeAllProjects = new TreeNode("All Projects");
            NodeInfo niAllProjects = new NodeInfo();
            niAllProjects.NodeType = NodeType.RootNode;
            nodeAllProjects.Tag = niAllProjects;
            nodeAllProjects.Expand();

            TreeNode nodeCurrentProject = new TreeNode(PROJECT_NODE_DEFAULT_NAME);
            nodeCurrentProject.Name = "curProject";
            NodeInfo niCurrentProject = new NodeInfo();
            niCurrentProject.NodeType = NodeType.CurrentProjectNode;
            nodeCurrentProject.Tag = niCurrentProject;
            nodeCurrentProject.Expand();

            TreeNode nodeRecentProjects = new TreeNode("Recent Projects");
            NodeInfo niRProjects = new NodeInfo();
            niRProjects.NodeType = NodeType.RootNode;
            nodeRecentProjects.Tag = niRProjects;
            nodeRecentProjects.Expand();

            var projects = xm.RecentProjects.Projects;

            // Create datatable for recent project
            DataTable dataTable = new DataTable();

            DataColumn Name = new DataColumn("ProjectName");
            dataTable.Columns.Add(Name);
            DataColumn Path = new DataColumn("ProjectPath");
            dataTable.Columns.Add(Path);
            DataColumn TimeAccessed = new DataColumn("TimeAccessed");
            TimeAccessed.DataType = typeof(DateTime);
            dataTable.Columns.Add(TimeAccessed);
            // Populate table
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
                if (i < 10)
                {
                    TreeNode nodeProject = new TreeNode(project.ItemArray[0].ToString());
                    NodeInfo niProject = new NodeInfo();
                    niProject.NodeType = NodeType.ProjectNode;
                    niProject.Info = project.ItemArray[0].ToString();
                    nodeProject.Tag = niProject;
                    if (File.Exists(project.ItemArray[1].ToString()))
                    {
                        nodeRecentProjects.Nodes.Add(nodeProject);
                        i++;
                    }
                }
            }

            nodeAllProjects.Nodes.Add(nodeCurrentProject);
            nodeAllProjects.Nodes.Add(nodeRecentProjects);
            tvProjects.Nodes.Clear();
            tvProjects.Nodes.Add(nodeAllProjects);

        }
        /// <summary>
        /// Check where the right click is arrived and show the context menu accordingly
        /// </summary>
        /// <param name="nodeInfo"></param> Selecte node info
        /// <param name="node"></param> Selected node 
        /// <param name="x"></param> X coordinate of Clicked object to show the context menu near the click
        /// <param name="y"></param> Y coordinate of Clicked object to show the context menu near the click
        private void ShowTreeContextMenu(NodeInfo nodeInfo, TreeNode node, int x, int y)
        {
            var nodeType = (int)nodeInfo.NodeType;
            ResetContextMenu();
            if (!_LoggedIn)
            {
                switch (nodeType)
                {

                    case 1: // RootNode
                        {
                            break;
                        }

                    case 2: // ListNode

                        {
                            tvProjects.SelectedNode = node;
                            if ((node.Text.ToString() == "Remote I/O") || (node.Text.ToString() == "Expansion I/O"))
                            {
                                _devicetype = node.Text.ToString();
                                tsmAddDevice.Visible = true;

                                // tsmAddModbus.Visible = true;


                            }
                            else if ((node.Text.ToString() == "Resistance Lookup Table"))
                            {
                                //_devicetype = node.Text.ToString();
                                tsmAddResiTable.Visible = true;
                            }
                            else if ((node.Parent.Text.ToString() == "Resistance Lookup Table"))
                            {
                                tsmAddResiValues.Visible = true;
                                tsmEditResistanceTable.Visible = true;
                                tsmDeleteResistanceTable.Visible = true;
                            }
                            else
                            {
                                tsmDelete.Visible = (node.Parent.Text.ToString() == "Remote I/O" || node.Parent.Text.ToString() == "Expansion I/O") ? true : false;
                                if (node.Text == "User Defined Tags")
                                {
                                    tsmAddTag.Visible = true;
                                    tsmImportTags.Visible = true;
                                    tsmExportTags.Visible = true;
                                }
                                else if (node.Parent.Parent.Text.Equals("UDFB"))
                                {
                                    tsmAddTag.Visible = true;
                                }
                                tsmAddDevice.Visible = false;            //If SubNode is Selected
                            }
                            ctmMain.Show(tvProjects, new Point(x, y));
                            break;
                        }


                    case 3: // ProjectNode
                        {
                            break;
                        }

                    case 4: // CurrentProjectNode
                        {
                            break;
                        }

                    case 5: // BlockNode
                        {
                            tvProjects.SelectedNode = node;
                            if (nodeInfo.Info == "LogicBlock" || nodeInfo.Info == "HIBlock")
                            {
                                tsmAddBlock.Visible = true;
                                tsmImportLogicBlock.Text = "Import Logic Block";
                                tsmImportLogicBlock.Visible = true;
                            }
                            else if (nodeInfo.Info == "UserFunctionBlock")
                            {
                                tsmAddUDFB.Visible = true;
                                tsmImportLogicBlock.Text = "ImportUDFB";
                                tsmImportLogicBlock.Visible = true;
                            }
                            else if (nodeInfo.Info == "UDFB")
                            {
                                PerformTreeNodeActions(nodeInfo, node, node.Text);
                                tsmEditUDFB.Visible = true;
                                tsmDeleteUDFB.Visible = true;
                                tsmExportLogicBlock.Text = "ExportUDFB";
                                tsmExportLogicBlock.Visible = true;
                            }
                            else if (nodeInfo.Info == "Ladder" && !node.Text.Contains("Interrupt_Logic_Block"))
                            {
                                tsmDeleteBlock.Visible = true;
                                tsmRenameBlock.Visible = true;
                                tsmExportLogicBlock.Text = "Export Logic Block";
                                tsmExportLogicBlock.Visible = true;
                            }
                            else if (nodeInfo.Info == "Ladder" && node.Text.Contains("Interrupt_Logic_Block"))
                            {
                                tsmImportLogicBlock.Text = "Import Logic Block";
                                tsmImportLogicBlock.Visible = true;
                                tsmExportLogicBlock.Text = "Export Logic Block";
                                tsmExportLogicBlock.Visible = true;
                            }
                            else if (nodeInfo.Info == "UDFLadder" && node.Text.EndsWith("Logic"))
                            {
                                //tsmImportLogicBlock.Visible = true;
                                //tsmExportLogicBlock.Visible = true;
                            }
                            ctmMain.Show(tvProjects, new Point(x, y));
                            break;
                        }

                    case 6: // DeviceNode
                        {
                            tvProjects.SelectedNode = node;

                            // 🚫 Prevent right-click on "MODBUS RTU Master" if RS485 mode is Slave
                            if (node.Text == "MODBUS RTU Master" && xm.LoadedProject?.RS485Mode == "Slave")
                            {
                                MessageBox.Show("Cannot configure MODBUS RTU Master when RS485 mode is set to 'Slave'.", "Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            tsmAddTag.Visible = (node.Text == "User Defined Tag");
                            tsmRequestAddReq.Visible = (node.Text == "MODBUS TCP Server");
                            tsmAddSlave.Visible = (node.Text == "MODBUS TCP Client" || node.Text == "MODBUS RTU Master");
                            tsmDelete.Visible = (node.Text == "MODBUS RTU Master" || node.Text.Contains("MODBUS RTU Slave"));
                            addPublishBlockToolStripMenuItem.Visible = (node.Text == "MQTT Publish");
                            tsmAddDevice.Visible = (node.Text == "RS485");

                            this.CntxaddSusBlock.Visible = (node.Text == "MQTT Subscribe");
                            this.CntxAddMQTTForm.Visible = (node.Text == "MQTT");
                            ctmMain.Show(tvProjects, new Point(x, y));
                            break;
                        }
                    case 8: // BacnetNode
                        {
                            tvProjects.SelectedNode = node;
                            if (node.Text != "BACNET IP" && node.Text != "Hardware IO's" && node.Text != "File" && node.Text != "Device" && node.Text != "Network Port")
                            {
                                tsmAddObject.Visible = true;
                                ctmMain.Show(tvProjects, new Point(x, y));
                            }
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// Re arrange or hide context menu
        /// </summary>
        private void ResetContextMenu()
        {
            tsmRequestAddReq.Visible = false;
            tsmAddSlave.Visible = false;
            tsmAddBlock.Visible = false;
            tsmAddRemoteIO.Visible = false;
            tsmAddExpansionIO.Visible = false;
            tsmDelete.Visible = false;
            tsmDeleteBlock.Visible = false;
            tsmRenameBlock.Visible = false;
            tsmAddTag.Visible = false;
            tsmAddDevice.Visible = false;
            tsmAddUDFB.Visible = false;
            tsmEditUDFB.Visible = false;
            tsmDeleteUDFB.Visible = false;
            addPublishBlockToolStripMenuItem.Visible = false;
            CntxaddSusBlock.Visible = false;
            CntxAddMQTTForm.Visible = false;
            tsmImportTags.Visible = false;
            tsmExportTags.Visible = false;
            tsmImportLogicBlock.Visible = false;
            tsmExportLogicBlock.Visible = false;
            tsmAddObject.Visible = false;
            tsmDeleteKey.Visible = false;
            tsmAddResiTable.Visible = false;
            tsmAddResiValues.Visible = false;
            tsmDeleteResistanceTable.Visible = false;
            tsmEditResistanceTable.Visible = false;
            OnGridDataChanged();
        }
        /// <summary>
        /// Check what type of node is selected and do the needfull to delete that node and rearrrange things after that if required
        /// </summary>
        /// <param name="treeNode"></param> Selected tree node
        private void DeleteNode(TreeNode treeNode)
        {
            bool delete = false;
            if (treeNode.Text.ToString().Contains("MODBUSRTUMaster"))
            {
                var mainnode = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
                MODBUSRTUMaster_Slave rtuslave = new MODBUSRTUMaster_Slave();
                rtuslave = mainnode.Slaves.Where(d => d.Name == treeNode.Text.ToString()).FirstOrDefault();
                if (rtuslave != null)
                {
                    mainnode.Slaves.Remove(rtuslave);
                    xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "MODBUSRTUMaster");
                    xm.LoadedProject.Devices.Add(mainnode);
                    delete = true;
                }
            }
            else if (treeNode.Text.ToString().Contains("MODBUSTCPClient"))
            {
                var mainnode = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
                MODBUSTCPClient_Slave tcpslave = new MODBUSTCPClient_Slave();
                tcpslave = mainnode.Slaves.Where(d => d.Name == treeNode.Text.ToString()).FirstOrDefault();
                if (tcpslave != null)
                {
                    mainnode.Slaves.Remove(tcpslave);
                    xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "MODBUSTCPClient");
                    xm.LoadedProject.Devices.Add(mainnode);
                    delete = true;
                }
            }
            else if (treeNode.Text.ToString().Contains("MODBUSTCPServer"))
            {
                var mainnode = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
                MODBUSTCPServer_Request tcpreq = new MODBUSTCPServer_Request();
                tcpreq = mainnode.Requests.Where(d => d.Name == treeNode.Text.ToString()).FirstOrDefault();
                if (tcpreq != null)
                {
                    mainnode.Requests.Remove(tcpreq);
                    xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "MODBUSTCPServer");
                    xm.LoadedProject.Devices.Add(mainnode);
                    delete = true;
                }
            }
            else if (treeNode.Text.ToString().Contains("UDFB"))
            {
                string[] parts = treeNode.Text.Split('-');
                UDFBInfo udfbinfo = (UDFBInfo)XMPS.Instance.LoadedProject.UDFBInfo.Where(u => u.UDFBName == parts[0]).FirstOrDefault();
                XMPS.Instance.LoadedProject.UDFBInfo.Remove(udfbinfo);
                LadderEditorControl l1 = new LadderEditorControl();
                l1.ReScale();
                l1.Update();
                l1.Invalidate();
                ShowDefaultLogicalBlocks();
                delete = true;
            }
            else if (((NodeInfo)treeNode.Tag).Info == "Ladder")
            {
                if (xm.LoadedProject.Blocks.Count == 1)
                {
                    MessageBox.Show("Atleast one block is required in the project, can't delete this block", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var logicBlocks = (List<Block>)xm.LoadedProject.Blocks.Where(b => b.Type == "LogicBlock").ToList();
                Block blockToDelete = logicBlocks.Where(d => d.Name == treeNode.Text.ToString()).FirstOrDefault();
                if (blockToDelete != null)
                {
                    string Blockname = blockToDelete.Name.Replace("LadderForm#", "");
                    var checkinmain = xm.LoadedProject.MainLadderLogic.Where(R => R.Replace("'", "").Equals(blockToDelete.Name.Replace("LadderForm#", ""))).FirstOrDefault();
                    if (checkinmain != null && checkinmain.Count() > 0)
                    {
                        MessageBox.Show("This Block is already used in Main Ladder block remove it from Main Ladder and then try to delete this block", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    LadderWindow window = (LadderWindow)xm.LoadedScreens[xm.CurrentScreen];
                    List<LadderElement> allElements = window.getLadderEditor().getCanvas().getDesignView().Elements;
                    foreach (LadderElement rung in allElements)
                    {
                        if (rung.Elements.Count > 1)
                        {
                            window.DeleteItem(rung.Elements[0].getRoot());
                        }
                    }
                    window.getLadderEditor().getCanvas().getDesignView().Elements.Clear();
                    xm.LoadedProject.LogicRungs.RemoveAll(d => d.WindowName == $"LadderForm#{blockToDelete.Name}");
                    xm.LoadedProject.Blocks.RemoveAll(d => d.Name == blockToDelete.Name);
                    LadderEditorControl l1 = new LadderEditorControl();
                    l1.ReScale();
                    l1.Update();
                    l1.Invalidate();
                    delete = true;
                    ShowDefaultLogicalBlocks();
                    xm.LoadedScreens.Remove(xm.LoadedScreens.Where(d => d.Key.Equals($"LadderForm#{blockToDelete.Name}")).Select(d => d.Key).FirstOrDefault());
                }
            }
            else if (treeNode.Parent != null && (treeNode.Parent.ToString().Replace("TreeNode: ", "") == "Expansion I/O" || treeNode.Parent.ToString().Replace("TreeNode: ", "") == "Remote I/O"))
            {
                string DelModel = ((NodeInfo)treeNode.Tag).Info.ToString();
                string RetentiveAddress = "";
                /////Check if retentive addresses are used in selected model
                var ORetentive = (xm.LoadedProject.Tags.Where(d => d.Model == ((NodeInfo)treeNode.Tag).Info.ToString() && d.RetentiveAddress != null && d.RetentiveAddress != "")).OrderBy(x => x.RetentiveAddress);
                List<XMIOConfig> retentiveCountFromNode = new List<XMIOConfig>(ORetentive);
                if (ORetentive.Count() > 0)
                {
                    RetentiveAddress = ORetentive.AsEnumerable().Where(d => d.RetentiveAddress != null && d.RetentiveAddress != "").First().RetentiveAddress.ToString();
                }
                /////Get the First and Last Input and Output Address to be deleted
                int FirstOP = 0, FirstIP = 0, LastOP = 0, LastIP = 0;
                var OList = (xm.LoadedProject.Tags.Where(d => d.Label.ToString().Substring(1, 1) == "O" && !d.Label.Contains("_") && d.Model == ((NodeInfo)treeNode.Tag).Info.ToString()));
                var OTagList = (xm.LoadedProject.Tags.Where(d => !d.Label.Contains("_") && d.Model == ((NodeInfo)treeNode.Tag).Info.ToString()));
                var OTList = OTagList.AsEnumerable().Where(d => d.Label.ToString().Substring(1, 1) == "O" && !d.Label.Contains("_")).OrderBy(d => d.LogicalAddress);
                if (OTList.Count() > 0)
                {
                    FirstOP = OTList.AsEnumerable().Select(d => Convert.ToInt32(d.LogicalAddress.ToString().Substring(3, 3))).First();
                    LastOP = OTList.AsEnumerable().Select(d => Convert.ToInt32(d.LogicalAddress.ToString().Substring(3, 3))).Last();
                }
                var IList = (xm.LoadedProject.Tags.Where(d => d.Label.ToString().Length > 1 && d.Label.ToString().Substring(1, 1) == "I" /*&& !d.Label.Contains("_")*/));
                var IModel = (IList.AsEnumerable().Where(d => d.Model == ((NodeInfo)treeNode.Tag).Info.ToString()));
                if (IModel.Count() > 0)
                {
                    FirstIP = IModel.AsEnumerable().Select(d => Convert.ToInt32(d.LogicalAddress.ToString().Substring(3, 3))).First();
                    LastIP = IModel.AsEnumerable().Select(d => Convert.ToInt32(d.LogicalAddress.ToString().Substring(3, 3))).Last();
                }
                ////Check Model which lies after the selected Model
                List<String> LaterModels = new List<String> { };
                for (int i = 0; i < xm.LoadedProject.Tags.Count; i++)
                {

                    string LogicalAddress = xm.LoadedProject.Tags[i].LogicalAddress.ToString();
                    string actno = LogicalAddress.Substring(3, 3);
                    if (FirstIP > 0 && Convert.ToInt32(actno) > LastIP && LogicalAddress.StartsWith("I"))
                    {
                        if (!LaterModels.Contains(xm.LoadedProject.Tags[i].Model.ToString()))
                        {
                            LaterModels.Add(xm.LoadedProject.Tags[i].Model.ToString());
                        }
                    }
                    else if (FirstOP > 0 && Convert.ToInt32(actno) > LastOP && LogicalAddress.StartsWith("Q"))
                    {
                        if (!LaterModels.Contains(xm.LoadedProject.Tags[i].Model.ToString()))
                        {
                            LaterModels.Add(xm.LoadedProject.Tags[i].Model.ToString());
                        }
                    }
                }

                //// Delete selected Model
                xm.LoadedProject.Tags.RemoveAll(d => d.Model == ((NodeInfo)treeNode.Tag).Info.ToString());
                //// Rearrange all IO Logical Addresses which are falling after the selected Model
                for (int cnt = 0; cnt < LaterModels.Count; cnt++)
                {
                    string oldLogicalAddress = string.Empty;
                    string newLogicalAddress = string.Empty;

                    string selectedModel1 = LaterModels[cnt].ToString();
                    if (LastIP != 0 && FirstIP != 0)
                    {
                        foreach (var Tag in xm.LoadedProject.Tags.Where(d => d.Model == selectedModel1 && d.LogicalAddress.StartsWith("I") && d.LogicalAddress.Contains(".")))
                        {
                            oldLogicalAddress = Tag.LogicalAddress;
                            Tag.LogicalAddress = Tag.LogicalAddress.Substring(0, 2).ToString()
        + ":" + CommonFunctions.padding(((Convert.ToInt32(Tag.LogicalAddress.Substring(3, 3)) - ((LastIP + 1) - FirstIP))).ToString(), 3) + Tag.LogicalAddress.Substring(6, 3);
                            newLogicalAddress = Tag.LogicalAddress;
                            UpdateUpdateTagInfo(oldLogicalAddress, newLogicalAddress);
                        };

                    }
                    if (LastOP != 0 && FirstOP != 0)
                    {
                        foreach (var Tag in xm.LoadedProject.Tags.Where(d => d.Model == selectedModel1 && d.LogicalAddress.StartsWith("Q") && d.LogicalAddress.Contains(".")))
                        {
                            oldLogicalAddress = Tag.LogicalAddress;
                            Tag.LogicalAddress = Tag.LogicalAddress.Substring(0, 2).ToString()
        + ":" + CommonFunctions.padding(((Convert.ToInt32(Tag.LogicalAddress.Substring(3, 3)) - ((LastOP + 1) - FirstOP))).ToString(), 3) + Tag.LogicalAddress.Substring(6, 3);
                            newLogicalAddress = Tag.LogicalAddress;
                            UpdateUpdateTagInfo(oldLogicalAddress, newLogicalAddress);
                        };
                    }
                    if (LastIP != 0 && FirstIP != 0)
                    {
                        foreach (var Tag in xm.LoadedProject.Tags.Where(d => d.Model == selectedModel1 && d.LogicalAddress.StartsWith("I") && !d.LogicalAddress.Contains(".")))
                        {
                            oldLogicalAddress = Tag.LogicalAddress;
                            Tag.LogicalAddress = Tag.LogicalAddress.Substring(0, 2).ToString()
        + ":" + CommonFunctions.padding(((Convert.ToInt32(Tag.LogicalAddress.Substring(3, 3)) - ((LastIP + 1) - FirstIP))).ToString(), 3);
                            newLogicalAddress = Tag.LogicalAddress;
                            UpdateUpdateTagInfo(oldLogicalAddress, newLogicalAddress);
                        };
                    }
                    if (LastOP != 0 && FirstOP != 0)
                    {
                        foreach (var Tag in xm.LoadedProject.Tags.Where(d => d.Model == selectedModel1 && d.LogicalAddress.StartsWith("Q") && !d.LogicalAddress.Contains(".")))
                        {
                            oldLogicalAddress = Tag.LogicalAddress;
                            Tag.LogicalAddress = Tag.LogicalAddress.Substring(0, 2).ToString()
        + ":" + CommonFunctions.padding(((Convert.ToInt32(Tag.LogicalAddress.Substring(3, 3)) - ((LastOP + 1) - FirstOP))).ToString(), 3);
                            newLogicalAddress = Tag.LogicalAddress;
                            UpdateUpdateTagInfo(oldLogicalAddress, newLogicalAddress);
                        };
                    }

                }
                ///Manage Retentive Addresses
                if (RetentiveAddress != "") CommonFunctions.UpdatePrecedingRetentiveAddresses(RetentiveAddress, retentiveCountFromNode);
                delete = true;
            }
            if (delete)
            {
                tvProjects.Nodes.Remove(treeNode);
                xm.MarkProjectModified(true);
            }

        }

        private void UpdateUpdateTagInfo(string oldLogicalAddress, string newLogicalAddress)
        {
            BacNetIP bacNetIP = xm.LoadedProject.BacNetIP;
            if (bacNetIP != null)
            {
                var checkInAnalog = bacNetIP.AnalogIOValues.Where(t => t.LogicalAddress.Equals(oldLogicalAddress)).FirstOrDefault();
                var checkInBinary = bacNetIP.BinaryIOValues.Where(t => t.LogicalAddress.Equals(oldLogicalAddress)).FirstOrDefault();
                if (checkInAnalog != null)
                {
                    checkInAnalog.LogicalAddress = newLogicalAddress;
                }
                if (checkInBinary != null)
                {
                    checkInBinary.LogicalAddress = newLogicalAddress;
                }
            }
        }



        /// <summary>
        /// Rename Node of Ladder Block type
        /// </summary>
        /// <param name="treeNode"></param> selected node
        /// <param name="newName"></param> updated name
        private void RenameNode(TreeNode treeNode, string newName)
        {
            bool rename = false;
            if (((NodeInfo)treeNode.Tag).Info == "Ladder")
            {
                var oldName = treeNode.Text;
                var file = xm.CurrentProjectData.ProjectPath.ToString().Replace(xm.CurrentProjectData.ProjectName.ToString(), string.Empty);

                treeNode.Text = newName;
                var logicBlocks = (List<Block>)xm.LoadedProject.Blocks.Where(b => b.Type == "LogicBlock").ToList();
                var flagNameIsPresent = xm.LoadedProject.Blocks.Where(b => b.Name == $"{newName}").Any() ? true : false;

                // Create new block
                Block newBlk = new Block();
                newBlk.Name = newName;
                newBlk.Type = "LogicBlock";

                // Check if name already exist
                if (flagNameIsPresent == false)
                {

                    var ApplicationRecs = xm.LoadedProject.LogicRungs.Where(d => d.WindowName.StartsWith($"LadderForm#{oldName}")).OrderBy(o => o.LineNumber);
                    foreach (ApplicationRung ApRec in ApplicationRecs)
                    {
                        if (ApRec.WindowName == $"LadderForm#{oldName}")
                        {
                            string str = ApRec.WindowName.ToString().Replace($"LadderForm#{oldName}", $"LadderForm#{newName}");
                            ApRec.WindowName = str;
                        }
                    }

                    xm.LoadedProject.Blocks.RemoveAll(b => b.Type == "LogicBlock");
                    if (logicBlocks != null)
                    {
                        foreach (Block blk in logicBlocks)
                        {
                            if (blk.Name == oldName)
                            {

                                xm.LoadedProject.Blocks.Add(newBlk);
                            }
                            else
                            {
                                xm.LoadedProject.Blocks.Add(blk);
                            }
                        }

                    }

                    // Rename in MainLadderLogic
                    if (xm.LoadedProject.MainLadderLogic.Any())
                    {
                        for (int index = 0; index < xm.LoadedProject.MainLadderLogic.Count; index++)
                        {
                            if (xm.LoadedProject.MainLadderLogic[index] == oldName)
                            {
                                xm.LoadedProject.MainLadderLogic[index] = newName;
                            }

                            if (xm.LoadedProject.MainLadderLogic[index].Contains("["))
                            {
                                int startIndex = xm.LoadedProject.MainLadderLogic[index].IndexOf("[") + 1;
                                int endIndex = xm.LoadedProject.MainLadderLogic[index].IndexOf("]");
                                string logicBlkName = xm.LoadedProject.MainLadderLogic[index].Substring(startIndex, endIndex - startIndex);

                                if (logicBlkName == oldName)
                                {
                                    xm.LoadedProject.MainLadderLogic[index] = xm.LoadedProject.MainLadderLogic[index].Replace(logicBlkName, newName);
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Give warning (Block name already exist)
                    tssStatusLabel_msg("Block Name already used", 3000);
                    xm.LoadedProject.Blocks.RemoveAll(b => b.Type == "LogicBlock");
                    if (logicBlocks != null)
                    {
                        foreach (Block blk in logicBlocks)
                        {
                            if (blk.Name == oldName)
                            {

                                xm.LoadedProject.Blocks.Add(blk);
                            }
                            else
                            {
                                xm.LoadedProject.Blocks.Add(blk);
                            }
                        }
                    }
                    return;
                }
                rename = true;
            }
            if (rename)
            {
                xm.MarkProjectModified(true);
            }
        }

        #endregion

        #region Ladder Function Tree

        public void AddBlocksData()
        {
            this.tvBlocks.Nodes.Add("Instructions", "Instructions", 0, 0);
            this.BooleansBlockData();
            this.ArithmeticBlockData();
            this.BitShiftBlockData();
            this.LimitBlockData();
            this.ComparisonsBlockData();
            this.EdgeDetectorBlockData();
            this.CountersBlockData();
            this.TimersTONBlockData();
            this.TimersTOFFBlockData();
            this.TimersTPBlockData();
            this.FlipFlopBlockData();
        }
        public void EdgeDetectorBlockData()
        {
            int count = 6;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Edge Detector", "Edge Detector", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("Rising Edge", "Rising Edge", 0, 0);
            this.AddBlockInfoTagData(this.tvBlocks, count, "Rising Edge");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("Falling Edge", "Falling Edge", 0, 0);
            this.AddBlockInfoTagData(this.tvBlocks, count, "Falling Edge");
        }
        public void TimersTONBlockData()
        {
            int count = 8;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Timer TON", "Timer TON", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("0.01s TON", "0.01s TON", 0, 0);
            this.AddBlockInfoTagData(this.tvBlocks, count, "0.01s TON");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("0.1s TON", "0.1s TON", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "0.1s TON");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("1s TON", "1s TON", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "1s TON");
        }

        public void TimersTOFFBlockData()
        {
            int count = 9;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Timer TOFF", "Timer TOFF", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("0.01s TOFF", "0.01s TOFF", 0, 0);
            this.AddBlockInfoTagData(this.tvBlocks, count, "0.01s TOFF");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("0.1s TOFF", "0.1s TOFF", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "0.1s TOFF");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("1s TOFF", "1s TOFF", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "1s TOFF");
        }

        public void TimersTPBlockData()
        {
            int count = 10;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Timer TP", "Timer TP", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("0.01s TP", "0.01s TP", 0, 0);
            this.AddBlockInfoTagData(this.tvBlocks, count, "0.01s TP");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("0.1s TP", "0.1s TP", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "0.1s TP");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("1s TP", "1s TP", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "1s TP");
        }

        public void LimitBlockData()
        {
            int count = 4;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("LIMIT", "LIMIT", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("LIMIT", "LIMIT", 0, 0);
            this.AddBlockInfoTagData(this.tvBlocks, count, "LIMIT");
        }
        public void FlipFlopBlockData()
        {
            int count = 11;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Flipflop", "Flipflop", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("RS", "RS", 0, 0);
            this.AddBlockInfoTagData(this.tvBlocks, count, "RS");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SR", "SR", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SR");
        }

        public void AddDefaultTags()
        {
            xm.LoadedProject.Tags.AddRange(CommonFunctions.GetSystemTagList(xm.PlcModel));
            List<XMIOConfig> data = xm.LoadedProject.Tags.OrderBy(r => r.Model).ThenBy(r => r.LogicalAddress).OrderBy(r => r.Key).ToList();
            List<XMIOConfig> separatedData = data.Where(r => r.IoList.ToString() == "OnBoardIO").ToList();
            XMIOConfig firstElement = separatedData.Count > 0 ? separatedData.FirstOrDefault() : null;
            if (firstElement.Model == "XM-14-DT-HIO" || firstElement.Model == "XM-14-DT-HIO-E")
            {
                foreach (XMIOConfig tag in separatedData)
                {
                    if (tag.Mode == null && tag.Type == Core.Types.IOType.DigitalInput && (tag.Label == "DI0" || tag.Label == "DI1" || tag.Label == "DI2" || tag.Label == "DI3" || tag.Label == "DI4" || tag.Label == "DI5" || tag.Label == "DI6" || tag.Label == "DI7"))
                    {
                        tag.Mode = "Digital Input";
                    }
                    else if (tag.Mode == null && tag.Type == Core.Types.IOType.DigitalOutput && (tag.Label == "DO0" || tag.Label == "DO1"))
                    {
                        tag.Mode = "Digital Output";
                    }
                }
            }
            //for adding default BacNet Objects
            List<XMIOConfig> tags = xm.LoadedProject.Tags.Where(t => t.IoList == IOListType.OnBoardIO).ToList();
            if (xm.LoadedProject.PlcModel.StartsWith("XBLD"))
            {
                AddOnBoardBacNetObjects(tags);
            }
            //Adding input filter for the OnBoardIO
            xm.LoadedProject.Tags.Where(T => T.Type == IOType.DigitalInput && T.IoList == IOListType.OnBoardIO).ToList()
            .ForEach(T =>
            {
                // Only set if not already configured
                if (!T.IsEnableInputFilter)
                {
                    T.IsEnableInputFilter = true;
                    T.InpuFilterValue = "10";
                }
            });
            xm.LoadedProject.IsDigitalFilterApply = true;
            //if (xm.LoadedProject.PlcModel == "XM-14-DT-HIO" || xm.LoadedProject.PlcModel == "XM-14-DT-HIO-E")
            //{
            //    xm.LoadedProject.Tags.Where(T => T.Type == IOType.DigitalInput && T.IoList == IOListType.OnBoardIO && (T.Label == "DI5" || T.Label == "DI7")).ToList()
            //    .ForEach(T =>
            //    {
            //        T.IsEnableInputFilter = false;
            //        T.InpuFilterValue = string.Empty;
            //    });
            //}
        }
        /// <summary>
        /// Get list of system tags from .plc file and return that list to the call of function
        /// </summary>
        /// <returns></returns>

        private void AddOnBoardBacNetObjects(List<XMIOConfig> tags)
        {
            BacNetIP bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            if (bacNetIP == null) bacNetIP = new BacNetIP();

            int binaryInputCounter = 0;
            int binaryOutputCounter = 0;
            int analogInputCounter = 0;
            int analogOutputCounter = 0;
            var existingBinaryAddresses = new HashSet<string>(bacNetIP.BinaryIOValues.Select(b => b.LogicalAddress));
            var existingAnalogAddresses = new HashSet<string>(bacNetIP.AnalogIOValues.Select(b => b.LogicalAddress));

            foreach (XMIOConfig tag in tags)
            {
                string logicalAddress = tag.LogicalAddress;

                if (tag.IoList == Core.Types.IOListType.ExpansionIO || tag.IoList == Core.Types.IOListType.RemoteIO)
                    continue;

                if (logicalAddress.Contains(".") && !tag.Label.EndsWith("_OR") && !tag.Label.EndsWith("_OL"))
                {
                    BacNetObjectHelper.AddBinaryIOV(ref bacNetIP, logicalAddress, tag.Tag.ToString(), existingBinaryAddresses, ref binaryInputCounter, ref binaryOutputCounter);
                }
                else if ((tag.Type == Core.Types.IOType.AnalogInput || tag.Type == Core.Types.IOType.AnalogOutput) && !tag.Label.EndsWith("_OR") && !tag.Label.EndsWith("_OL"))
                {
                    BacNetObjectHelper.AddAnalogIOV(ref bacNetIP, logicalAddress, tag.Tag.ToString(), existingAnalogAddresses, ref analogInputCounter, ref analogOutputCounter, tag.IoList.ToString());
                }

                BacNetObjectHelper.IncrementCounters(tag.Type, ref binaryInputCounter, ref binaryOutputCounter, ref analogInputCounter, ref analogOutputCounter);
            }
            XMPS.Instance.LoadedProject.BacNetIP = bacNetIP;
            //Creating Device Object at the time of creating new project.
            if (bacNetIP.Device.ObjectType == null)
            {
                bacNetIP.Device.ObjectIdentifier = "Device:2000";
                bacNetIP.Device.ObjectName = "Device";
                bacNetIP.Device.InstanceNumber = "2000";
                bacNetIP.Device.ObjectType = "8:Device";
                bacNetIP.Device.APDUSegmentTimout = "5000";
                bacNetIP.Device.APDUTimeout = "6000";
                bacNetIP.Device.APDURetries = "3";
                bacNetIP.Device.Location = "";
                bacNetIP.Device.IsEnable = true;
            }
            // ✅ Creating Network Port Object at the time of creating new project
            if (bacNetIP.NetworkPort.ObjectType == null)
            {
                bacNetIP.NetworkPort.ObjectIdentifier = "NetworkPort:0";
                bacNetIP.NetworkPort.ObjectName = "Network_port";
                bacNetIP.NetworkPort.InstanceNumber = "0";
                bacNetIP.NetworkPort.ObjectType = "56:NetworkPort";
                bacNetIP.NetworkPort.NetworkType = "IPv4";
                bacNetIP.NetworkPort.IsEnable = true;
            }
        }

        public void BitShiftBlockData()
        {
            int count = 3;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Bit Shift", "Bit Shift", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROL", "ROL", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROL");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROR", "ROR", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROR");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SHL", "SHL", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SHL");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SHR", "SHR", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SHR");

        }
        public void AddIECInstructionInTreeNode()
        {
            DataSet dataSet = new DataSet();
            try
            {
                try
                {
                    dataSet.ReadXml("InstructionGrp.xml");
                    DataRow[] dataRowArray = dataSet.Tables["Group"].Select();
                    if (this.tvBlocks.Nodes != null)
                    {
                        this.tvBlocks.Nodes.Clear();
                    }
                    int num = 0;
                    DataRow[] dataRowArray1 = dataRowArray;
                    for (int i = 0; i < (int)dataRowArray1.Length; i++)
                    {
                        DataRow dataRow = dataRowArray1[i];
                        string str = Convert.ToString(dataRow["InstnGrp"]);
                        TreeNode treeNode = this.tvBlocks.Nodes.Add(str);
                        if (num > 0)
                        {
                            this.AddInstructionSubNode(treeNode, dataSet, str, num);
                        }
                        if ((treeNode.Nodes.Count != 0 ? true : num == 0))
                        {
                            num++;
                        }
                        else
                        {
                            this.tvBlocks.Nodes.Remove(treeNode);
                        }
                    }
                }
                catch (FileNotFoundException fileNotFoundException)
                {
                    MessageBox.Show("Instruction list file not found " + fileNotFoundException.Message);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error occured in adding Instruction " + exception.Message);
                }
            }
            finally
            {
                if (dataSet != null)
                {
                    ((IDisposable)dataSet).Dispose();
                }
            }
        }

        private void AddInstructionSubNode(TreeNode objNode, DataSet objSet, string GrpName, int cnt)
        {
            string str = string.Concat("Model914");
            DataTable item = objSet.Tables["SubNode"];
            try
            {
                if (item.Columns.Contains(str))
                {
                    string[] grpName = new string[] { "InstnGrp='", GrpName, "' and ", str, "= True" };
                    DataRow[] dataRowArray = item.Select(string.Concat(grpName));
                    for (int i = 0; i < (int)dataRowArray.Length; i++)
                    {
                        DataRow dataRow = dataRowArray[i];
                        objNode.Nodes.Add(Convert.ToString(dataRow["Name"]));
                    }
                }
            }
            finally
            {
                if (item != null)
                {
                    ((IDisposable)item).Dispose();
                }
            }
        }
        public void UDFBlockData()
        {
            int count = 0;

            var udfLocalBlocks = (List<Block>)xm.LoadedProject.Blocks.Where(b => b.Type == "UserFunctionBlock").ToList();
            if (udfLocalBlocks != null)
            {
                foreach (var block in udfLocalBlocks)
                {
                    this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add(block.Name, block.Name, 3, 3);
                    this.AddBlockInfoTagData(this.tvBlocks, count, block.Name);
                }
            }
        }
        public void ArithmeticBlockData()
        {
            int count = 2;
            this.tvBlocks.Nodes.Add("Arithmetic", "Arithmetic", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ADD", "ADD", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ADD");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SUB", "SUB", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SUB");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("MUL", "MUL", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "MUL");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DIV", "DIV", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DIV");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("MOD", "MOD", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "MOD");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("MOV", "MOV", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "MOV");
        }
        public void BooleansBlockData()
        {
            int count = 1;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Logical", "Logical", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("AND", "AND", 0, 0);
            this.AddBlockInfoTagData(this.tvBlocks, count, "AND");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("OR", "OR", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "OR");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("XOR", "XOR", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "XOR");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NOT", "NOT", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NOT");
        }

        public void ClockBlockData()
        {
            int count = 8;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("DAY_TIME (*Get current day and time (STRING)*)", "DAY_TIME (*Get current day and time (STRING)*)", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTAT (*Pulse at a date/time*)", "DTAT (*Pulse at a date/time*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTAT (*Pulse at a date/time*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTCURDATE (*Get current date stamp*)", "DTCURDATE (*Get current date stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTCURDATE (*Get current date stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTCURDATETIME (*Get current date and time*)", "DTCURDATETIME (*Get current date and time*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTCURDATETIME (*Get current date and time*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTCURTIME (*Get current time stamp*)", "DTCURTIME (*Get current time stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTCURTIME (*Get current time stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTDAY (*Get day from date stamp*)", "DTDAY (*Get day from date stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTDAY (*Get day from date stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTEVERY (*Periodical pulse*)", "DTEVERY (*Periodical pulse*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTEVERY (*Periodical pulse*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTFORMAT (*Format current date*)", "DTFORMAT (*Format current date*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTFORMAT (*Format current date*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTHOUR (*Get hours from time stamp*)", "DTHOUR (*Get hours from time stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTHOUR (*Get hours from time stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTMONTH (*Get month from date stamp*)", "DTMONTH (*Get month from date stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTMONTH (*Get month from date stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTMS (*Get milliseconds from time stamp*)", "DTMS (*Get milliseconds from time stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTMS (*Get milliseconds from time stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTSEC (*Get seconds from time stamp*)", "DTSEC (*Get seconds from time stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTSEC (*Get seconds from time stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTYEAR (*Get year from date stamp*)", "DTYEAR (*Get year from date stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTYEAR (*Get year from date stamp*)");
        }

        public void ComparisonsBlockData()
        {
            int count = 5;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Compare", "Compare", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("GT", "GT", 5, 5);
            this.AddBlockInfoTagData(this.tvBlocks, count, "GT");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("GE", "GE", 5, 5);
            this.AddBlockInfoTagData(this.tvBlocks, count, "GE");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("LT", "LT", 5, 5);
            this.AddBlockInfoTagData(this.tvBlocks, count, "LT");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("LE", "LE", 5, 5);
            this.AddBlockInfoTagData(this.tvBlocks, count, "LE");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("EQ", "EQ", 5, 5);
            this.AddBlockInfoTagData(this.tvBlocks, count, "EQ");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NE", "NE", 5, 5);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NE");
        }

        public void ConversionsBlockData()
        {
            int count = 4;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("ANY_TO_BOOL (*Convert to boolean*)", "ANY_TO_BOOL (*Convert to boolean*)", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_DINT (*Convert to 32 bit integer*)", "ANY_TO_DINT (*Convert to 32 bit integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_DINT (*Convert to 32 bit integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_INT (*Convert to 16 bit integer*)", "ANY_TO_INT (*Convert to 16 bit integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_INT (*Convert to 16 bit integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_LINT (*Convert to long integer*)", "ANY_TO_LINT (*Convert to long integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_LINT (*Convert to long integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_LREAL (*Convert to double precision real*)", "ANY_TO_LREAL (*Convert to double precision real*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_LREAL (*Convert to double precision real*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_REAL (*Convert to real*)", "ANY_TO_REAL (*Convert to real*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_REAL (*Convert to real*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_SINT (*Convert to small integer*)", "ANY_TO_SINT (*Convert to small integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_SINT (*Convert to small integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_STRING (*Convert to string*)", "ANY_TO_STRING (*Convert to string*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_STRING (*Convert to string*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_TIME (*Convert to time*)", "ANY_TO_TIME (*Convert to time*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_TIME (*Convert to time*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_UDINT (*Convert to unsigned 32 bit integer*)", "ANY_TO_UDINT (*Convert to unsigned 32 bit integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_UDINT (*Convert to unsigned 32 bit integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_UINT (*Convert to unsigned 16 bit integer*)", "ANY_TO_UINT (*Convert to unsigned 16 bit integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_UINT (*Convert to unsigned 16 bit integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_USINT (*Convert to unsigned small integer*)", "ANY_TO_USINT (*Convert to unsigned small integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_USINT (*Convert to unsigned small integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("BCD_TO_BIN (*BCD to binary conversion*)", "BCD_TO_BIN (*BCD to binary conversion*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "BCD_TO_BIN (*BCD to binary conversion*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("BIN_TO_BCD (*binary to BCD conversion*)", "BIN_TO_BCD (*binary to BCD conversion*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "BIN_TO_BCD (*binary to BCD conversion*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NUM_TO_STRING (*Convert number to string*)", "NUM_TO_STRING (*Convert number to string*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NUM_TO_STRING (*Convert number to string*)");
        }

        public void CountersBlockData()
        {
            int count = 7;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Counter", "Counter", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("CTU", "CTU", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "CTU");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("CTD", "CTD", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "CTD");
        }

        public void AddBlockInfoTagData(TreeView objTreeView, int Count, string BlockName)
        {
        }

        #endregion

        private void AddNewUserDefinedTag()
        {
            TreeNode IONode = tvProjects.SelectedNode;
            NodeInfo niRemoteIONode = (NodeInfo)IONode.Tag;
            if (niRemoteIONode.Info == "UDFTags")
            {
                string tagNodeText = IONode.Text;
                string normalizedNode = tagNodeText.EndsWith(" Tags", StringComparison.OrdinalIgnoreCase)? tagNodeText.Substring(0, tagNodeText.Length - 5).Trim(): tagNodeText;

                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string libraryRoot = Path.Combine(basePath, "MessungSystems", "XMPS2000", "Library");
                string modelFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD") ? "XBLDLibraries" : "XMLibraries";
                string libraryPath = Path.Combine(libraryRoot, modelFolder);
                List<string> udfbNames = XMPS.Instance.LoadedProject.UDFBInfo.Select(u => u.UDFBName).ToList();

                var fileNames = Directory.Exists(libraryPath) ? Directory.GetFiles(libraryPath, "*.csv")
                    .Select(Path.GetFileNameWithoutExtension)
                    .Select(name => name.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase)
                            ? name.Substring(0, name.Length - 6).Trim(): name): Enumerable.Empty<string>();

                bool isUdfbMatch = fileNames.Any(fileName =>
                    fileName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase) && udfbNames.Any(udfbName => udfbName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase)));

                if (isUdfbMatch)
                {
                    string savedChoice = XMPS.Instance.LoadedProject.GetUDFBChoice(normalizedNode);
                    string savedLocalCopyName = null;

                    if (!string.IsNullOrEmpty(savedChoice) && savedChoice.StartsWith("CreateLocalCopy:"))
                    {
                        savedLocalCopyName = savedChoice.Substring("CreateLocalCopy:".Length);
                    }

                    bool localCopyWithDifferentNameExists = !string.IsNullOrEmpty(savedLocalCopyName) &&
                        !savedLocalCopyName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase) &&
                        XMPS.Instance.LoadedProject.Blocks.Any(b => b.Type == "UserFunctionBlock" &&b.Name.Equals(savedLocalCopyName, StringComparison.OrdinalIgnoreCase));

                    if (localCopyWithDifferentNameExists)
                    {
                        using (var optionsForm = new UDFBEditOptionsForm(normalizedNode))
                        {
                            if (optionsForm.ShowDialog() == DialogResult.OK)
                            {
                                if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                                {
                                    XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "EditMainFile");
                                }
                                else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                                {
                                    string newLocalCopyName = optionsForm.LocalCopyName;
                                    XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "CreateLocalCopy:" + newLocalCopyName);
                                    CreateLocalCopyAndAddTag(normalizedNode, newLocalCopyName);
                                    return;
                                }
                            }
                            else
                            {
                                return; 
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(savedChoice))
                    {
                        if (savedChoice == "EditMainFile")
                        {
                            // Continue to add tag
                        }
                        else if (savedChoice.StartsWith("CreateLocalCopy:"))
                        {
                            string existingLocalCopyName = savedChoice.Substring("CreateLocalCopy:".Length);
                            bool specificLocalCopyExists = XMPS.Instance.LoadedProject.Blocks.Any(b =>
                                b.Type == "UserFunctionBlock" && b.Name.Equals(existingLocalCopyName, StringComparison.OrdinalIgnoreCase));

                            if (specificLocalCopyExists)
                            {
                                // The local copy exists, continue to add tag
                            }
                            else
                            {
                                XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "");

                                using (var optionsForm = new UDFBEditOptionsForm(normalizedNode))
                                {
                                    if (optionsForm.ShowDialog() == DialogResult.OK)
                                    {
                                        if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                                        {
                                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "EditMainFile");
                                        }
                                        else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                                        {
                                            string recreatedLocalCopyName = optionsForm.LocalCopyName;
                                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "CreateLocalCopy:" + recreatedLocalCopyName);
                                            CreateLocalCopyAndAddTag(normalizedNode, recreatedLocalCopyName);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        return; 
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        using (var optionsForm = new UDFBEditOptionsForm(normalizedNode))
                        {
                            if (optionsForm.ShowDialog() == DialogResult.OK)
                            {
                                if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                                {
                                    XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "EditMainFile");                                    
                                }
                                else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                                {
                                    string initialLocalCopyName = optionsForm.LocalCopyName;
                                    XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "CreateLocalCopy:" + initialLocalCopyName);
                                    CreateLocalCopyAndAddTag(normalizedNode, initialLocalCopyName);
                                    return;
                                }
                            }
                            else
                            {
                                return; 
                            }
                        }
                    }
                }
            }

        // Continue with normal tag addition
        repeat:
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tempForm.Text = "Add User Defined IO Model";
            TagsUserControl userControl = new TagsUserControl("", "$", tvProjects.SelectedNode.Text.ToString(),
                tvProjects.SelectedNode.Text.ToString().Equals("Analog Value") ? "Real" :
                tvProjects.SelectedNode.Text.ToString().Equals("Multistate Value") ? "Word" : "Bool");
            tempForm.Height = userControl.Height + 25;
            tempForm.Width = userControl.Width;
            tempForm.Controls.Add(userControl);
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.Cancel || (result == DialogResult.OK && !tvProjects.SelectedNode.Text.ToString().EndsWith("Tags")))
            {
                PerformTreeNodeActions(niRemoteIONode, IONode, niRemoteIONode.Info.ToString());
            }
            else if (result == DialogResult.OK)
            {
                PerformTreeNodeActions(niRemoteIONode, IONode, niRemoteIONode.Info.ToString());
                goto repeat;
            }
        }

        private void CreateLocalCopyAndAddTag(string originalName, string localCopyName)
        {
            try
            {
                TreeNode tagsNode = tvProjects.SelectedNode;
                if (tagsNode == null || !tagsNode.Text.EndsWith(" Tags"))
                {
                    MessageBox.Show("Please select a UDFB Tags node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                TreeNode udfbNode = tagsNode.Parent;
                if (udfbNode == null || udfbNode.Parent == null)
                {
                    MessageBox.Show("Unable to locate the UDFB node in TreeView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                TreeNode udfbParentNode = udfbNode.Parent;
                string logicBlockName = originalName + " Logic";
                int _blockIndex = XMPS.Instance.LoadedProject.Blocks.FindIndex(d => d.Name == logicBlockName);
                List<string> rungs = new List<string>();

                if (_blockIndex >= 0)
                {
                    rungs = new List<string>(XMPS.Instance.LoadedProject.Blocks[_blockIndex].Elements);
                }
                UDFBInfo originalUDFBInfo = XMPS.Instance.LoadedProject.UDFBInfo.FirstOrDefault(t => t.UDFBName.Equals(originalName));
                if (originalUDFBInfo == null)
                {
                    MessageBox.Show($"Unable to find UDFB '{originalName}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UDFBInfo newUDFBInfo = new UDFBInfo();
                newUDFBInfo.UDFBName = localCopyName;
                newUDFBInfo.Inputs = originalUDFBInfo.Inputs;
                newUDFBInfo.Outputs = originalUDFBInfo.Outputs;

                newUDFBInfo.UDFBlocks = new List<UserDefinedFunctionBlock>();
                foreach (var udfBlock in originalUDFBInfo.UDFBlocks)
                {
                    UserDefinedFunctionBlock newBlock = new UserDefinedFunctionBlock();
                    newBlock.Type = udfBlock.Type;
                    newBlock.DataType = udfBlock.DataType;
                    newBlock.Text = udfBlock.Text;
                    newBlock.Name = localCopyName;
                    newUDFBInfo.UDFBlocks.Add(newBlock);
                }

                XMPS.Instance.LoadedProject.UDFBInfo.Add(newUDFBInfo);
                XMPS.Instance.LoadedProject.UDFBInfo.RemoveAll(t => t.UDFBName.Equals(originalName));
                udfbParentNode.Nodes.Remove(udfbNode);
                XMPS.Instance.LoadedProject.Blocks.RemoveAll(t => (t.Type.Equals("UDFB") || t.Type.Equals("UserFunctionBlock")) && t.Name.Equals(originalName + " Logic"));
                XMPS.Instance.LoadedProject.Blocks.RemoveAll(t => (t.Type.Equals("UDFB") || t.Type.Equals("UserFunctionBlock")) && t.Name.Equals(originalName));

                TreeNode tnUDFLocalBlock = new TreeNode(localCopyName);
                NodeInfo nitnUDFLocalBlock = new NodeInfo();
                nitnUDFLocalBlock.NodeType = NodeType.BlockNode;
                nitnUDFLocalBlock.Info = "UDFB";
                tnUDFLocalBlock.Tag = nitnUDFLocalBlock;

                TreeNode tnUDFTag = new TreeNode(localCopyName + " Tags");
                NodeInfo nitnUDFTag = new NodeInfo();
                nitnUDFTag.NodeType = NodeType.ListNode;
                nitnUDFTag.Info = "UDFTags";
                tnUDFTag.Tag = nitnUDFTag;
                tnUDFLocalBlock.Nodes.Add(tnUDFTag);

                TreeNode tnUDFLogic = new TreeNode(localCopyName + " Logic");
                NodeInfo nitnUDFLogic = new NodeInfo();
                nitnUDFLogic.NodeType = NodeType.BlockNode;
                nitnUDFLogic.Info = "UDFLadder";
                tnUDFLogic.Tag = nitnUDFLogic;
                tnUDFLocalBlock.Nodes.Add(tnUDFLogic);

                udfbParentNode.Nodes.Add(tnUDFLocalBlock);

                Block blk = new Block();
                blk.Name = localCopyName;
                blk.Type = "UserFunctionBlock";
                XMPS.Instance.LoadedProject.Blocks.Add(blk);

                Block blk1 = new Block();
                blk1.Name = localCopyName + " Logic";
                blk1.Type = "UDFB";
                XMPS.Instance.LoadedProject.Blocks.Add(blk1);

                int _blockIndexNew = XMPS.Instance.LoadedProject.Blocks.FindIndex(d => d.Name == localCopyName + " Logic");
                if (_blockIndexNew >= 0)
                {
                    XMPS.Instance.LoadedProject.Blocks[_blockIndexNew].Elements.Clear();
                    XMPS.Instance.LoadedProject.Blocks[_blockIndexNew].Elements.AddRange(rungs);
                }

                tvProjects.SelectedNode = tnUDFTag;
                tnUDFLocalBlock.Expand();

                save(false);
                ShowAddTagDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating local copy: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowAddTagDialog()
        {
        repeat:
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tempForm.Text = "Add User Defined IO Model";
            TreeNode IONode = tvProjects.SelectedNode;
            NodeInfo niRemoteIONode = (NodeInfo)IONode.Tag;
            TagsUserControl userControl = new TagsUserControl("", "$", tvProjects.SelectedNode.Text.ToString(), tvProjects.SelectedNode.Text.ToString().Equals("Analog Value") ? "Real" : tvProjects.SelectedNode.Text.ToString().Equals("Multistate Value") ? "Word" : "Bool");
            tempForm.Height = userControl.Height + 25;
            tempForm.Width = userControl.Width;
            tempForm.Controls.Add(userControl);
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.Cancel || (result == DialogResult.OK && !tvProjects.SelectedNode.Text.ToString().EndsWith("Tags")))
            {
                PerformTreeNodeActions(niRemoteIONode, IONode, niRemoteIONode.Info.ToString());
            }
            else if (result == DialogResult.OK)
            {
                PerformTreeNodeActions(niRemoteIONode, IONode, niRemoteIONode.Info.ToString());
                goto repeat;
            }
        }



        private void LoginToPLC()
        {
            try
            {
                RefreshGridFormIfEditing("MODBUSRTUSlavesForm");
                if (!ValidateAndInitializeProject())
                {
                    return;
                }
                if (!ValidateBacnetObjects())
                {
                    return;
                }
                if (xm.CurrentScreen.Contains("TaskConfigForm"))
                {
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.Name == "frmTaskConfiguration")
                        {
                            form.Visible = false;
                            form.Hide();
                        }
                    }
                }
                if (xm.LoadedProject.MainLadderLogic.Count() > 0)
                {
                    // Disable CVX buttons on login
                    {
                        strpBtnPaste.Enabled = false;
                        strpBtnCut.Enabled = false;
                        strpBtnCopy.Enabled = false;
                        strpBtnUndo.Enabled = false;
                        strpBtnRedo.Enabled = false;
                    }

                    progressBar1.Visible = true;
                    progressBar1.Value = 10;
                    progressBar1.Update();
                    PLCCommunications pLCCommunications = new PLCCommunications();
                    this.Cursor = Cursors.WaitCursor;
                    //Message Box form asking with download or withoutDownload
                    ///<Comment Custom Dialog for Plc Synchronization>
                    ///</Comment>
                    Compile();
                    if (xm.errorInCompiler)
                    {
                        MessageBox.Show("Resolve compilation errors before continue", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        progressBar1.Value = 100;
                        progressBar1.Visible = false;

                        strpBtnPaste.Enabled = true;
                        strpBtnCut.Enabled = true;
                        strpBtnCopy.Enabled = true;
                        strpBtnUndo.Enabled = true;
                        strpBtnRedo.Enabled = true;
                        StatusShow(false);
                        return;
                    }
                    pLCCommunications.Checkconnection(5);
                    Byte[] response = pLCCommunications.PLCSyncLogin();
                    if (response.Length < 3)
                    {
                        if (response.Length == 1 && response[0] == 255)
                        {
                            string errmsg = XMPS2000.CommonFunctions.GetEasyConnection(xm._connectedIPAddress);
                            MessageBox.Show(errmsg,
                                "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ;
                            tssStatusLabel_show(errmsg, "Red");
                            tssStatusLabel_show(string.Empty, "control");
                        }
                        else
                        {
                            tssStatusLabel_show("Invalid response", "Red");
                            MessageBox.Show("Invalid response", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tssStatusLabel_show(string.Empty, "control");
                        }
                        this.Cursor = Cursors.Default;
                        progressBar1.Value = 100;
                        progressBar1.Visible = false;

                        strpBtnPaste.Enabled = true;
                        strpBtnCut.Enabled = true;
                        strpBtnCopy.Enabled = true;
                        strpBtnUndo.Enabled = true;
                        strpBtnRedo.Enabled = true;
                        StatusShow(false);

                        return;
                    }
                    byte SOF = response[0];                            //sof
                    byte ProgramCRCAck = response[1];                 // Program CRC ack.
                    byte PlcModeAck = response[2];                  //Plc Mode Ack. (RUN/STOP)
                    byte ExpansionModuleAck = response[3];          //ExpansionModuleAck
                    byte ExpansionModuleIdAck = response[4];       //ExpansionModuleAckId Not set Ack
                    byte ExpansionAI_AO = response[5];            //ExpansionAI_AO_UI_UO Not set Ack
                    byte PLCModuleTypeAck = response[6];         //PLC Module Type Ack
                    byte CRC = response[7];                     //CRC
                    byte EOF = response[8];                    //EOF
                    progressBar1.Value = 50;
                    byte FinalCRC = (byte)(ProgramCRCAck ^ PlcModeAck ^ ExpansionModuleAck ^ ExpansionModuleIdAck ^ ExpansionAI_AO ^ PLCModuleTypeAck ^ 151);
                    if (CRC == FinalCRC)
                    {
                        if (PLCModuleTypeAck.ToString() != "0")
                        {
                            //"AI &AO & Id not set"
                            tssStatusLabel_show("Module Mismatch, kindly check the module used in project and one you are using", "Red");
                            MessageBox.Show("Module Missmatch, kindly check the module used in project and one you are using", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            goto Exitprocess;
                        }
                        if (ProgramCRCAck == 0)
                        {
                            string sFilepath = XMPS.Instance.LoadedProject.ProjectPath.Replace(XMPS.Instance.LoadedProject.ProjectPath.Split('\\').Last(), "");
                            string dFilepath = Path.Combine(sFilepath, "DownloadedFiles");
                            if (Directory.Exists(dFilepath))
                            {
                                if (!ProjectHelper.FileCompare(Path.Combine(sFilepath, "McodeVersion.txt"), Path.Combine(dFilepath, "McodeVersion.txt")))
                                    ProgramCRCAck = 1;
                                if (!ProjectHelper.FileCompare(Path.Combine(sFilepath, "CcodeVersion.txt"), Path.Combine(dFilepath, "CcodeVersion.txt")))
                                    ProgramCRCAck = 2;
                                if (!ProjectHelper.FileCompare(Path.Combine(sFilepath, "MQTT_CNF.txt"), Path.Combine(dFilepath, "MQTT_CNF.txt")))
                                    ProgramCRCAck = 3;
                            }
                            else
                                ProgramCRCAck = 4;

                        }
                        if (ProgramCRCAck != 0)
                        {
                            if (MessageBox.Show("Program is not matched... Do you want to download again ?", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                Download().ConfigureAwait(false);
                                LoginToPLC();
                                this.Cursor = Cursors.Default;
                                progressBar1.Value = 100;
                                progressBar1.Visible = false;
                                return;
                            }
                            this.Cursor = Cursors.Default;
                            progressBar1.Value = 100;
                            progressBar1.Visible = false;
                            strpBtnPaste.Enabled = true;
                            strpBtnCut.Enabled = true;
                            strpBtnCopy.Enabled = true;
                            strpBtnUndo.Enabled = true;
                            strpBtnRedo.Enabled = true;
                            StatusShow(false);

                            return;
                        }


                        if (((ExpansionModuleIdAck.ToString() != "0" || ExpansionModuleAck.ToString() != "0" || ExpansionAI_AO.ToString() != "0") && xm.LoadedProject.ExpansionErrorType != "Old") || ((ExpansionModuleIdAck.ToString() != "10" || ExpansionModuleAck.ToString() != "10" || ExpansionAI_AO.ToString() != "10") && xm.LoadedProject.ExpansionErrorType == "Old"))
                        {
                            string errorText = "";
                            if (xm.LoadedProject.ExpansionErrorType == "Old")
                            {
                                errorText = ExpansionModuleIdAck.ToString() != "10" ? errorText + "Expansion Module No. " + ExpansionModuleIdAck.ToString() + ": ID not found \n" : errorText;
                                errorText = ExpansionModuleAck.ToString() != "10" ? errorText + "Expansion Module No. " + ExpansionModuleAck.ToString() + ": Mismatch Error \n" : errorText;
                                errorText = ExpansionAI_AO.ToString() != "10" ? errorText + "Expansion Module No. " + ExpansionAI_AO.ToString() + ": ID not found \n" : errorText;
                            }
                            else
                            {
                                string expansionErrValue = Int32.TryParse(ExpansionModuleIdAck.ToString(), out _) ? CommonFunctions.GetBinaryValue(Convert.ToInt32(ExpansionModuleIdAck.ToString())).Substring(0, 5) : string.Empty;
                                errorText = errorText + ExpansionModuleIdAck.ToString() != "0" ? " STATUS_EXPN_ID_ERR_R in following expansions : " + CommonFunctions.GetPositionsOfOnes(expansionErrValue) : "";
                                expansionErrValue = Int32.TryParse(ExpansionModuleAck.ToString(), out _) ? CommonFunctions.GetBinaryValue(Convert.ToInt32(ExpansionModuleAck.ToString())).Substring(0, 5) : string.Empty;
                                errorText = errorText + ((ExpansionModuleAck.ToString() != "0") ? " STATUS_EXPN_MISMATCH_ERR_R in following expansions : " + CommonFunctions.GetPositionsOfOnes(expansionErrValue) : "");
                                expansionErrValue = Int32.TryParse(ExpansionAI_AO.ToString(), out _) ? CommonFunctions.GetBinaryValue(Convert.ToInt32(ExpansionAI_AO.ToString())).Substring(0, 5) : string.Empty;
                                errorText = errorText + ((ExpansionAI_AO.ToString() != "0") ? " STATUS_EXPN_MODE_ERR_R in following expansions : " + CommonFunctions.GetPositionsOfOnes(expansionErrValue) : "");
                            }
                            tssStatusLabel_show(errorText.Replace("\n", ","), "Red");
                            DialogResult dialogResult = MessageBox.Show(errorText + " Do you want to continue", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dialogResult == DialogResult.No)
                            {
                                this.Cursor = Cursors.Default;
                                progressBar1.Value = 100;
                                progressBar1.Visible = false;
                                strpBtnPaste.Enabled = true;
                                strpBtnCut.Enabled = true;
                                strpBtnCopy.Enabled = true;
                                strpBtnUndo.Enabled = true;
                                strpBtnRedo.Enabled = true;
                                StatusShow(false);
                                return;
                            }
                            else if (ExpansionAI_AO.ToString() != "0")
                            {
                                FormReconnect frmreconnect = new FormReconnect("Expansion Module No. " + ExpansionAI_AO.ToString() + ": Mode not set");
                                frmreconnect.ShowDialog();
                                string Tftpaddress;
                                if (frmreconnect.status == "Reconnect")
                                {
                                    if (pLCCommunications.GetIPAddress() == "Error")
                                    {
                                        tssStatusLabel_show("Unable to connect,Select the device from Easy Connection & Retry...", "Red");
                                        goto Exitprocess;
                                    }
                                    else
                                        Tftpaddress = pLCCommunications.Tftpaddress.ToString();
                                    Ping x = new Ping();
                                    PingReply reply = x.Send(IPAddress.Parse(Tftpaddress));
                                    pLCCommunications.Connect(Tftpaddress, pLCCommunications.ReconnectFrame(), false);
                                    tssStatusLabel_show("Reconnection request sent. . .", "Orange");
                                }
                            }
                        }
                        if (ProgramCRCAck == 0)
                        {

                            if (PlcModeAck == 1)
                            {
                                HideMenuesAfterLogin(true);
                                xm.PlcStatus = "LogIn";
                                tssStatusLabel_show("Login sucessfully, PLC is in Run mode", "Green");
                            }
                            else if (PlcModeAck == 0)
                            {
                                HideMenuesAfterLogin(false);
                                xm.PlcStatus = "LogIn";
                                tssStatusLabel_show("Login sucessfully, PLC is in Stop mode", "Green");
                            }
                            else
                            {
                                ///Since LogIn is unsucessfull reset menues as loggedout
                                NormaliseMenuesPostLogout();
                            }
                            OnlineMonitoringHelper.HoldOnlineMonitor = false;
                        }
                    }
                    else if (CRC != FinalCRC)
                    {
                        tssStatusLabel_show("Frame CRC is not matching try again", "Red");
                        StatusShow(false);
                    }
                    progressBar1.Value = 75;
                    // Load the screen again if it is on ladderform
                    // For online monotor buttons visible 
                    OnlineMonitoringHelper.Instance.PopulateTagToAddress();
                    if (xm.CurrentScreen.StartsWith("LadderForm") && xm.PlcStatus == "LogIn")
                    {
                        LoadCurrentBlock(xm.CurrentScreen);
                        ActivateForm(xm.CurrentScreen);
                    }
                    else if ((xm.CurrentScreen.StartsWith("MainLadderForm") || xm.CurrentScreen.StartsWith("MainForm")) && xm.PlcStatus == "LogIn")
                    {
                        ShowOnlineMonitoringMainForm();
                    }
                    else if (xm.CurrentScreen.StartsWith("MODBUS") || xm.CurrentScreen.ToUpper().StartsWith("TAGS") || xm.CurrentScreen.ToUpper().StartsWith("MQTT") || xm.CurrentScreen.ToUpper().StartsWith("ETHERNET") || xm.CurrentScreen.ToUpper().StartsWith("COMDEV"))
                    {
                        frmGridLayout grd = Application.OpenForms.OfType<frmGridLayout>().FirstOrDefault();
                        grd.StartOMTime();
                    }
                    this.Cursor = Cursors.Default;
                    progressBar1.Value = 100;
                    progressBar1.Visible = false;
                    if (xm.PlcStatus == "LogIn")
                        StatusShow(true);
                    else
                        StatusShow(false);
                    return;
                Exitprocess:
                    this.Cursor = Cursors.Default;
                    progressBar1.Value = 100;
                    progressBar1.Visible = false;
                    strpBtnPaste.Enabled = true;
                    strpBtnCut.Enabled = true;
                    strpBtnCopy.Enabled = true;
                    strpBtnUndo.Enabled = true;
                    strpBtnRedo.Enabled = true;
                    StatusShow(false);
                    return;
                }
                else
                {
                    MessageBox.Show("Kindly add atleast one block in Main block to continue with PLC operations", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StatusShow(false);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, Please try again " + ex.Message, "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        /// <summary>
        /// Update Menues after loging in 
        /// </summary>
        /// <param name="isrun"></param> send state is it run or stop
        private void HideMenuesAfterLogin(bool isrun)
        {
            MenuModePLCStop.Enabled = isrun;
            MenuModePLCStart.Enabled = !isrun;
            MenuModeLogout.Enabled = true;
            traceWindowToolStripMenuItem.Enabled = true;
            strpBtnLogout.Enabled = true;
            MenuModeLogin.Enabled = false;
            strpBtnLogin.Enabled = false;
            MenuEditconvertApplication.Enabled = false;
            MenuModeUpldProject.Enabled = false;
            strpBtnUploadProject.Enabled = false;
            MenuModeDnldProject.Enabled = false;
            strpBtnDownloadProject.Enabled = false;
            MenuModePLCResetOrigin.Enabled = true;
            MenuModePLCResetCold.Enabled = true;
            MenuModePLCResetwarm.Enabled = true;
            MenuModePLCMode.Enabled = true;
            MenuModeUpldSourceCode.Enabled = false;
            strpBtnPrvScreen.Enabled = false;
            strpBtnNxtScreen.Enabled = false;
            strpBtnCompile.Enabled = false;
            strpBtnOnlineMonitor.Enabled = true;
            forceUnforceMenu.Enabled = true;
            strpBtnFind.Enabled = false;
            strpBtnDelete.Enabled = false;
            EnableDiableEditFunctions(false);
            _LoggedIn = true;

        }

        private void EnableDiableEditFunctions(bool Status)
        {
            MenuProject.Enabled = Status;
            for (int i = 0; i < MenuProject.DropDownItems.Count; i++)
            {
                MenuProject.DropDownItems[i].Enabled = Status;
            }
            MenuEdit.Enabled = Status;
            for (int i = 0; i < MenuEdit.DropDownItems.Count; i++)
            {
                MenuEdit.DropDownItems[i].Enabled = Status;
            }
            tvBlocks.Enabled = Status;
            strpBtnNewProject.Enabled = Status;
            strpBtnOpenProject.Enabled = Status;
            strpBtnSaveProject.Enabled = Status;
            strpBtnCloseProject.Enabled = Status;

            //MenuProject.DropDownItems["MenuProjectPrint"].Enabled = false;
            //MenuProject.DropDownItems["MenuProjectPrintPreview"].Enabled = false;
            //MenuProject.DropDownItems["MenuProjectPageSetup"].Enabled = false;

        }


        private void RunPLC()
        {
            PLCCommunications pLCCommunications = new PLCCommunications();
            var resultRun = pLCCommunications.RunPLC();
            tssStatusLabel_show(resultRun.Item1, resultRun.Item2);
            XMPS.Instance.LoadedProject._plcStatus = resultRun.Item1;
            if (tssStatusLabel.Text.Contains("PLC is in Run Mode"))
            {
                MenuModePLCStop.Enabled = true;
                MenuModeLogout.Enabled = true;
                strpBtnLogout.Enabled = true;
                MenuModePLCStart.Enabled = false;
                MenuModeLogin.Enabled = false;
                strpBtnLogin.Enabled = false;
                //MenuModePLCResetOrigin.Enabled = true;
                //MenuModePLCResetCold.Enabled = true;
                //MenuModePLCResetwarm.Enabled = true;
                statusIndicator.Status = true;

            }
            else
            {
                MenuModePLCStop.Enabled = false;
                MenuModeLogout.Enabled = false;
                strpBtnLogout.Enabled = true;
                MenuModePLCStart.Enabled = true;
                MenuModeLogin.Enabled = false;
                strpBtnLogin.Enabled = false;
                //MenuModePLCResetOrigin.Enabled = false;
                //MenuModePLCResetCold.Enabled = false;
                //MenuModePLCResetwarm.Enabled = false;
                statusIndicator.Status = false;

            }
        }
        private void RefreshModbusFormOnLogout()
        {
            try
            {
                string formNameContains = "MODBUSRTUSlavesForm";
                if (XMPS.Instance.CurrentScreen.ToString().Contains(formNameContains) || XMPS.Instance.LoadedScreens.Keys.Any(k => k.Contains(formNameContains)))
                {
                    var screenKey = XMPS.Instance.LoadedScreens.Keys.FirstOrDefault(k => k.Contains(formNameContains));
                    if (screenKey != null)
                    {
                        var curForm = XMPS.Instance.LoadedScreens[screenKey] as frmGridLayout;
                        if (curForm != null)
                        {
                            curForm.OnShown(); 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Warning: Could not refresh MODBUS Slave Form after logout.\n" + ex.Message,"Refresh Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LogOutOfPLC()
        {
            try
            {
                if (!xm.onlinemonitoring)
                {
                    if (xm.isforced)
                    {
                        if (MessageBox.Show("While Logging Out, do you want to unforce all the values forced earlier ?", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            PLCForceFunctionality forceFunctionality = PLCForceFunctionality.Instance;
                            MessageBox.Show(forceFunctionality.SendUnforceAllFrame(), "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    PLCCommunications pLCCommunications = new PLCCommunications();
                    var resultLogout = pLCCommunications.LogOut();
                    tssStatusLabel_msg(resultLogout.Item1, 3000, resultLogout.Item2);
                    if (tssStatusLabel.Text == "PLC is in Stop mode")
                    {
                        if (MessageBox.Show("PLC is in Stop mode Do you want to Run PLC ?", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            RunPLC();
                        }
                    }
                    if (Application.OpenForms.Count > 0)
                    {
                        Form parallelWindow = Application.OpenForms
                                     .Cast<Form>()
                                     .FirstOrDefault(f => f.GetType().Name == "ParallelWindow" && f.Text == "ParallelWindow");
                        if (parallelWindow != null)
                        {
                            parallelWindow.Close();
                        }
                    }
                    if (splitContainer1.Panel1.Controls.ContainsKey("TraceWindow"))
                    {
                        if (XMPS.Instance.CurrentScreen.Equals("TraceWindow#"))
                            XMPS.Instance.CurrentScreen = string.Empty;
                        if (XMPS.Instance.LoadedScreens.ContainsKey("TraceWindow#"))
                            XMPS.Instance.LoadedScreens.Remove("TraceWindow#");
                        if (XMPS.Instance.ScreensToNavigate.Contains("TraceWindow#"))
                            XMPS.Instance.ScreensToNavigate.Remove("TraceWindow#");
                        if (XMPS.Instance.ScreensTreeNode.ContainsKey("TraceWindow#"))
                            XMPS.Instance.ScreensTreeNode.Remove("TraceWindow#");
                        TraceWindow trace = splitContainer1.Panel1.Controls.OfType<TraceWindow>().FirstOrDefault(c => c.Name == "TraceWindow");
                        splitContainer1.Panel1.Controls.Remove(trace);
                    }
                    NormaliseMenuesPostLogout();
                    RefreshModbusFormOnLogout();
                    OnlineMonitoring.DestroyInstance();
                }
                else
                {
                    MessageBox.Show("Stop Online Monitoring Before Logging Out of PLC Communications....", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //adding for refreshing the Ladder Design of UDFB ladder logic if directly logout from UDFB Ladder Logic screen.
                NodeInfo nodeInfo = tvProjects.SelectedNode.Tag as NodeInfo;
                if (currentUDFBElements.Item1 != null)
                    PerformTreeNodeActions(nodeInfo, tvProjects.SelectedNode, tvProjects.SelectedNode.Text);
            }
            catch (Exception e) { MessageBox.Show(e.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void NormaliseMenuesPostLogout()
        {
            ButtonStatus(Save: strpBtnSaveProject.Enabled, Close: strpBtnCloseProject.Enabled, Upload: true, Download: true,
                                     Compile: true, Login: true, Logout: false, CVX: true, PLCAction: MenuModePLCStart.Enabled, Find: true, Delete: true);
            MenuModePLCStop.Enabled = false;
            MenuModePLCStart.Enabled = false;
            MenuModePLCResetOrigin.Enabled = false;
            MenuModePLCResetCold.Enabled = false;
            MenuModePLCResetwarm.Enabled = false;
            strpBtnPrvScreen.Enabled = true;
            strpBtnNxtScreen.Enabled = true;
            MenuEditconvertApplication.Enabled = true;
            strpBtnOnlineMonitor.Enabled = false;
            traceWindowToolStripMenuItem.Enabled = false;
            EnableDiableEditFunctions(true);
            _LoggedIn = false;
            xm.PlcStatus = "LogOut";
            tssStatusLabel_msg("Logout successful", 3000, "LimeGreen");
            OnlineMonitorTimer.Stop();
            OnlineMonitoringStatus.isOnlineMonitoring = false;
            if (xm.CurrentScreen.StartsWith("LadderForm"))
            {
                ActivateForm(xm.CurrentScreen);
                LoadCurrentBlock(xm.CurrentScreen);
            }
            else if (xm.CurrentScreen.StartsWith("MainForm") || xm.CurrentScreen.StartsWith("MainLadderForm"))
            {
                ActivateForm(xm.CurrentScreen);
                ToolStrip curWindowControl = ((MainLadderForm)xm.LoadedScreens[xm.CurrentScreen]).getLadderEditorToolStrip();
                curWindowControl.Enabled = true;
            }
            MenuModeDnldSourceCode.Enabled = true;
            MenuModeUpldSourceCode.Enabled = true;
            forceUnforceMenu.Enabled = false;
            statusIndicator.Visible = false;
        }

        private void CommandStopPLC()
        {
            PLCCommunications pLCCommunications = new PLCCommunications();
            var resultStop = pLCCommunications.StopPLC();
            tssStatusLabel_show(resultStop.Item1, resultStop.Item2);
            XMPS.Instance.LoadedProject._plcStatus = resultStop.Item1;
            UpdatePLCStatusMode(tssStatusLabel.Text);
            ShowOnlineMonitor();
        }

        private void ResetPLC(string ResetType)
        {
            this.Cursor = Cursors.WaitCursor;
            PLCCommunications pLCCommunications = new PLCCommunications();
            tssStatusLabel_msg(pLCCommunications.ResetPLC(ResetType), 3000);
            if (tssStatusLabel.Text.Contains("Waiting"))
            {
                Thread.Sleep(10000);
                LoginToPLC();
                Thread.Sleep(2000);
                tssStatusLabel_show("");
            }
            this.Cursor = Cursors.Default;
        }
        private void UploadProject()
        {
            DialogResult result = NewProjectDialog();
            if (result == DialogResult.OK)
            {
                PLCCommunications pLCCommunications = new PLCCommunications();
                //tssStatusLabel.Text = pLCCommunications.up();
                progressBar1.Visible = true;
                progressBar1.Value = 25;
                var resultUpload = pLCCommunications.UplodFiles();
                progressBar1.Value = 100;
                //tssStatusLabel_msg(resultUpload.Item1, 3000, resultUpload.Item2);
                progressBar1.Visible = false;
                if (resultUpload.ToString().Contains("File Downloaded Successfully"))
                {
                    UpdateProjectFile();
                    MessageBox.Show("Project Uploaded Successfully....", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(resultUpload.ToString().Split(',').First(), "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Operation Terminated", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateProjectFile()
        {
            string prvfilepath = xm.LoadedProject.ProjectPath;
            LoadCurrentProject();
            xm.LoadedProject.ProjectPath = prvfilepath;
            xm.LoadedProject.ProjectName = prvfilepath.Split('\\').Last().ToString();
            SaveProject();

        }

        private void ReadandUpdateConfigCSV()
        {
            List<ModbusFunctionCode> modbuslist = ModbusFunctionCode.List;
            DataTable Modbus = ProjectHelper.ToDataTable(modbuslist);
            string[] ConfigurationColumnsToValidate = {
                "ConfigType",
                "Use DHCP",
                "IP Address",
                "Subnet",
                "Gateway",
                "Port Number",
                "Baud Rate",
                "Data Length",
                "Stop Bit",
                "Parity",
                "SendDelay",
                "MinimumInterface",
                "On-Board IO",
                "Remote IO",
                "ModbusType",
                "Slave ID",
                "CommunicationTimeout",
                "NoOfRetries",
                "Polling",
                "SlaveIPAddress",
                "TCPPort",
                "Variable",
                "Data Start address",
                "Data Size",
                "FunctionCode",
                "IO List",
                "Model",
                "Type",
                "Mode",
                "Label",
                "Logical Address",
                "Tag",
                "RetAdd",
                "Init_val"
            };
            string ConFilePath = xm.LoadedProject.ProjectPath.Replace(".xmprj", "Config.CSV");
            DataTable ConfFile = ProjectHelper.NewDataTable(ConFilePath, ",", ConfigurationColumnsToValidate);
            string Model = "";
            IOListType IOList = IOListType.OnBoardIO;
            IOType Type = IOType.DigitalInput;
            foreach (DataRow dr in ConfFile.Rows)
            {
                string colnm = dr[0].ToString();
                switch (colnm)
                {
                    case "COM Settings":     // Update Com Settings
                        COMDevice dataSource = new COMDevice();
                        dataSource.BaudRate = Convert.ToInt32(Enum.Parse(typeof(COMBaudRate), Convert.ToString(Convert.ToInt32(dr["Baud Rate"]) + 1)).ToString().Replace("_", ""));
                        dataSource.DataLength = Convert.ToInt32(Enum.Parse(typeof(COMDataLength), Convert.ToString(Convert.ToInt32(dr["Data Length"]) + 1)).ToString().Replace("_", ""));
                        dataSource.Parity = (Enum.Parse(typeof(COMParity), Convert.ToString(Convert.ToInt32(dr["Parity"]) + 1)).ToString().Replace("_", ""));
                        dataSource.StopBit = Convert.ToInt32(Enum.Parse(typeof(COMStopBit), Convert.ToString(Convert.ToInt32(dr["Stop Bit"]) + 1)).ToString().Replace("_", ""));
                        dataSource.SendDelay = Convert.ToInt32(dr["SendDelay"].ToString());
                        dataSource.MinimumInterface = Convert.ToDecimal(dr["MinimumInterface"].ToString());
                        xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "COMDevice");
                        xm.LoadedProject.Devices.Add(dataSource);
                        break;
                    case "Ethernet Settings":     // Update Ethernet Settings
                        Ethernet edataSource = new Ethernet();
                        edataSource.UseDHCPServer = (dr["Use DHCP"].ToString() == "0") ? false : true;
                        edataSource.EthernetIPAddress = IPAddress.Parse(dr["IP Address"].ToString());
                        edataSource.EthernetSubNet = IPAddress.Parse(dr["Subnet"].ToString());
                        edataSource.EthernetGetWay = IPAddress.Parse(dr["Gateway"].ToString());
                        edataSource.Port = Convert.ToInt32(dr["Port Number"].ToString());
                        xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "Ethernet");
                        xm.LoadedProject.Devices.Add(edataSource);
                        break;
                    case "Digital Input":
                        break;
                    case "Digital Output":
                        break;
                    case "Analog Input":
                        break;
                    case "Analog Output":
                        break;
                    case "IO_Mapping":     // IO Mapping Data
                        XMIOConfig IO = new XMIOConfig();
                        if (dr["IO List"].ToString() != "")
                        {
                            IOList = (IOListType)Enum.Parse(typeof(IOListType), dr["IO List"].ToString());
                        }
                        if (dr["Model"].ToString() != "")
                        {
                            string modelcode = ProjectHelper.GetModelName(dr["Model"].ToString());
                            Model = modelcode == null ? dr["Model"].ToString() : modelcode;
                            IOList = (IOListType)Enum.Parse(typeof(IOListType), dr["IO List"].ToString());
                            if (IOList != IOListType.OnBoardIO)
                            {
                                if (xm.LoadedProject.Tags.Where(M => M.Model == Model).Count() > 0)
                                {
                                    if ((xm.LoadedProject.Tags.Where(T => T.Model != null && T.Model != "" && T.Model.StartsWith(Model)).GroupBy(T => T.Model).Count() > 0) && (IOList != IOListType.OnBoardIO))
                                    {
                                        string mymodel = Model;
                                        Model = Model + "_" + xm.LoadedProject.Tags.Where(T => T.Model != null && T.Model != "" && T.Model.StartsWith(mymodel)).GroupBy(T => T.Model).Count().ToString();
                                    }
                                }

                            }
                        }
                        if (dr["Type"].ToString() != "")
                        {
                            Type = (IOType)Enum.Parse(typeof(IOType), dr["Type"].ToString());
                        }
                        IO.IoList = IOList;
                        IO.Type = Type;
                        IO.Model = Model;
                        IO.Label = dr["Label"].ToString();
                        IO.LogicalAddress = dr["Logical Address"].ToString();
                        IO.Tag = dr["Tag"].ToString();
                        IO.Mode = (dr["Mode"].ToString() == null ? "-" : dr["Mode"].ToString());
                        IO.Key = xm.LoadedProject.Tags.Count() + 1;
                        xm.LoadedProject.Tags.RemoveAll(d => d.LogicalAddress == IO.LogicalAddress);
                        xm.LoadedProject.Tags.Add(IO);
                        break;
                    case "Memory_Address":     // Memory Address Data
                        XMIOConfig MIO = new XMIOConfig();
                        MIO.IoList = IOListType.NIL;
                        MIO.Type = IOType.DataType;
                        MIO.Model = "";
                        MIO.Label = dr["Label"].ToString(); ;
                        MIO.LogicalAddress = dr["Logical Address"].ToString();
                        MIO.Tag = dr["Tag"].ToString();
                        xm.LoadedProject.Tags.RemoveAll(d => d.LogicalAddress == MIO.LogicalAddress);
                        xm.LoadedProject.Tags.Add(MIO);
                        break;
                    case "Retentive":     // Retentive Address Data
                        var Ret = xm.LoadedProject.Tags.Where(R => R.LogicalAddress.Contains(dr["Logical Address"].ToString())).FirstOrDefault();
                        Ret.Retentive = true;
                        Ret.RetentiveAddress = dr["RetAdd"].ToString();
                        break;
                    case "Init_val":     // Address Initial Value Data
                        var Ini = xm.LoadedProject.Tags.Where(R => R.LogicalAddress.Contains(dr["Logical Address"].ToString())).FirstOrDefault();
                        Ini.InitialValue = dr["Init_val"].ToString();
                        break;
                    case "Modbus":     // ModBus Data
                        int modbustype = Convert.ToInt32(dr["ModbusType"].ToString()); // Check for ModBus Type
                        string modbusfuntypecode = (dr["FunctionCode"].ToString()); //Get Modbus Function Type Code
                        string expression = "ID =" + Convert.ToString(modbusfuntypecode);//Convert the Hex value to Integer for Checking 
                        DataRow[] selectedRows = Modbus.Select(expression);//Get Modbus Function Type Name from Code
                        string ModbusFunctionType = selectedRows[0][1].ToString();  //Get Modbus Function Type Name
                        int NumberOfRetries = 0, CommunicationTimeout = 0;
                        switch (modbustype)
                        {
                            case 1:
                                MODBUSRTUMaster_Slave RTUMst_slave = new MODBUSRTUMaster_Slave();
                                NodeInfo nitnTCPServerSlave = new NodeInfo();
                                nitnTCPServerSlave.NodeType = NodeType.DeviceNode;
                                nitnTCPServerSlave.Info = "ModbusRequest";
                                NumberOfRetries = Convert.ToInt32(dr["NoOfRetries"].ToString());
                                CommunicationTimeout = Convert.ToInt32(dr["CommunicationTimeout"].ToString());

                                var modBUSRTUMaster = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
                                if (modBUSRTUMaster != null)
                                {
                                    modBUSRTUMaster.AddSlave(
                                        xm.LoadedProject.GetSlaveName(nitnTCPServerSlave),
                                        Convert.ToInt32(dr["Polling"].ToString()),
                                        Convert.ToInt32(dr["Slave ID"].ToString()),
                                        Convert.ToInt32(dr["Data Start address"].ToString()),
                                        Convert.ToInt32(dr["Data Size"].ToString()),
                                        dr["Variable"].ToString(),
                                        dr["Tag"].ToString(),
                                       // dr["disablingVariables"].ToString(),
                                       "",
                                        ModbusFunctionType,
                                       dr["MultiplicationFactor"].ToString()
                                        );

                                    xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "MODBUSRTUMaster");
                                    xm.LoadedProject.Devices.Add(modBUSRTUMaster);
                                }
                                break;
                            case 2:
                                NodeInfo nitnTCPClientSlave = new NodeInfo();
                                nitnTCPClientSlave.NodeType = NodeType.DeviceNode;
                                nitnTCPClientSlave.Info = "ModbusTCPSlave";
                                var modBUSTCPClient = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
                                if (modBUSTCPClient != null)
                                {
                                    modBUSTCPClient.AddSlave(
                                        xm.LoadedProject.GetSlaveName(nitnTCPClientSlave),
                                        IPAddress.Parse(dr["SlaveIPAddress"].ToString()),
                                        Int32.Parse(dr["TCPPort"].ToString()),
                                        Int32.Parse(dr["Polling"].ToString()),
                                        Int32.Parse(dr["Slave ID"].ToString()),
                                        Int32.Parse(dr["Data Start address"].ToString()),
                                        Int32.Parse(dr["Data Size"].ToString()),
                                        dr["Variable"].ToString(),
                                        dr["Tag"].ToString(),
                                        ModbusFunctionType,
                                        dr["MultiplicationFactor"].ToString()
                                        );

                                    xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "MODBUSTCPClient");
                                    xm.LoadedProject.Devices.Add(modBUSTCPClient);
                                }
                                break;
                            case 3:
                                TreeNode tnTCPServer = new TreeNode("MODBUS TCP Server");
                                NodeInfo niTCPServer = new NodeInfo();
                                niTCPServer.NodeType = NodeType.DeviceNode;
                                niTCPServer.Info = "MODBUSTCPServer";
                                var modBUSTCPServer = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
                                if (modBUSTCPServer != null)
                                {
                                    modBUSTCPServer.AddRequest(
                                        xm.LoadedProject.GetRequestName(niTCPServer),
                                        Int32.Parse(dr["Data Start address"].ToString()),
                                        Int32.Parse(dr["Data Size"].ToString()),
                                        Int32.Parse(dr["TCPPort"].ToString()),
                                        dr["Variable"].ToString(),
                                        Int32.Parse(dr["Slave ID"].ToString()),
                                        dr["Tag"].ToString(),
                                        ModbusFunctionType
                                        );

                                    xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "MODBUSTCPServer");
                                    xm.LoadedProject.Devices.Add(modBUSTCPServer);
                                }
                                break;
                        }
                        if (NumberOfRetries > 0 || CommunicationTimeout > 0)
                        {
                            COMDevice comsettings = (COMDevice)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "COMDevice").FirstOrDefault();
                            comsettings.NumberOfRetries = NumberOfRetries;
                            comsettings.CommunicationTimeout = CommunicationTimeout;
                        }

                        break;
                }

            }
        }

        private string GetIOModelFromCode(string v)
        {
            throw new NotImplementedException();
        }

        private void ReadandUpdateAppCSV()
        {
            string AppFilePath = xm.LoadedProject.ProjectPath.Replace(".xmprj", "App.CSV");

            string[] columnsToValidate = {
                "Line Number",
                "T/C Name",
                "OutputType",
                "DataType",
                "Enable",
                "Output1",
                "Output2",
                "Op Code",
                "Input1",
                "Input2",
                "Input3",
                "Input4",
                "Comments",
                "WindowName"
            };
            DataTable dt = ProjectHelper.NewDataTable(AppFilePath, ",", columnsToValidate);
            DataTable Orgdt = dt;
            ///Copy Name of window from First Rung and paste it to all following Rungs
            ///Add Distinct block names with proper sequence in List so that we can bind this list directly to MainLadder Form
            List<string> CompletedBlocks = new List<string>();
            string windowname = "";
            int line = 0;
            foreach (DataRow Acdr in dt.Rows)
            {
                if (Acdr["WindowName"].ToString() != "")
                {
                    windowname = Acdr["WindowName"].ToString();
                    line = 1;
                    CompletedBlocks.Add(windowname);
                }
                else
                {
                    Acdr["WindowName"] = windowname;
                    line++;
                }
                Acdr["Line Number"] = line;
                Acdr.AcceptChanges();
                dt.AcceptChanges();
            }
            List<Tuple<String, String>> Addedbock = new List<Tuple<String, String>>();

            bool newwindow = false;
            int LastRung = 0;
            string BLockName = "", WindowName = "";
            DataView view = new DataView(dt);
            if (dt.Rows.Count > 0)
            {
                DataTable distinctValues = view.ToTable(true, columnsToValidate);

                foreach (DataRow dr in distinctValues.Rows)
                {

                    ApplicationRung AppRecs = new ApplicationRung();
                    AppRecs.LineNumber = Convert.ToInt32(dr["Line Number"].ToString());
                    AppRecs.TC_Name = dr["T/C Name"].ToString();
                    AppRecs.OutputType = dr["OutputType"].ToString();
                    AppRecs.OutPutType_NM = OutputType.List.Find(i => i.ID == Convert.ToInt32(AppRecs.OutputType?.ToString())).Text;
                    AppRecs.DataType = dr["DataType"].ToString();
                    var dataTypeInteger = Int32.Parse(AppRecs.DataType?.ToString(), System.Globalization.NumberStyles.HexNumber);
                    // Retrieve Data Type from OutputType_Hex column 
                    AppRecs.DataType_Nm = DataType.List.Find(i => i.ID == dataTypeInteger).Text;
                    AppRecs.Enable = dr["Enable"].ToString();
                    AppRecs.Outputs.Add("Output1", dr["Output1"].ToString());
                    AppRecs.Outputs.Add("Output2", dr["Output2"].ToString());
                    AppRecs.Outputs.Add("Output3", dr["Output3"].ToString());
                    AppRecs.OpCode = dr["Op Code"].ToString();
                    var opCodeInHex = AppRecs.OpCode?.ToString();
                    // Convert the op code from hex to decimal integer
                    var opCodeInteger = Int32.Parse(opCodeInHex, System.Globalization.NumberStyles.HexNumber);
                    // Subtract data type value from OpCode to obtain instruciton code value
                    int instructionCode = opCodeInteger - dataTypeInteger;
                    AppRecs.OpCodeNm = Instruction.List.Find(i => i.ID == instructionCode).Text;
                    AppRecs.Inputs.Add("Input1", dr["Input1"].ToString());
                    AppRecs.Inputs.Add("Input2", dr["Input2"].ToString());
                    AppRecs.Inputs.Add("Input3", dr["Input3"].ToString());
                    AppRecs.Inputs.Add("Input4", dr["Input4"].ToString());
                    AppRecs.Inputs.Add("Input5", dr["Input5"].ToString());
                    AppRecs.Comments = dr["Comments"].ToString();
                    BLockName = dr["WindowName"].ToString();
                    if (!(Addedbock.Where(d => d.Item2 == BLockName).Count() > 0))
                    {
                        newwindow = true;
                        if (Addedbock.Where(d => d.Item2 == BLockName).Count() > 0)
                        {
                            if (AppRecs.LineNumber == 1)
                            {
                                BLockName = Addedbock.Where(d => d.Item2 == BLockName).Select(d => d.Item1).ToString();
                                WindowName = BLockName.Replace("B", "LadderForm#LogicBlock");
                            }
                            BLockName = "";
                        }
                        else
                        {

                            BLockName = "B" + (Addedbock.Count() + 1).ToString("00");
                            var tuple = Tuple.Create(BLockName, dr["WindowName"].ToString());
                            Addedbock.Add(tuple);
                            WindowName = BLockName.Replace("B", "LadderForm#LogicBlock");

                            if (xm.LoadedProject.Blocks.Where(B => B.Name == WindowName.Replace("LadderForm#", "")).Count() == 0)
                            {
                                Block NewBlock = new Block();
                                NewBlock.Name = WindowName.Replace("LadderForm#", "");
                                NewBlock.Type = "LogicBlock";
                                xm.LoadedProject.Blocks.Add(NewBlock);
                            }
                        }
                    }
                    AppRecs.WindowName = WindowName;
                    AppRecs.Name = WindowName;
                    if (BLockName != "")
                    {
                        if ((LastRung != AppRecs.LineNumber && !newwindow) || (AppRecs.LineNumber == 1 && newwindow))
                        {

                            if (xm.LoadedProject.LogicRungs.Where(d => d.WindowName == WindowName && d.LineNumber == AppRecs.LineNumber).Count() <= 0)
                            {
                                xm.LoadedProject.AddRung(AppRecs);
                                newwindow = false;
                            }
                        }
                        LastRung = AppRecs.LineNumber;
                    }

                }
            }
            ///Add all completed blocks in Main Ladder block logic
            foreach (string BlockNm in CompletedBlocks)
            {
                if (BlockNm != "")
                {
                    BLockName = Addedbock.Where(d => d.Item2 == BlockNm.ToString()).Select(d => d.Item1).FirstOrDefault();
                    WindowName = BLockName.Replace("B", "LadderForm#LogicBlock");
                    xm.LoadedProject.MainLadderLogic.Add(WindowName.Replace("LadderForm#", ""));
                }
            }
        }

        /// <summary>
        /// Printting the generated PDF file
        /// </summary>
        /// <param name="prntpath"></param>Path of the printable file.
        public void printPDF(string prntpath)
        {
            /*
             * 
            ////https://ourcodeworld.com/articles/read/502/how-to-print-a-pdf-from-your-winforms-application-in-c-sharp
            ///Use Raw Printer option from this post
            */
            PrintDialog Dialog = new PrintDialog();
            Dialog.AllowPrintToFile = true;
            Dialog.AllowSomePages = true;
            Dialog.PrinterSettings.MinimumPage = 1;
            Dialog.PrinterSettings.FromPage = 1;
            Dialog.PrinterSettings.ToPage = 1;
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                // Absolute path to your PDF to print (with filename)
                string Filepath = prntpath;
                // The name of the PDF that will be printed (just to be shown in the print queue)
                string Filename = "Output.pdf";
                // The name of the printer that you want to use
                // Note: Check step 1 from the B alternative to see how to list
                // the names of all the available printers with C#
                string PrinterName = Dialog.PrinterSettings.PrinterName;

                // Create an instance of the Printer
                IPrinter printer = new Printer();

                if (Dialog.PrinterSettings.FromPage.ToString() != "1" && Dialog.PrinterSettings.ToPage.ToString() != "1")
                {
                    ///Read the PDF document to be copied
                    PdfReader reader = new PdfReader(Filepath);
                    ///Selected only the desired pages from the list of pages
                    reader.SelectPages(Dialog.PrinterSettings.FromPage.ToString() + "-" + Dialog.PrinterSettings.ToPage.ToString());
                    ///Save the selected pages in the same location with appended name as Selected
                    PdfStamper stamper = new PdfStamper(reader, new FileStream(Filepath.Replace(".pdf", "selected.pdf"), FileMode.Create, FileAccess.Write));
                    ///Clost all components used to read and write the file
                    stamper.Close();
                    reader.Close();
                    // Print the updated file
                    printer.PrintRawFile(PrinterName, Filepath.Replace(".pdf", "selected.pdf"), Filename);
                }
                else
                {
                    // Print the file
                    printer.PrintRawFile(PrinterName, Filepath, Filename);
                }
            }
            else
            {
                MessageBox.Show("Open The File To Print ....", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Set visiblity for the buttons on strip and menu 
        /// </summary>
        /// <param name="Save">Save button enable</param>
        /// <param name="Close">Close button enable</param>
        /// <param name="Upload">Upload button enable</param>
        /// <param name="Download">Download button enable</param>
        /// <param name="Compile">Compile button enable</param>
        /// <param name="Login">Login button enable</param>
        /// <param name="Logout">Logout button enable</param>
        /// <param name="CVX">Copy paste and cut button enable</param>
        /// <param name="PLCAction">PLC menu button enable</param>
        private void ButtonStatus(bool Save, bool Close, bool Upload, bool Download, bool Compile, bool Login, bool Logout, bool CVX, bool PLCAction, bool Find, bool Delete)
        {
            // Screen 
            //FindAndReplace
            MenuEditFindNReplace.Enabled = Find;
            strpBtnFind.Enabled = Find;
            // Save
            MenuProjectSave.Enabled = Save;
            strpBtnSaveProject.Enabled = Save;

            // Close
            strpBtnCloseProject.Enabled = Close;

            // Upload
            MenuModeUpldProject.Enabled = Upload;
            strpBtnUploadProject.Enabled = Upload;

            // Download
            MenuModeDnldProject.Enabled = Download;
            strpBtnDownloadProject.Enabled = Download;

            // Compile
            MenuModeCompile.Enabled = Compile;
            strpBtnCompile.Enabled = Compile;

            // Login
            MenuModeLogin.Enabled = Login;
            strpBtnLogin.Enabled = Login;

            traceWindowToolStripMenuItem.Enabled = MenuModeLogin.Enabled ? true : false;
            // Logout
            MenuModeLogout.Enabled = Logout;
            strpBtnLogout.Enabled = Logout;

            // PLC action
            MenuModePLCStart.Enabled = PLCAction;
            MenuModePLCStop.Enabled = PLCAction;
            MenuModePLCResetOrigin.Enabled = PLCAction;
            MenuModePLCResetCold.Enabled = PLCAction;
            MenuModePLCResetwarm.Enabled = PLCAction;
            //MenuModePLCMode.Enabled = PLCAction;

            // Cut Copy Paste
            MenuEditCopy.Enabled = CVX;
            MenuEditCut.Enabled = CVX;
            MenuEditPaste.Enabled = CVX;
            strpBtnPaste.Enabled = CVX;
            strpBtnCut.Enabled = CVX;
            strpBtnCopy.Enabled = CVX;
            strpBtnUndo.Enabled = CVX;
            strpBtnRedo.Enabled = CVX;
            strpBtnDelete.Enabled = Delete;
        }


    }

    public class TagComparer : IEqualityComparer<XMIOConfig>
    {
        public bool Equals(XMIOConfig x, XMIOConfig y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;

            // Compare all properties except those we want to ignore
            return string.Equals(x.LogicalAddress, y.LogicalAddress, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(x.Model, y.Model, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(x.Tag, y.Tag, StringComparison.OrdinalIgnoreCase) &&
                   (x.ReadOnly == y.ReadOnly) &&
                   (x.Editable == y.Editable) &&
            string.Equals(x.Label, y.Label, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(XMIOConfig obj)
        {
            if (obj == null) return 0;

            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (obj.LogicalAddress?.ToUpperInvariant().GetHashCode() ?? 0);
                hash = hash * 23 + (obj.Model?.ToUpperInvariant().GetHashCode() ?? 0);
                hash = hash * 23 + (obj.Tag?.ToUpperInvariant().GetHashCode() ?? 0);
                // Handle boolean properties directly
                hash = hash * 23 + obj.ReadOnly.GetHashCode();
                hash = hash * 23 + obj.Editable.GetHashCode();
                // Add other properties used in Equals
                hash = hash * 23 + (obj.Label?.ToUpperInvariant().GetHashCode() ?? 0);

                return hash;
            }
        }
    }
    public class TagAddressChange
    {
        public string Tag { get; set; }
        public string OldAddress { get; set; }
        public string NewAddress { get; set; }
    }

    public class TagInfo
    {
        public string TagName { get; set; }
        public string LogicalAddress { get; set; }
        public string ModelName { get; set; }

        public int Index { get; set; }

        public override string ToString() => $"{TagName} | {LogicalAddress} | {ModelName} | {Index}";
    }

    public class SearchResult
    {
        public List<TagInfo> NotFoundTags { get; set; } = new List<TagInfo>();
        public int FoundCount { get; set; }
        public int TotalCount { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString() => Success ?
            $"Found: {FoundCount}/{TotalCount}, Missing: {NotFoundTags.Count}" :
            $"Error: {ErrorMessage}";
    }


}
