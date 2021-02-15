using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.TaskCoordination
{
    public static class ResetEventsDemo
    {
        public static void Manual()
        {
            var evt = new ManualResetEventSlim();

            Task.Factory.StartNew(() => 
            {
                Console.WriteLine("Boiling water");
                // signal that we can continue
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() => 
            {
                Console.WriteLine("Waiting for water...");
                evt.Wait();
                Console.WriteLine("Here is your tea"); 
            });
            makeTea.Wait();
        }

        public static void Auto()
        {
            // AutoResetEvent as soon as it's done waiting it sets back to false
            var evt = new AutoResetEvent(false);

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                // signal that we can continue
                evt.Set(); // true
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water...");
                evt.WaitOne(); // false false
                Console.WriteLine("Here is your coffee");

                // WaitOne in AutoResetEvent gives us a hint as to the fact we are ony waiting for the signal to turn green once
                // after it checked it turns back to false, and we have to check it again
                var ok = evt.WaitOne(1000); // false, if we won't pass timeout we will stuck here
                if(ok)
                {
                    Console.WriteLine("Enjoy your coffee");
                }
                else
                {
                    // timed out
                    Console.WriteLine("No tea for coffee");
                }
            });
            makeTea.Wait();
        }
    }
}
