using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_parallel.ConcurrentCollections
{
    public class ConcurrentStackDemo
    {
        public static void Test()
        {
            // last element considered as a top one
            var stack = new ConcurrentStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            int result;
            if(stack.TryPeek(out result))
                Console.WriteLine($" {result} is on top"); 

            if (stack.TryPop(out result))
                Console.WriteLine($"Popped {result}");

            var items = new int[5];
            // attempts to pop, then returns popped elements from the top
            // items an array where we put popped elements from stack
            // 0 - startIndex, 5 - count
            if(stack.TryPopRange(items, 0, 5) > 0)
            {
                var text = string.Join(", ", items.Select(i => i.ToString()));
                Console.WriteLine($"Popped these items: {text}");
            }

        }
    }
}
