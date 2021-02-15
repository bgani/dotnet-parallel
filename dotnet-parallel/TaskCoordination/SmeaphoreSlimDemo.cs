using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.TaskCoordination
{
    public static class SmeaphoreSlimDemo
    {
        public static void Test()
        {
            var semaphore = new SemaphoreSlim(2, 10);

            for (int i = 0; i < 20; i++)
            {
                Task.Factory.StartNew(() => 
                {
                    Console.WriteLine($"Entereing task {Task.CurrentId}");
                    // block
                    semaphore.Wait(); // ReleaseCount--
                    Console.WriteLine($"Processing task {Task.CurrentId}");
                });
            }

            while(semaphore.CurrentCount <= 2)
            {
                Console.WriteLine($"Semaphore count: {semaphore.CurrentCount}");
                Console.ReadKey();
                // unblock, allowing 2 threads to execute
                semaphore.Release(2); // ReleaseCount +=2
            }
        }
    }
}
