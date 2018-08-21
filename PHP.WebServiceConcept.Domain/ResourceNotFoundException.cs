using System;
using System.Runtime.Serialization;

namespace PHP.WebServiceConcept.Domain
{
    public class ResourceNotFoundException : Exception
    {
        public string ResourceName { get; }

        public ResourceNotFoundException(string resourceName) : base()
        {
            ResourceName = resourceName;
        }
        
        public ResourceNotFoundException(string resourceName, string message) : base(message)
        {
            ResourceName = resourceName;
        }
        
        public ResourceNotFoundException(string resourceName, string message, Exception innerException) 
            : base(message, innerException)
        {
            ResourceName = resourceName;
        }

        public ResourceNotFoundException(string resourceName, SerializationInfo serializationInfo,
            StreamingContext context)
            : base(serializationInfo, context)
        {
            ResourceName = resourceName;
        }
    }
}