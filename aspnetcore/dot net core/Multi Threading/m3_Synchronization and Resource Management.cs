<<h2>> Synchnorization

Thread Synchronization Overview

	-   Definition: Synchronization is the act of coordinating multiple threads or tasks to achieve a predictable outcome.
	-   Why Needed:
		-   Multiple threads accessing shared data can lead to unpredictable results.
		-   Synchronization ensures consistency and correctness.



Ways to Achieve Synchronization

	1.  Blocking Methods
		-   Examples: `Thread.Sleep`, `Thread.Join`, `Task.Wait`
		-   Behavior:
			-   Stops execution until the task/thread completes.
			-   Misconception: Blocked threads consume CPU → Incorrect.
				-   Blocked threads do not consume CPU, only memory.
				-   They release their time slice to the OS until the condition resolves.
		-   I/O Bound Calls: Prefer asynchronous mechanisms with callbacks.  

	2.  Locks
		-   Purpose: Restrict access to a section of code.
		-   Types:
			-   Exclusive Locks:
				-   Only one thread can access the code at a time.
				-   Examples: `lock` keyword, `Monitor.Enter`/`Monitor.Exit`, `Mutex`.
			-   Non-Exclusive Locks:
				-   Allow multiple threads to access a resource with limits.
				-   Examples: `Semaphore`, `SemaphoreSlim`, `ReaderWriterLock`.
				-   Note: Semaphore with max count = 1 ≈ Mutex (with slight differences).

	3.  Signaling Constructs
		-   Purpose: One thread waits for a signal from another.
		-   Examples:
			-   `EventWaitHandle`
			-   `Monitor.Wait` / `Monitor.Pulse`
			-   .NET Framework 4 additions: `CountdownEvent`, `Barrier`.

	4.  Non-Blocking Constructs
		-   Purpose: Synchronize without blocking threads.
		-   Examples:
			-   `Thread.MemoryBarrier`
			-   `volatile` keyword
			-   `Volatile.Read` / `Volatile.Write`
			-   `Interlocked` class



Key Concepts
	-   Blocking vs Spinning:
		-   Blocking: Thread waits without consuming CPU.
		-   Spinning: Thread continuously checks a condition (e.g., `while` loop), consuming CPU.

	-   Locks Recap:
		-   Exclusive: One thread only.
		-   Non-Exclusive: Multiple threads with limits.

	-   Signaling:
		-   Enables coordination between threads for sequential execution.





      
<<h2>> Monitor and Locks

(1) compiler converts Lock(obj){} into Monitor as below
Monitor.Enter() 
try
{
}
finally
{
	Monitor.Exit(lock)
} 

(2) Use Monitor when lock doesn't fit the requirement

Implementation using lock()
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
namespace Threads.App
{
    class Program
    {
       static object lockObj = new object();
       public static void Main(string[] args)
        {
                var t1 = Task.Run(M1);
                var t2 = Task.Run(M1);
                Task.WaitAll(t1,t2);
                
                Console.WriteLine("Program Exit");
        }

        
        public static void M1()
        {
            lock(lockObj)
            {
                Console.WriteLine($"Started with thread id: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine("This is shared code");
                Console.WriteLine($"Completed with thread id: {Thread.CurrentThread.ManagedThreadId}");
            }
        }
    }
}

Implementation using Monitor
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
namespace Threads.App
{
    class Program
    {
       static object lockObj = new object();
       public static void Main(string[] args)
        {
                var t1 = Task.Run(M1);
                var t2 = Task.Run(M1);
                Task.WaitAll(t1,t2);
                
                Console.WriteLine("Program Exit");
        }

        
        public static void M1()
        {
            Monitor.Enter(lockObj);
            try
            {
                {
                    Console.WriteLine($"Started with thread id: {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine("This is shared code");
                    Console.WriteLine($"Completed with thread id: {Thread.CurrentThread.ManagedThreadId}");
                }
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }
    }
}







<<h2>> Understanding Internals of Locking 

static object lockObj = new object(); 

//Developer code
lock(lockObj)
{
    // code
}

// C# compiler transforms into
bool lockTaken = false;
try
{
    Monitor.Enter(lockObj, ref lockTaken);
    // code
}
finally
{
    if (lockTaken)
        Monitor.Exit(lockObj);
}


- declares a static lock object used for thread synchronization. //static object lockObj
- The lockObj variable itself is just a regular object. It doesn't have any special "locking" properties. What makes it function as a lock is the lock statement that uses it.
- Every object in .NET has a hidden "sync block" (a data structure in memory) associated with it. Its memory address/identity uniquely identifies which sync block to use
- Monitor.Enter(lockObj) checks if any other thread currently holds the lock on that object's sync block
- If the lock is available: the current thread acquires it and proceeds
- If the lock is held by another thread: the current thread is blocked (put into a wait state) until the lock is released
- Monitor.Exit(lockObj) releases the lock, allowing one waiting thread to acquire it


bool lockTaken = false
- Monitor.Enter(lockObj, ref lockTaken) sets lockTaken = true only if the lock was successfully acquired
- If an exception occurs (like ThreadAbortException) before the lock is acquired, lockTaken remains false
- The finally block checks if (lockTaken) before calling Monitor.Exit(), preventing an error from trying to release a lock that was never acquired

Reentrant Locking
•	The same thread can acquire the same lock multiple times (reentrant)
•	The runtime tracks how many times the thread entered the lock
•	The lock is only released when all lock blocks exit

static object lockObj = new object();
public static void M1()
{
    lock(lockObj)
    {
        M2();
        lock(lockObj)
        {
            M2();
        }
    }
}
public static void M2()
{
    lock (lockObj)
    {
        Console.WriteLine("M2");
    }
}







<<h2>> Nested Locks

- M1() holds the lockObj and calls M2() 
- as M2() uses same lockObj as M1() thread can acquire it causing reentrant lock (recursion depth icreases)
- Reentrant locking is super cool 

using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
namespace Threads.App
{
    class Program
    {
       static object lockObj = new object();
       public static void Main(string[] args)
        {
            var t1 = Task.Run(M1);
            Task.WaitAll(t1);
                
            Console.WriteLine("Program Exit");
        }
        public static void M1()
        {
            lock(lockObj)
            {
                Console.WriteLine($"Started with thread id: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine("This is shared code");
                Console.WriteLine($"Completed with thread id: {Thread.CurrentThread.ManagedThreadId}");
                M2();
            }
        }
        public static void M2()
        {
            lock (lockObj)
            {
                Console.WriteLine("M2");
            }
        }
    }
}







<<h2>> Dead Lock

 - A deadlock is a situation in computing where two or more processes are unable to proceed because each is waiting for a resource held by another process

using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
namespace Threads.App
{
    class Program
    {
       static object lockObj = new object();
       static object lockObj2 = new object();
       
       public static void Main(string[] args)
        {
            Console.WriteLine("Starting deadlock demonstration...\n");
            
            var t1 = Task.Run(M1);
            var t2 = Task.Run(M2);
            
            // Wait for 5 seconds to see if tasks complete
            bool completed = Task.WaitAll(new[] { t1, t2 }, TimeSpan.FromSeconds(5));
            
            if (!completed)
            {
                Console.WriteLine("\n DEADLOCK DETECTED! Tasks did not complete within 5 seconds.");
                Console.WriteLine("Thread 1 is waiting for lockObj2 (held by Thread 2)");
                Console.WriteLine("Thread 2 is waiting for lockObj (held by Thread 1)");
            }
            else
            {
                Console.WriteLine("\nProgram Exit - No deadlock occurred");
            }
        }
        
        public static void M1()
        {
            lock(lockObj)
            {
                Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}] M1: Acquired lockObj");
                
                // Simulate some work
                Thread.Sleep(100);
                
                Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}] M1: Trying to acquire lockObj2...");
                lock(lockObj2)
                {
                    Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}] M1: Acquired lockObj2");
                    Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}] M1: Critical section completed");
                }
            }
        }
        
        public static void M2()
        {
            lock (lockObj2)
            {
                Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}] M2: Acquired lockObj2");
                
                // Simulate some work
                Thread.Sleep(100);
                
                Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}] M2: Trying to acquire lockObj...");
                lock (lockObj)
                {
                    Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}] M2: Acquired lockObj");
                    Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}] M2: Critical section completed");
                }
            }
        }
    }
}







<<h2>> Reader or Writer Lock 

- Readers: Multiple readers can read the shared data simultaneously
- Writers: Only one writer can access the data at a time, and no readers are allowed while writing, to prevent data corruption
- Idea is to Multiple readers can access data together if no writer is writing. Writers have exclusive access no other reader or writer can enter during writing
- scenario A :	2 tasks are reading 3rd task acquires rwLock.EnterWriteLock() what happens
	- Writer Blocks: The writer task will block at EnterWriteLock() and wait until all current readers release their read locks.
	- Current Readers Continue: The 2 tasks that already acquired read locks can continue reading and will complete normally. They are not interrupted.
- scenario B: t3 acquires write lock, now t4 waits for readlock and t5 waits for writelock. who will get lock after t3 exits?
	- t4 gets the lock as FIFO approach is followed by ReaderWriterLockSlim

using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
namespace Threads.App
{
    class Program
    {
        static ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
        static Dictionary<int, string> persons = new Dictionary<int, string>();
        static Random random = new Random();
        public static void Main(string[] args)
        {
            var t0 = Task.Run(Write);
            Task.WaitAll(t0);

            var t1 = Task.Run(Read);
			var t2 = Task.Run(Read);
			var t3 = Task.Run(Write);
			var t4 = Task.Run(Read);
			var t5 = Task.Run(Write);
			 

			Task.WaitAll(t1,t2,t3,t4,t5);
        }

        static void Read()
        {
            int curr_thread = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($" Thread:: {curr_thread} is reading ");
            
            readerWriterLockSlim.EnterReadLock();
            foreach (var item in persons)
            {

                Console.WriteLine($" ({curr_thread}) :: {item.Key} {item.Value}");
                
                Thread.Sleep(2000);
            }
            readerWriterLockSlim.ExitReadLock();
            
            Console.WriteLine($" Thread:: {curr_thread} reading completed");
        }

        static void Write()
        {
            int curr_thread = Thread.CurrentThread.ManagedThreadId;
            int r = random.Next(2000,5000);

            readerWriterLockSlim.EnterWriteLock();
            Console.WriteLine($" Thread:: {curr_thread} is writing ");
            persons.Add(r, "Tony");
            persons.Add(r + 1, "Hulk");
            Console.WriteLine($" Thread:: {curr_thread} writing completed");
            readerWriterLockSlim.ExitWriteLock();

        }
    }
}

Output:
Thread:: 10 is writing
 Thread:: 10 writing completed
 Thread:: 7 is reading
 (7) :: 2452 Tony
 Thread:: 10 is reading
 (10) :: 2452 Tony
 Thread:: 9 is reading
 (9) :: 2452 Tony
 (7) :: 2453 Hulk
 (10) :: 2453 Hulk
 (9) :: 2453 Hulk
 Thread:: 7 reading completed
 Thread:: 10 reading completed
 Thread:: 9 reading completed
 Thread:: 5 is writing
 Thread:: 5 writing completed
 Thread:: 11 is writing
 Thread:: 11 writing completed


 
 <<h2>> Notes
 - Race condition occurs when two threads try to execute shared code, to avoid this use locks 







<<h2>> Mutex
- Heavier, can work across processes, more complex, slower
- works at OS Level not just at app level. 


When Mutex is actually useful:
	- Preventing multiple instances of an application from running
	- Synchronizing between different processes/applications
	- Cross-process named synchronization
	
Limitations
	- Limited to single machine, doesn't work across network boundaries, distributed systems
	- Redis is used for beyond network

Summary:
	- Single machine, multiple processes → Named Mutex works
	- Multiple machines (distributed) → Need distributed locking mechanism like Redis
	- Single process, multiple threads → Use lock() instead


using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
namespace Threads.App
{
    class Program
    {
        // Shared resource - simulating a bank account balance
        private static int accountBalance = 1000;
        
        // Mutex for synchronizing access to the shared resource
        private static Mutex mutex = new Mutex();

        public static void Main(string[] args)
        {
            Console.WriteLine("=== Mutex Example: Bank Account Withdrawals ===\n");
            Console.WriteLine($"Initial Balance: ${accountBalance}\n");

            // Create multiple threads that will try to withdraw money simultaneously
            Thread thread1 = new Thread(() => WithdrawMoney("Alice", 200));
            Thread thread2 = new Thread(() => WithdrawMoney("Bob", 300));
            Thread thread3 = new Thread(() => WithdrawMoney("Charlie", 400));
            Thread thread4 = new Thread(() => WithdrawMoney("Diana", 250));

            // Start all threads
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();

            // Wait for all threads to complete
            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();

            Console.WriteLine($"\nFinal Balance: ${accountBalance}");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void WithdrawMoney(string person, int amount)
        {
            Console.WriteLine($"{person} is attempting to withdraw ${amount}...");

            // Acquire the mutex - only one thread can enter this section at a time
            mutex.WaitOne();

            try
            {
                // Critical section - accessing shared resource
                Console.WriteLine($"{person} is checking the balance...");
                Thread.Sleep(100); // Simulate processing time

                if (accountBalance >= amount)
                {
                    Console.WriteLine($"{person} has sufficient funds. Current balance: ${accountBalance}");
                    Thread.Sleep(100); // Simulate withdrawal processing
                    
                    accountBalance -= amount;
                    Console.WriteLine($"✓ {person} withdrew ${amount}. New balance: ${accountBalance}");
                }
                else
                {
                    Console.WriteLine($"✗ {person} has insufficient funds. Cannot withdraw ${amount}. Balance: ${accountBalance}");
                }
            }
            finally
            {
                // Always release the mutex, even if an exception occurs
                mutex.ReleaseMutex();
                Console.WriteLine($"{person} has released the lock.\n");
            }
        }
    }
}






<<h2>> Semaphore

	- Semaphore sets a limit on concurrent access to a shared resource. It's about concurrent access, not threads
	- A semaphore controls how many threads can execute a specific code section simultaneously
	- FIFO approach is followed internally

using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
namespace Threads.App
{
    class Program
    {
        // Shared resource - simulating a parking lot with limited spaces
        private static int availableSpaces = 3;
        
        // Semaphore to limit concurrent access (max 3 threads can enter)
        private static SemaphoreSlim semaphore = new SemaphoreSlim(3, 3);

        public static async Task Main(string[] args)
        {
            Console.WriteLine("=== Semaphore Example: Parking Lot (3 spaces) ===\n");
            Console.WriteLine($"Total Parking Spaces: {availableSpaces}\n");

            // Create multiple tasks representing cars trying to park
            Task task1 = Task.Run(() => ParkCarAsync("Car 1", 2000));
            Task task2 = Task.Run(() => ParkCarAsync("Car 2", 3000));
            Task task3 = Task.Run(() => ParkCarAsync("Car 3", 1500));
            Task task4 = Task.Run(() => ParkCarAsync("Car 4", 2500));
            Task task5 = Task.Run(() => ParkCarAsync("Car 5", 1000));
            Task task6 = Task.Run(() => ParkCarAsync("Car 6", 2000));

            // Wait for all tasks to complete
            await Task.WhenAll(task1, task2, task3, task4, task5, task6);

            Console.WriteLine("\nAll cars have finished parking.");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static async Task ParkCarAsync(string carName, int parkingDuration)
        {
            Console.WriteLine($"{carName} is looking for a parking space...");

            // Try to acquire a semaphore slot (async, non-blocking)
            await semaphore.WaitAsync();

            try
            {
                // Critical section - limited number of threads can be here
                int currentCount = semaphore.CurrentCount;
                Console.WriteLine($"✓ {carName} has parked! (Available spaces: {currentCount})");
                
                // Simulate parking time (async)
                await Task.Delay(parkingDuration);
                
                Console.WriteLine($"{carName} is leaving the parking lot.");
            }
            finally
            {
                // Release the semaphore slot
                semaphore.Release();
                int currentCount = semaphore.CurrentCount;
                Console.WriteLine($"{carName} has left. (Available spaces: {currentCount})\n");
            }
        }
    }
}
