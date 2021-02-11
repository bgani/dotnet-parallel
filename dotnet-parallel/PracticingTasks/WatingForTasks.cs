using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.PracticingTasks
{
    public class WatingForTasks
    {
        public static void Wait()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() => 
            {
                Console.WriteLine("I take 5 seconds");

                for(int i = 0; i < 5; i++)
                {
                  token.ThrowIfCancellationRequested();
                  Thread.Sleep(1000);
                }

                Console.WriteLine("The 5 sec task is done");
            }, token);
            t.Start();

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);
            
            // waiting with timeout and token, 
            // canceling with token fires an exception
            Task.WaitAny(new[] { t, t2 }, 4000, token);

            Console.WriteLine($"Task t status is {t.Status}");
            Console.WriteLine($"Task t2 status is {t2.Status}");

            // Task.WaitAll(t); // waits for all provided tasks
            // t.Wait(); 
            // t.Wait(token); // the wait terminates if token is canceled before the task completes
        }
    }
}
