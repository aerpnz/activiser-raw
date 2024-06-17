using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using activiser.Library.MinorPlanet.Properties;

namespace activiser.Library.MinorPlanet
{
    public partial class DcuClient
    {
        public event EventHandler<DeviceStateChangedEventArgs> DeviceIdChanged;
        public event EventHandler<PositionChangedEventArgs> PositionChanged;
        public event EventHandler<DeviceStateChangedEventArgs> GprsIpChanged;
        public event EventHandler<DeviceStateChangedEventArgs> DcuVersionChanged;

        public bool Running { get; private set; }

        private string _DcuId;
        public string DcuId
        {
            get
            {
                return _DcuId;
            }
            private set
            {
                _DcuId = value;
                EventHandler<DeviceStateChangedEventArgs> eh = DeviceIdChanged;
                if (eh != null) eh(this, new DeviceStateChangedEventArgs());
            }
        }

        public string PreviousLocation { get; private set; }
        private string _Location;
        public string Location
        {
            get
            {
                return _Location;
            }
            private set
            {
                try
                {
                    GpsPosition newPosition = new GpsPosition(value);
                    if (Position == null)
                    {
                        Position = newPosition;
                        if(PreviousPosition == null) PreviousPosition = Position;
                    }
                    else if (newPosition.PositionValid && newPosition.HasMovedFrom(Position))
                    {
                        PreviousPosition = Position;
                        PreviousLocation = _Location;
                        Position = newPosition;
                    }
                    _Location = value;
                    EventHandler<PositionChangedEventArgs> eh = PositionChanged;
                    if (eh != null) eh(this, new PositionChangedEventArgs(newPosition));
                }
                catch (FormatException)
                {
                    _Location = string.Empty;
                    Position = null;
                }
            }
        }

        public GpsPosition Position { get; private set; }
        public GpsPosition PreviousPosition { get; private set; }

        private IPAddress _GprsIpAddress;
        public IPAddress GprsIpAddress
        {
            get
            {
                return _GprsIpAddress;
            }
            private set
            {
                _GprsIpAddress = value;
                EventHandler<DeviceStateChangedEventArgs> eh = GprsIpChanged;
                if (eh != null) eh(this, new DeviceStateChangedEventArgs());
            }
        }

        private string _DcuVersion;
        public string DcuVersion
        {
            get
            {
                return _DcuVersion;
            }
            private set
            {
                _DcuVersion = value;
                EventHandler<DeviceStateChangedEventArgs> eh = DcuVersionChanged;
                if (eh != null) eh(this, new DeviceStateChangedEventArgs());
            }
        }

        public void Start()
        {
            if (_disposed) throw new ObjectDisposedException("DcuClient");
            if (_inboundMessages != null) throw new InvalidOperationException("Already started");
            _abort = false;
            CreateMessageQueueWaitHandler();
            CreateMessageQueue();
            StartInterpreter();
            StartListener();
            StartTalker();
            RequestDcuVersion();
            RequestDeviceId();
            RequestLocation();
            Running = true;
        }

        public bool Tracking { get { return this._positionPollingThread != null; } }

        public void StartTracking()
        {
            this.StartPositionPoller();
        }

        public void StopTracking()
        {
            this.StopPositionPoller();
        }

        public void RequestLocation()
        {
            SendMessage(1, Resources.PositionRequestString);
        }

        public void RequestDeviceId()
        {
            SendMessage(7, null);
        }

        public void RequestGprsIp()
        {
            SendMessage(8, null);
        }

        public void RequestDcuVersion()
        {
            SendMessage(10, null);
        }
    }
}
