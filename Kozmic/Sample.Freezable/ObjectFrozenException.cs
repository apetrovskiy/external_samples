namespace Sample.Freezable
{
    using System;
    using System.Runtime.Serialization;

    public sealed class ObjectFrozenException : Exception
    {
        public ObjectFrozenException()
        {
        }

        public ObjectFrozenException(string message) : base(message)
        {
        }

        public ObjectFrozenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private ObjectFrozenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}