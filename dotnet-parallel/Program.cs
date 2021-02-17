using System;
using System.Threading.Tasks;
using dotnet_parallel.PracticingTasks;
using dotnet_parallel.DataSharingAndSynchronization;
using dotnet_parallel.ConcurrentCollections;
using dotnet_parallel.TaskCoordination;
using dotnet_parallel.ParallelLoops;


namespace dotnet_parallel
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Practicing Tasks 

            //CreatingAndStartingTasks.SimpleTasks();
            //CreatingAndStartingTasks.TasksWithState();
            //CreatingAndStartingTasks.TasksWithReturnValues();

            //CancellingTasks.CancelableTasks();
            //CancellingTasks.CompositeCancellationToken();
            //CancellingTasks.MonitoringCancelation();

            // WaitingTimeToPass.Wait();

            // WatingForTasks.Wait();

            //ExceptionHandling.BasicHandling();
            //ExceptionHandling.IterativeHandling();

            #endregion

            #region Data Sharing and Synchronization
            // CriticalSections.Test();
            // SpinLockinigAndLockRecursion.TestSpinLock();
            // SpinLockinigAndLockRecursion.TestLockRecutsion();
            // TestingMutex.LocalMutex();
            // TestingMutex.GlobalMutex();
            // ReaderWriterLocks.Test();
            #endregion

            #region Concurrent Collections
            // ConcurrentDictionaryDemo.Test();
            // ConcurrentQueueDemo.Test();
            // ConcurrentStackDemo.Test();
            // ConcurrentBagDemo.Test();
            // BlockingCollectionDemo.Test();
            #endregion

            #region
            // ContinuationsDemo.SimpleContinuation();
            // ContinuationsDemo.ContinueWhen();

            // ChildTasksDemo.Test();

            // BarrierDemo.Test();

            // CountdownEventDemo.Test();

            // ResetEventsDemo.Manual();
            // ResetEventsDemo.Auto();

            //SmeaphoreSlimDemo.Test();
            #endregion

            #region Parallel Loops
            // ParallelLoopsDemo.Test();

            BreakingCancellationAndExceptionDemo.Test();

            #endregion



            Console.WriteLine("the program done");
            Console.ReadKey();
        }
    }
}
