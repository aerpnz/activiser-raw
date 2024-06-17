using System;
using System.Runtime.InteropServices;


namespace activiser.Library.Gps
{
    /// <summary>
    /// State of the GPS Service
    /// </summary>
    public enum GpsServiceState : int
    {
        /// <summary>
        /// 
        /// </summary>
        Off = 0,
        /// <summary>
        /// 
        /// </summary>
        On = 1,
        /// <summary>
        /// 
        /// </summary>
        StartingUp = 2, 
        /// <summary>
        /// 
        /// </summary>
        ShuttingDown = 3,
        /// <summary>
        /// 
        /// </summary>
        Unloading = 4,
        /// <summary>
        /// 
        /// </summary>
        Uninitialized = 5,
        /// <summary>
        /// 
        /// </summary>
        Unknown = -1
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct FileTime
    {
        int dwLowDateTime;
        int dwHighDateTime;
    }

    /// <summary>
    /// GpsDeviceState holds the state of the gps device and the friendly name if the 
    /// gps supports them.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class GpsDeviceState
    {
        /// <summary>
        /// 
        /// </summary>
        public const int GpsMaxFriendlyName = 64;
        /// <summary>
        /// 
        /// </summary>
        public const int GpsDeviceStructureSize = 216;

        int serviceState;// = 0;
        /// <summary>
        /// State of the GPS Intermediate Driver service
        /// </summary>
        public GpsServiceState ServiceState
        {
            get {return (GpsServiceState)serviceState;}
        }

        int deviceState;// = 0;
        /// <summary>
        /// Status of the actual GPS device driver.
        /// </summary>
        public GpsServiceState DeviceState
        {
            get {return (GpsServiceState)deviceState;}
        }

        string friendlyName = "";
        /// <summary>
        /// Friendly name of the real GPS device we are currently using.
        /// </summary>
        public string FriendlyName
        {
            get {return friendlyName;}
        }

        /// <summary>
        /// Constructor of GpsDeviceState.  It copies values from the native pointer 
        /// passed in. 
        /// </summary>
        /// <param name="gpsDeviceHandle">Native pointer to memory that contains
        /// the GPS_DEVICE data</param>
        public GpsDeviceState(IntPtr gpsDeviceHandle)
        {
            // make sure our pointer is valid
            if (gpsDeviceHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException("gpsDeviceHandle");
            }

            // read in the service state which starts at offset 8
            serviceState = Marshal.ReadInt32(gpsDeviceHandle, 8);
            // read in the device state which starts at offset 12
            deviceState = Marshal.ReadInt32(gpsDeviceHandle, 12);

            // the friendly name starts at offset 88
            IntPtr pFriendlyName = (IntPtr)(gpsDeviceHandle.ToInt32() + 88);
            // marshal the native string into our gpsFriendlyName
            friendlyName = Marshal.PtrToStringUni(pFriendlyName);
        }
    }
}
