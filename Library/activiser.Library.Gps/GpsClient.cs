using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Text;


namespace activiser.Library.Gps
{
    //public delegate void PositionChangedEventHandler(object sender, PositionChangedEventArgs args);
    //public delegate void DeviceStateChangedEventHandler(object sender, DeviceStateChangedEventArgs args);

    /// <summary>
    /// Summary description for GpsClient.
    /// </summary>
    public class GpsClient : IDisposable
    {
        IntPtr gpsHandle = IntPtr.Zero;                 // handle to the gps device
        IntPtr newPositionHandle = IntPtr.Zero;         // handle to the native event that is signalled when the GPS device gets a new position
        IntPtr deviceStateChangedHandle = IntPtr.Zero;  // handle to the native event that is signalled when the GPS device state changes
        IntPtr stopHandle = IntPtr.Zero;                // handle to the native event that we use to stop our event thread

        // holds our event thread instance
        System.Threading.Thread gpsEventThread;

        private event EventHandler<PositionChangedEventArgs> positionChanged;

        /// <summary>
        /// Event that is raised when the GPS locaction data changes
        /// </summary>
        public event EventHandler<PositionChangedEventArgs> PositionChanged
        {
            add
            {
                positionChanged += value;

                // create our event thread only if the user decides to listen
                CreateGpsEventThread();
            }
            remove
            {
                positionChanged -= value;
            }
        }


        private event EventHandler<DeviceStateChangedEventArgs> deviceStateChanged;

        /// <summary>
        /// Event that is raised when the GPS device state changes
        /// </summary>
        public event EventHandler<DeviceStateChangedEventArgs> DeviceStateChanged
        {
            add
            {
                deviceStateChanged += value;

                // create our event thread only if the user decides to listen
                CreateGpsEventThread();
            }
            remove
            {
                deviceStateChanged -= value;
            }
        }

        /// <summary>
        /// True: The GPS device has been opened. False: It has not been opened
        /// </summary>
        public bool Opened
        {
            get { return gpsHandle != IntPtr.Zero; }
        }

        /// <summary>
        /// 
        /// </summary>
        public GpsClient()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        ~GpsClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Opens the GPS device and prepares to receive data from it.
        /// </summary>
        public void Open()
        {
            if (!Opened)
            {
                // create handles for GPS events
                newPositionHandle = NativeMethods.CreateEvent(IntPtr.Zero, 0, 0, null);
                deviceStateChangedHandle = NativeMethods.CreateEvent(IntPtr.Zero, 0, 0, null);
                stopHandle = NativeMethods.CreateEvent(IntPtr.Zero, 0, 0, null);

                gpsHandle = NativeMethods.GPSOpenDevice(newPositionHandle, deviceStateChangedHandle, null, 0);
                if (gpsHandle == IntPtr.Zero)
                {
                    throw new GpsException("Error initialising GPS hardware");
                }
                else
                {
                    // if events were hooked up before the device was opened, we'll need
                    // to create the gps event thread.
                    if (positionChanged != null || deviceStateChanged != null)
                    {
                        CreateGpsEventThread();
                    }
                }
            }
        }

        /// <summary>
        /// Closes the gps device.
        /// </summary>
        public void Close()
        {
            if (gpsHandle != IntPtr.Zero)
            {
                int dontCare = NativeMethods.GPSCloseDevice(gpsHandle);
                gpsHandle = IntPtr.Zero;
            }

            // Set our native stop event so we can exit our event thread.
            if (stopHandle != IntPtr.Zero)
            {
                int dontCare = NativeMethods.EventModify(stopHandle, eventSet);
            }

            // block until our event thread is finished before
            // we close our native event handles
            lock (this)
            {
                if (newPositionHandle != IntPtr.Zero)
                {
                    int dontCare = NativeMethods.CloseHandle(newPositionHandle);
                    newPositionHandle = IntPtr.Zero;
                }

                if (deviceStateChangedHandle != IntPtr.Zero)
                {
                    int dontCare = NativeMethods.CloseHandle(deviceStateChangedHandle);
                    deviceStateChangedHandle = IntPtr.Zero;
                }

                if (stopHandle != IntPtr.Zero)
                {
                    int dontCare = NativeMethods.CloseHandle(stopHandle);
                    stopHandle = IntPtr.Zero;
                }
            }
        }

        /// <summary>
        /// Get the position reported by the GPS receiver
        /// </summary>
        /// <returns>GpsPosition class with all the position details</returns>
        public GpsPosition GetPosition()
        {
            return GetPosition(TimeSpan.Zero);
        }


        /// <summary>
        /// Get the position reported by the GPS receiver that is no older than
        /// the maxAge passed in
        /// </summary>
        /// <param name="maxAge">Max age of the gps position data that you want back. 
        /// If there is no data within the required age, null is returned.  
        /// if maxAge == TimeSpan.Zero, then the age of the data is ignored</param>
        /// <returns>GpsPosition class with all the position details</returns>
        public GpsPosition GetPosition(TimeSpan maxAge)
        {
            GpsPosition gpsPosition = null;
            if (Opened)
            {
                // allocate the necessary memory on the native side.  We have a class (GpsPosition) that 
                // has the same memory layout as its native counterpart
                IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(GpsPosition)));

                // fill in the required fields 
                gpsPosition = new GpsPosition();
                gpsPosition._version = 1;
                gpsPosition._size = Marshal.SizeOf(typeof(GpsPosition));

                // Marshal our data to the native pointer we allocated.
                Marshal.StructureToPtr(gpsPosition, ptr, false);

                // call native method passing in our native buffer
                int result = NativeMethods.GPSGetPosition(gpsHandle, ptr, 500000, 0);
                if (result == 0)
                {
                    // native call succeeded, marshal native data to our managed data
                    gpsPosition = (GpsPosition)Marshal.PtrToStructure(ptr, typeof(GpsPosition));

                    if (maxAge != TimeSpan.Zero)
                    {
                        // check to see if the data is recent enough.
                        if (!gpsPosition.TimeValid || DateTime.Now - maxAge > gpsPosition.Time)
                        {
                            gpsPosition = null;
                        }
                    }
                }

                // free our native memory
                Marshal.FreeHGlobal(ptr);
            }

            return gpsPosition;
        }

        /// <summary>
        /// Queries the device state.
        /// </summary>
        /// <returns>Device state information</returns>
        static public GpsDeviceState GetDeviceState()
        {
            GpsDeviceState device = null;

            // allocate a buffer on the native side.  Since the
            IntPtr pGpsDevice = Marshal.AllocHGlobal(GpsDeviceState.GpsDeviceStructureSize);

            // GPS_DEVICE structure has arrays of characters, it's easier to just
            // write directly into memory rather than create a managed structure with
            // the same layout.
            Marshal.WriteInt32(pGpsDevice, 1);	// write out GPS version of 1
            Marshal.WriteInt32(pGpsDevice, 4, GpsDeviceState.GpsDeviceStructureSize);	// write out dwSize of structure

            int result = NativeMethods.GPSGetDeviceState(pGpsDevice);

            if (result == 0)
            {
                // instantiate the GpsDeviceState class passing in the native pointer
                device = new GpsDeviceState(pGpsDevice);
            }
            else
            {
                // TODO: GetLastError
            }

            // free our native memory
            Marshal.FreeHGlobal(pGpsDevice);

            return device;
        }

        /// <summary>
        /// Creates our event thread that will receive native events
        /// </summary>
        private void CreateGpsEventThread()
        {
            // we only want to create the thread if we don't have one created already 
            // and we have opened the gps device
            if (gpsEventThread == null && gpsHandle != IntPtr.Zero)
            {
                // Create and start thread to listen for GPS events
                gpsEventThread = new System.Threading.Thread(new System.Threading.ThreadStart(WaitForGpsEvents));
                gpsEventThread.Name = "GPS Listener";
                gpsEventThread.Start();
            }
        }

        /// <summary>
        /// Method used to listen for native events from the GPS. 
        /// </summary>
        private void WaitForGpsEvents()
        {
            lock (this)
            {
                IntPtr handles = IntPtr.Zero;
                try
                {
                    bool listening = true;
                    // allocate 3 handles worth of memory to pass to WaitForMultipleObjects
                    handles = Marshal.AllocHGlobal(12);

                    // write the three handles we are listening for.
                    Marshal.WriteInt32(handles, 0, stopHandle.ToInt32());
                    Marshal.WriteInt32(handles, 4, deviceStateChangedHandle.ToInt32());
                    Marshal.WriteInt32(handles, 8, newPositionHandle.ToInt32());

                    while (listening)
                    {
                        int obj = NativeMethods.WaitForMultipleObjects(3, handles, 0, -1);
                        if (obj != waitFailed)
                        {
                            switch (obj)
                            {
                                case 0:
                                    // we've been signalled to stop
                                    listening = false;
                                    break;
                                case 1:
                                    // device state has changed
                                    if (deviceStateChanged != null)
                                    {
                                        deviceStateChanged(this, new DeviceStateChangedEventArgs(GetDeviceState()));
                                    }
                                    break;
                                case 2:
                                    // position has changed
                                    if (positionChanged != null)
                                    {
                                        positionChanged(this, new PositionChangedEventArgs(GetPosition()));
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            // wait failed, abort continued efforts....
                            listening = false;
                            break;
                        }
                    }
                }
                catch (System.Threading.ThreadAbortException)
                {
                    // expected, on occasion.
                }
                finally
                {
                    // free the memory we allocated for the native handles
                    if (handles != IntPtr.Zero) Marshal.FreeHGlobal(handles);

                    // clear our gpsEventThread so that we can recreate this thread again
                    // if the events are hooked up again.
                    gpsEventThread = null;
                }
            }
        }

        const int waitFailed = -1;
        const int eventSet = 3;

        #region IDisposable Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            this.Close(); // if not already closed !
            if(this.gpsEventThread != null) this.gpsEventThread.Abort();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
