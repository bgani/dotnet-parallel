using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.ParallelLoops
{
    public class BreakingCancellationAndExceptionDemo
    {
        private static ParallelLoopResult result;
        public static void Test()
        {
            try
            {
                var cts = new CancellationTokenSource();
                ParallelOptions po = new ParallelOptions();
                po.CancellationToken = cts.Token;

                // state is object that allows cancel execution of the loop
                result = Parallel.For(0, 20, po, (int x, ParallelLoopState state) =>
                {
                    Console.WriteLine($"{x} task id [{Task.CurrentId}]\t");

                    if (x == 10)
                    {
                        // recommended
                        // state.Stop(); // tries to stop ASAP, doesn't stop immediately, only new tasks won't be executed
                        // state.Break(); // less immidiate stop, stops the execution of iterations beyond the current iteration

                        // propogates up to Test(), propogates up to Main, 
                        // crashes the program if we won't catch
                        // throw new Exception();


                        // cancellation throws OperationCanceledException and it won't be wrapped in AggregateException, so we have to catch it separately
                        cts.Cancel();
                    }
                });

                Console.WriteLine();
                Console.WriteLine($"was the loop completed? {result.IsCompleted}");

                // check if we did a break
                if(result.LowestBreakIteration.HasValue)
                {
                    Console.WriteLine($"Lowest break iteration is {result.LowestBreakIteration}");
                }
            }
            catch(AggregateException ae)
            {
                ae.Handle(e => 
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }
            catch(OperationCanceledException oce)
            {
                 
            }
            
        }
    }
}
