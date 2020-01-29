using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    class EmptyDatabaseException : Exception
    {
        public EmptyDatabaseException(string message) : base(message)
        {
        }
    }
}
