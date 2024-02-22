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
                case "/casesensitive=off":
                    Console.WriteLine("casesensitive is off");
                    CasesensitiveFlag = false;
                    break;
                case "/casesensitive=on":
                    Console.WriteLine("casesensitive=on");
                    CasesensitiveFlag = true;
                    break;
            }
        }
    }
}
