using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.TaskCoordination
{
    // Continuation is a mechanis for specifying how a particular task follows on from another task(s) that's been completed before
    public static class ContinuationsDemo
    {
        public static void SimpleContinuation()
        {
            var task = Task.Factory.StartNew(() => 
            {
                Console.WriteLine("Boiling water");
            });

            // t refers to original task
            var task2 = task.ContinueWith(t => 
            {
                Console.WriteLine($"Complete task {t.Id}, pour water into cup.");
            });

            try
            {
                task2.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine("Exception: " + e);
                    return true;
                });
            }
        }

        public static void ContinueWhen()
        {
            var task = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");

            // one to many
            var task3 = Task.Factory.ContinueWhenAll(new[] { task, task2}, 
                tasks => {
                    Console.WriteLine("Tasks completed:");
                    foreach(var t in tasks)
                        Console.WriteLine(" - " + t.Result);

                    Console.WriteLine("All tasks done");
                });

            task3.Wait();
        }
    }
}
