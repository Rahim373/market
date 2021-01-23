using System;

namespace Market.Common.Exceptions
{
    public class InvalidQueryException : Exception
    {
        public InvalidQueryException() : base("Invalid query.")
        {
            
        }

        public InvalidQueryException(string message) : base(message)
        {
            
        }

        public InvalidQueryException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}