using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fundation.Generic
{
    class Runner
    {
        static void Main(string[] args)
        {
            GenericBasicClass<string> gbc = new GenericBasicClass<string>();
            gbc.PrintType();
        }
    }
}
