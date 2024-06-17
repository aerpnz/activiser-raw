using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace activiser.Library.MinorPlanet
{
    struct DcuMessageItem
    {
        public DateTime Timestamp;
        public string Message;

        public DcuMessageItem(DateTime timestamp, string message)
        {
            Timestamp = timestamp;
            Message = message;
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}", Timestamp, Message);
        }
    }
}
