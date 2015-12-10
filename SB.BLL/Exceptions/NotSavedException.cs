using System;
using System.Runtime.Serialization;

namespace SB.Domain.Exceptions
{
    [Serializable]
    public class NotSavedException : Exception
    {
        public NotSavedException()
        {
        }

        public NotSavedException(string message) : base(message)
        {
        }

        public NotSavedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotSavedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}