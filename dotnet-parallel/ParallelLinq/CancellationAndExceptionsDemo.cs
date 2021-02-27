using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.ParallelLinq
{
    public class CancellationAndExceptionsDemo
    {
        public static void Test()
        {
            var cts = new CancellationTokenSource();

            var items = ParallelEnumerable.Range(1, 20);

            var results = items.WithCancellation(cts.Token).Select(i =>
            {
                double result = Math.Log10(i);
                // if (result > 1) throw new InvalidOperationException();
                Console.WriteLine($"i = {i}, taskId = {Task.CurrentId}");
                return result;
            });

            try
            {
                foreach(var c in results)
                {
                    // causes parallel loop to cancel
                    // it is cuncurrent operation, and some of elements will be processed even they are greater than 1
                    if (c > 1)
                        cts.Cancel();
                    Console.WriteLine($"result = {c}");
                }    
            }
            catch(AggregateException ae)
            {
                ae.Handle(e => 
                {
                    Console.WriteLine($"{e.GetType().Name} : {e.Message}");
                    return true;
                });
            }
            catch(OperationCanceledException oce)
            {
                Console.WriteLine("Canceled");
            }
        }
    }
}
