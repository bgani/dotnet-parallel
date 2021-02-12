using System;
using System.Threading.Tasks;
using dotnet_parallel.PracticingTasks;
using dotnet_parallel.DataSharingAndSynchronization;


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
            SpinLockinigAndLockRecursion.TestLockRecutsion();


            #endregion


            Console.WriteLine("the program done");
            Console.ReadKey();
        }
    }
}
