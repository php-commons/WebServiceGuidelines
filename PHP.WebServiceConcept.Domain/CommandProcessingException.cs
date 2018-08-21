using System;
using System.Runtime.Serialization;

namespace PHP.WebServiceConcept.Domain
{
    [Serializable]
    public class CommandProcessingException : Exception
    {
        /// <summary>
        /// A hint that a command can be resubmitted and the failure was intermittent.
        /// This is merely a hint and does not need to be followed.
        /// </summary>
        public bool CanRetry { get; }

        public CommandProcessingException(bool canRetry=true)
        {
            CanRetry = canRetry;
        }

        public CommandProcessingException(string message, bool canRetry=true) : base(message)
        {
            CanRetry = canRetry;
        }

        public CommandProcessingException(string message, Exception innerException, bool canRetry=true) 
            : base(message, innerException)
        {
            CanRetry = canRetry;
        }

        protected CommandProcessingException(SerializationInfo info, StreamingContext context, bool canRetry=true) 
            : base(info, context)
        {
            CanRetry = canRetry;
        }
    }
}