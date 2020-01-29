using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    class ConsoleLogger : ILogger
    {
        public void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[WARNING]: ");
            Console.ResetColor();
            Console.Write(message);
            Console.WriteLine();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR]: ");
            Console.ResetColor();
            Console.Write(message);
            Console.WriteLine();
        }

        public void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[INFO]: ");
            Console.ResetColor();
            Console.Write(message);
            Console.WriteLine();
        }

        public ConsoleLogger()
        {
        }
    }
}
