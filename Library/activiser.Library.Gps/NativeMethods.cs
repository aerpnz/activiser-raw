using System;
using System.Runtime.InteropServices;
using System.Text;

namespace activiser.Library.Gps
{
    internal sealed class NativeMethods
    {
        private NativeMethods() { }
        [DllImport("gpsapi.dll")]
        public static extern IntPtr GPSOpenDevice(IntPtr hNewPositionData, IntPtr hDeviceStateChange, string szDeviceName, int dwFlags);

        [DllImport("gpsapi.dll")]
        public static extern int GPSCloseDevice(IntPtr hGPSDevice);

        [DllImport("gpsapi.dll")]
        public static extern int GPSGetPosition(IntPtr hGPSDevice, IntPtr pGPSPosition, int dwMaximumAge, int dwFlags);

        [DllImport("gpsapi.dll")]
        public static extern int GPSGetDeviceState(IntPtr pGPSDevice);

        [DllImport("coredll.dll")]
        public static extern IntPtr CreateEvent(IntPtr lpEventAttributes, int bManualReset, int bInitialState, StringBuilder lpName);

        [DllImport("coredll.dll")]
        public static extern int CloseHandle(IntPtr hObject);

        [DllImport("coredll.dll")]
        public static extern int WaitForMultipleObjects(int nCount, IntPtr lpHandles, int fWaitAll, int dwMilliseconds);

        [DllImport("coredll.dll")]
        public static extern int EventModify(IntPtr hHandle, int dwFunc);
    }
}
