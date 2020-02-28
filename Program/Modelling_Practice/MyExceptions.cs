using System;
using System.Collections.Generic;
using System.Text;

namespace api
{
    class MyExceptions : Exception
    {
        public MyExceptions(string message) : base(message) { }
    }

    class EmptyDatabaseException : MyExceptions
    {
        public EmptyDatabaseException(string message) : base(message) { }
    }

    class InvalidInputException : MyExceptions
    {
        public InvalidInputException(string message) : base(message) { }
    }

    class UnknownKeyException : MyExceptions
    {
        public UnknownKeyException(string message) : base(message) { }
    }

    class SWWException : MyExceptions
    {
        public SWWException(string message) : base(message) { }
    }

    class InvalidCar : MyExceptions
    {
        public InvalidCar(string message) : base(message) { }
    }
}
