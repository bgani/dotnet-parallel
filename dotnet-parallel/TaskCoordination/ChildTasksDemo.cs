using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.TaskCoordination
{
    // the idea of child tasks that we can set up special relationship between parent and child
    // waiting on parent automatically waits on child and forces child to be completed
    public static class ChildTasksDemo
    {
        public static void Test()
        {
            var parent = new Task(() => 
            {
                // without TaskCreationOptions.AttachedToParent the child task is detached = just a subtask within a task
                // no relationship, and it parent doesn't wait for child (nested) tasks
                var child = new Task(() => 
                {
                    Console.WriteLine("child task starting");
                    Thread.Sleep(3000);
                    Console.WriteLine("child task fininshing");
                    // throw new Exception();  // to run failHandler
                }, TaskCreationOptions.AttachedToParent);

                var completionHandler = child.ContinueWith(t => 
                {
                    Console.WriteLine($"Task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | 
                TaskContinuationOptions.OnlyOnRanToCompletion);

                var failHandler = child.ContinueWith(t => 
                {
                    Console.WriteLine($"oops, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent |
                TaskContinuationOptions.OnlyOnFaulted);


                child.Start();
            });

            parent.Start();
            try
            {
                parent.Wait();
            }
            catch(AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }
    }
}
