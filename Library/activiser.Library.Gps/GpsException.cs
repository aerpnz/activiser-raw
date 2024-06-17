using System;
using System.Collections.Generic;
using System.Text;

namespace activiser.Library.Gps
{
    /// <summary>
    /// General-purpose exception used by the library to trap 'underlying' errors.
    /// </summary>
    [global::System.Serializable]
    public class GpsException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        /// <summary>
        /// TBS
        /// </summary>
        public GpsException() { }
        /// <summary>
        /// TBS
        /// </summary>
        /// <param name="message"></param>
        public GpsException(string message) : base(message) { }
        /// <summary>
        /// TBS
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public GpsException(string message, Exception innerException) : base(message, innerException) { }
        //protected GpsException(
        //  global::System.Runtime.Serialization.SerializationInfo info,
        //  global::System.Runtime.Serialization.StreamingContext context)
        //    : base(info, context) { }
    }
}
