using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    class InvalidInputException : Exception
    {
        public InvalidInputException (string message) : base(message)
        {
        }
    }
}
