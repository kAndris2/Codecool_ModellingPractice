using System;
using System.Collections.Generic;
using System.Text;

namespace api
{
    class InvalidInputException : Exception
    {
        public InvalidInputException (string message) : base(message)
        {
        }
    }
}
