using System;

namespace DomainLayer.Exceptions.Models
{
    public class ForestException : Exception
    {

        public ForestException()
        {
            
        }

        public ForestException(string message) :base(message)
        {
            
        }

        public ForestException(string message, Exception innerException ) : base(message, innerException)
        {
            
        }
    }
}