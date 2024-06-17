using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsMobile.Status;

namespace activiser.Library.Forms
{
    sealed class Phone
    {
        private static readonly System.Version minimumVersion = new Version(5, 0);
        private static readonly bool versionOk = Environment.OSVersion.Version >= minimumVersion; 

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
    }
}
