using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.ParallelLoops
{
    public static class ThreadLocalStorageDemo
    {
        public static void Test()
        {
            int sum = 0;
            // for each of the separate thread we can have a some sort of storage which local only to that thread and not accessable to others 
            // instead of doing Interlocked on the sum as 100 times (which is inefficinet), we can take each of the thread and calc the partial sum             
            // then we get the partial sum, and do the Interlock
            // 0 is a initial local storage value
            // (x, state) x - counter, state - state of the parellel execution, tls - current value of the thraed's local storage    
            Parallel.For(1, 1001,
                () => 0,
                (x, state, tls) =>
                {
                    // we don't need interlock, tls is thread local
                    tls += x;
                    Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
                    return tls;
                },
                partialSum =>
                {
                    Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                    Interlocked.Add(ref sum, partialSum);
                });
            Console.WriteLine($"Sum of 1..100 = {sum}");
        }
    }
}
