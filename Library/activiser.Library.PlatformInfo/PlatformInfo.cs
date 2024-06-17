using System;

#if WINDOWSMOBILE
using Microsoft.WindowsMobile.Status;
#endif

namespace activiser.Library.PlatformInfo
{
    // TODO: Add Smartphones & Desktop.
    public enum Platform
    {
        PocketPC2000,
        PocketPC2002,
        PocketPC2003,
        WM5PocketPC,
        WM6PocketPC,
        Smartphone2002,
        Smartphone2003,
        WM5Smartphone,
        WM6Smartphone,
        Unknown
    }

    sealed public class Info
    {
        private static string _platformString;
        private static string _oemInfoString = GetOemInfo();
        private static Platform _platform = GetPlatform();
        private Info() { }

        public static Platform Platform { get { return _platform; } }
        public static string PlatformName { get { return _platformString; } }
        public static string DeviceName { get { return _oemInfoString; } }

        private const int SPI_GETPLATFORMTYPE = 257;
        private const int SPI_GETOEMINFO = 258;

        private static string GetOemInfo()
        {
            return NativeMethods.GetSystemParameter(SPI_GETOEMINFO);
        }

        private static Platform GetPlatform()
        {
            string platform ;

            //Get OSVersion
            System.Version osVersion = Environment.OSVersion.Version;

            // Get Platform
            try
            {
                platform = NativeMethods.GetSystemParameter(SPI_GETPLATFORMTYPE);
                if (platform == "PocketPC")
                {
                    switch (osVersion.Major)
                    {
                        case 5:
                            if (osVersion.Minor >= 2)
                            {
#if WINDOWSMOBILE
                                if (SystemState.PhoneRadioPresent)
                                {
                                    _platformString = Properties.Resources.WindowsMobile6Professional;
                                }
                                else
                                {
                                    _platformString = Properties.Resources.WindowsMobile6Classic;
                                }
#else
                                _platformString = Properties.Resources.WindowsMobile6;

#endif
                                return Platform.WM6PocketPC;
                            }
                            else
                            {
                                _platformString = Properties.Resources.WindowsMobile5;
                                return Platform.WM5PocketPC;
                            }

                        case 4:
                            if (osVersion.Minor == 21) 
                                _platformString = Properties.Resources.WindowsMobile2003SEPocketPC; 
                            else 
                                _platformString = Properties.Resources.PocketPC2003; 

                            return Platform.PocketPC2003;

                        case 3:
                            _platformString = Properties.Resources.PocketPC;
                            if (osVersion.Minor == 1)
                                return Platform.PocketPC2002;
                            else
                                return Platform.PocketPC2000;

                        default:
                            return Platform.Unknown;
                        //break;
                    }
                    //break;
                }
                else if (platform == "Smartphone")
                {
                    switch (osVersion.Major)
                    {
                        case 5:
                            if (osVersion.Minor == 2)
                                _platformString = Properties.Resources.WindowsMobile6Standard;
                            else
                                _platformString = Properties.Resources.WindowsMobile5Smartphone;
                            return Platform.WM5Smartphone;
                        case 4:
                            switch (osVersion.Minor)
                            {
                                case 21: _platformString = Properties.Resources.WindowsMobile2003SESmartphone; break;
                                default: _platformString = Properties.Resources.Smartphone2003; break;
                            }
                            return Platform.Smartphone2003;
                        case 3:
                            _platformString = Properties.Resources.Smartphone2002;
                            return Platform.Smartphone2002;
                        default:
                            return Platform.Unknown;
                    }
                    //break;
                }
                else
                {
                    _platformString = platform;
                    return Platform.Unknown;
                }
            }

            catch
            {

            }
            return Platform.Unknown;
        }
    }
}
