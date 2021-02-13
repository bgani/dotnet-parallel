using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.ConcurrentCollections
{
    public class ConcurrentQueueDemo
    {
        public static void Test()
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            // 2 1 <-front

            int result;
            // tries to remove and return element at the beginnig of queue
            if(q.TryDequeue(out result))
            {
                Console.WriteLine($"Removed element {result}");
            }

            // tries to return 1st element without removing, if there is no element returns false
            if(q.TryPeek(out result))
            {
                Console.WriteLine($"Front element is {result}");
            }
        }
    }
}
