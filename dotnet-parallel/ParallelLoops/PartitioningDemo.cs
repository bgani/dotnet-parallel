using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace dotnet_parallel.ParallelLoops
{
    // We can optimize the performance of parallel algorithms by using Partitioner
    public class PartitioningDemo
    {

        public static void Test()
        {
            var summary = BenchmarkRunner.Run<PartitioningDemo>();
            Console.WriteLine(summary);
        }

        [Benchmark]
        public void SquareEachValue()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            
            Parallel.ForEach(values, x => 
            {
                // any time we call it, a new delegate will be created which might be inefficient
                results[x] = (int) Math.Pow(x, 2); 
            });
        } 

        [Benchmark]
        public void SquareEachValueChunked()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];

            // Partitioning is the class which specifies how to take a range of values and split them
            var part = Partitioner.Create(0, count, 10000);
            Parallel.ForEach(part, range => 
            {
             for(int  i = range.Item1; i < range.Item2; i++)
             {
                results[i] = (int)Math.Pow(i, 2);
             }
            });
        }
    }
}
