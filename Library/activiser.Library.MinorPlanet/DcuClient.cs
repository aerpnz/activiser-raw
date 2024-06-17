using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using activiser.Library.MinorPlanet.Properties;

namespace activiser.Library.MinorPlanet
{
    public partial class DcuClient : Component
    {
        private bool _disposed;
        private bool _abort;
        private bool _abortPositionPoller;

        IPAddress DcuIp = new IPAddress(new byte[] { 192, 168, 1, 10 });
        int DcuPort = 20111;

        IPAddress MyIp = new IPAddress(new byte[] { 192, 168, 1, 11 });

        Thread _listenerThread;
        int _listenerPort = (new Random()).Next(4200, 64200);

        private UdpClient _listener;
        private UdpClient _talker;

        Thread _interpreterThread;
        Thread _positionPollingThread;

        private Queue<DcuMessageItem> _inboundMessages;
        private AutoResetEvent messageEnqueued;

        private readonly static char[] WhiteSpaceChars = new char[] { '\n', '\r', '\t', ' ' };

        public DcuClient()
        {
            Start();
        }

        public DcuClient(bool startGpsPolling)
        {
            Start();
            if (startGpsPolling)
            {
                StartPositionPoller();
            }
        }

        ~DcuClient()
        {
            Stop();
        }

        private void CreateMessageQueue()
        {
            _inboundMessages = new Queue<DcuMessageItem>(10);
        }

        private void CreateMessageQueueWaitHandler()
        {
            messageEnqueued = new AutoResetEvent(false);
        }

        public void Stop()
        {
            _abort = true;
            StopPositionPoller();
            StopTalker();
            StopListener();
            StopInterpreter();
        }

        private void StartTalker()
        {
            if (_disposed || _abort) return;
            _talker = new UdpClient(0);
            _talker.Connect(DcuIp, DcuPort);
        }

        private void StopTalker()
        {
            if (_talker == null) return;
            _talker.Close();
            _talker = null;
        }

        private void StartListener()
        {
            if (_disposed || _abort) return;
            if (_listenerThread != null) throw new InvalidOperationException(Resources.ListenerThreadNotNull);
            _listenerThread = new Thread(new ThreadStart(Listener));
            _listenerThread.Name = string.Format(Resources.ListenerThreadName, DateTime.UtcNow); 
            _listenerThread.Start();
        }

        private void StopListener()
        {
            if (_listenerThread == null) return;
            if (_listener != null) _listener.Close();
            _listenerThread.Abort();
            _listenerThread = null;
        }

        private void Listener()
        {
            _listener = new UdpClient(_listenerPort);
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);

            while (_listener != null)
            {
                byte[] returnedBytes = _listener.Receive(ref ep);
                if (_abort) return;
                string returnedString = Encoding.ASCII.GetString(returnedBytes, 0, returnedBytes.Length);

                lock (_inboundMessages)
                {
                    DcuMessageItem newDcuMessageItem = new DcuMessageItem(DateTime.Now, returnedString);
                    //_log += DateTime.Now.ToString() + ' ' + returnedString + "\r\n";
                    _inboundMessages.Enqueue(newDcuMessageItem);
                    messageEnqueued.Set();
                }
                if (_disposed || _abort) return;
            }
        }

        private void StartPositionPoller()
        {
            if (_disposed || _abort) return;
            if (_positionPollingThread != null) throw new InvalidOperationException(Properties.Resources.PositionPollingThreadNotNull);
            _abortPositionPoller = false;
            _positionPollingThread = new Thread(new ThreadStart(PositionPoller));
            _positionPollingThread.Name = string.Format(Resources.PositionPollingThreadName, DateTime.UtcNow); 
            _positionPollingThread.Start();
        }

        private void StopPositionPoller()
        {
            if (_positionPollingThread == null) return;
            _abortPositionPoller = true;
            _positionPollingThread.Abort();
            _positionPollingThread = null;
        }

        private void PositionPoller()
        {
            while (true)
            {
                RequestGprsIp();
                RequestLocation();
                if (_disposed || _abort || _abortPositionPoller) return;
                Thread.Sleep(10000);
                if (_disposed || _abort || _abortPositionPoller) return;
            }
        }

        private void StartInterpreter()
        {
            if (_disposed || _abort) throw new ObjectDisposedException("DcuClient");
            if (_interpreterThread != null) throw new InvalidOperationException(Resources.InterpreterThreadNotNull);
            _interpreterThread = new Thread(new ThreadStart(Interpreter));
            _interpreterThread.Name = string.Format(Resources.InterpreterThreadName, DateTime.UtcNow);
            _interpreterThread.Start();
        }

        private void StopInterpreter()
        {
            if (_interpreterThread == null) return;
            _interpreterThread.Abort();
            _interpreterThread = null;
        }

        private void Interpreter()
        {
            while (!_abort)
            {
                lock (_inboundMessages)
                {
                    while (_inboundMessages.Count != 0)
                    {
                        DcuMessageItem message = _inboundMessages.Dequeue();
                        if (string.IsNullOrEmpty(message.Message)) continue;

                        if(message.Message.StartsWith("1|")) {
                            Location = message.Message.Substring(2, message.Message.Length - 4); // trim CR/LF from end.
                        }
                        else if (message.Message.StartsWith("7|")) {
                            DcuId = message.Message.Substring(2);
                        }
                        else if(message.Message.StartsWith("8|")){
                            GprsIpAddress = IPAddress.Parse(message.Message.Substring(2));
                        }
                        else if(message.Message.StartsWith("10|")) {
                            DcuVersion = message.Message.Substring(3);
                        }
                    }
                }
                if (_disposed || _abort) return;
                messageEnqueued.WaitOne();
                if (_disposed || _abort) return;
            }
        }

        private void SendMessage(int messageId, string arguments)
        {
            if (_disposed || _abort) return;
            if (_talker == null) StartTalker();

            string message = string.Format("{0}|{1}|{2}|{3}",
                messageId,
                MyIp.ToString(),
                _listenerPort,
                string.IsNullOrEmpty(arguments) ? string.Empty : arguments
            );
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            _talker.Send(messageBytes, messageBytes.Length);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposed = true;
                Stop();
            }
            base.Dispose(disposing);
        }
    }
}
