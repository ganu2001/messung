using Force.DeepCloner;
using LadderEditorLib.MementoDesign;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.Core.LadderLogic;

namespace LadderDrawing
{
    public enum AddressDataTypes
    {
        BYTE,
        BOOL,
        WORD,
        INT,
        DWORD,
        REAL,
        DINT,
        UDINT,
        STRING
    }
    public class OnlineMonitoringStatus
    {
        public static bool isOnlineMonitoring = false;
        public static Dictionary<string, string> AddressValues = new Dictionary<string, string>();
        public static Dictionary<string, Tuple<string, AddressDataTypes>> CurBlockAddress = new Dictionary<string, Tuple<string, AddressDataTypes>>();
        public static void PopulateAddressValues(ref Dictionary<string, string> updatedValues, ref Dictionary<string, Tuple<string, AddressDataTypes>> updatedAddresses)
        {
            AddressValues = updatedValues;
            CurBlockAddress = updatedAddresses;
        }
    }
    public class LadderDesign
    {
        public string SetResetElement;
        public bool CheckNegation;
        public string parallelParent;
        public string contactConnectTo;
        public string negationId;
        static int contactCounter = 0;
        static int parallelCounter = 0;
        public string pnStatus;
        public bool negation = false;
        public static int currentRungScroll = 0;
        XMPS xm = XMPS.Instance;
        private static int IncrementContactCounter() { return ++contactCounter; }
        private string NextContactCounter() { return "c" + IncrementContactCounter().ToString(); }
        private static int IncrementParallelCounter() { return ++parallelCounter; }
        private string NextParallelCounter() { return "p" + IncrementParallelCounter().ToString(); }
        string m_Id = "";
        public string Id { set { m_Id = value; } get { return m_Id; } }
        string m_Name = "";
        public string Name { set { m_Name = value; } get { return m_Name; } }
        Position m_Position = new Position();
        public Position Position { set { m_Position = value; } get { return m_Position; } }
        LadderElements m_Elements = new LadderElements();
        public LadderElements Elements { set { m_Elements = value; } get { return m_Elements; } }
        public Dictionary<int, LadderElement> m_Height_dic = new Dictionary<int, LadderElement>();
        public static List<int> ActiveRungNo = new List<int>();
        public static Dictionary<string, Tuple<string, string>> BlockTagAddress = new Dictionary<string, Tuple<string, string>>();
        public static LadderElement HoverElement = null;
        public static LadderElement ClickedElement = null;
        //for Saving the current selected Contact if for changing PN status.
        public static LadderElement PNStatusElement = null;
        //adding new property for the EasyInstruction Task
        public static LadderElement PrevClickedElement = null;
        public static LadderElement CurrentAddedElement = null;
        public static Graphics Window = null;
        public static LadderDesign Active;
        public static Font Font = new Font(new FontFamily("Arial"), 8);
        public static int StartX = 10;
        public static int StartY = 20;
        public static int ControlSpacing = 50;
        public static int ControlWidth = 150;
        public static int ControlHeight = 60;
        public int setResetCount = 0;
        Caretaker caretaker = new Caretaker();
        #region HashSet Approach Changes
        public Dictionary<HashSet<LadderElement>, LadderElements> parallelElementsDictionary = new Dictionary<HashSet<LadderElement>, LadderElements>(HashSet<LadderElement>.CreateSetComparer());

        int GetHeightOfParallelsUnderSet(HashSet<LadderElement> setOfContacts)
        {
            parallelElementsDictionary.TryGetValue(setOfContacts, out LadderElements parallelsUnderSet);
            int heightOfParallelsUnderSet = 0;
            foreach (var childParallel in parallelsUnderSet)
                heightOfParallelsUnderSet += childParallel.Position.Height;
            return heightOfParallelsUnderSet;
        }
        int GetMaxHeightOfParallelsUnderSet(HashSet<LadderElement> set)
        {
            parallelElementsDictionary.TryGetValue(set, out LadderElements parallelsUnderSet);
            int maxHeightOfParallelsUnderSet = 0;
            foreach (var childParallel in parallelsUnderSet)
                if (maxHeightOfParallelsUnderSet < childParallel.Position.Height)
                    maxHeightOfParallelsUnderSet = childParallel.Position.Height;
            return maxHeightOfParallelsUnderSet;
        }
        int GetMaxHeightOfParallelsUnderSubSet(HashSet<LadderElement> set)
        {
            int maxHeightOfParallelsUnderSubSets = 0;
            // See if there are any subsets of set of currently selected contacts, if yes, take their height into consideration 
            foreach (var subSet in parallelElementsDictionary.Keys)
            {
                if (subSet.IsProperSubsetOf(set))
                {
                    int heightOfParallelsUnderSubSet = GetHeightOfParallelsUnderSet(subSet);
                    if (maxHeightOfParallelsUnderSubSets < heightOfParallelsUnderSubSet)
                        maxHeightOfParallelsUnderSubSets = heightOfParallelsUnderSubSet;
                }
            }
            return maxHeightOfParallelsUnderSubSets;
        }
        private LadderElement CreateDummyParallelParent(List<LadderElement> selectedElements)
        {
            LadderElement dummyParent = new LadderElement();
            dummyParent.CreateCustom(new DummyParallelParent(),
                0,
                selectedElements[0].Position.Y,
                0,
                ControlHeight);
            return dummyParent;
        }
        private bool CheckIfIsParent(LadderElement selectedContact)
        {
            bool CheckedValue = false;
            var hashSetOfSelectedContact = new HashSet<LadderElement>() { selectedContact };
            // Prepare a list of all sub sets of set of selected contacts
            List<HashSet<LadderElement>> listOfSubSets = new List<HashSet<LadderElement>>();
            foreach (var set in parallelElementsDictionary.Keys)
                if (set.IsSupersetOf(hashSetOfSelectedContact))
                    CheckedValue = true;
            return CheckedValue;
        }
        private List<HashSet<LadderElement>> GetListofAllSupersetOf(LadderElement selectedContact)
        {
            var hashSetOfSelectedContact = new HashSet<LadderElement>() { selectedContact };
            // Prepare a list of all sub sets of set of selected contacts
            List<HashSet<LadderElement>> listOfSubSets = new List<HashSet<LadderElement>>();
            foreach (var set in parallelElementsDictionary.Keys)
                if (set.IsSupersetOf(hashSetOfSelectedContact))
                    listOfSubSets.Add(set);

            // Sort the list of supersets as per order of superiority i.e. set with max no. of contacts is the most superior
            listOfSubSets.Sort((x, y) => x.Count.CompareTo(y.Count));
            return listOfSubSets;
        }
        private HashSet<LadderElement> GetImmediateSupersetOf(LadderElement selectedContact)
        {
            var hashSetOfSelectedContact = new HashSet<LadderElement>() { selectedContact };
            // Prepare a list of all sub sets of set of selected contacts
            List<HashSet<LadderElement>> listOfSubSets = new List<HashSet<LadderElement>>();
            foreach (var set in parallelElementsDictionary.Keys)
                if (set.IsSupersetOf(hashSetOfSelectedContact))
                    listOfSubSets.Add(set);

            // Sort the list of supersets as per order of superiority i.e. set with max no. of contacts is the most superior
            listOfSubSets.Sort((x, y) => x.Count.CompareTo(y.Count));
            if (listOfSubSets.Count > 0)
                return listOfSubSets[0];
            else
                return null;
        }
        private int GetMaxWidthOfImmediateSuperset(LadderElement selectedContact)
        {
            int numberOfConnects = 1;
            var parentSet = GetImmediateSupersetOf(selectedContact);
            if (parentSet != null)
            {
                var childParallels = parallelElementsDictionary[parentSet];
                numberOfConnects = GetMaxConnect(childParallels[0]);
            }
            return numberOfConnects * ControlWidth;
        }
        private int GetMaxWidthOfCompleteParentSet(LadderElement selectedContact)
        {
            int numberOfParents = 0;
            var parentSet = GetImmediateSupersetOf(selectedContact);
            if (parentSet != null)
            {
                var childParallels = parallelElementsDictionary[parentSet];
                if (childParallels.Count > 0)
                    numberOfParents = childParallels[0].Position.RelateTo.Count;
            }
            return numberOfParents * ControlWidth;
        }

        private int GetMaxWidthOfOuterCompleteParentSet(LadderElement selectedContact)
        {
            var parentSet = GetListofAllSupersetOf(selectedContact);
            int maxTotalWidth = 0;
            foreach (HashSet<LadderElement> keys in parentSet)
            {
                parallelElementsDictionary.TryGetValue(keys, out LadderElements parallelsUnderSet);
                foreach (LadderElement values in parallelsUnderSet)
                {
                    int valuewidth = values.Position.ConnectTo.Count;
                    int parentwidth = GetImmediateSupersetOf(selectedContact).Count() - 1;// values.Position.RelateTo.Count;
                    if (parentwidth < valuewidth && valuewidth > maxTotalWidth)
                        maxTotalWidth = valuewidth;
                }
            }
            return maxTotalWidth;
        }

        private int GetYofTopmostSubSet(HashSet<LadderElement> hashSetOfSelectedContacts)
        {
            // Prepare a list of all sub sets of set of selected contacts
            List<HashSet<LadderElement>> listOfSubSets = new List<HashSet<LadderElement>>();
            foreach (var set in parallelElementsDictionary.Keys)
                if (set.IsProperSubsetOf(hashSetOfSelectedContacts))
                    listOfSubSets.Add(set);
            // Sort the list of supersets as per order of superiority i.e. set with max no. of contacts is the most superior
            listOfSubSets.Sort((x, y) => x.Count.CompareTo(y.Count));
            int maxY = 0;
            foreach (var set in listOfSubSets)
            {
                int childY = 0;
                parallelElementsDictionary.TryGetValue(set, out LadderElements parallelsUnderSet);
                if (parallelsUnderSet != null && parallelsUnderSet.Count > 0)
                    foreach (LadderElement childElement in parallelsUnderSet)
                        childY += childElement.Position.Y;
                if (maxY < childY)
                    maxY = childY;
            }
            return maxY;
        }
        private void AddContactToSuperSets(LadderElement selectedContact, LadderElement newContactToBeAdded)
        {
            // Create a hashset of selected contact to be used for matching keys (supersets) in parallel elements dictionary
            var setToBeSearched = new HashSet<LadderElement>() { selectedContact };
            // Prepare a list of all matching super sets (if any) of selectedContact including set of self
            List<HashSet<LadderElement>> listOfSuperSetsToUpdate = new List<HashSet<LadderElement>>();
            foreach (var set in parallelElementsDictionary.Keys)
                if (set.IsSupersetOf(setToBeSearched))
                    listOfSuperSetsToUpdate.Add(set);
            foreach (var setToUpdate in listOfSuperSetsToUpdate)
            {
                var value = parallelElementsDictionary[setToUpdate];
                parallelElementsDictionary.Remove(setToUpdate);
                var listOfSetElements = setToUpdate.ToList();
                listOfSetElements.Add(newContactToBeAdded);
                // Sort the list as per X coordinate of each element, such that element with lowest X comes at 0th position
                listOfSetElements.Sort((x, y) => x.Position.X.CompareTo(y.Position.X));
                // Prpare the key (set) to update from above list of elements
                var updatedSet = new HashSet<LadderElement>(listOfSetElements);
                // For each element in parallels list update RelateTo
                foreach (var parallelElement in value)
                {
                    parallelElement.Position.RelateTo.Clear();
                    parallelElement.Position.RelateTo.AddRange(updatedSet);

                    foreach (var contactInSeries in parallelElement.Position.ConnectTo)
                    {
                        parallelElement.Position.RelateTo.Clear();
                        parallelElement.Position.RelateTo.AddRange(updatedSet);
                    }
                }
                // Finally update the dictionary by adding updatedSet as key with Old Key's value
                parallelElementsDictionary.TryGetValue(updatedSet, out LadderElements childParallelsUnderSet);
                if (childParallelsUnderSet is null || childParallelsUnderSet.Count == 0)
                    if (value.Count() > 0) parallelElementsDictionary.Add(updatedSet, value);
                    else
                    {
                        LadderElement dummyParallelParent = childParallelsUnderSet[childParallelsUnderSet.Count - 1];
                        // Add newly created parallel contact to existing child parallels in the dictionary under selected parent contacts set
                        childParallelsUnderSet.AddRange(value);
                        parallelElementsDictionary[updatedSet] = childParallelsUnderSet;
                        value[0].Position.RelateTo.AddRange(updatedSet);
                        //dummyParallelParent.Elements.AddRange(value);
                    }
            }
        }
        public void RemoveContactFromSuperSets(LadderElement selectedContact, List<LadderElement> replaceWithContact = null)
        {
            // Create a hashset of selected contact to be used for matching keys (supersets) in parallel elements dictionary
            var setToBeSearched = new HashSet<LadderElement>() { selectedContact };

            // Prepare a list of all matching super sets (if any) of selectedContact 
            List<HashSet<LadderElement>> listOfSuperSetsToUpdate = new List<HashSet<LadderElement>>();
            List<HashSet<LadderElement>> listOfDirectChildren = new List<HashSet<LadderElement>>();
            foreach (var set in parallelElementsDictionary.Keys)
                if (set.IsProperSupersetOf(setToBeSearched))
                    listOfSuperSetsToUpdate.Add(set);

            foreach (var setToUpdate in listOfSuperSetsToUpdate)
            {
                var value = parallelElementsDictionary[setToUpdate];
                value.Sort((x, y) => x.Position.X.CompareTo(y.Position.X));
                parallelElementsDictionary.Remove(setToUpdate);
                var listOfSetElements = setToUpdate.ToList();
                listOfSetElements.Remove(selectedContact);
                if (replaceWithContact != null)
                {
                    listOfSetElements.AddRange(replaceWithContact);
                }
                // Sort the list as per X coordinate of each element, such that element with lowest X comes at 0th position
                listOfSetElements.Sort((x, y) => x.Position.X.CompareTo(y.Position.X));

                // Prpare the key (set) to update from above list of elements
                var updatedSet = new HashSet<LadderElement>(listOfSetElements);

                // For each element in parallels list update RelateTo
                foreach (var parallelElement in value)
                {
                    parallelElement.Position.RelateTo.Clear();
                    parallelElement.Position.RelateTo.AddRange(updatedSet);

                    foreach (var contactInSeries in parallelElement.Position.ConnectTo)
                    {
                        parallelElement.Position.RelateTo.Clear();
                        parallelElement.Position.RelateTo.AddRange(updatedSet);
                    }
                }
                // Finally update the dictionary by adding updatedSet as key with Old Key's value
                parallelElementsDictionary.TryGetValue(updatedSet, out LadderElements childParallelsUnderSet);
                if (!parallelElementsDictionary.ContainsKey(updatedSet))
                {
                    if (value.Count() > 0) parallelElementsDictionary.Add(updatedSet, value);
                }
                else
                {
                    LadderElement dummyParallelParent = childParallelsUnderSet[childParallelsUnderSet.Count - 1];
                    // Add newly created parallel contact to existing child parallels in the dictionary under selected parent contacts set
                    if (updatedSet.Count() > 1)
                    {
                        while (dummyParallelParent.customDrawing.toString() != "DummyParallelParent")
                        {
                            dummyParallelParent = dummyParallelParent.Position.Parent;
                        }
                    }
                    childParallelsUnderSet.AddRange(value);
                    int height = childParallelsUnderSet.Max(x => x.Position.Y);
                    parallelElementsDictionary[updatedSet] = childParallelsUnderSet;
                    //value[0].Position.Y = height + ControlHeight;
                    value[0].Position.RelateTo.Clear();
                    ///value[0].Position.Height = dummyParallelParent.Position.Height + (ControlHeight / 2);
                    value[0].Position.RelateTo.AddRange(updatedSet);
                    //DecreaseHeightOfSuperSets(updatedSet, value[0]);
                    LadderElement checkDummyParallel = dummyParallelParent;
                    foreach (var parallelElement in value)
                    {
                        List<LadderElement> connectedElements = parallelElement.Position.ConnectTo;
                        if (updatedSet.Count != 1)
                        {
                            while (checkDummyParallel.customDrawing.toString() != "DummyParallelParent")
                            {
                                checkDummyParallel = checkDummyParallel.Position.Parent;
                            }
                        }
                        checkDummyParallel.Position.Parent.Elements.Remove(parallelElement);
                        if (parallelElement.Position.Parent.customDrawing.toString() == "DummyParallelParent")
                        {
                            parallelElement.Position.Parent.Elements.Remove(parallelElement);
                            if (parallelElement.Position.Parent.Elements.Count() == 0)
                                parallelElement.getRoot().Elements.Remove(parallelElement.Position.Parent);
                        }
                        parallelElement.Position.Parent = dummyParallelParent;
                        if (updatedSet.Count() == 1)
                        {
                            //dummyParallelParent.Elements.Clear();
                            parallelElement.Position.Y = ControlHeight;
                        }
                        foreach (LadderElement connection in connectedElements)
                        {
                            if (!parallelElement.Elements.Contains(connection))
                                parallelElement.Elements.Add(connection);
                        }
                        if (!dummyParallelParent.Elements.Contains(parallelElement))
                            dummyParallelParent.Elements.Add(parallelElement);
                        //stacklist.Push(parallelElement);
                        dummyParallelParent = parallelElement;
                    }
                    IncreaseHeightOfSuperSets(updatedSet, value[0]);
                }
            }
        }

        public LadderElement InsertContactParallelNew(ref LadderElement clickedElement, ref List<LadderElement> selectedElements)
        {
            if (clickedElement == null || Elements.Count <= 0)
                return null;
            if (LadderDesign.ClickedElement.customDrawing.GetType() != typeof(Contact))  //----> Checks for Undo Redo
                return null;
            LadderElement dummyParallelParent = null;
            LadderElement newParallelContact = new LadderElement();
            newParallelContact.BackgroundColor = Color.White;
            if (LadderDesign.Active.parallelParent != null)
            {
                newParallelContact.Attributes["caption"] = LadderDesign.Active.parallelParent.ToString();
            }
            else
                newParallelContact.Attributes["caption"] = Debugger.IsAttached ? NextParallelCounter() : "???";
            // If there are no multiple selected elements, then set the clicked element as the selected element
            if (selectedElements.Count == 0)
                selectedElements.Add(clickedElement);
            selectedElements.Sort((x, y) => x.Position.X.CompareTo(y.Position.X));
            var hashSetOfSelectedContacts = new HashSet<LadderElement>(selectedElements);
            // From the dictionary, see if there are any parallels already under set of selected contacts
            parallelElementsDictionary.TryGetValue(hashSetOfSelectedContacts, out LadderElements childParallelsUnderSet);
            if (childParallelsUnderSet != null && childParallelsUnderSet.Count > 0)
            {
                newParallelContact.CreateCustom(new Contact(), 0, 10, ControlWidth, ControlHeight);
                //newParallelContact.Position.Height += GetMaxHeightOfParallelsUnderSet(hashSetOfSelectedContacts);
                newParallelContact.Position.Y = newParallelContact.Position.Height; // = GetYofTopmostSubSet(hashSetOfSelectedContacts);
                // Set the last Parallel in hierarchy under this set as dummy Parent for this new Parallel.
                dummyParallelParent = childParallelsUnderSet[childParallelsUnderSet.Count - 1];
                // Add newly created parallel contact to existing child parallels in the dictionary under selected parent contacts set
                childParallelsUnderSet.Add(newParallelContact);
                parallelElementsDictionary[hashSetOfSelectedContacts] = childParallelsUnderSet;
            }
            else // This set is not yet added to the dictionary. Let's add it with thid new parallel under it.
            {
                newParallelContact.CreateCustom(new Contact(), selectedElements[0].Position.X, 10, ControlWidth, ControlHeight);
                newParallelContact.Position.Y = GetYofTopmostSubSet(hashSetOfSelectedContacts) + newParallelContact.Position.Height;
                // Add this new Parallel Contact to dictionary of Sets.
                LadderElements ladderElements = new LadderElements() { newParallelContact };
                parallelElementsDictionary.Remove(hashSetOfSelectedContacts);
                parallelElementsDictionary.Add(hashSetOfSelectedContacts, ladderElements);
                // Create a Dummy parent for this parallel and add it to Rung.
                dummyParallelParent = CreateDummyParallelParent(selectedElements);
                clickedElement.Position.Parent.Elements.Insert(clickedElement.Position.Index, dummyParallelParent);
            }
            newParallelContact.Position.RelateTo.AddRange(hashSetOfSelectedContacts);
            // Add the newly created parallel as a child of dummy parent
            dummyParallelParent.Elements.Add(newParallelContact);
            IncreaseHeightOfSuperSets(hashSetOfSelectedContacts, newParallelContact);
            selectedElements.Clear();
            AdjustControls(clickedElement.getRoot().Elements, StartX);
            //Adding "isCommented" Attribute when parent is Commented
            foreach (Attribute attribute in clickedElement.Attributes)
            {
                if (attribute.Name == "isCommented")
                {
                    Attribute attribute1 = new Attribute();
                    attribute1.Name = "isCommented";
                    newParallelContact.Attributes.Add(attribute1);
                }
            }
            return newParallelContact;
        }
        private void IncreaseHeightOfSuperSets(HashSet<LadderElement> hashSetOfSelectedContacts, LadderElement newParallelContact)
        {
            int maxYOfParallelsUnderSubSet = newParallelContact.getY();

            // Prepare a list of all super sets 
            List<HashSet<LadderElement>> listOfSuperSets = new List<HashSet<LadderElement>>();
            foreach (var set in parallelElementsDictionary.Keys)
                if (set.IsProperSupersetOf(hashSetOfSelectedContacts))
                    listOfSuperSets.Add(set);
            // Sort the list of supersets as per order of superiority i.e. set with max no. of contacts is the most superior
            listOfSuperSets.Sort((x, y) => x.Count.CompareTo(y.Count));
            foreach (var set in listOfSuperSets)
            {
                parallelElementsDictionary.TryGetValue(set, out LadderElements parallelsUnderSet);
                if (parallelsUnderSet != null && parallelsUnderSet.Count > 0)
                {
                    // If our super set's 1st element->Y is already greater than max Y of it's subset then no need to increase Y coordinates any further
                    if (parallelsUnderSet[0].getY() > maxYOfParallelsUnderSubSet)
                        break;
                    else
                        parallelsUnderSet[0].Position.Y += ControlHeight;
                    // Finally set maxY according to this superset's updated Y positions
                    maxYOfParallelsUnderSubSet = parallelsUnderSet[parallelsUnderSet.Count - 1].getY();
                }
            }
        }
        public Dictionary<HashSet<LadderElement>, LadderElements> GetDict()
        {
            return parallelElementsDictionary;
        }
        #endregion
        //designer functions
        public LadderElement InsertBlankRung()
        {
            LadderElement rungDiv = new LadderElement();
            int newy = 0;
            if (Elements.Count > 0)
                newy = (int)(Elements[Elements.Count - 1].getY() + (Elements[Elements.Count - 1].getHeight() * 1.4));
            rungDiv.CreateCustom(new Rung(), 0, newy, 0, 0);
            LadderElement elementComment = new LadderElement();
            elementComment.BackgroundColor = Color.White;
            elementComment.Attributes["caption"] = "Comments";
            elementComment.CreateCustom(new Comment(), 0, 0, 1000, 10);
            LadderElement element1 = new LadderElement();
            element1.BackgroundColor = Color.White;
            element1.Attributes["caption"] = "BlankLine";
            element1.CreateCustom(new BlankLine(), 0, StartY, 800, 50);
            rungDiv.Elements.Add(element1);
            rungDiv.Elements.Add(elementComment);
            Elements.Add(rungDiv);
            return rungDiv;
        }
        public LadderElement InsertNewBlankRung(LadderElement ele)
        {
            LadderElement rungDiv = new LadderElement();
            int newy = 0;
            if (Elements.Count > 0)
                newy = (int)(Elements[Elements.Count - 1].getY() + (Elements[Elements.Count - 1].getHeight() * 1.4));
            rungDiv.CreateCustom(new Rung(), 0, newy, 0, 0);
            LadderElement elementComment = new LadderElement();
            elementComment.BackgroundColor = Color.White;
            elementComment.Attributes["caption"] = "Comments";
            elementComment.CreateCustom(new Comment(), 0, 0, 1000, 10);
            rungDiv.Elements.Add(elementComment);
            Elements.Add(rungDiv);
            return rungDiv;
        }
        public int IsBlankLineSelected(LadderElement element)
        {
            if (element == null)
                return -1;
            if (element.Type == LadderDrawingTypes.CustomDrawing)
            {
                if (element.customDrawing.GetType() == typeof(BlankLine))
                {
                    return element.Position.Index;
                }
            }
            return -1;
        }
        public void ClearBlankLine(LadderElement element)
        {
            if (element == null)
                return;
            element.getRoot().Elements.Remove(element);
        }
        private bool ValidateUDFBEditPermission()
        {
            string currentScreenName = xm.CurrentScreen.Split('#')[1];

            if (!currentScreenName.EndsWith(" logic", StringComparison.OrdinalIgnoreCase))
                return true; // Allow editing for non-UDFB screens

            string normalizedNode = currentScreenName.Replace(" Logic", "").Trim();
            string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MessungSystems", "XMPS2000", "Library");
            string librarySubFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD", StringComparison.OrdinalIgnoreCase)
                ? "XBLDLibraries"
                : "XMLibraries";
            string libraryPath = Path.Combine(basePath, librarySubFolder);
            string[] csvFiles = Directory.Exists(libraryPath) ? Directory.GetFiles(libraryPath, "*.csv") : Array.Empty<string>();
            List<string> udfbNames = XMPS.Instance.LoadedProject.UDFBInfo.Select(u => u.UDFBName).ToList();

            var fileNames = csvFiles.Select(Path.GetFileNameWithoutExtension)
                .Select(name => name.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase)
                    ? name.Substring(0, name.Length - 6).Trim()
                    : name);

            bool isUdfbMatch = fileNames.Any(fileName => fileName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase) &&
                            udfbNames.Any(udfbName => udfbName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase)));

            if (isUdfbMatch)
            {
                string savedChoice = XMPS.Instance.LoadedProject.GetUDFBChoice(normalizedNode);

                if (string.IsNullOrEmpty(savedChoice))
                {
                    // Show message box when user hasn't made a choice before
                    MessageBox.Show($"UDFB '{normalizedNode}' is a library function. Please configure UDFB edit preferences first.",
                                  "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;
        }
        public LadderElement InsertRung(int Rungno = 0)
        {
            if (!ValidateUDFBEditPermission())
                return null;
            ///<>
            ///Added Check for the Adding only 10 Rungs in Interrupt Logic Block
            ///
            if (m_Height_dic.Count > 0 && (65535 - m_Height_dic.Last().Key) < 500)
            {
                MessageBox.Show("Maximum Limit of Rung is reached", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            string currentScreenName = xm.CurrentScreen.Split('#')[1];           
            if (currentScreenName.StartsWith("Interrupt_Logic_Block") && Elements.Count >= 10)
            {
                MessageBox.Show("Maximum Limit of Rung For Interrupt Block Exceed", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            LadderElement rungDiv = new LadderElement();
            int newy = 0;
            if (Elements.Count > 0)
                newy = (int)(Elements[Elements.Count - 1].getY() + (Elements[Elements.Count - 1].getHeight())) - 30;
            rungDiv.CreateCustom(new Rung(), 0, newy, 0, 0);
            LadderElement elementComment = new LadderElement();
            elementComment.BackgroundColor = Color.White;
            elementComment.Attributes["caption"] = "Comments";
            elementComment.CreateCustom(new Comment(), 0, 0, 1000, 10);
            LadderElement element1 = new LadderElement();
            element1.BackgroundColor = Color.White;
            element1.Attributes["caption"] = Debugger.IsAttached ? NextContactCounter() : "???";
            element1.CreateCustom(new Contact(), StartX, StartY, ControlWidth, ControlHeight);
            LadderElement elementCoil = new LadderElement();
            elementCoil.BackgroundColor = Color.White;
            elementCoil.Attributes["caption"] = "???";
            elementCoil.CreateCustom(new Coil(), 800, StartY, ControlWidth, ControlHeight);
            rungDiv.Elements.Add(elementComment);
            rungDiv.Elements.Add(element1);
            rungDiv.Elements.Add(elementCoil);
            if (Rungno == 0)
                Elements.Add(rungDiv);
            else
                Elements.Insert(Rungno, rungDiv);
            PopulateHeightList();
            //Adding for the EasyInstruction for storing the latest added Rung 
            LadderDesign.CurrentAddedElement = rungDiv;
            //set the Rung scroll position
            //currentRungScroll = Rungno == 0 ?  (Elements.Count() > 0 ?
            //                    m_Height_dic.Last().Key - 450
            //                    : rungDiv.Position.Y)
            //                    : Elements.Count() > 2 ? m_Height_dic.Count() < Rungno + 1 ? m_Height_dic.ElementAt(Rungno+1).Key -60 : m_Height_dic.ElementAt(Rungno).Key - 60
            //                    : m_Height_dic.ElementAt(Rungno).Key - 60;
            PopulateHeightList();
            if (Rungno == 0)
            {
                currentRungScroll = Elements.Any()
                    ? m_Height_dic.Last().Key - 450
                    : rungDiv.Position.Y;
            }
            else
            {
                if (Elements.Count() > 2)
                {
                    currentRungScroll = (m_Height_dic.Count() > Rungno + 1)
                        ? m_Height_dic.ElementAt(Rungno + 1).Key - 30
                        : m_Height_dic.ElementAt(Rungno).Key - 30;
                }
                else
                {
                    currentRungScroll = m_Height_dic.ElementAt(Rungno).Key - 60;
                }
            }

            if (Rungno != 0)
                LadderDesign.ClickedElement = null;
            return rungDiv;
        }
        public LadderElement InsertNewBlock(string BlockName, int Rungno = 0)
        {           
            ///<>
            ///Added Check for the Adding only 10 Rungs in Interrupt Logic Block
            string currentScreenName = xm.CurrentScreen.Split('#')[1];
            LadderElement rungDiv = new LadderElement();
            int newy = 0;
            if (Elements.Count > 0)
                newy = (int)(Elements[Elements.Count - 1].getY() + (Elements[Elements.Count - 1].getHeight()));
            rungDiv.CreateCustom(new Rung(), 0, newy, 0, 0);
            LadderElement elementLadderBlock = new LadderElement();
            elementLadderBlock.BackgroundColor = Color.White;
            elementLadderBlock.Attributes["caption"] = BlockName;
            //check if Logical Block is commented or not
            if (BlockName.StartsWith("'"))
                elementLadderBlock.Attributes["isCommented"] = "";
            Rungno = 0;
            elementLadderBlock.CreateCustom(new LadderBlock(), 50, StartY, 600, ControlHeight);
            rungDiv.Elements.Add(elementLadderBlock);
            if (Rungno == 0)
                Elements.Add(rungDiv);
            else
                Elements.Insert(Rungno, rungDiv);
            PopulateHeightList();
            return elementLadderBlock;
        }
        public LadderElement InsertCoil()
        {
            if (LadderDesign.ClickedElement == null)
                return null;
            if (LadderDesign.ClickedElement.customDrawing.GetType() == typeof(FunctionBlock))
                return null;
            foreach (LadderElement checkFB in LadderDesign.ClickedElement.getRoot().Elements)
            {
                if (checkFB.customDrawing.GetType() == typeof(FunctionBlock))
                    return null;
            }
            if (LadderDesign.ClickedElement != null)
            {
                if (LadderDesign.ClickedElement.customDrawing.GetType() == typeof(Coil) || LadderDesign.ClickedElement.customDrawing.GetType() == typeof(CoilParallel))
                {
                    LadderElement clicked = LadderDesign.ClickedElement;
                    ////Get the last parallel coil so that new parallel coil can get added after that
                    while (clicked.Elements.Count > 0)
                        clicked = clicked.Elements[0];
                    LadderElement elementCoil1 = new LadderElement();
                    elementCoil1.BackgroundColor = Color.White;
                    elementCoil1.Attributes["caption"] = Debugger.IsAttached ? NextContactCounter() : "???";
                    elementCoil1.CreateCustom(new CoilParallel(), 0, 30, ControlWidth, ControlHeight);
                    elementCoil1.Position.Y = elementCoil1.Position.Height + 30;
                    clicked.Elements.Add(elementCoil1);
                    return elementCoil1;
                }
                LadderElement rungDiv = LadderDesign.ClickedElement.getRoot();
                bool foundcoil = false;
                for (int x = 0; x < rungDiv.Elements.Count; x++)
                {
                    if (rungDiv.Elements[x].customDrawing.GetType() == typeof(LadderDrawing.Coil))
                    {
                        foundcoil = true;
                    }
                }
                if (foundcoil)
                    return null;
                LadderElement elementCoil = new LadderElement();
                elementCoil.BackgroundColor = Color.White;
                elementCoil.Attributes["caption"] = "???";
                LadderElement lastelement = GetLastElement(rungDiv);
                elementCoil.CreateCustom(new Coil(), lastelement.getX() + lastelement.Position.Width, StartY, ControlWidth, ControlHeight);
                rungDiv.Elements.Add(elementCoil);
                if (IsBlankLineSelected(LadderDesign.ClickedElement) == 0)
                    ClearBlankLine(LadderDesign.ClickedElement);
                return elementCoil;
            }
            return null;
        }
        /// <summary>
        /// User can add contact on Rung line or in Parallel line by selecting contact or contact parallel 
        /// </summary>
        /// <param name="element"> element is the selected or clicked element  </param> 
        /// <returns></returns>
        public LadderElement InsertContactBefore(ref LadderElement element)
        {
            if (IsBlankLineSelected(LadderDesign.ClickedElement) == 0)
                ClearBlankLine(LadderDesign.ClickedElement);
            if (element != null)
            {
                if (ClickedElement.Position.Parent.customDrawing.GetType() == typeof(Contact) && ClickedElement.Position.RelateTo.Count == 0)
                {
                    element = ClickedElement.Position.Parent;
                }
                ///Check if the selected component supports insertion of Contact Before it
                Type typeOf = element.customDrawing.GetType();
                bool isTypeOf = (typeOf == typeof(Contact) || typeOf == typeof(FunctionBlock) || typeOf == typeof(LadderBlock) || typeOf == typeof(BlankLine) || typeOf == typeof(Coil));
                if (Elements.Count > 0 && element != null && isTypeOf && ClickedElement.Position.RelateTo.Count == 0 && ClickedElement.Position.Parent.Position.RelateTo.Count == 0)
                {
                    LadderElement SelectedElement = element;
                    LadderElement element1 = new LadderElement();
                    element1.BackgroundColor = Color.White;
                    element1.Attributes["caption"] = "???";
                    int startx = 0;
                    int starty = 0;
                    startx = StartX;
                    ///Default height of Rung is set to y axis so that control can get drawn excatly on the same line
                    starty = StartY;
                    ///Logic for addition of Normal Contact After slected element
                    element1.CreateCustom(new Contact(), startx, starty, ControlWidth, ControlHeight);
                    if (element.customDrawing.GetType() == typeof(FunctionBlock) || element.customDrawing.GetType() == typeof(Coil))
                    {
                        element1.Position.X = GetXofLastElement(element) + ControlWidth;
                    }
                    else
                    {
                        element1.Position.X = element.Position.X;
                    }
                    element.Position.Parent.Elements.Insert(element.Position.Index, element1);
                    int xRow = startx;
                    AdjustControls(element.getRoot().Elements, xRow);
                    AddContactToSuperSets(element, element1);
                    return element1;
                }
            }
            return null;
        }
        public void NegateStyle(ref LadderElement element)
        {
            if (element == null)
                return;
            if (IsBlankLineSelected(LadderDesign.ClickedElement) == 0)
                return;
            if (ClickedElement.customDrawing.GetType() != typeof(Contact))
            {
                return;
            }
            int _pnStatusLength = element.Attributes["PNStatus"].ToString().Length;

            if (element != null && _pnStatusLength == 0)
            {
                element.Negation = !element.Negation;
            }
        }
        /// <summary>
        /// User can add Contact after selected contact on the Rung or on the Parallel contact
        /// </summary>
        /// <param name="element"> element is the selected element  </param>
        /// <returns></returns>
        public LadderElement InsertContactAfter(ref LadderElement element, bool addToSuperSet = true)
        {           
            if (element != null)
            {
                ///Check if the selected item is contact and is in series with a parallel contact 
                if (ClickedElement.Position.RelateTo.Count == 0 && ClickedElement.Position.Parent.Position.RelateTo.Count > 0)
                {
                    element = ClickedElement.Position.Parent;
                }
                ///Check if the selected component supports insertion of Contact After it
                Type typeOf = element.customDrawing.GetType();
                bool isTypeOf = (typeOf == typeof(Contact) || element.Position.RelateTo.Count > 0);
                if (Elements.Count > 0 && element != null && isTypeOf)
                {
                    if (IsBlankLineSelected(LadderDesign.ClickedElement) == 0)
                        ClearBlankLine(LadderDesign.ClickedElement);
                    LadderElement clickedElement = element;
                    LadderElement element1 = new LadderElement();
                    element1.BackgroundColor = Color.White;
                    ///Default Test 
                    if (LadderDesign.Active.contactConnectTo != null)
                        element1.Attributes["caption"] = LadderDesign.Active.contactConnectTo.ToString();
                    else
                        element1.Attributes["caption"] = Debugger.IsAttached ? NextContactCounter() : "???";
                    int startx = StartX;
                    ///Default height of Rung is set to y axis so that control can get drawn excatly on the same line
                    int starty = StartY;
                    int index = clickedElement.Position.Index + 1;
                    LadderElement elementAfterParent = null;
                    LadderElement parentElementOnRung = null;
                    ///Find Root of the Parallel to shift x axis of contact which is next to the parent of this parallel to generate some space in between
                    LadderElement root = clickedElement.Position.Parent.getRoot();
                    ///Check if selected contact is parallel contact in that case we need to do few more changes
                    if (element.Position.RelateTo.Count > 0)
                    {
                        // Reset x and y so that upcomming contact will get drawn in front of selected one
                        starty = 0;
                        startx = 0;
                        ///Draw the contact with provided parameters
                        element1.CreateCustom(new Contact(), 0, starty, ControlWidth, ControlHeight);
                        ///Add this contact in Connect To attriute of parallel contact so that we can draw things with this respect
                        clickedElement.Position.ConnectTo.Add(element1);
                        ///Insert drown elemennt in clicked elements 
                        clickedElement.Elements.Add(element1);
                        ///Get next element to the parent 
                        parentElementOnRung = root.Elements[clickedElement.Position.RelateTo[clickedElement.Position.RelateTo.Count - 1].Position.Index];
                        if (clickedElement.Position.RelateTo.Count <= clickedElement.Position.ConnectTo.Count && addToSuperSet)
                        {
                            int nextElementIndex = parentElementOnRung.Position.Index + 1;
                            do
                                elementAfterParent = root.Elements[nextElementIndex++];
                            while (elementAfterParent.customDrawing.GetType() == typeof(DummyParallelParent) && root.Elements.Count > nextElementIndex);
                        }
                        if (GetOtherMaxConnect(clickedElement) < clickedElement.Position.ConnectTo.Count) //
                        {
                            int parentXRow = StartX;
                            /////Make space to add Horizontal line to adjust the width of overall contacts
                            LadderElements rootElements = root.Elements;
                            ///Adjust x axis of element next to parent element 
                            for (int ee = 0; ee < rootElements.Count; ee++)
                            {
                                if (rootElements[ee] == elementAfterParent)
                                    parentXRow += (rootElements[ee].Position.Width);
                                if (rootElements[ee].Position.X < parentXRow && rootElements[ee].customDrawing.toString() != "DummyParallelParent")
                                    rootElements[ee].Position.X = parentXRow;
                                parentXRow += rootElements[ee].Position.Width;
                            }
                            ///Draw the Horizontal Line with provided parameters
                            if (clickedElement.Position.RelateTo.Count <= clickedElement.Position.ConnectTo.Count)
                            {
                                LadderElement element2 = new LadderElement();
                                element2.CreateCustom(new HorizontalLine(), parentElementOnRung.getX() + (ControlWidth), parentElementOnRung.Position.Y, ControlWidth, ControlHeight); //root.Elements[GetParentIndex(clickedElement)].getY()
                                root.Elements.Insert(parentElementOnRung.Position.Index + 1, element2);
                            }
                            ///LadderElement root = clickedElement.Position.Parent.getRoot();
                            parentXRow = StartX;
                            /////Make space to add Horizontal line to adjust the width of overall contacts
                            rootElements = root.Elements;
                            ///Adjust x axis of element next to parent element
                            for (int ee = 0; ee < rootElements.Count; ee++)
                            {
                                if (rootElements[ee] == elementAfterParent)
                                    parentXRow += (rootElements[ee].Position.Width);
                                if (rootElements[ee].Position.X < parentXRow && rootElements[ee].customDrawing.toString() != "DummyParallelParent")
                                    rootElements[ee].Position.X = parentXRow;
                                parentXRow += rootElements[ee].Position.Width;
                            }
                        }
                        ///Adjust adjecent contacts to make space for this comming contact
                        int Parallelx = clickedElement.Position.Width;
                        LadderElements connectedElements = clickedElement.Position.ConnectTo;
                        for (int ee = 0; ee < connectedElements.Count; ee++)
                        {
                            connectedElements[ee].Position.X = Parallelx;
                            Parallelx += connectedElements[ee].Position.Width;
                        }
                    }
                    else
                    {
                        if (clickedElement.getRoot().Elements.Count > index)
                        {
                            LadderElement blankLine = root.Elements[index];
                            if (!addToSuperSet)
                            {
                                while (blankLine.customDrawing.GetType() == typeof(HorizontalLine) && index < root.Elements.Count)
                                {
                                    index++;
                                    blankLine = root.Elements[index];
                                }
                            }
                            else
                            {
                                if (blankLine.customDrawing.GetType() == typeof(HorizontalLine) && index < root.Elements.Count)
                                {
                                    element.getRoot().Elements.RemoveAt(blankLine.Position.Index);
                                }
                            }
                        }
                        ///Logic for addition of Normal Contact After slected element
                        element1.CreateCustom(new Contact(), startx + ControlWidth, starty, ControlWidth, ControlHeight);
                        clickedElement.Position.Parent.Elements.Insert(index, element1);
                    }
                    //Adjust Index and x axis of all the added elements in this rung
                    int xRow = startx;
                    LadderElements elements = clickedElement.getRoot().Elements;
                    AdjustControls(elements, StartX);
                    if (addToSuperSet)
                        AddContactToSuperSets(clickedElement, element1);
                    return element1;
                }
            }
            return null;
        }
        private int GetParentIndex(LadderElement clickedElement)
        {
            if (clickedElement.Position.Parent != null &&
                clickedElement.Position.Parent.customDrawing.GetType() != typeof(DummyParallelParent) ||
                clickedElement.Position.Parent.customDrawing.GetType() == typeof(Contact))
            {
                return clickedElement.Position.Parent.Position.Index;
            }
            else
            {
                return GetParentIndex(clickedElement.Position.Parent);
            }
        }
        public LadderElement InsertFBBefore(ref LadderElement element)
        {            
            if (IsBlankLineSelected(LadderDesign.ClickedElement) == 0)
                ClearBlankLine(LadderDesign.ClickedElement);
            Type typeOf = element.customDrawing.GetType();
            bool isTypeOf = (typeOf == typeof(Contact) || typeOf == typeof(FunctionBlock));
            if (Elements.Count > 0 && element != null && isTypeOf)
            {
                LadderElement clickedElement = element;
                LadderElement element1 = new LadderElement();
                element1.BackgroundColor = Color.White;
                element1.Attributes["caption"] = "FunctionBlock";
                element1.Attributes["function_name"] = "And";
                int startx = 0;
                element1.CreateCustom(new FunctionBlock(), startx, 10, 90, 90);
                clickedElement.Position.Parent.Elements.Insert(clickedElement.Position.Index, element1);
                int xRow = StartX;
                LadderElements elements = clickedElement.Position.Parent.Elements;
                for (int ee = 0; ee < elements.Count; ee++)
                {
                    if (elements[ee].Attributes["caption"].ToString() != "???")
                    {
                        elements[ee].Position.Index = ee;
                        elements[ee].Position.X = xRow;
                        xRow += elements[ee].Position.Width;
                    }
                }
                return element1;
            }
            return null;
        }
        public LadderElement GetLastElement(LadderElement Root)
        {
            LadderElement Lastelment = null;
            int MaxX = 0;
            foreach (LadderElement ladderElement in Root.Elements)
            {
                if ((ladderElement.customDrawing.GetType() != typeof(Coil) && ladderElement.customDrawing.GetType() != typeof(Comment)) && ladderElement.Position.Parent == Root)
                {
                    if (MaxX <= (ladderElement.getX()))
                    {
                        MaxX = ladderElement.getX();
                        Lastelment = ladderElement;
                    }
                }
            }
            return Lastelment;
        }
        public LadderElement InsertFBAfter(ref LadderElement element)
        {
            if (IsBlankLineSelected(LadderDesign.ClickedElement) == 0)
                ClearBlankLine(LadderDesign.ClickedElement);
            foreach (LadderElement checkFB in LadderDesign.ClickedElement.getRoot().Elements)
            {
                if (checkFB.customDrawing.GetType() == typeof(FunctionBlock))
                    return null;
            }
            if (element != null)
            {
                Type typeOf = element.customDrawing.GetType();
                bool isTypeOf = (typeOf == typeof(Contact) || typeOf == typeof(FunctionBlock) || typeOf == typeof(BlankLine));
                LadderElement rootElement = element.getRoot();
                if (element != null && isTypeOf)
                {
                    if (typeOf != typeof(BlankLine))
                        element = GetLastElement(element.getRoot());
                    LadderElement clickedElement = element;
                    //parent, elements typeof customdesign == coil remove at elements
                    for (int i = 0; i < rootElement.Elements.Count;)
                    {
                        if (rootElement.Elements[i].customDrawing.GetType() == typeof(LadderDrawing.Coil))
                        {
                            rootElement.Elements.RemoveAt(i);
                        }
                        else
                            i++;
                    }
                    LadderElement element1 = new LadderElement();
                    element1.BackgroundColor = Color.White;
                    element1.Attributes["caption"] = "FunctionBlock";
                    element1.Attributes["function_name"] = "AND";
                    int startx = 0;
                    element1.CreateCustom(new FunctionBlock(), startx, StartY, 90, 90);
                    if (clickedElement.Position.Index > 0)
                    {
                        clickedElement.Position.Parent.Elements.Insert(clickedElement.getRoot().Elements.Count(), element1);
                    }
                    else
                    {
                        clickedElement.Position.Parent.Elements.Add(element1);
                    }
                    int xRow = StartX;
                    LadderElements elements = clickedElement.Position.Parent.Elements;
                    AdjustControls(elements, xRow);
                    clickedElement.getRoot().Position.Width += 700;
                    return element1;
                }
            }
            return null;
        }
        public static void AdjustControls(LadderElements elements, int xRow)
        {
            for (int ee = 0; ee < elements.Count; ee++)
            {
                elements[ee].Position.Index = ee;
                if (elements[ee].customDrawing != null)
                {
                    if (elements[ee].customDrawing.toString() != "DummyParallelParent" && elements[ee].customDrawing.toString() != "Comment")
                    {
                        elements[ee].Position.X = xRow;
                        xRow += elements[ee].Position.Width;
                    }
                }
            }
        }
        //end designer functions
        public void Render(Graphics graphics, int yaxis)
        {
            Window = graphics;

            if (XMPS.Instance.PlcStatus != "LogIn")
            {
                graphics.Clear(Color.White);
            }
            graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(0, 0), new Size(10, Position.Height)));
            // QuickRender - Only render the rungs visible on the screen (also covers online monitoring)
            List<LadderElement> drawElements = GetVisibleRungs(yaxis);
            foreach (LadderElement _element in drawElements)
            {
                _element.Render();
            }
        }

        public void ApplyTable(int[] columns)
        {
            int x = 0;
            int y = 0;
            int maxY = 0;
            int index = 0;
            int useindex = 0;
            for (int i = 0; i < columns.Length; i++)
            {
                if (useindex >= Elements.Count)
                    break;
                if (columns[i] == -2)
                {
                    if (maxY > Elements[useindex].Position.Y + Elements[useindex].Position.Height)
                        maxY = Elements[useindex].Position.Y + Elements[useindex].Position.Height;

                    Elements[useindex].Position.X = x;
                    Elements[useindex].Position.Y = y;

                    useindex++;
                }
                if (columns[index] > 0)
                    x += columns[index];

                if (useindex >= columns.Length - 1 || columns[index] == -1)
                {
                    x = 0;
                    y += maxY;
                    if (columns[index] != -1)
                        index = 0;
                }
                index++;
            }
        }
        /// <summary>
        /// Get the maximum length of parallel contacts in this branch excluding currunt contact so that we can draw a line accordingly
        /// </summary>
        /// <param name="element"></param> current element
        /// <returns></returns>
        public static int GetOtherMaxConnect(LadderElement element)
        {
            int maxconnct = 0;
            LadderElement BaseElement = GetBaseParent(element);
            /////Get base element on the rung to check for maximum added contacts under that
            while (BaseElement.Elements.Count > 0)
            {
                for (int cnt = 0; cnt < BaseElement.Elements.Count; cnt++)
                {
                    if (BaseElement.Elements[cnt].Id != element.Id)
                    {
                        if (maxconnct < BaseElement.Elements[cnt].Position.ConnectTo.Count)
                            maxconnct = BaseElement.Elements[cnt].Position.ConnectTo.Count;
                    }
                }
                BaseElement = BaseElement.Elements[0];
            }
            return maxconnct;
        }
        /// <summary>
        /// Get the maximum length of parallel contacts in this branch so that we can draw a line accordingly
        /// </summary>
        /// <param name="element"></param> current element
        /// <returns></returns>
        public static int GetMaxConnect(LadderElement element)
        {
            int maxconnct = 0;
            LadderElement BaseElement = GetBaseParent(element);
            /////Get next element to the parent 
            while (BaseElement.Elements.Count > 0)
            {
                for (int cnt = 0; cnt < BaseElement.Elements.Count; cnt++)
                {
                    if (maxconnct < BaseElement.Elements[cnt].Position.ConnectTo.Count)
                        maxconnct = BaseElement.Elements[cnt].Position.ConnectTo.Count;
                }
                BaseElement = GetNextParallel(BaseElement);
            }
            return maxconnct;
        }
        public static bool CheckIfForced(string LogicalAddress)
        {
            if (XMPS.Instance.Forcedvalues.Contains(LogicalAddress))
                return true;
            else
                return false;
        }
        public void PopulateHeightList()
        {
            m_Height_dic.Clear();
            foreach (LadderElement _element in Elements)
            {
                m_Height_dic[_element.getY()] = _element;
            }
        }
        public static void PopulateAddressDic(ref object curBlockData)
        {
            BlockTagAddress.Clear();
        }
        public static List<int> GetActiveRungNo()
        {
            return ActiveRungNo;
        }
        public List<LadderElement> GetVisibleRungs(int height)
        {
            var asd = m_Height_dic.Keys.OrderBy(x => Math.Abs(x - height));
            var li = asd.Take(10);
            ActiveRungNo.Clear();
            List<LadderElement> _visibleRungs = new List<LadderElement>();
            foreach (int _elementHeight in li)
            {
                LadderElement firstElement = m_Height_dic[_elementHeight].Elements.Where(T => !T.CustomType.Equals("LadderDrawing.Comment")).FirstOrDefault();
                if (firstElement != null && !firstElement.Attributes.Where(T => T.Name.Equals("isCommented")).Any())
                {
                    ActiveRungNo.Add(m_Height_dic[_elementHeight].Position.Index);
                }
                _visibleRungs.Add(m_Height_dic[_elementHeight]);
            }
            return _visibleRungs;
        }
        private static LadderElement GetNextParallel(LadderElement baseElement)
        {
            int maxYElement = baseElement.Elements.Max(y => y.Position.Y);
            return baseElement.Elements.Where(M => M.Position.Y == maxYElement).FirstOrDefault();
        }
        /// <summary>
        /// Get the element on Rung to check all parallel elements under this base contact
        /// </summary>
        /// <param name="clickedElement"></param> Element on which working now
        /// <returns></returns>
        private static LadderElement GetBaseParent(LadderElement clickedElement)
        {
            if (clickedElement.Position.Parent.customDrawing.GetType() != typeof(DummyParallelParent) && clickedElement.Position.Parent.customDrawing.GetType() != typeof(Rung) && clickedElement.Position.Parent.Position.Parent.customDrawing.GetType() != typeof(Rung))
            {
                return GetBaseParent(clickedElement.Position.Parent);
            }
            return clickedElement.Position.Parent;
        }
        /// <summary>
        /// Update values in dictionary to tackle deleting of any of the keys
        /// </summary>
        /// <param name="hashSetOfContactToBeDeleted"></param> contact deleted by user
        /// <param name="firstChildParallel"></param> First child of that contact
        /// <param name="childParallels"></param> All childs which are following the first contact
        internal void UpdateDictionary(HashSet<LadderElement> hashSetOfContactToBeDeleted, LadderElement firstChildParallel, LadderElements childParallels)
        {
            var newParents = new List<LadderElement>() { firstChildParallel };
            foreach (LadderElement connectedChild in firstChildParallel.Position.ConnectTo)
            {
                newParents.Add(connectedChild);
            }
            newParents.Sort((x, y) => x.Position.X.CompareTo(y.Position.X));
            HashSet<LadderElement> newParentHashSet = new HashSet<LadderElement>(newParents) { };
            // Remove the dictionary item that belongs to the cotact being deleted
            parallelElementsDictionary.Remove(hashSetOfContactToBeDeleted);
            // And replace the above by adding a new item with new parent under the topmost child from hierarchy under cotact being deleted
            if (childParallels.Count() > 0) parallelElementsDictionary.Add(newParentHashSet, childParallels);
            for (int i = 0; i < childParallels.Count; i++)
            {
                childParallels[i].Position.RelateTo.Clear();
                childParallels[i].Position.RelateTo.AddRange(newParentHashSet);
            }
        }
        public void DeleteSelectedControl(bool isdeleted)
        {
            if (!ValidateUDFBEditPermission())
                return;
            if (LadderDesign.ClickedElement == null)
                return;
            LadderElement selectedElement = LadderDesign.ClickedElement;
            LadderElement selectedRoot = LadderDesign.ClickedElement.getRoot();
            bool removeFromDictionary = false;
            if (selectedElement == null)
            {
                return;
            }
            var hashSetOfSelectedContacts = new HashSet<LadderElement>() { selectedElement };
            parallelElementsDictionary.TryGetValue(hashSetOfSelectedContacts, out LadderElements childParallels);
            parallelElementsDictionary.ContainsKey(hashSetOfSelectedContacts);
            if (childParallels != null && childParallels.Count > 0)
            {
                RemoveDictionaryReferances(selectedElement, childParallels, hashSetOfSelectedContacts);
                removeFromDictionary = true;
            }
            else if (childParallels == null)
            {
                ///Check for all dictionaries where this contact is present in key
                var keysFound = parallelElementsDictionary.Keys.Where(x => x.Contains(selectedElement));
                hashSetOfSelectedContacts.Clear();
                if (keysFound.Count() > 0 && keysFound.First().Count() > 0)
                {
                    foreach (LadderElement keyElement in keysFound.First())
                    {
                        hashSetOfSelectedContacts.Add(keyElement);
                    }
                }
                parallelElementsDictionary.TryGetValue(hashSetOfSelectedContacts, out childParallels);
                ///Delete the element from all keys and update the superset
                if (childParallels != null && childParallels.Count > 0)
                {
                    int maxConnect = GetMaxConnect(childParallels[0]) + 1;
                    int maxOtherConnect = GetOtherMaxConnect(childParallels[0]) + 1;
                    int maxParents = GetMaxWidthOfCompleteParentSet(selectedElement);
                    int maxConnectAllParents = GetMaxWidthOfOuterCompleteParentSet(selectedElement);
                    if (maxConnect < maxConnectAllParents) maxConnect = maxConnectAllParents;
                    if (maxOtherConnect > maxConnect) maxConnect = maxOtherConnect;
                    if (maxConnect > (maxParents / ControlWidth) - 1)
                    {
                        int nextX = selectedElement.getX(), nextIndex = selectedElement.Position.Index;
                        hashSetOfSelectedContacts.Remove(selectedElement);
                        foreach (LadderElement nextLadderElement in hashSetOfSelectedContacts)
                        {
                            if (nextLadderElement.Position.X > selectedElement.Position.X)
                            {
                                nextLadderElement.Position.X = nextX;
                                nextLadderElement.Position.Index = nextIndex;
                                nextX += ControlWidth;
                                nextIndex++;
                            }
                        }
                        LadderElement element2 = new LadderElement();
                        if (selectedRoot.Elements[nextIndex].customDrawing.toString() == "DummyParallelParent")
                            nextIndex++;
                        element2.CreateCustom(new HorizontalLine(), nextX, selectedElement.Position.Y, ControlWidth, ControlHeight); //root.Elements[GetParentIndex(clickedElement)].getY()
                        selectedRoot.Elements.Insert(nextIndex + 1, element2);
                    }
                    RemoveContactFromSuperSets(selectedElement);
                    removeFromDictionary = true;
                }
            }
            if ((selectedElement.customDrawing.GetType() == typeof(Contact) || selectedElement.customDrawing.GetType() == typeof(FunctionBlock) || selectedElement.customDrawing.GetType() == typeof(Coil)) && selectedElement.Position.Parent.customDrawing.GetType() == typeof(Rung))
            {
                LadderElement checkedElement = LadderDesign.ClickedElement;
                String funName = LadderDesign.ClickedElement.Attributes["function_name"].ToString();
                //retentive function block is deleted and that deleted retentive timer is not present in another function block then delete retentive address and unchech checkbox 
                if (funName.Contains("RTON"))
                {
                    var countOfRetentiveAdd = 0;
                    var Output2 = LadderDesign.ClickedElement.Attributes["output2"].ToString();
                    XMIOConfig OutputTag = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress == Output2);
                    if (OutputTag != null)
                    {
                        countOfRetentiveAdd++;
                    }
                    var isthere = xm.LoadedProject.LogicRungs.ToList();
                    foreach (var i in isthere)
                    {
                        if (i.DataType_Nm == "RTON")
                        {
                            var output2 = LadderDesign.ClickedElement.Attributes["output2"].ToString();
                            XMIOConfig outputTag = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress == Output2);
                            if (outputTag != null)
                            {
                                countOfRetentiveAdd++;
                            }
                        }
                    }
                    if (countOfRetentiveAdd == 1)
                    {
                        OutputTag.Retentive = false;
                        OutputTag.RetentiveAddress = null;
                    }
                }
                //to make retentive tag false when function block is deleted
                if (removeFromDictionary)
                {
                    selectedRoot.Elements.Remove(selectedElement);
                    LadderDesign.AdjustControls(selectedRoot.Elements, LadderDesign.StartX);
                    LadderDesign.ClickedElement = null;
                }
                else
                    DeleteSimpleElements();
            }
            else if ((selectedElement.customDrawing.GetType() == typeof(Contact) || selectedElement.customDrawing.GetType() == typeof(FunctionBlock) || selectedElement.customDrawing.GetType() == typeof(Coil)) && selectedElement.Position.Parent.customDrawing.GetType() != typeof(Rung))
            {
                DeleteElementsinParallel();
            }
            else if (selectedElement.customDrawing.GetType() == typeof(CoilParallel))
            {
                DeleteParallelCoil();
            }
            CheckAndDeleteBlnkDummyParallels(selectedRoot);
            if (GetactualCountofElement(selectedRoot) == 0)
                DeleteRung(selectedRoot, false);
            caretaker.SetStateForUndoRedo(this);       //Insert current Object into undo stack after delete single element
        }
        private void CheckAndDeleteBlnkDummyParallels(LadderElement selectedRoot)
        {
            List<LadderElement> listToRemove = new List<LadderElement>();
            foreach (LadderElement checkDummy in selectedRoot.Elements)
            {
                if (checkDummy.customDrawing != null)
                {
                    if (checkDummy.customDrawing.toString() == "DummyParallelParent" && checkDummy.Elements.Count == 0)         //As it has Single Element
                    {
                        listToRemove.Add(checkDummy);
                    }
                }
            }
            for (int cnt = 0; cnt < listToRemove.Count; cnt++)
                selectedRoot.Elements.Remove(listToRemove[cnt]);
        }
        private void DeleteParallelCoil()
        {
            LadderElement elemetnTobeDeleted = LadderDesign.ClickedElement;
            LadderElement mainElement = elemetnTobeDeleted.Position.Parent;
            if (elemetnTobeDeleted.Elements.Count > 0)
            {
                mainElement.Elements.AddRange(elemetnTobeDeleted.Elements);
                elemetnTobeDeleted.Elements.Clear();
            }
            mainElement.Elements.Remove(elemetnTobeDeleted);
        }
        private int GetactualCountofElement(LadderElement selectedRoot)
        {
            int cnt = 0;
            foreach (LadderElement checkElements in selectedRoot.Elements)
            {
                if (checkElements.customDrawing != null)
                {
                    if (checkElements.customDrawing.toString() == "Contact" || checkElements.customDrawing.toString() == "Coil" || checkElements.customDrawing.toString() == "FunctionBlock" || checkElements.customDrawing.toString() == "LadderBlock")
                        cnt++;
                }
            }
            return cnt;
        }
        private void DeleteElementsinParallel()
        {
            LadderElement elemetnTobeDeleted = LadderDesign.ClickedElement;
            LadderElement mainElement = elemetnTobeDeleted.Position.Parent;
            LadderElement connectedElement = new LadderElement();
            if (mainElement.Position.Parent.customDrawing.GetType() != typeof(Rung))
            {
                if (elemetnTobeDeleted.Position.RelateTo.Count == 0)
                {
                    if (GetOtherMaxConnect(elemetnTobeDeleted.Position.Parent) < elemetnTobeDeleted.Position.Parent.Position.ConnectTo.Count)
                    {
                        //Delete horizontal lines on main rung which were added to support this contact
                        if (elemetnTobeDeleted.Position.Parent.Position.RelateTo.Count <= elemetnTobeDeleted.Position.Parent.Position.ConnectTo.Count)
                        {
                            if (elemetnTobeDeleted.getRoot().Elements[elemetnTobeDeleted.Position.Parent.Position.RelateTo[elemetnTobeDeleted.Position.Parent.Position.RelateTo.Count - 1].Position.Index + 1].customDrawing.toString() == "HorizontalLine")
                            {
                                elemetnTobeDeleted.getRoot().Elements.Remove(elemetnTobeDeleted.getRoot().Elements[elemetnTobeDeleted.Position.Parent.Position.RelateTo[elemetnTobeDeleted.Position.Parent.Position.RelateTo.Count - 1].Position.Index + 1]);
                            }
                        }
                    }
                }
            }
            if (elemetnTobeDeleted.Elements.Count > 0)
            {
                LadderElement nextElement = new LadderElement();
                int maxConnect = GetMaxConnect(elemetnTobeDeleted);
                int maxOtherConnect = GetOtherMaxConnect(elemetnTobeDeleted);
                int maxParents = 0;
                if (elemetnTobeDeleted.Position.RelateTo.Count > 0)
                    maxParents = GetMaxWidthOfCompleteParentSet(elemetnTobeDeleted.Position.RelateTo[0]) / ControlWidth;
                int maxConnectAllParents = GetMaxWidthOfOuterCompleteParentSet(elemetnTobeDeleted.Position.RelateTo[0]);
                if (maxConnect < maxConnectAllParents) maxConnect = maxConnectAllParents;
                if (maxOtherConnect > maxConnect) maxConnect = maxOtherConnect;

                if (maxConnect < (maxParents / ControlWidth) - 1)
                {
                    if (elemetnTobeDeleted.getRoot().Elements[elemetnTobeDeleted.Position.RelateTo[elemetnTobeDeleted.Position.RelateTo.Count - 1].Position.Index + 1].customDrawing.toString() == "HorizontalLine")
                    {
                        elemetnTobeDeleted.getRoot().Elements.Remove(elemetnTobeDeleted.getRoot().Elements[elemetnTobeDeleted.Position.RelateTo[elemetnTobeDeleted.Position.RelateTo.Count - 1].Position.Index + 1]);
                    }
                }
                if (elemetnTobeDeleted.Position.ConnectTo.Count == 0)
                {
                    nextElement = elemetnTobeDeleted.Elements[0];
                }
                else
                {
                    nextElement = elemetnTobeDeleted.Position.ConnectTo[0];
                    connectedElement = nextElement;
                    foreach (LadderElement connElements in elemetnTobeDeleted.Position.ConnectTo)
                        connElements.Position.X -= ControlWidth;
                    nextElement.Position.ConnectTo.Clear();
                    ///Connected element is parallel and not next parallel element
                }
                nextElement.Position.RelateTo.Clear();
                elemetnTobeDeleted.Elements.Remove(nextElement);
                mainElement.Elements.Remove(elemetnTobeDeleted);
                elemetnTobeDeleted.Position.ConnectTo.Remove(nextElement);
                nextElement.Position.RelateTo = elemetnTobeDeleted.Position.RelateTo;
                nextElement.Position.Y = elemetnTobeDeleted.Position.Y;
                nextElement.Position.X = elemetnTobeDeleted.Position.X;
                nextElement.Position.Height = elemetnTobeDeleted.Position.Height;
                nextElement.Position.ConnectTo.AddRange(elemetnTobeDeleted.Position.ConnectTo);
                mainElement.Elements.Add(nextElement);
                nextElement.Elements.AddRange(elemetnTobeDeleted.Elements);
            }
            HashSet<LadderElement> hashSetOfSelectedContacts = new HashSet<LadderElement>(elemetnTobeDeleted.Position.RelateTo);
            mainElement.Elements.Remove(elemetnTobeDeleted);
            mainElement.Position.ConnectTo.Remove(elemetnTobeDeleted);
            LadderDesign.AdjustControls(mainElement.getRoot().Elements, LadderDesign.StartX);
            ///Adjust adjecent contacts to make space for this comming contact
            int Parallelx = mainElement.Position.Width;
            LadderElements connectedElements = mainElement.Position.ConnectTo;
            for (int ee = 0; ee < connectedElements.Count; ee++)
            {
                connectedElements[ee].Position.X = Parallelx;
                Parallelx += connectedElements[ee].Position.Width;
            }
            RemoveContactFromSets(elemetnTobeDeleted, connectedElement);
            if (elemetnTobeDeleted.Position.Parent.customDrawing.toString() == "DummyParallelParent" && elemetnTobeDeleted.Position.Parent.Elements.Count == 0)
            {
                mainElement.getRoot().Elements.Remove(elemetnTobeDeleted.Position.Parent);
            }
            LadderDesign.ClickedElement = null;
        }

        private void RemoveContactFromSets(LadderElement selectedContact, LadderElement connectedElement)
        {
            // Create a hashset of selected contact to be used for matching keys (supersets) in parallel elements dictionary
            var key = parallelElementsDictionary.FirstOrDefault(x => x.Value.Contains(selectedContact)).Key;
            if (key != null)
            {
                parallelElementsDictionary.TryGetValue(key, out LadderElements childValues);
                childValues.Remove(selectedContact);
                var updatedSet = new HashSet<LadderElement>(key);
                if (connectedElement != null && connectedElement.customDrawing != null) childValues.Insert(0, connectedElement);
                // Finally update the dictionary by adding updatedSet as key with Old Key's value
                parallelElementsDictionary.Remove(key);
                childValues.OrderBy(x => x.Position.Y);
                if (childValues.Count > 0)
                    parallelElementsDictionary.Add(updatedSet, childValues);
            }
        }
        public void DeleteRung(LadderElement selectedRoot, bool isDeleted)
        {
            if (!ValidateUDFBEditPermission())
                return;
            selectedRoot.Elements.Clear();
            LadderDesign.ClickedElement = null;
        }
        /// <summary>
        /// Delete dictionary entries of contact which is parent of another contact
        /// </summary>
        /// <param name="selectedElement"></param> Selected contact which is suppose to get deleted
        /// <param name="childParallels"></param> All the childs of this contact
        /// <param name="hashSetOfSelectedContacts"></param> All the set siblings of selected contact
        private void RemoveDictionaryReferances(LadderElement selectedElement, LadderElements childParallels, HashSet<LadderElement> hashSetOfSelectedContacts)
        {
            LadderElement firstChildParallel = childParallels[0];
            LadderElement root = selectedElement.getRoot();
            List<LadderElement> replaceKeywith = new List<LadderElement>();
            if (firstChildParallel != null)
            {
                // Remove the firstChildParallel from child Elements of dummyParallelParent
                var dummyParallelParent = firstChildParallel.Position.Parent;
                if (dummyParallelParent != null) dummyParallelParent.Elements.Remove(firstChildParallel);
                replaceKeywith.Add(firstChildParallel);
                // Set dummy parallel parent as parent of the first child of firstChildParallel
                if (firstChildParallel.Elements.Count > 0)
                {
                    if (firstChildParallel.Position.ConnectTo.Count > 0)
                    {
                        foreach (LadderElement childs in firstChildParallel.Elements)
                        {
                            if (!firstChildParallel.Position.ConnectTo.Contains(childs))
                            {
                                childs.Position.Parent = dummyParallelParent;
                                dummyParallelParent.Elements.Add(childs);
                                break;
                            }
                        }
                    }
                    else
                    {
                        firstChildParallel.Elements[0].Position.Parent = dummyParallelParent;
                        dummyParallelParent.Elements.Add(firstChildParallel.Elements[0]);
                    }
                }
            }
            firstChildParallel.Position.Parent = selectedElement.Position.Parent;
            firstChildParallel.Position.X = selectedElement.Position.X;
            firstChildParallel.Position.Y = selectedElement.Position.Y;
            firstChildParallel.Position.Index = selectedElement.Position.Index;
            firstChildParallel.Position.Parent = selectedElement.Position.Parent;
            var keysFound = parallelElementsDictionary.Keys.Where(x => x.Contains(selectedElement));

            if (root.Elements.Count > selectedElement.Position.Index + 1 && keysFound.Count() == 1 && GetMaxConnect(firstChildParallel) == 1)
            {
                while (root.Elements[selectedElement.Position.Index + 1].customDrawing.toString() == "HorizontalLine")
                {
                    root.Elements.Remove(root.Elements[selectedElement.Position.Index + 1]);
                }
            }
            int newIndex = selectedElement.Position.Index;
            // Replace contact being deleted with the first child parallel 
            root.Elements.Insert(newIndex, firstChildParallel);
            if (firstChildParallel.Position.RelateTo.Count > 0)
            {
                int nextX = firstChildParallel.Position.X;
                int nextIndex = firstChildParallel.Position.Index;
                foreach (LadderElement connectedElement in firstChildParallel.Position.ConnectTo)
                {
                    nextX += ControlWidth;
                    nextIndex++;
                    connectedElement.Position.Index = nextIndex;
                    connectedElement.Position.X = nextX;
                    connectedElement.Position.Y = firstChildParallel.Position.Y;
                    connectedElement.Position.Parent = firstChildParallel.Position.Parent;
                    root.Elements.Insert(nextIndex, connectedElement);
                    replaceKeywith.Add(connectedElement);
                    firstChildParallel.Position.RelateTo.Remove(connectedElement);
                    firstChildParallel.Elements.Remove(connectedElement);
                }
                firstChildParallel.Position.RelateTo.Clear();
            }
            // Remove first child parallel from the dictionary value 
            childParallels.Remove(firstChildParallel);
            UpdateDictionary(hashSetOfSelectedContacts, firstChildParallel, childParallels);
            firstChildParallel.Position.RelateTo.Remove(selectedElement);
            RemoveContactFromSuperSets(selectedElement, replaceKeywith);
        }
        /// <summary>
        /// Delete all simple elements like simple Contact,Function block,Coil etc.
        /// </summary>
        public void DeleteSimpleElements()
        {
            LadderElement elemetnTobeDeleted = LadderDesign.ClickedElement;
            LadderElement rootElement = elemetnTobeDeleted.getRoot();
            if (rootElement.Elements.Count > elemetnTobeDeleted.Position.Index + 1)
            {
                List<string> notRequired = new List<string>();
                LadderElement nextElement = new LadderElement();
                nextElement = GetNextElement(rootElement, elemetnTobeDeleted);
                nextElement.Position.Parent = elemetnTobeDeleted.Position.Parent;
                nextElement.Position.X = elemetnTobeDeleted.Position.X;
                nextElement.Position.Y = elemetnTobeDeleted.Position.Y;
                nextElement.Position.ConnectTo = elemetnTobeDeleted.Position.ConnectTo;
                nextElement.Position.RelateTo = elemetnTobeDeleted.Position.RelateTo;
            }
            //If it a coil which has parallel coils attached with it then transfer all properties of next parallel coil to this coil and delete next coil instead 
            if (elemetnTobeDeleted.customDrawing.toString() == "Coil" && elemetnTobeDeleted.Elements.Count > 0)
            {
                LadderElement nextElement = new LadderElement();
                nextElement = elemetnTobeDeleted.Elements[0];
                elemetnTobeDeleted.Attributes.Clear();
                foreach (var targetAttribute in nextElement.Attributes)
                {
                    // Transfer each attribute to the target element
                    elemetnTobeDeleted.Attributes.Add(targetAttribute);
                }
                elemetnTobeDeleted.Elements.Remove(nextElement);
                elemetnTobeDeleted.Elements.AddRange(nextElement.Elements);
                elemetnTobeDeleted = nextElement;
            }
            rootElement.Elements.Remove(elemetnTobeDeleted);
            LadderDesign.AdjustControls(rootElement.Elements, LadderDesign.StartX);
            LadderDesign.ClickedElement = null;
        }

        private void DeleteTCCount(LadderElement elemetnTobeDeleted)
        {
            ApplicationRung AppRecs = new ApplicationRung();
            AppRecs = (ApplicationRung)xm.LoadedProject.LogicRungs.Where(R => R.TC_Name == elemetnTobeDeleted.Attributes["TcName"].ToString()).FirstOrDefault();
            xm.LoadedProject.LogicRungs.Remove(AppRecs);
            string tcName = elemetnTobeDeleted.Attributes["TcName"].ToString();
            string opCode = elemetnTobeDeleted.Attributes["OpCode"].ToString();
            int tccount = Convert.ToInt32(tcName.Replace("T", "").Replace("C", "").Replace("FB", ""));
            foreach (LadderElement rungElements in this.Elements)
            {
                LadderElement fbelement = (LadderElement)rungElements.Elements.Where(E => E.CustomType.ToString() == "LadderDrawing.FunctionBlock" && E.Attributes["OpCode"].Equals(opCode) && Convert.ToInt32(E.Attributes["TCName"].ToString().Replace("T", "").Replace("C", "").Replace("FB", "")) > tccount).FirstOrDefault();
                if (fbelement != null)
                {
                    string oldtcname = fbelement.Attributes["TcName"].ToString();
                    int result = Convert.ToInt32(Regex.Match(fbelement.Attributes["TcName"].ToString(), @"\d+").Value);
                    fbelement.Attributes["TcName"] = fbelement.Attributes["TcName"].ToString().Replace(result.ToString(), (result - 1).ToString());
                    AppRecs = (ApplicationRung)xm.LoadedProject.LogicRungs.Where(R => R.TC_Name == oldtcname).FirstOrDefault();
                    if (AppRecs != null)
                        AppRecs.TC_Name = fbelement.Attributes["TcName"].ToString();
                }
            }
        }
        private LadderElement GetNextElement(LadderElement rootElement, LadderElement elemetnTobeDeleted)
        {
            LadderElement nextElement = new LadderElement();
            foreach (LadderElement child in rootElement.Elements)
            {
                if ((child.Position.X > elemetnTobeDeleted.Position.X) && child.customDrawing.toString() != "DummyParallelParent" && child.customDrawing.toString() != "HorizontalLine" && child.customDrawing.toString() != "Comment")
                {
                    nextElement = child;
                    break;
                }
            }
            return nextElement;
        }
        /// <summary>
        /// Get Last element of the Rung which is not Coil
        /// </summary>
        /// <param name="element"></param> Send the element of which root has to be searched
        /// <returns></returns> Return X of last element + width of that element
        public static int GetXofLastElement(LadderElement element)
        {
            int MaxX = 0;
            LadderElement Root = element.getRoot();
            foreach (LadderElement ladderElement in Root.Elements)
            {
                if ((ladderElement.customDrawing.GetType() == typeof(Contact) || ladderElement.customDrawing.GetType() == typeof(HorizontalLine)) && element.Position.Parent == Root)
                {
                    if (MaxX < (ladderElement.getX()))
                        MaxX = ladderElement.getX();
                }
            }
            return MaxX;
        }
        public (List<LadderElement>, Dictionary<HashSet<LadderElement>, LadderElements>) CopyElement(List<LadderElement> elements)
        {
            if (ClickedElement != null)
            {
                if (!elements.Any())
                    elements.Add(ClickedElement);
                List<LadderElement> clickedElements = new List<LadderElement>();
                foreach (LadderElement element in elements)
                    clickedElements.Add(element.getRoot());
                return (clickedElements, parallelElementsDictionary);
            }
            return (null, null);
        }
        public static void PasteElement()
        {
            MessageBox.Show("Paste the element");
        }
        public static void CutElement()
        {
            MessageBox.Show("Cut the element");
        }
        public void DeleteElement()
        {
            DeleteSelectedControl(false);
        }
        internal void UpdateSetStatus(ref LadderElement element)
        {
            if (element != null)
            {
                Type typeOf = element.customDrawing.GetType();
                bool isTypeOf = (typeOf == typeof(Coil) || typeOf == typeof(CoilParallel));
                if (element != null && isTypeOf)
                {
                    if (ClickedElement.Attributes["SetReset"].ToString() == "S")
                        ClickedElement.Attributes["SetReset"] = "";
                    else
                        ClickedElement.Attributes["SetReset"] = "S";
                }
            }
        }
        internal void UpdateResetStatus(ref LadderElement element)
        {
            if (element != null)
            {
                Type typeOf = element.customDrawing.GetType();
                bool isTypeOf = (typeOf == typeof(Coil) || typeOf == typeof(CoilParallel));
                if (element != null && isTypeOf)
                {
                    if (ClickedElement.Attributes["SetReset"].ToString() == "R")
                        ClickedElement.Attributes["SetReset"] = "";
                    else
                        ClickedElement.Attributes["SetReset"] = "R";
                }
            }
        }
        public void Undo()
        {
            var lastMemento = caretaker.getUndoMemento();
            if (lastMemento == null)
                return;
            this.Elements = lastMemento.Elements.DeepClone();
            this.parallelElementsDictionary = lastMemento.CloneParallelDictionary(lastMemento.ParallelElementsDictionary, Elements);
            xm.LoadedProject.LogicRungs = lastMemento.XMPSLogicRungs.DeepClone();
        }
        public void RedoNew()
        {
            var lastMemento = caretaker.getRedoMemento();
            if (lastMemento == null)
                return;
            this.Elements = lastMemento.Elements.DeepClone();
            this.parallelElementsDictionary = lastMemento.CloneParallelDictionary(lastMemento.ParallelElementsDictionary, Elements);
            xm.LoadedProject.LogicRungs = lastMemento.XMPSLogicRungs.DeepClone();
        }
        internal void UpdatePNStatus(ref LadderElement element)
        {
            if (element != null)
            {
                Type typeOf = element.customDrawing.GetType();
                bool isTypeOf = (typeOf == typeof(Contact));
                bool isNegation = element.Negation;
                if (!element.Attributes.Where(T => T.Name == "PNStatus").Any())
                {
                    Attribute attribute = new Attribute();
                    attribute.Name = "PNStatus";
                    element.Attributes.Add(attribute);
                }
                if (isTypeOf && !isNegation)
                {
                    if (ClickedElement.Attributes["PNStatus"].ToString() == "")
                        ClickedElement.Attributes["PNStatus"] = "P";
                    else if (ClickedElement.Attributes["PNStatus"].ToString() == "P")
                        ClickedElement.Attributes["PNStatus"] = "N";
                    else
                        ClickedElement.Attributes["PNStatus"] = "";
                }
            }
        }
        internal static bool CheckinUDFB(string caption)
        {
            foreach (UDFBInfo uDFBInfo in XMPS.Instance.LoadedProject.UDFBInfo)
            {
                if (uDFBInfo.UDFBlocks.Where(b => b.Text.Equals(caption)).Count() > 0)
                    return true;
            }
            return false;
        }
        internal void ClearStatus(ref LadderElement element)
        {
            if (element != null && element.customDrawing.GetType() == typeof(Contact))
            {
                ClickedElement.Attributes["PNStatus"] = "";
                element.Negation = false;
            }
        }
        public void SetStateForUndoRedo()
        {
            // Set the empty blank state when Elements Count = 0
            caretaker.SetStateForUndoRedo(this);
        }
    }
}
