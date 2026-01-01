Inside OS, everything is a process
Task Manager > Processes > Google chrome, word, music palyer etc…

By default, every process has at least one thread that is responsible for executing the application code, and that thread is called Main Thread. So, every application by default is a single-threaded application.

All the threading-related classes in C# belong to System.Threading namespace. 


Thread class allows to create custom threards. Thread class can create foreground and background threads

<<h2>> Context Switching and Thread Naming
multi-threading is complex, needs OS understanding
UI always runs on main thread
heavy load is given to worker threads, so main thread stays responsive

main thread, worker thread can be assigned custom names using name property
t1.Name = "m1 thread"

Default name of main thread: Main Thread
Default name of worker thread: <No Name>
how to see - add break point > Debug > Windows > Thread


namespace Threads.App
{
    class Program
    {
        static void Main(string[]  args)
        { 
            //worker thread
            Thread t1 = new Thread(DisplayNumbers);
            t1.Name = "m1 thread";
            t1.Start();

            //Main Thread
            Thread.CurrentThread.Name = "Threads.App Main Thread";
            for (int i = 1; i <= 5; i++)
            {
                Console.Write(" M:" + i);
            }

        }
        static void DisplayNumbers()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.Write(" T1:" + i);
            }
        }
    }
}






<<h2>> Shared Resources
Global variables → stored on heap (must persist beyond a single method call and be accessible by all threads).
Local variables → stored on stack (thread-specific).
Join() → blocks only the thread that calls it, not all threads.

Todo: Understand race condition with deep example

IncrementWithOutLock()
 - not thread safe
 - leads to race condition as 2 threads access the shared resources


IncrementWithLock()
 - thread safe as shared resources are locked, locking ensures that shared resources are available to only 1 thread at a time
 
LocalWork()
 - thread safe, as scope is local. thread maintains its own stack

using System;
using System.Threading;

class Program
{
    static int sharedCounter = 0; // Shared on heap
    static object lockObj = new object();

    static void Main()
    {
        Console.WriteLine("=== Race Condition Example ===");
        Thread t1 = new Thread(IncrementWithoutLock);
        Thread t2 = new Thread(IncrementWithoutLock);
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
        Console.WriteLine($"Shared Counter (without lock): {sharedCounter}");

        // Reset counter
        sharedCounter = 0;

        Console.WriteLine("\n=== Thread-Safe Example (with lock) ===");
        Thread t3 = new Thread(IncrementWithLock);
        Thread t4 = new Thread(IncrementWithLock);
        t3.Start();
        t4.Start();
        t3.Join();
        t4.Join();
        Console.WriteLine($"Shared Counter (with lock): {sharedCounter}");

        Console.WriteLine("\n=== Local Variable Example ===");
        Thread t5 = new Thread(LocalWork);
        Thread t6 = new Thread(LocalWork);
        t5.Start();
        t6.Start();
        t5.Join();
        t6.Join();
    }

    static void IncrementWithoutLock()
    {
        for (int i = 0; i < 10; i++)
        {
            sharedCounter++; // Not thread-safe
        }
    }

    static void IncrementWithLock()
    {
        for (int i = 0; i < 10; i++)
        {
            lock (lockObj)
            {
                sharedCounter++; // Thread-safe
            }
        }
    }

    static void LocalWork()
    {
        int localCounter = 0; // Each thread has its own stack
        for (int i = 0; i < 5; i++)
        {
            localCounter++;
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} LocalCounter: {localCounter}");
        }
    }
}

Output:
=== Race Condition Example ===
Shared Counter (without lock): 16   // (Expected < 20 due to race condition)

=== Thread-Safe Example (with lock) ===
Shared Counter (with lock): 20      // (Correct result with locking)

=== Local Variable Example ===
Thread 1 LocalCounter: 1
Thread 1 LocalCounter: 2
Thread 1 LocalCounter: 3
Thread 1 LocalCounter: 4
Thread 1 LocalCounter: 5
Thread 2 LocalCounter: 1
Thread 2 LocalCounter: 2
Thread 2 LocalCounter: 3
Thread 2 LocalCounter: 4
Thread 2 LocalCounter: 5




  
<<h2>> Thread Vs Process

Threads
 - run in parallel with each other
 - shares heap memory between threads, stack memory is scoped to threads
 
 Processes 
  - Fully isolated from each other




  
  
<<h2>> Local Memory

1. Main Thread, Worker thread both call PrintNumbers()
2. as scope is local, no race conditions would occur

using System;
using System.Threading;
namespace Threads.App
{
    class Program
    {
        public static int sharedCounter;
        public static void Main(String[] args)
        {
            //worker thread
            Thread t1 = new Thread(PrintNumbers);
            t1.Start();

			//Main Thread
            PrintNumbers();
        }

        static void PrintNumbers()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i+" ");
            }
        }
    }
}

Output:
0 0 1 2 3 4 5 6 7 8 1 2 3 9 4 5 6 7 8 9
 





  
<<h2>> Thread Pool

--Thread Basics--
Each thread requires:
    - Overhead: A few hundred milliseconds (for stack creation and spawning).
    - Memory: \~1 MB per thread.
Too many active threads can:
    - Throttle the OS with administrative overhead.
    - Render CPU caches ineffective.

--Thread Pool--
Purpose: Reduces performance penalty by reusing threads.
Creates only background threads.
Limit: Maintains an upper count of simultaneous worker threads (e.g., max 5).
Jobs exceeding the limit are queued until a thread becomes free.

--Background Threads--
Similar to foreground threads except:
    - Do not keep the managed execution environment alive.
    - If the main thread ends, background threads terminate abruptly.
-Creation: `Thread.IsBackground = true`. //marks thread as background thread
 - If Main (foreground) thread exits, runtime will terminate all background threads even if they are still executing
 - If you need the thread to finish its work even after the main thread exits, you should use a foreground thread (default) or ensure the main thread waits (e.g., Join() or use    Task.Wait() in TPL).
 - Foreground thread: Keeps the application running until it completes.
 - Background thread: Does not keep the application alive; terminates when all foreground threads finish.
 - The main thread is the primary foreground thread, but it’s not the only one
	- Any thread you create without setting IsBackground = true is also a foreground thread.
	- So, you can have multiple foreground & background threads in an application.

--Thread Pool Behavior--

Check if running on a pool thread:
    -   `Thread.CurrentThread.IsThreadPoolThread`.
-   Ways to enter the thread pool:
    -   --Task Parallel Library (TPL).--
    -   --Asynchronous delegates.--
    -   --BackgroundWorker.--
    -   --ThreadPool.QueueUserWorkItem.--






      


<<h2>> ThreadPool Demo 

(1)
With ThreadPool.QueueUserWorkItem, the only way to supply input to the callback is via that object state parameter. 

(2)
State object is passed so worker thread has the data it needs when the callback runs.

using System.Threading;
namespace Threads.App
{
    class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);

            Employee e1 = new Employee();
            e1.empId = 1001;
            e1.empName = "Jon";

            ThreadPool.QueueUserWorkItem(new WaitCallback(DisplayEmployeeInfo), e1);

            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);
            Console.ReadKey();

        }

        public static void DisplayEmployeeInfo(object employee)
        {
            Console.WriteLine("Display Employee Info : "+ Thread.CurrentThread.IsThreadPoolThread);
            Employee emp = employee as Employee;

            Console.WriteLine(emp.empId + "\t" + emp.empName);
        }

        public class Employee
        {
            public int empId { get; set; }
            public string empName { get; set; }
        }
    }
}






<<h2>> Thread.Join() - blocking main thread 

 - Thread.Join() blocks only the calling thread (e.g., main thread) until the specified thread finishes; other threads continue running normally.
 
 - If t2 finishes while the main thread is waiting on t1.Join(), the main thread remains blocked until t1 completes. To wait for both threads, call Join() on each or use Task.WhenAll
 - if t2.join() is not mentioned // might lead to production issues
using System.Threading;
namespace Threads.App
{
    class Program
    {
        public static void Main(String[] args)
        {
            Thread t1 = new Thread(M1);
            Thread t2 = new Thread(M1);
            
            t2.Start();
            t1.Start();
            t1.Join();

            Console.WriteLine("Main thread started after completing M1");

            Console.ReadKey();
        }

        public static void M1()
        {
            Console.WriteLine("M1 Started");
            Thread.Sleep(2000);
            Console.WriteLine("M1 completed");
        }
    }
}





<<h2>> Internal request handling by Kestrel server

In ASP.NET Core, ThreadPool threads handle requests, not a dedicated main thread per user.
Users send requests to the website.
Kestrel (ASP.NET Core server) handles requests using a Thread Pool.
Each request is processed by a ThreadPool thread, not a dedicated “main thread” per user.
Threads are reused for efficiency; async/await frees them during I/O for scalability.
Threads are per request, not per user session.
Join() blocks only the thread that calls it, not the entire app.

Request Handling
	button click triggers an HTTP request to the server.
	ASP.NET Core (via Kestrel) picks a ThreadPool thread to process this new request.
	After the request completes, thread is claimed back by the threadpool to reuse for upcoming requests.
	user stays idle for 10min and click a button 
	 - during idle time no thread is active. only client side code runs (html,css, JS)
	 - click button will travel through all middlewares defined in program.cs 
	It does not reuse a dedicated thread for User2 because:
		Threads are not tied to user sessions.
		They are allocated per request and returned to the pool after completion.

Why ThreadPool?
	ThreadPool manages concurrency.
	ThreadPool is designed for short-lived tasks like handling web requests.
	It reuses threads to avoid overhead of creating/destroying threads for every request.



    
<<h2>> Exception Handling

checkout: a worker thread throws exception, does it get caught by main thread?
Exception middleware role in multi thread apps
	
