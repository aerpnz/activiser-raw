namespace activiser.WebService.XmlFolderOutputGateway
{
    [global::System.Serializable]
    public class TransactionSaveException : System.Exception
    {
        public TransactionSaveException() { }
        public TransactionSaveException(string message) : base(message) { }
        public TransactionSaveException(string message, System.Exception inner) : base(message, inner) { }
        protected TransactionSaveException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}