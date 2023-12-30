namespace SqlCrudCreatorCore.Utilites
{
    
    public class SqlCrudCreatorException : Exception
        {
            public SqlCrudCreatorException() { }
            public SqlCrudCreatorException(string message) : base(message) { }
            public SqlCrudCreatorException(string message, Exception inner) : base(message, inner) { }
            protected SqlCrudCreatorException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

}
