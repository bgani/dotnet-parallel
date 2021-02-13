using System;
using System.Threading.Tasks;
using dotnet_parallel.PracticingTasks;
using dotnet_parallel.DataSharingAndSynchronization;
using dotnet_parallel.ConcurrentCollections;


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
            ConcurrentBagDemo.Test();

            #endregion 




            Console.WriteLine("the program done");
            Console.ReadKey();
        }
    }
}
