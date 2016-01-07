using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;

namespace Advance.Asynchronous
{
    class TaskSample
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread:{0}", Thread.CurrentThread.ManagedThreadId);

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            TaskFactory factory = new TaskFactory(token);

            Task<int> firstThreadId = factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne(10000);
                Thread thread = Thread.CurrentThread;
                Console.WriteLine("First Thread:{0}", thread.ManagedThreadId);
                return thread.ManagedThreadId;
            });

            Task<int> secondThreadId = Task.Factory.StartNew(() =>
            {

                Thread thread = Thread.CurrentThread;
                Console.WriteLine("Second Thread:{0}", thread.ManagedThreadId);
                return thread.ManagedThreadId;
            });

            //获取task的结果，需要等到task执行完毕，所以此处会阻塞Main所在线程。使用ContinueWith，将
            //设置回调函数，则不会阻塞线程。
            //Console.WriteLine("First Thread outer:{0}", firstThreadId.Result);
            firstThreadId.ContinueWith(t => 
            {
                Console.WriteLine("First Thread outer:{0}", firstThreadId.Result);
            });            

            Console.WriteLine("Main Thread:{0}", Thread.CurrentThread.ManagedThreadId);
            
            Console.ReadKey();

            source.Cancel();
            
            Console.ReadKey();
        }
    }
}
