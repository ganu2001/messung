using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XMPS2000.Core.Base.Helpers
{
    /// <summary>
    /// This Class is used for all common validations and is accessible all over the project
    /// </summary>
    public class XMProBaseValidator
    {
        public string ErrorText { get; set; }
        /// <summary>
        /// Function to Validate OutPut Addresses
        /// </summary>
        /// <param name="ControlText"></param> Text in the controll
        /// <param name="chktype"></param> Check Waht type of Data is passed Bit or Word Address
        /// <returns></returns>If the control have valid data
        public bool ValidateOutputAddress(string ControlText, string chktype)
        {
            bool validationSuccessful = true;
            string error = string.Empty;

            if (string.IsNullOrEmpty(ControlText))
            {
                validationSuccessful = false;
                error = "Output address cannot be blank";
            }
            else if (ControlText.Length > 9)
            {
                validationSuccessful = false;
                error = "Output address cannot contain more than 9 characters";
            }
            else if (ControlText.Contains(" "))
            {
                validationSuccessful = false;
                error = "Output address cannot contain a space";
            }
            else
            {
                string regExString1 =
                // Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
                "^(Q0|MX):" +
                // Followed by 3 digit word number i.e. 000 to 255
                "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])" +
                // Followed by optional 0 to 15 bit numbers for digital input types
                "(.0[0-9]|.1[0-5]){0,1}$";

                string regExString2 =
                // Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
                "^(F2|W4|P5|T6|C7|X8|Y9):" +
                // Followed by 3 digit word number i.e. 000 to 255
                "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$";

                Regex regEx1 = new Regex(regExString1);
                Regex regEx2 = new Regex(regExString2);
                //Check for Type Bit Address OR word

                if (chktype == "Bit Address")
                {
                    if (!regEx1.IsMatch(ControlText))
                    {
                        validationSuccessful = false;
                        error = "Variable is not as per selected Function Code";
                    }
                }
                else if (chktype == "word")
                {
                    if (!regEx2.IsMatch(ControlText))
                    {
                        validationSuccessful = false;
                        error = "Variable is not as per selected Function Code";
                    }
                }
            }

            if (validationSuccessful)
            {
                ErrorText = null;
                return true;
            }
            else
            {
                ErrorText = error;
                return false;
            }
        }
        /// <summary>
        /// Function to Validate the Operand, if it is in correct format or not
        /// </summary>
        /// <param name="ControlText"></param> Text entered in Controll
        /// <param name="datatype"></param> Data Type selected by the user this is mostly used for Numeric Controlls
        /// <param name="operandType"></param> Operand Type selected by the user i.e. Normal, Value or Negation
        /// <returns></returns>If the control have valid data
        public bool ValidateOperand(string ControlText, string datatype, int operandType)
        {
            bool validationSuccessful = true;
            string error = string.Empty;

            if (string.IsNullOrEmpty(ControlText))     // Allow untouched or emptied operands.
            {
                validationSuccessful = true;
            }
            else if (ControlText.Contains(" "))
            {
                validationSuccessful = false;
                error = "Operand cannot contain a space";
            }
            else
            {
                //int operandType = GetOperandTypeForControl(control);
                switch (operandType)
                {
                    case 0:     // Normal i.e. Address
                        validationSuccessful = ValidateAddressOperand(ControlText, out error);
                        break;
                    case 1:     // Numeric Operand
                        validationSuccessful = ValidateNumericOperand(ControlText, datatype, out error);
                        break;
                    case 2:     // Negation Operand
                        validationSuccessful = ValidateAddressOperand(ControlText, out error);
                        break;
                }
            }

            if (validationSuccessful)
            {
                ErrorText = null;
                return true;
            }
            else
            {
                ErrorText = error;
                return false;
            }
        }

        /// <summary>
        /// Function to validate Numeric Values
        /// </summary>
        /// <param name="number"></param>Value entered by the user
        /// <param name="datatype"></param>Data Type Selected by the user
        /// <param name="error"></param>Error to be shown if control is having  invalid text.
        /// <returns></returns>If the control have valid data
        private bool ValidateNumericOperand(string number, string datatype, out string error)
        {
            switch (datatype)
            {
                case "Bool":
                    if (!number.Equals("1") && !number.Equals("0"))
                    {
                        error = "Invalid input value. Value does not match for Boolean data type";
                        return false;
                    }
                    break;
                case "Byte":
                    if (!byte.TryParse(number, out _))
                    {
                        error = "Invalid input value. Value does not match for Byte data type";
                        return false;
                    }
                    break;
                case "Word":
                    if (!ushort.TryParse(number, out _))
                    {
                        error = "Invalid input value. Value does not match for Word data type";
                        return false;
                    }
                    break;
                case "Double Word":
                    if (!uint.TryParse(number, out _))
                    {
                        error = "Invalid input value. Value does not match for Double Word data type";
                        return false;
                    }
                    break;
                case "Int":
                    if (!short.TryParse(number, out _))
                    {
                        error = "Invalid input value. Value does not match for Integer data type";
                        return false;
                    }
                    break;
                case "Real":
                    if (!float.TryParse(number, out _))
                    {
                        error = "Invalid input value. Value does not match for Real data type";
                        return false;
                    }
                    break;
            }
            error = string.Empty;
            return true;
        }

        /// <summary>
        /// Function to Validate Address Types 
        /// </summary>
        /// <param name="address"></param>Test entered by the user
        /// <param name="error"></param>Error to be shown
        /// <returns></returns>If the control is Validated properly
        private bool ValidateAddressOperand(string address, out string error)
        {
            string regExString =
                // Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
                "^(Q0|I1|F2|S3|W4|P5|T6|C7|U10|U60):" +
                // Followed by 3 digit word number i.e. 000 to 255
                "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])"
                //+
                //// Followed by optional 0 to 15 bit numbers for digital input types
                //"(.0[0-9]|.1[0-5]){0,1}$"
                ;
            Regex regEx = new Regex(regExString);

            if (!regEx.IsMatch(address))
            {
                error = "Invalid operand address";
                return false;
            }
            else
            {
                error = string.Empty;
                return true;
            }
        }
        internal static string ConvertToDintValue(string value)
        {
            int intValue = int.Parse(value);

            if (intValue < 0)
            {
                long bit_value_32 = 4294967296; // 2^32
                int absIntValue = Math.Abs(intValue);
                long result = bit_value_32 - absIntValue;
                return result.ToString();
            }
            else
            {
                return intValue.ToString();
            }
        }

        internal static string ConvertToRealValue(string value)
        {
            float float_variable = float.Parse(value);
            byte[] bytes = BitConverter.GetBytes(float_variable);
            int convertedInt = BitConverter.ToInt32(bytes, 0);              //Float Number has converted to int
            //AS If Number is -ve then Get The Differnce Between Standard Bit & Actual value
            if (float_variable < 0)          //Datatype Is Real ==> Convert Numeric Values to Float
            {
                long bit_value_32 = 4294967296;
                int absIntValue = Math.Abs(convertedInt); // Take the absolute value

                long Real_result = bit_value_32 - absIntValue;
                return Real_result.ToString();
            }
            else
                return convertedInt.ToString();
        }

        internal static long ConvertStringToLong(string input)
        {
            long result = 0;
            if (input!=null)
            {
                foreach (char c in input)
                {
                    result += (int)c;   // result = result * 256+ (int)c  256 is used as each char is 8 bits (1 byte)
                }
            }
            return result;
        }
    }
}
