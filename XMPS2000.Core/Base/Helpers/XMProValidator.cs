using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace XMPS1000.Core.Base.Helpers
{
    public static class XMProValidator
    {
     
        /// <summary>
        /// Funcntion to check if the entered Value for Bit Address it Valid or Noe
        /// </summary>
        /// <param name="address"></param>Value Entered By The User
        /// <returns></returns>Is the Bit Value Entered Valid
        public static bool IsValidBitAddress(this string address)
        {
            // Let's setup regular expression for validating bit address
            string regExString1;
            //Since . is special character replace it with @ for temporary checking and also convert @ with another spectial char # so that it does not accept # and @ both
            address = address.Replace("@", "#");
            address = address.Replace(".", "@");
            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            //regExString1 = "^(Q0|I1|X8|Y9):"; Commenting as X and Y are not valid IO's
            regExString1 = "^(Q0|I1):";
            // And then append 3 digit word number i.e. 000 to 255
            regExString1 += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])";
            // And then .00 to .15 digital bit numbers 
            //Check for @ instead of . as we have already replaced the same in the code above
            regExString1 += "(@0[0-9]|@1[0-5])$";

            // Special handling of Flag addresses (it is a bit address without having .00 to .15 values at end! e.g.F2:000 to F2:255)
            string regExString2 =
                "^F2:" /* starting string */ +
                "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$" /* 000 to 255 */;

            Regex regEx1 = new Regex(regExString1);
            Regex regEx2 = new Regex(regExString2);

            if (!regEx1.IsMatch(address) && !regEx2.IsMatch(address))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Function to check if user have entered valid Word Address
        /// </summary>
        /// <param name="address"></param> Value entered by the user
        /// <returns></returns>Is the value enteted by the user valid Word Address
        public static bool IsValidWordAddress(this string address)
        {
            // Let's setup regular expression for validating bit address
            string regExString;
            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            //regExString = "^(Q0|I1|S3|W4|P5|T6|C7|X8|Y9|U10):"; Commenting others not valid IO's
            regExString = "^(Q0|I1|S3|W4|P5):";

            // And then append the 3 digit word number i.e. 000 to 255
            regExString += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$";

            Regex regEx2 = new Regex(regExString);

            if (!regEx2.IsMatch(address))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Function to check if the value enterd by the user for Memory Adress is Valid or not
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <returns></returns>Is the value entered by the user a valid Memory Address
        public static bool IsValidMemoryAddress(this string address)
        {
            // Let's setup regular expression for validating bit address
            string regExString;

            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            regExString = "^(F2|W4|P5|S3|T6|C7):";

            // And then append the 3 digit word number i.e. 000 to 255
            regExString += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$";

            Regex regEx2 = new Regex(regExString);

            if (!regEx2.IsMatch(address))
                return false;
            else
                return true;
        }

        public static bool IsValidInitialValueForAddress(this string value, string address)
        {
            try
            {
                string ValueType = "";
                if (address.Contains(".") || address.StartsWith("F2") || address.StartsWith("X8"))
                {
                    ValueType = "Bit";
                }
                else if (address.StartsWith("Q0") || address.StartsWith("Y9"))
                {
                    ValueType = "Word";
                }
                else if (address.StartsWith("W4"))
                {
                    ValueType = "Interger Word";
                }
                else if (address.StartsWith("P5"))
                {
                    ValueType = "Real";
                }

                if (ValueType == "Bit")
                {
                    if (Convert.ToInt16(value) == 0 || Convert.ToInt16(value) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (ValueType == "Word")
                {
                    if (Enumerable.Range(0, 65536).Contains(Convert.ToInt32(value)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (ValueType == "Interger Word")
                {
                    if (Type.GetTypeCode(Convert.ToInt32(value).GetType()) == TypeCode.Int32)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (ValueType == "Real")
                {
                    if (Type.GetTypeCode(Convert.ToDouble(value).GetType()) == TypeCode.Double)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// Function to check if the Value entered by the user is valid word address but not the Floting Address used for Time, Counter Data types and Bit Shift,Arithmatic,Limit and Compare Instructions only
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <returns></returns>If the value entered by the user a valid Non Floating Word Address 
        public static bool IsValidNonFlotingWordAddress(this string address)
        {
            // Let's setup regular expression for validating bit address
            string regExString;

            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            //regExString = "^(Q0|I1|S3|W4|T6|C7|X8|Y9|U10):"; Commenting others not valid IO's
            regExString = "^(Q0|I1|W4):";
            // And then append the 3 digit word number i.e. 000 to 255
            regExString += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$";

            Regex regEx2 = new Regex(regExString);

            if (!regEx2.IsMatch(address))
                return false;
            else
                return true;
        }


        /// <summary>
        /// Function to check if the value entered by the user is valid Bit Address for Output i.e. it should start with Q0 always
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <param name="outputType"></param>Output type selected by the user
        /// <returns></returns>If the Value entered is valid Bit Address for Output
        public static bool IsValidOutputBitAddress(this string address, string outputType)
        {
            string regExString;

            switch (outputType)
            {
                case "On-board":
                    address = address.Replace("@", "#");
                    address = address.Replace(".", "@");
                    regExString =
                        "^Q0:000" /* starting string */ +
                        //Check for @ instead of . as we have already replaced the same in the code above
                        "@0[0-5]$" /* .00 to .05 */;
                    break;
                case "Remote":                    
                case "Expansion":
                    // Acceptable format e.g.Q0:000.00 to Q0:255.15
                    //Since . is special character replace it with @ for temporary checking and also convert @ with another spectial char # so that it does not accept # and @ both
                    address = address.Replace("@", "#");
                    address = address.Replace(".", "@");
                    regExString =
                        "^Q0:" /* starting string */ +
                        "([0][0][2-9]|[0][1-9][0-9]|[1][0-9][0-9]||2[0-4][0-9]|25[0-5])" /* 002 to 255 */ +
                        //Check for @ instead of . as we have already replaced the same in the code above
                        "(@0[0-9]|@1[0-5])$" /* .06 to .15 */;
                    break;
                case "Memory Address Variable":
                    // Acceptable format e.g.F2:000 to F2:255
                    regExString =
                        "^F2:" /* starting string */ +
                        "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$" /* 000 to 255 */;
                    break;
                default:
                    return true;
            }

            Regex regEx1 = new Regex(regExString);

            if (!regEx1.IsMatch(address))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Function to check if the value entered by the user is valid word Address for Output i.e. it should start with Q0 always
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <param name="outputType"></param>Output type selected by the user
        /// <returns></returns>If the Value entered is valid word Address for Output
        public static bool IsValidOutputWordAddress(this string address, string outputType)
        {
            string regExString;

            // Let's setup regular expression for validating Word address. Acceptable format e.g. Q0:000 to Q0:255
            switch (outputType)
            { 
                case "On-board":

                    if (address.Equals("Q0:001")) /* complete string */
                        return true;
                    else
                        return false;
                case "Remote":
                case "Expansion":
                    // Acceptable format e.g.Q0:002 to Q0:255
                    regExString = "^Q0:" /* starting string */;
                    regExString += "([0][0][2-9]|[0][1-9][0-9]|[1][0-9][0-9]|2[0-4][0-9]|25[0-5])$" /* 000 to 255 */;
                    break;
                case "Memory Address Variable":
                    // Acceptable format e.g.W4:000 to W4:255
                    regExString =
                        // "^(W4|T6|C7):" /* starting string (any of the 'W4:' or 'T6:' or 'C7:' ) */;
                        "^(W4):" /* starting string (any of the 'W4:' or 'T6:' or 'C7:' ) */;
                    regExString += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$" /* 000 to 255 */;
                    break;
                default:
                    return true;
            }
            

            Regex regEx2 = new Regex(regExString);
            
            if (!regEx2.IsMatch(address))
                return false;
            else
                return true;
        }

       
        /// <summary>
        /// Function to check if the value entered by user is valid Real Word Address
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <returns></returns>Is the value entered by the user valid Real Word Address
        public static bool IsValidRealWordAddress(this string address)
        {
            // Let's setup regular expression for validating Word address. Acceptable format e.g. Q0:000 to Q0:255
            string regExString =
                "^P5:" /* starting string */ +
                "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$" /* 000 to 255 */;

            Regex regEx2 = new Regex(regExString);

            if (!regEx2.IsMatch(address))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Function to pad Ip Addresses with .000 Formatting
        /// </summary>
        /// <param name="IPAddress"></param> IP Address Entered by the user
        /// <returns></returns>Formatted IP Address
        public static string FormatIPAddress(string IPAddress)
        {
            //Padding 000 in IP address field
            string IPAdd = IPAddress;
            //Split the string with considerring . as delimitter and store it in an string array
            string[] str = IPAdd.Split('.');
            //use for loop to check each part of array and pad it with 000
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == "")
                {
                    str[i] = "000";
                }
                else
                {
                    str[i] = int.Parse(str[i]).ToString("000");
                }
            }
            //Join the string with considerring . as delimitter
            IPAdd = string.Join(".", str);
            //return joined string
            return IPAdd;
        }
    }
}
