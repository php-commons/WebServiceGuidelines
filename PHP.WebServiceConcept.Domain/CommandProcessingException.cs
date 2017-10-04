﻿using System;
using System.Runtime.Serialization;

namespace PHP.WebServiceConcept.Domain
{
    [Serializable]
    public class CommandProcessingException : Exception
    {
        public CommandProcessingException()
        {
        }

        public CommandProcessingException(string message) : base(message)
        {
        }

        public CommandProcessingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CommandProcessingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}