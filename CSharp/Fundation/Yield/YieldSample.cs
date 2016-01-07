using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fundation.Yield
{
    class YieldSample
    {

        static void Main(string[] args)
        {
            foreach (int i in Power(2, 5))
            {
                Console.WriteLine("{0}", i);
            }

            foreach (int i in PowerWithSimulatingYield(2, 5))
            {
                Console.WriteLine("{0}", i);
            }

            Console.ReadKey();
        }
        
        
        public static IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }

            yield return 6;
            yield return 7;
            yield return 8;
        }

        public static IEnumerable<int> PowerWithSimulatingYield(int number, int exponent)
        {
            PowerWithSimulatingYield pwsy = new PowerWithSimulatingYield(number, exponent);
            return pwsy;
        }

    }

    class PowerWithSimulatingYield : IEnumerable<int>, IEnumerable, IEnumerator<int>, IEnumerator
    {
        private int number;
        private int exponent;
        private int state;
        private int current = 1;
        private int spp;

        public PowerWithSimulatingYield(int number, int exponent)
        {
            this.number = number;
            this.exponent = exponent;            
        }

        public object Current
        {
            get { return current; }
        }

        public bool MoveNext()
        {
            bool result = false;
            switch (state)
            {
                case 0:
                    state = 1;
                    result = true;
                    current = current * number;
                    spp++;
                    break;
                case 1:
                    result = true;
                    current = current * number;
                    spp++;
                    break;
                case 3:
                    state = 4;
                    current = 6;
                    return true;
                case 4:
                    state = 5;
                    current = 7;
                    return true;
                case 5:
                    state = 6;
                    current = 8;
                    return true;
                case 6:
                    return false;
                default:
                    result = false;
                    break;                    
            }

            if (spp >= exponent)
            {
                state = 3;
                result = true;
            }

            return result;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return this;
        }

        int IEnumerator<int>.Current
        {
            get { return current; }
        }

        public void Dispose()
        {            
        }
    }
}
