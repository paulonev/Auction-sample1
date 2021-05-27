using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Exceptions
{
    public class AddNullSlotException : Exception
    {
        public AddNullSlotException(Slot slot) : base("Attempted to add item that references to `null`")
        {
            
        }
        
        protected AddNullSlotException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public AddNullSlotException(string message) : base(message)
        {
        }

        public AddNullSlotException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}