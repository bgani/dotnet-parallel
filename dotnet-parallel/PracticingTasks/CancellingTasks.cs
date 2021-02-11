using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.PracticingTasks
{
    public class CancellingTasks
    {
        // cancelling a task using 'break'
        // if we don't throw exception task will be considered as completed successfully
        public static void CancelableTasks()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            Task t = new Task(() => {
                int i = 0;
                while(true)
                {
                    if (token.IsCancellationRequested)
                        break;

                    Console.WriteLine($"{i++}\t");
                }
            });
            t.Start();

            Console.ReadKey();
            cts.Cancel();
            Console.WriteLine("Task has been cancelled");
        }

        // creating composite cancellation toke source
        public static void CompositeCancellationToken()
        {
            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            // toke source that is linked on their tokens
            var paranoid = CancellationTokenSource
                .CreateLinkedTokenSource(planned.Token, preventative.Token, emergency.Token);

            Task.Factory.StartNew(() => {

                int i = 0;
                while(true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.Write($"{i++}\t");
                    Thread.Sleep(100);
                }
            }, paranoid.Token);

            paranoid.Token.Register(() => Console.WriteLine("Cancellation requested"));
            Console.ReadKey();

            // any of the aftermentioned token sources (planned, preventative, emergency) will cancel the task
            planned.Cancel();
        }

        public static void MonitoringCancelation()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            
            //  register a delegate to fire to notify that task cancelled, the function executes as soon as cts.Cancel() executed
            token.Register(() => {
                Console.WriteLine("Cancellation has been requested");
            });

            Task t = new Task(() => {
                int i = 0;
                while(true)
                {
                    
                    if (token.IsCancellationRequested)
                    {
                        // soft exit, RanToCompletion
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{i++}\t");
                        Thread.Sleep(100);
                    }
                }
            });
            t.Start();

            // cancellling multiple tasks
            Task t2 = Task.Factory.StartNew(() => {
                char c = 'a';
                while(true)
                {
                    // Hard exit, Canceled
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{c++}\t");
                    Thread.Sleep(200);

                    // alternative to what is above
                    /*
                    if(token.IsCancellationRequested)
                    {
                        // release resources, if any
                        throw new OperationCanceledException("No longer interested in printing letters");
                    }
                    else
                    {
                        Console.WriteLine($"{c++}\t");
                        Thread.Sleep(200);
                    }*/
                }
            }, token);


            // cancellation on a wait handle 
            Task.Factory.StartNew(() => {
                // the code is blocked until token.Cancel()
                token.WaitHandle.WaitOne(); 
                Console.WriteLine("Wait handle realeased, thus cancellation was requested");
            });

            Console.ReadKey();
            cts.Cancel();
            Thread.Sleep(5000);

            Console.WriteLine($"Task has been cancelled. The staus of the canceled task 't' is {t.Status}.");
            Console.WriteLine($"Task has been cancelled. The staus of the canceled task 't2' is {t2.Status}.");
            Console.WriteLine($"t.IsCanceled = {t.IsCanceled}, t2.IsCanceled = {t2.IsCanceled}");
        }


        /* 
        Summary
        -. There are 2 ways of cancelling execution, 
         1. soft - we end the exuction using break 
         3. hard and canonical way - throw exception, e.g. token.ThrowIfCancellationRequested();
        - To monitor cancellation subscribe to an event using token.Register()
        - We can create composite cancellation token 
        */
    }
}
