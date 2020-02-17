using System;
using System.Collections.Generic;
using System.Text;

namespace api
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
            Console.Write(message + "\n");
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
