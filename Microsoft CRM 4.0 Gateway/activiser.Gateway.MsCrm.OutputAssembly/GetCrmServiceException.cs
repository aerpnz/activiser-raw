namespace activiser.WebService.CrmOutputGateway
{
    [global::System.Serializable]
    public class GetCrmServiceException : System.Exception
    {
        public GetCrmServiceException() { }
        public GetCrmServiceException(string message) : base(message) { }
        public GetCrmServiceException(string message, System.Exception inner) : base(message, inner) { }
        protected GetCrmServiceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}