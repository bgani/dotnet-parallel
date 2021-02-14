using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace dotnet_parallel.TaskCoordination
{
    // Barrier enables multiple tasks to cooperatively work on algorithm in parallel through multiple phases
    public static class BarrierDemo
    {
        
        static Barrier barrier = new Barrier(2, b => 
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
            //b.ParticipantCount
            //b.ParticipantsRemaining

            // add/remove participants
        });

        public static void Water()
        {
            Console.WriteLine("Puttin the kettle on (takes a bit longer)");
            Thread.Sleep(2000);
            // we have some sort of a counter inside barrier which expects count to reach value of 2
            barrier.SignalAndWait(); // count = 2
            Console.WriteLine("Pouring water into cup"); // count reset to 0 
            barrier.SignalAndWait(); // count = 1
            Console.WriteLine("Putting the kettle away");
        }

        public static void Cup()
        {
            Console.WriteLine("Finding the nicest cup of tea (fast)");
            // signaling and waiting fused
            barrier.SignalAndWait(); // count = 1
            Console.WriteLine("Adding tea");
            barrier.SignalAndWait(); // count = 2
            Console.WriteLine("Adding some sugar");
        }

        public static void Test()
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);
            var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea");
            });

            tea.Wait();
        }

    }
}
