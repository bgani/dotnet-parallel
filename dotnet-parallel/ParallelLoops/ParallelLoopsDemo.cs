using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.ParallelLoops
{
    // Invoke, For, ForEeach examples
    public class ParallelLoopsDemo
    {
        public static void Test()
        {
            // Invoke
            var a = new Action(() => Console.WriteLine($"1st action, task id {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"2nd action, task id {Task.CurrentId}"));
            var c = new Action(() => Console.WriteLine($"3rd action, task id {Task.CurrentId}"));
            // takes actions we want to run, and runs them concurrently
            Parallel.Invoke(a, b, c);

            // For
            // executes all actions at the same time
            // step for each i is always 1, we can't have a custom step size 
            Console.WriteLine("parallel for");
            Parallel.For(1, 11, i => 
            {
                Console.WriteLine($"{i * i}");
            });

            // parallel options can specify MaxDegreeOfParallelism, CancellationToken & TaskScheduler
            // var po = new ParallelOptions();
            

            // ForEach
            string[] words = { "oh", "what", "a", "day" };
            Parallel.ForEach(words, word => 
            {
               Console.WriteLine($"{word} has lenght {word.Length} (task {Task.CurrentId})");
            });

            // if we want custom step size
            Console.WriteLine("custom steps");
            Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);

        }

       
        public static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = 0; i < end; i += step)
            {
                yield return i;
            }
        }
    }
}
