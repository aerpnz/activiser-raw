using System;
using System.Text;
using System.Runtime.InteropServices;

namespace activiser.Library.PlatformInfo
{
    [global::System.Serializable]
    public class SystemParametersInfoException : Exception
    {
        public SystemParametersInfoException() { }
        public SystemParametersInfoException(string message) : base(message) { }
        public SystemParametersInfoException(string message, Exception inner) : base(message, inner) { }
    }
}