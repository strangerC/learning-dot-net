using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace Advance.Asynchronous
{
    /// <summary>
    /// 反编使用async/await的类，研究它的实现
    /// </summary>
    class AsyncAndAwait
    {
        static void Main(string[] args)
        {
            Console.WriteLine("begin main");

            Task<string> result = FooAsync();

            Console.WriteLine("after async function");

            Console.WriteLine("result:{0}", result.Result);//此处会阻塞线程
            
            Console.ReadKey();
        }
        
        
        public static async Task<string> FooAsync()
        {
            HttpClient client = new HttpClient();

            Task<string> versionTask = client.GetStringAsync("http://web.myresource.org/angular/current/version.txt");
            Task<string> tempTask = client.GetStringAsync("http://web.myresource.org/angular/current/temp.txt");

            Console.WriteLine("hello, asynchronous!");

            string version = await versionTask;

            string temp = await tempTask;

            Console.WriteLine("{0}", version);

            return version + temp;
        }
    


    }
}
