using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace dotnet_parallel.ConcurrentCollections
{
    public class ConcurrentDictionaryDemo
    {
        private static ConcurrentDictionary<string, string> capitals =
            new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            // there is no add, since we don't know if it will succeed
            bool success = capitals.TryAdd("France", "Paris");
            // if you are not executing the method as part of a task than the value of CurrentId will be null
            string who = Task.CurrentId.HasValue ? ("Task" + Task.CurrentId) : "Main thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element");
        }

        public static void Test()
        {
            // Accessing a dictionary concurrently
            // Task 1 adds the element
            Task.Factory.StartNew(AddParis).Wait();
            // but main thread can not add element because the element was already added
            AddParis();


            // capitals["Japan"] = "Tokyo";
            // adds an element, then modifies it
            // capitals["Japan"] = "Kyoto";

            // if el is already there, then update, otherwise add el
            capitals.AddOrUpdate("Japan", "Tokyo", (k, old) => old + " --> Tokyo");
            Console.WriteLine($"The capital of Japan is {capitals["Japan"]}");


            // capitals["Sweden"] = "Uppsala";
            // gets value if present, if not present inserts new value
            var capOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"The capital of Sweden is {capOfSweden}");

            // tries to remove, and returns what's actually removed
            const string toRemove = "Japan";
            string removed;
            var didRemove = capitals.TryRemove(toRemove, out removed);
            if(didRemove)
            {
                Console.WriteLine($"We just removed {removed}");
            }
            else
            {
                Console.WriteLine($"Failed to remove the capital of {toRemove}");
            }

            foreach(var capital in capitals)
            {
                Console.WriteLine($"{capital.Value} is the capital of {capital.Key}");
            }

            // capitals.Count is an expensive operation, try to keep doing this as little as possible
        }
    }
}
