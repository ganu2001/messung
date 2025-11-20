using Force.DeepCloner;
using LadderDrawing;
using System.Collections.Generic;
using System.Linq;
using XMPS2000.Core;
using XMPS2000.Core.LadderLogic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace LadderEditorLib.MementoDesign
{
    public class LadderDesignMemento
    {
        public List<ApplicationRung> XMPSLogicRungs;
        public LadderElements Elements;
        public Dictionary<HashSet<LadderElement>, LadderElements> ParallelElementsDictionary;
        public LadderDesignMemento(LadderDesign ladderDesign)
        {
            Elements = ladderDesign.Elements.DeepClone();
            ParallelElementsDictionary = CloneParallelDictionary(ladderDesign.parallelElementsDictionary, Elements);
            XMPSLogicRungs = XMPS.Instance.LoadedProject.LogicRungs.DeepClone();
        }

        public Dictionary<HashSet<LadderElement>, LadderElements> CloneParallelDictionary(Dictionary<HashSet<LadderElement>, LadderElements> originalParallelElementsDictionary, LadderElements elements)
        {
            var parallelElementsDictionary = new Dictionary<HashSet<LadderElement>, LadderElements>(HashSet<LadderElement>.CreateSetComparer());

            if (originalParallelElementsDictionary.Count == 0)
                return parallelElementsDictionary;  // return empty dictionary

            Dictionary<string, LadderElement> refdict = new Dictionary<string, LadderElement>();
            foreach (LadderElement a in elements)
                a.AddToReferenceDictionary(refdict); // uses recurssion to add child elements to ref dictionary

            foreach (var keyValue in originalParallelElementsDictionary)
            {
                if (keyValue.Value.Count == 0) continue;
                // First Clone the Keys which is HashSet of LadderElements from original parallelElementsDictionary
                HashSet<LadderElement> ladderElementsNewHashSet = new HashSet<LadderElement>();
                foreach (var ladderElementInHashSet in keyValue.Key)
                {
                    refdict.TryGetValue(ladderElementInHashSet.Id, out var element);
                    ladderElementsNewHashSet.Add(element);
                }
                // Then Clone the Values which is (Parallel) LadderElements 
                LadderElements newElements = new LadderElements();
                foreach (var parallelLadderElement in keyValue.Value)
                {
                    refdict.TryGetValue(parallelLadderElement.Id, out var element);
                    newElements.Add(element);
                }
                if (parallelElementsDictionary.TryGetValue(ladderElementsNewHashSet, out var existingElements))
                {
                    bool areValuesEqual = existingElements.SequenceEqual(newElements);
                    if (areValuesEqual)
                    {
                        // If the values are equal, remove the existing key-value pair
                        parallelElementsDictionary.Remove(ladderElementsNewHashSet);
                    }
                }
                // Finally add the hash set as key and cloned values to the new parallelElementsDictionary
                parallelElementsDictionary.Add(ladderElementsNewHashSet, newElements);
            }
            return parallelElementsDictionary;
        }

        public LadderDesignMemento GetLadderDesignMemento()
        {
            return this;
        }
    }
}
