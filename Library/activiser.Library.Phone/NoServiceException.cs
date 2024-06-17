using System;

namespace activiser.Library
{
    [global::System.Serializable]
    public class NoServiceException : Exception
    {
        public NoServiceException() { }
        public NoServiceException(string message) : base(message) { }
        public NoServiceException(string message, Exception inner) : base(message, inner) { }
    }
}
