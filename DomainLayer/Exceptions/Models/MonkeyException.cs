using System;

namespace DomainLayer.Exceptions.Models
{
    public class MonkeyException : Exception
    {

        public MonkeyException()
        {

        }

        public MonkeyException(string message) : base(message)
        {

        }

        public MonkeyException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}