using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Fundation.IO
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseDirectory = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;// System.AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(baseDirectory);
            Stream stream = new FileStream("Resource\\ResourceForIO.resx", FileMode.Open);
            byte[] bytes = new byte[100];
            stream.Read(bytes, 0, 100);
            stream.Close();
            foreach(byte aByte in bytes) {
                Console.WriteLine(aByte);
            }
        }
    }
}
