using System;

namespace CSharp.Series.Tests.Asserters
{
    public class NonExistingEventLogException : Exception
    {
        public NonExistingEventLogException()
        {
        }

        public NonExistingEventLogException(string message) : base(message)
        {
        }

        public NonExistingEventLogException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}