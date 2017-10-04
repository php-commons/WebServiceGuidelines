using System;
using System.Runtime.Serialization;

namespace PHP.WebServiceConcept.Domain
{
    [Serializable]
    public class QueryProcessingException : Exception
    {
        public QueryProcessingException()
        {
        }

        public QueryProcessingException(string message) : base(message)
        {
        }

        public QueryProcessingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QueryProcessingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}