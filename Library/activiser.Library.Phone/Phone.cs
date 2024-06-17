using System;
using System.Windows.Forms;
using Microsoft.WindowsMobile.Status;
using MSPhone = Microsoft.WindowsMobile.Telephony;

namespace activiser.Library
{
    public sealed partial class Phone
    {
        private static readonly System.Version minimumVersion = new Version(5, 0);
        private static readonly bool versionOk = Environment.OSVersion.Version >= minimumVersion;
        private static MSPhone.Phone _phone = new MSPhone.Phone();

        Phone()
        {
            if (versionOk && SystemState.PhoneRadioPresent) _phone = new MSPhone.Phone();
        }
        public static bool HavePhone()
        {
            if (!(Environment.OSVersion.Platform == PlatformID.WinCE))
            {
                return false;
            }

#if DEBUG
            return versionOk;
#else
            return versionOk && SystemState.PhoneRadioPresent;
#endif
        }

        public static void MakePhoneCall(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException("phoneNumber");

            if (!versionOk)
                throw new NotSupportedException(Properties.Resources.UnsupportedWindowsVersion);
            else if (!SystemState.PhoneRadioPresent)
                throw new NoRadioException(Properties.Resources.PhoneNotDetected);
            else if (Microsoft.WindowsMobile.Status.SystemState.PhoneNoService)
                throw new NoServiceException(Properties.Resources.NoService);

            string validatedPhoneNumber = ValidatePhoneNumber(phoneNumber);

            if (validatedPhoneNumber == null)
                throw new FormatException(Properties.Resources.UnableToParsePhoneNumber);

            _phone.Talk(validatedPhoneNumber, true);

        }

        public static void MakePhoneCall(TextBoxBase textBox)
        {
            if (textBox == null) throw new ArgumentNullException("textBox");

            if (!string.IsNullOrEmpty(textBox.SelectedText))
                MakePhoneCall(textBox.SelectedText.Trim());
            else if (!string.IsNullOrEmpty(textBox.Text))
                MakePhoneCall(textBox.Text);
            else
                throw new ArgumentNullException("textBox");
        }

        //TODO: Finish this. 
        //public void SendSms(string phoneNumber)
        //{
        //    if (!versionOk)
        //    {
        //        return;
        //    }
        //    else if (!Microsoft.WindowsMobile.Status.SystemState.PhoneRadioPresent)
        //    {
        //        // display no phone message 
        //        return;
        //    }
        //    else if (Microsoft.WindowsMobile.Status.SystemState.PhoneNoService)
        //    {
        //        // display no service message 
        //        return;
        //    }
        //    else
        //    {

        //        string prompt;
        //        if (ValidatePhoneNumber(phoneNumber))
        //        {
        //            prompt = "ThisNumberPrompt";
        //        }
        //        else
        //        {
        //            prompt = "ParseProblem";
        //        }

        //        phoneNumber = Terminology.AskQuestion(null, "PhoneCall", prompt, ParseAlphaPhoneNumber(phoneNumber), false, 30);

        //        if (!string.IsNullOrEmpty(phoneNumber))
        //        {
        //            PocketOutlook.SmsMessage msg = new PocketOutlook.SmsMessage();

        //        }


        //    }
        //}

        //public void SendSms(TextBox textbox)
        //{

        //}

        public static bool CanPhone()
        {
            if (!(Environment.OSVersion.Platform == PlatformID.WinCE))
            {
                return false;
            }

#if DEBUG
            return versionOk;
#else 
            return versionOk && Microsoft.WindowsMobile.Status.SystemState.PhoneRadioPresent && !Microsoft.WindowsMobile.Status.SystemState.PhoneNoService; 
#endif
        }

        public static bool CanPhone(string phoneNumber)
        {
            return CanPhone() && (ValidatePhoneNumber(phoneNumber) != null);
        }

        public static bool CanPhone(TextBoxBase textBox)
        {
            if (!(Environment.OSVersion.Platform == PlatformID.WinCE))
            {
                return false;
            }
            if (textBox == null)
                return false;
            if (Environment.OSVersion.Version < minimumVersion)
                return false;
            if (!Microsoft.WindowsMobile.Status.SystemState.PhoneRadioPresent || Microsoft.WindowsMobile.Status.SystemState.PhoneNoService)
            {
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(textBox.SelectedText))
                    return false;
                return ValidatePhoneNumber(textBox.SelectedText.Trim()) != null;
            }
        }

        #region "Phone Number Validation"
        const string STR_PhoneRegex = "((?<idd>(\\+\\d{1,3}))(-| )?)?(?<areaprefix>\\(\\d\\)?)?(-| )?(?<area>(\\d{1,5})|(\\(?\\d{2,6}\\)?))(-| )?(?<number>((\\d|\\w|-| )?)+)((x|ext) ?(?<ext>\\d{1,5})){0,1}";
        private static System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(STR_PhoneRegex);

        /// <summary>
        /// Validates a supplied string as a phone number; will also convert text to numbers and use those.
        /// </summary>
        /// <param name="phoneNumber">
        /// A string of letters, numbers and punctuation that could reasonably be intepreted as a 
        /// phone number
        /// </param>
        /// <returns>
        /// If the number as supplied was a 'normal' number, then the number will be returned as supplied.
        /// If the supplied number was alpha-numeric, and after translating the letters to numbers is successfully validated
        /// as a phone number, then the translated number is returned.
        /// Null if validation fails or the supplied phone number is null or empty.
        /// </returns>
        public static string ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return null;
            
            if (regex.IsMatch(phoneNumber)) return phoneNumber;
            
            phoneNumber = ParseAlphaPhoneNumber(phoneNumber);
            if (regex.IsMatch(phoneNumber)) return phoneNumber;
            
            return null;
        }

        public static string ParseAlphaPhoneNumber(string phoneNumber)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder(phoneNumber);
            
            for (int i = 0; i <= result.Length - 1; i++)
            {
                char thisChar = result[i];
                if ("ABCabc".IndexOf(thisChar) != -1)
                {
                    result[i] = '2';
                }
                else if ("DEFdef".IndexOf(thisChar) != -1)
                {
                    result[i] = '3';
                }
                else if ("GHIghi".IndexOf(thisChar) != -1)
                {
                    result[i] = '4';
                }
                else if ("JKLjkl".IndexOf(thisChar) != -1)
                {
                    result[i] = '5';
                }
                else if ("MNOmno".IndexOf(thisChar) != -1)
                {
                    result[i] = '6';
                }
                else if ("PQRSpqrs".IndexOf(thisChar) != -1)
                {
                    result[i] = '7';
                }
                else if ("TUVtuv".IndexOf(thisChar) != -1)
                {
                    result[i] = '8';
                }
                else if ("WXYZwxyz".IndexOf(thisChar) != -1)
                {
                    result[i] = '9';
                }
            }
            return result.ToString();
        }
        #endregion

    }
}
