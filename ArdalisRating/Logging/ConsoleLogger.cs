using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArdalisRating.Logging
{
    internal class ConsoleLogger
    {
        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
