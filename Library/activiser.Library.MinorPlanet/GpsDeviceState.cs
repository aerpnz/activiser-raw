using System;
using System.Runtime.InteropServices;


namespace activiser.Library.Gps
{
    /// <summary>
    /// State of the GPS Service
    /// Implemented in Minor Planet library for compatibility
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

    /// <summary>
    /// GpsDeviceState holds the state of the gps device and the friendly name if the 
    /// gps supports them.
    /// Implemented in Minor Planet library for compatibility
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class GpsDeviceState
    {
        GpsServiceState serviceState;// = 0;
        public GpsServiceState ServiceState
        {
            get {return serviceState;}
        }

        GpsServiceState deviceState;// = 0;
        /// <summary>
        /// Status of the actual GPS device driver.
        /// </summary>
        public GpsServiceState DeviceState
        {
            get {return deviceState;}
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
        /// Constructor of GpsDeviceState.  
        /// Implemented in Minor Planet library for compatibility
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

            serviceState = GpsServiceState.Off;
            deviceState = GpsServiceState.Off;

            friendlyName = "Minor Planet DCU";
        }
    }
}
