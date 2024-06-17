using System;

namespace activiser.Library
{
    [global::System.Serializable]
    public class NoRadioException : Exception
    {
        public NoRadioException() { }
        public NoRadioException(string message) : base(message) { }
        public NoRadioException(string message, Exception inner) : base(message, inner) { }
    }
}
