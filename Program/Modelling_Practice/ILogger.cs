using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    interface ILogger
    {
        void Info(string message);
        void Error(string message);
        void Warning(string message);
    }
}
