using System;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_parallel.ParallelLinq
{
    public class AsParallelDemo
    {
        public static void Test()
        {
            // generationg array of number from 1 to 50
            const int count = 50;
            var items = Enumerable.Range(1, count).ToArray();
            var results = new int[count];

            // now we can get cubed values of each statement in the array
            // AsParallel returns ParallelQuery<T>
            items.AsParallel().ForAll(x =>
            {
                
                int newValue = x * x * x;
                // the values will be displayed unordered
                Console.Write($"{newValue} ({Task.CurrentId})\t");
                results[x - 1] = newValue;
            });
            Console.WriteLine();
            // getting results 
            foreach (var i in results)
                Console.WriteLine($"{i}\t");
            Console.WriteLine();


            // We can get results .AsOrdered() or .AsUnordered()
            // in this stage nothing, happens 
            // variable cubes becomes a plan of execution
            // Select projects in parellel each element of sequens into a new form
            var cubes = items.AsParallel().AsOrdered().Select(x => x*x*x);
            // the calculation happens when we enumerate e.g in foreach, or when want to materialize it e.g ToArray()
            foreach (var i in cubes)
                Console.Write($"{i}\t");
            Console.WriteLine();

        }
    }
}
