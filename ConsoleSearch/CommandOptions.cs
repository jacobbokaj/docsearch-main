using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSearch
{
    public class CommandOptions
    {
        public void Run(string input)
        {
            switch (input)
            {
                case "sovs":
                    Console.WriteLine("Sovs :D");
                    break;
            }
        }
    }
}
