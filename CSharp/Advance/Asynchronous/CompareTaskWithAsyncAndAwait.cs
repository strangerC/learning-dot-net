using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Advance.Asynchronous
{
    class CompareTaskWithAsyncAndAwait
    {

        static void Main(string[] args)
        {
            Console.WriteLine("before GetAngularVersionWithAsyncAndAwait");
            
            Task<string> stringTask = GetAngularVersionWithAsyncAndAwait();

            Console.WriteLine("after GetAngularVersionWithAsyncAndAwait");

            Console.WriteLine("GetAngularVersionWithAsyncAndAwait：{0}", stringTask.Result);


            Console.WriteLine("");


            Console.WriteLine("before GetAngularVersionWithTask");
            
            Task<string> angularVersion = GetAngularVersionWithTask();

            Console.WriteLine("after GetAngularVersionWithTask");

            Console.WriteLine("GetAngularVersionWithTask:{0}", angularVersion.Result);


            Console.WriteLine("");


            Console.WriteLine("before GetAngularVersion");

            string angularVersionText = GetAngularVersion();

            Console.WriteLine("after GetAngularVersion");

            Console.WriteLine("GetAngularVersion:{0}", angularVersionText);

            Console.ReadKey();

        }
        
        /// <summary>
        /// 通过async/await实现不阻塞当前线程的异步方法。注意如果某个线程最终需要获得异步方法的返回结果，那么线程还是会在
        /// 获取方法的地方阻塞。
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetAngularVersionWithAsyncAndAwait()
        {
            HttpClient client = new HttpClient();

            Task<string> stringTask = client.GetStringAsync("http://web.myresource.org/angular/current/version.txt");

            Console.WriteLine("before await");

            string stringGot = await stringTask;

            Console.WriteLine("after await");

            return stringGot;
        }

        /// <summary>
        /// 同步调用，则会在获取Task.Result的地方阻塞
        /// </summary>
        /// <returns></returns>
        public static string GetAngularVersion()
        {
            HttpClient client = new HttpClient();

            Task<string> stringTask = client.GetStringAsync("http://web.myresource.org/angular/current/version.txt");

            Console.WriteLine("before task");

            string stringGot = stringTask.Result;

            Console.WriteLine("after task");

            return stringGot;
        }

        /// <summary>
        /// 使用Task，重新开启一个线程用于处理会阻塞线程的操作。注意如果某个线程最终需要获得异步方法的返回结果，那么线程还是会在
        /// 获取方法的地方阻塞。
        /// </summary>
        /// <returns></returns>
        public static Task<string> GetAngularVersionWithTask()
        {
            Task<string> task = Task<string>.Factory.StartNew(() =>
            {

                HttpClient client = new HttpClient();

                Task<string> stringTask = client.GetStringAsync("http://web.myresource.org/angular/current/version.txt");

                Console.WriteLine("before task");

                string stringGot = stringTask.Result;

                Console.WriteLine("after task");

                return stringGot;
            });

            return task;
        }
    }
}
