using System.Collections.Generic;

namespace XMPS2000.Core.LadderLogic
{
    public class DataCVX
    {
        private ApplicationRung ApplicationRung
        {
            get { return ApplicationRung; }
            set { ApplicationRung = value; }
        }

        public static List<string> CurrentRung;
        public static List<string> CurrentRungComment;
        public static bool CopyPresent;
        public static bool CutTrue;

        /// <summary>
        /// Copy rung will store the sring passed in a static variable
        /// </summary>
        /// <param name="rung">List of rungs expression</param>
        /// <param name="isCut">If it is true the function act like cut</param>
        public static void CopyRung(List<string> rung, List<string> _comment, bool isCut = false)
        {
            CurrentRung = rung;
            CurrentRungComment = _comment;
            CopyPresent = true;
            CutTrue = isCut;
        }

        /// <summary>
        /// Get the current string of rungs in copy
        /// </summary>
        /// <returns>Return the current copied list of rung expressions</returns>
        public static (List<string>, List<string> comment) PasteRung()
        {
            if (CutTrue) CopyPresent = false;
            return (CurrentRung, CurrentRungComment);
        }
    }
}
