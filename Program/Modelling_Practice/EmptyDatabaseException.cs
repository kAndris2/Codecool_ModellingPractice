using System;
using System.Collections.Generic;
using System.Text;

namespace api
{
    class EmptyDatabaseException : Exception
    {
        public EmptyDatabaseException(string message) : base(message)
        {
        }
    }
}
