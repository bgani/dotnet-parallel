using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.PracticingTasks
{
    public class WaitingTimeToPass
    {
        public static void Wait()
        {
            // We have 3 ways to wait until a certain time to pass

            //1. Thread.Sleep(1000);

            //2. token.WaitHandle.WaitOne(1000);
            var cts = new CancellationTokenSource();
                var token = cts.Token;
                var t = new Task(() => 
                {
                    Console.WriteLine($"Press any key to disarm, you have 5 seconds");
                    bool cancelled = token.WaitHandle.WaitOne(5000);
                    Console.WriteLine(cancelled ? "Bomb disarmed": "boom!!");
                }, token);
                t.Start();

            // 3. SpinWait Pauses thread, but unlike Sleep and WaitOne we don't give up on our turn on the execution
            // we will be wasting resources, but avoiding context switches
            // Thread.SpinWait(10000);
            // SpinWait.SpinUntil();

             Console.ReadKey();
             cts.Cancel();
        }
    }
}
