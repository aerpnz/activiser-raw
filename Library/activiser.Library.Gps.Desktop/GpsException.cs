using System;
using System.Collections.Generic;
using System.Text;

namespace activiser.Library.Gps
{
    [global::System.Serializable]
    public class GpsException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public GpsException() { }
        public GpsException(string message) : base(message) { }
        public GpsException(string message, Exception inner) : base(message, inner) { }
        //protected GpsException(
        //  global::System.Runtime.Serialization.SerializationInfo info,
        //  global::System.Runtime.Serialization.StreamingContext context)
        //    : base(info, context) { }
    }
}
