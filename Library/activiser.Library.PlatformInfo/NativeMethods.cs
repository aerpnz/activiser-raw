using System;
using System.Text;
using System.Runtime.InteropServices;

namespace activiser.Library.PlatformInfo
{
    public static class NativeMethods
    {
        [DllImport("coredll.dll")]
        internal static extern int SystemParametersInfo(uint uiAction, uint uiParam, StringBuilder pvParam, uint fWinIni);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        internal static extern int SystemParametersInfoDT(uint uiAction, uint uiParam, StringBuilder pvParam, uint fWinIni);

        public static string GetSystemParameter(int parameter)
        {
            if (Environment.OSVersion.Platform == PlatformID.WinCE)
            {
                StringBuilder sb = new StringBuilder(128);
                if (SystemParametersInfo((uint)parameter, (uint)sb.Capacity, sb, 0) == 0)
                    throw new SystemParametersInfoException(Properties.Resources.GetSystemParameterExceptionMessage);
                return sb.ToString();
            }
            else
            {
                StringBuilder sb = new StringBuilder(128);
                if (SystemParametersInfoDT((uint)parameter, (uint)sb.Capacity, sb, 0) == 0)
                    throw new SystemParametersInfoException(Properties.Resources.GetSystemParameterExceptionMessage);
                return sb.ToString();
            }
        }
    }
}
