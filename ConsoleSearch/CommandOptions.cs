using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSearch
{
    public class CommandOptions
    {
        public bool CasesensitiveFlag { get; private set; }
        public void Run(string input)
        {
            switch (input)
            {
                case "/cs=off":
                    Console.WriteLine("casesensitive is now 'off'",ConsoleColor.Green);
                    CasesensitiveFlag = false;
                    break;
                case "/cs=on":
                    Console.WriteLine("casesensitive is now 'on'",ConsoleColor.Green);
                    CasesensitiveFlag = true;
                    break;
                default:
                    if (input.Contains('/'))
                    {
                        Console.WriteLine("command: " + input + " doesn't exist",ConsoleColor.Red);
                    }
                    break;
            }
        }
        public void Commands()
        {
            Console.WriteLine("Command options: for casesensitive write '/cs=on' can be 'on' or 'off'");
        }
    }
}
