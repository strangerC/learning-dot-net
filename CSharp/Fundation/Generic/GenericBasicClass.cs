using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fundation.Generic
{
    class  GenericBasicClass<T>
    {
        public void PrintType()
        {
           Type type = typeof(T);
           Console.WriteLine(type);
        }
    }
}
