using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.ParallelLinq
{
    public class MergeOptions
    {
        public static void Test()
        {
            var numbers = Enumerable.Range(1, 20).ToArray();


            // The Merge option lets us control how quickly we get results once they are calculated
            // items which are produced, they are also buffered. parallel linq puts them in goroup of N and returns  
            // and we can control the way these items are merged together
            // FullyBuffered = all results produced before they are consumed
            // NotBuffered = each result can be consumed right after it's produced
            // Default = AutoBuffered = buffer the number of results selected by the runtime

            // producing
            var results = numbers
                .AsParallel()
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .Select(x => 
            {
                var result = Math.Log10(x);
                Console.Write($"P {result}\t");
                return result;
            });

            // consuming
            foreach(var result in results)
            {
                Console.Write($"C {result}\t");
            }
        }
    }
}
