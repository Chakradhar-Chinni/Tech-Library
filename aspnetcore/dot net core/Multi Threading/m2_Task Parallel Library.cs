

<<h2>> Tasks VS Threads


| **Feature**           | **Thread**                           | **Task**                              |
| --------------------- | ------------------------------------ | ------------------------------------- |
| **Abstraction Level** | Low-level (manual thread management) | High-level (built on ThreadPool)      |
| **Thread Creation**   | Creates a new OS thread              | Uses ThreadPool threads (reused)      |
| **Overhead**          | High (expensive to create/destroy)   | Low (efficient reuse)                 |
| **Return Values**     | Manual handling (shared variables)   | Built-in (`Task<T>` supports results) |
| **Async Support**     | No                                   | Yes (`async/await`)                   |
| **Cancellation**      | Manual (custom flags)                | Built-in (`CancellationToken`)        |
| **Continuations**     | Manual (complex)                     | Built-in (`ContinueWith`, `await`)    |
| **Best For**          | Long-running, dedicated work         | Short-lived, concurrent operations    |


<<h2>> Tasks Implementation without returning anything

using System.Threading;
namespace Threads.App
{
    class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread); //False
            Task t1 = new Task(M1);
            t1.Start();
            Console.ReadKey();
        }

        public static void M1()
        {
            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread); //True
            Console.WriteLine("M1 Started");
            Thread.Sleep(2000);
            Console.WriteLine("M1 completed");
        }
    }
}



<<h2>> Tasks Implementation with string return type

- t1.Wait() blocks the main thread until t1 completes

using System.Threading;
namespace Threads.App
{
    class Program
    {
        public static void Main(String[] args)
        {
            Task<string> t1 = new Task<string>(M1);
            
            t1.Start();
            t1.Wait();

            Console.WriteLine(t1.Result);

            Console.WriteLine("Program execution completed");
            Console.ReadKey();
        }

        public static string M1()
        {
            Thread.Sleep(5000);
            return "Hello";
        }
    }
}
	
	
<<h2>> Tasks with IO 

1. t1.Result blocks the main thread until t1 exits and prints the result
	basically, t1.Result = t1.Wait() + holding result
  

https://jsonplaceholder.typicode.com/ - free REST API without limits

using System.Threading;
namespace Threads.App
{
    class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);

            Task<string> t1 = Task.Factory.StartNew<string>(() => GetPosts("https://jsonplaceholder.typicode.com/posts/12"));

            Console.WriteLine("Response\n");
            Console.WriteLine(t1.Result);

            Console.WriteLine("Program execution completed");
            Console.ReadKey();
        }

        public static string GetPosts(string uri)
        {
            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);
            using (var client = new System.Net.WebClient())
            {
                return client.DownloadString(uri);
            }
        }
    }
}

<<h2>> task.Start() vs task.Run()

task.start
 - Used when you explicitly create a Task instance using its constructor.
 - You must call Start() manually after creating the task.
 Example:  
	Task t = new Task(() => Console.WriteLine("Hello from Task.Start"));
	t.Start(); // Starts the task

task.Run()
	- static helper method that creates a task, starts immediately on ThreadPool
	- modern way, 
	- Automatically schedules the task on the ThreadPool.
	
Example:
	Task.Run(() => Console.WriteLine("Hello from Task.Run"));
 



<<h2>> Task.Run() example

- Task is run in async approach 

using System;
using System.Threading;
using System.Threading.Tasks;
namespace Threads.App
{
    class Program
    {
        public static async Task Main(String[] args)
        {
          await Task.Run(M1);
           await Task.Run(M2);
		}
		 public static void M1()
		 {
		  Console.WriteLine("M1");
		 }
		 public static void M2()
		 {
			 Console.WriteLine("M2");
		 }
	}
}

Output:
M1
M2

<<h2>> Tasks Continuation  with return 

1. after a method execution, callbacks are used to immediately invoke another method. In Task Parallel library - TPL, we have continuation
2. initial - Task.Run() queues a task to thread pool
3. When the antecedent transitions to a final state (RanToCompletion/Faulted/Canceled), the TaskScheduler queues the continuation according to its 	 TaskContinuationOptions.  The continuation receives the antecedent task instance (the antecedent parameter) to read status/result/exception.
4. ThreadPool internally uses System.Threading.Tasks.TaskScheduler for Task Management
5. The antecedent completes, its thread returns to the pool. The continuation is a separate task scheduled by TaskScheduler (thread pool in console apps), so it can run on any pool thread, not necessarily the same one. In other words, antecedent and continuation runs on different threads
6. You can return any type; the task becomes Task<T> for that type.

using System;
using System.Threading;
using System.Threading.Tasks;
namespace Threads.App
{
    class Program
    {
        public static async Task Main(String[] args)
        {
            var initial = Task.Run(() =>
            {
                Console.WriteLine("Initial task running...");
                Thread.Sleep(2000); // simulate work
                return 21;
            });

            var continuation = initial.ContinueWith(antecedent =>
            {
                Console.WriteLine($"Continuation received: {antecedent.Result}");
                return antecedent.Result * 2;
            });

            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread); //false
            int result = await continuation;
            Console.WriteLine($"Continuation result: {result}");
            
            Console.WriteLine("End of program");
        }
    }
}
   


<<h2>> Task Continuation Options (explain using AI)
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Threads.App
{
    class Program
    {
        public static async Task Main(String[] args)
        {
            // RanToCompletion example: task completes successfully and continuation runs only on success
            var success = Task.Run(() =>
            {
                Console.WriteLine("Success task running...");
                Thread.Sleep(300); // simulate work
                return 21; // result makes this a Task<int>
            });

            // Continuation filtered with OnlyOnRanToCompletion; will NOT run if faulted/canceled
            var successCont = success.ContinueWith(a =>
            {
                Console.WriteLine($"Success antecedent status: {a.Status}"); // should be RanToCompletion
                return a.Result * 2; // read antecedent result
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            Console.WriteLine($"Success result: {await successCont}"); // await continuation result

            // Faulted example: task throws; continuation runs only when antecedent faulted
            var faulted = Task.Run(() =>
            {
                Console.WriteLine("Faulted task running...");
                Thread.Sleep(200); // simulate work
                throw new InvalidOperationException("Simulated failure"); // causes Faulted
            });

            var onFault = faulted.ContinueWith(a =>
            {
                Console.WriteLine($"Faulted antecedent status: {a.Status}"); // Faulted
                Console.WriteLine($"Exception: {a.Exception?.GetBaseException().Message}"); // inspect error
            }, TaskContinuationOptions.OnlyOnFaulted);

            await onFault; // wait for fault-handling continuation

            // Canceled example: use CancellationToken and cancel before work completes
            using var cts = new CancellationTokenSource();
            var cancelTask = Task.Run(() =>
            {
                Console.WriteLine("Cancelable task running...");
                for (int i = 0; i < 5; i++)
                {
                    cts.Token.ThrowIfCancellationRequested(); // cooperatively observe cancellation
                    Thread.Sleep(100);
                }
                return 99; // would be result if not canceled
            }, cts.Token);

            cts.CancelAfter(150); // request cancellation after ~150ms

            var onCanceled = cancelTask.ContinueWith(a =>
            {
                Console.WriteLine($"Canceled antecedent status: {a.Status}"); // Canceled
            }, TaskContinuationOptions.OnlyOnCanceled);

            try
            {
                await onCanceled; // continuation that runs when canceled
                await cancelTask; // throws TaskCanceledException
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Task was canceled.");
            }

            // TaskCompletionSource-driven states: manually complete tasks based on business logic
            var tcsSuccess = new TaskCompletionSource<int>();
            tcsSuccess.SetResult(7); // set RanToCompletion explicitly
            var tcsSuccessCont = tcsSuccess.Task.ContinueWith(a =>
            {
                Console.WriteLine($"TCS success status: {a.Status}");
                return a.Result + 1; // read set result
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            Console.WriteLine($"TCS success result: {await tcsSuccessCont}");

            var tcsFault = new TaskCompletionSource<int>();
            tcsFault.SetException(new ApplicationException("Business rule failed")); // set Faulted
            var tcsFaultCont = tcsFault.Task.ContinueWith(a =>
            {
                Console.WriteLine($"TCS fault status: {a.Status}");
                Console.WriteLine($"TCS exception: {a.Exception?.GetBaseException().Message}");
            }, TaskContinuationOptions.OnlyOnFaulted);
            await tcsFaultCont;

            var tcsCancel = new TaskCompletionSource<int>();
            tcsCancel.SetCanceled(); // set Canceled
            var tcsCancelCont = tcsCancel.Task.ContinueWith(a =>
            {
                Console.WriteLine($"TCS cancel status: {a.Status}");
            }, TaskContinuationOptions.OnlyOnCanceled);
            await tcsCancelCont;

            Console.WriteLine("End of program");
        }
    }
}

<<h2>> Task Chaining (attaching child task to a parent task)

