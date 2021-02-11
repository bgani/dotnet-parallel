using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace dotnet_parallel.PracticingTasks
{
    public class CreatingAndStartingTasks
    {
        public static void Write(char c)
        {
            int i = 1000;
            while(i-- > 0)
            {
                Console.Write(c);
            }
        }

        public static void Write(object o)
        { 
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }

        // Ways of using the Task
        public static void SimpleTasks()
        {
            // the argument is an action, so it can be a delegate, a lambda or an anonymous method
            // using Factory
            Task.Factory.StartNew(() => Write('.'));

            // using explicit Task
            var t = new Task(() => Write('?'));
            t.Start();
            Write('-');
        }


        // Task with object argument
        public static void TasksWithState()
        {
            // object approach (Action<object> action, object state)
            Task t = new Task(Write, "hi");
            t.Start();
            Task.Factory.StartNew(Write, 111);
        }

        // Returning value from tasks
        public static void TasksWithReturnValues()
        {
            string text1 = "testing", text2 = "this";
            var task1 = new Task<int>(TextLength, text1);
            task1.Start();
            Task<int> task2 = Task.Factory.StartNew(TextLength, text2);

            // getting the Result is a blocking operation
            Console.WriteLine($"Length of '{text1}' is {task1.Result}");
            Console.WriteLine($"Length of '{text2}' is {task2.Result}");
        }


        // Summary:
        // Task is a unit of work in .NET
        // 1. Two ways of using tasks
        //    Task.Factory.StartNew() creates and starts a Task
        //    new Task(() => { ... }) creates a task; use Start() to fire it
        // 2. Tasks take an optional 'object' argument
        //    Task.Factory.StartNew(x => { foo(x) }, arg);
        // 3. To return values, use Task<T> instead of Task
        //    To get the return value. use t.Result (this waits until task is complete)
        // 4. Use Task.CurrentId to identify individual tasks.
    }
}
