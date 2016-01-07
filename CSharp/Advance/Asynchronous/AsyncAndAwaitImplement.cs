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
    class AsyncAndAwaitImplement
    {
        static void Main(string[] args)
        {
            Console.WriteLine("begin main");

            Task<string> result = FirstFooAsync();

            Console.WriteLine("after async function");

            Console.WriteLine("result:{0}", result.Result);//此处会阻塞线程
            
            Console.ReadKey();
        }
        
        
        public static async Task<string> FirstFooAsync()
        {
            HttpClient client = new HttpClient();

            Task<string> stringTask = client.GetStringAsync("http://web.myresource.org/angular/current/version.txt");

            Console.WriteLine("hello, asynchronous!");

            string stringGot = await stringTask;

            Console.WriteLine("{0}", stringGot);

            return stringGot;
        }
    


    		
		private struct StateMachine : IAsyncStateMachine
		{
			public int state;

			public AsyncTaskMethodBuilder<string> builder;

			public HttpClient httpClient;

			public Task<string> task;

			public string result;

			private TaskAwaiter<string> outerAwaiter;

			private object stack;

			void IAsyncStateMachine.MoveNext()
			{
				string result = null;
				try
				{
					int num = this.state;
					if (num != -3)
					{
						TaskAwaiter<string> taskAwaiter;
						if (num != 0)
						{
							this.httpClient = new HttpClient();
							this.task = this.httpClient.GetStringAsync("http://web.myresource.org/angular/current/version.txt");
							Console.WriteLine("hello, asynchronous!");
							taskAwaiter = this.task.GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								this.state = 0;
								this.outerAwaiter = taskAwaiter;
                                this.builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, AsyncAndAwaitImplement.StateMachine>(ref taskAwaiter, ref this);
								return;
							}
						}
						else
						{
							taskAwaiter = this.outerAwaiter;
							this.outerAwaiter = default(TaskAwaiter<string>);
							this.state = -1;
						}
						string arg_B3_0 = taskAwaiter.GetResult();
						taskAwaiter = default(TaskAwaiter<string>);
						string text = arg_B3_0;
						this.result = text;
						Console.WriteLine("{0}", this.result);
						result = this.result;
					}
				}
				catch (Exception exception)
				{
					this.state = -2;
					this.builder.SetException(exception);
					return;
				}
				this.state = -2;
				this.builder.SetResult(result);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				this.builder.SetStateMachine(param0);
			}
		}
		
		
		public static Task<string> FirstFooSimulatingAsyncAndAwait()
		{
            AsyncAndAwaitImplement.StateMachine stateMachine = new StateMachine();
			stateMachine.builder = AsyncTaskMethodBuilder<string>.Create();
			stateMachine.state = -1;
			AsyncTaskMethodBuilder<string> builder = stateMachine.builder;
            builder.Start<AsyncAndAwaitImplement.StateMachine>(ref stateMachine);
			return stateMachine.builder.Task;
		}	
    }
}
