using System;
using System.Windows.Forms;
using XMPS2000.Core;

namespace XMPS2000.Bacnet
{
    public static class BacNetValidator
    {
        /// <summary>
        /// Check if the valid input is entered in the BacNet IP object
        /// </summary>
        /// <param name="control"></param> Name of the control
        /// <param name="error"></param> Return the desciption of error
        /// <param name="dataType"></param> desired Datatype to check with 
        /// <returns></returns> Does it matches ?
        public static bool ValidateBacNetInputVal(string value, out string error, string dataType)
        {
            error = string.Empty;
            if (string.IsNullOrEmpty(value))     // Allow untouched or emptied operands.
            {
                return true;
            }
            bool validationSuccessful = false;
            switch (dataType)
            {
                case "Bool":
                    if (value.Length > 1)
                        validationSuccessful = false;
                    else
                        validationSuccessful = Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == 1 ? true : false;
                    error = "Invalid value boolean value it can be 0 or 1";
                    break;
                case "Real":
                    decimal parsedValue;
                    if (decimal.TryParse(value, out parsedValue))
                    {
                        // Check if the value is within the acceptable range
                        if (parsedValue >= Convert.ToDecimal(int.MinValue) && parsedValue <= Convert.ToDecimal(int.MaxValue))
                        {
                            // Check if the value has up to 2 decimal places
                            int decimalPlaces = BitConverter.GetBytes(decimal.GetBits(parsedValue)[3])[2];
                            validationSuccessful = decimalPlaces <= 2;
                            error = "Invalid value, onlye 2 decimal places allowed";
                        }
                        else
                            error = "Invalid value Real data type range is -2147483648 to 2147483647";
                    }
                    else
                        error = "Invalid value Real data type range is -2147483648 to 2147483647";
                    break;
                case "UDINT":
                    validationSuccessful = uint.TryParse(value, out _);
                    error = "Invalid value it should be between 0 to 4294967295";
                    break;
                case "Byte":
                    validationSuccessful = byte.TryParse(value, out _);
                    error = "Invalid value it should be between 0 to 255";
                    break;
                case "INT16":
                    validationSuccessful = ushort.TryParse(value, out _);
                    error = "Invalid value it should be between 0 to 65535";
                    break;
            }
            if (validationSuccessful)
            {
                return true;
            }
            else
                return false;
        }
        public static int GetEventCheckBoxResult(bool checkBox1Checked, bool checkBox2Checked, bool checkBox3Checked)
        {
            int result = 0;

            if (checkBox1Checked)
            {
                result |= 4;
            }

            if (checkBox2Checked)
            {
                result |= 2;
            }

            if (checkBox3Checked)
            {
                result |= 1;
            }

            return result;
        }
        public static int GetLimitCheckBoxResult(bool checkBox1Checked, bool checkBox2Checked)
        {
            int result = 0;

            if (checkBox1Checked)
            {
                result |= 2;
            }

            if (checkBox2Checked)
            {
                result |= 1;
            }

            return result;
        }
        public static void ControlChanged(object sender, EventArgs e)
        {
            XMPS.Instance.LoadedProject.isChanged = true;
        }
        public static bool CheckAndPromptSaveChanges()
        {
            if (XMPS.Instance.LoadedProject != null && XMPS.Instance.LoadedProject.isChanged)
            {
                DialogResult result = MessageBox.Show("Do you want to save current changes?", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("To Save current changes click on Save button", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {
                    XMPS.Instance.LoadedProject.isChanged = false;
                    return true;
                }
            }
            return true;
        }
    }
}
