using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.TaskCoordination
{
    ///  The idea is that we are going to inform somebody who's listening to the countdonw event
    ///  Identical to Barrier, the difference is that instead of barrier.SignalAndWait(),  we have Signal() and Wait() methods
    ///  We also can change the counter in Signal(taskCounter)
    public static class CountdownEventDemo
    {
        private static int taskCount = 5;
        static CountdownEvent cte = new CountdownEvent(taskCount);
        private static Random random = new Random();

        public static void Test()
        {
            for(int i = 0; i < taskCount; i++)
            {
                Task.Factory.StartNew(() => 
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    Thread.Sleep(random.Next(3000));
                    cte.Signal();
                    Console.WriteLine($"Exiting task {Task.CurrentId}");
                });
            }

            
            var finalTask = Task.Factory.StartNew(()  => 
            {
                Console.WriteLine($"Waiting for other tasks to complete in {Task.CurrentId}");
                
                // wait until countdown reaches 0, and all tasks completed
                // blocking invocation
                cte.Wait();
                Console.WriteLine($"All tasks completed");
            });
            finalTask.Wait();
        }
    }
}
