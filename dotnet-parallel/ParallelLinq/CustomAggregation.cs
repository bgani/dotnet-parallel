using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace dotnet_parallel.ParallelLinq
{
    public class CustomAggregation
    {
        public static void Test()
        {
            // sequentially, no parellel
            var sum1 = Enumerable.Range(1, 1000).Sum();
            Console.WriteLine($"sum1 = {sum1}");
            var sum2 = Enumerable.Range(1, 1000).Aggregate(0, (i, accumilator) => i + accumilator);
            Console.WriteLine($"sum2 = {sum2}");

            // parellel
            // Aggregate
            // 0 -  the starting value, (for multiplication use 1)
            // 2nd function is the function that happens per Task
            // 3rd function is the function that happens when we combine the results of all the tasks into a single value
            // 4th is for post processing the final result or returning it 
            var sumParallel = ParallelEnumerable.Range(1, 1000)
                .Aggregate(
                    0,
                    (partialSum, i) => partialSum += i,
                    (total, subtotal) => total += subtotal,
                    i => i
                );

            Console.WriteLine($"sumParallel = {sumParallel}");

        }
    }
}
