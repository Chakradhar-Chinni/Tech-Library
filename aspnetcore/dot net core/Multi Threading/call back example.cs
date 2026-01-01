// Define a callback delegate
delegate void Notify(string message);

class Program
{
    static void Main()
    {
        // Pass the callback method
        DoWork(OnWorkCompleted);
		
    }

    static void DoWork(Notify callback)
    {
        Console.WriteLine("Doing work...");
        // After work is done, call the callback
        callback("Work completed!");
    }

    static void OnWorkCompleted(string msg)
    {
        Console.WriteLine(msg);
    }
}

Notes:
(1)
Why not call both methods like this?
DoWork();
OnWorkCompleted();
	- Works only for synchronous execution.
	- Modern apps use async/multi-threading for responsiveness.
	- If DoWork() runs on another thread, the main thread calls OnWorkCompleted() too early, before work finishes.
	


(2)
Inside DoWork() why use callback() instead of directly calling OnWorkCompleted()?
	- Direct call → tight coupling, fixed behavior.
	- Callback → flexible and reusable; caller decides what happens after completion. (relates to I in solid principles)
	- Promotes loose coupling and inversion of control.
