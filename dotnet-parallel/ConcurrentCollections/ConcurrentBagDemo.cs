using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.ConcurrentCollections
{
    public class ConcurrentBagDemo
    {
        // ConcurrentStack Last in First out
        // concurrentQueue First in First out
        // ConcurrentBag No ordering, optimized for speed

        // ConcurrentBag keeps a list of elements per thread, each of the rhreads has its own bag of elements
        // typically requires no synchronization, unless a thread tries to remove an item
        // while the thread-local bag is empty (item stealing)
        public static void Test()
        {
            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for(int i = 0; i < 10; i++)
            {
                var i1 = i;
                tasks.Add(Task.Factory.StartNew(() => 
                {
                    bag.Add(i1);
                    Console.WriteLine($" Task {Task.CurrentId}  has added value {i1}");
                    int result;
                    if(bag.TryPeek(out result))
                    {
                        Console.WriteLine($" Task {Task.CurrentId} has peeked the value {result}"); 
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            int last;
            if(bag.TryTake(out last))
            {
                Console.WriteLine($"I got {last}");
            }
        }
    }
}
