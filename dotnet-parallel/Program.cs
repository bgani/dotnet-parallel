using System;
using System.Threading.Tasks;
using dotnet_parallel.PracticingTasks;


namespace dotnet_parallel
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CreatingAndStartingTasks 

            //CreatingAndStartingTasks.SimpleTasks();

            //CreatingAndStartingTasks.TasksWithState();

            //CreatingAndStartingTasks.TasksWithReturnValues();

            #endregion

            #region CancellingTasks
            //CancellingTasks.CancelableTasks();
            //CancellingTasks.CompositeCancellationToken();
            //CancellingTasks.MonitoringCancelation();
            #endregion

            #region WaitingTimeToPass
            // WaitingTimeToPass.Wait();
            #endregion

            #region WatingForTasks
            WatingForTasks.Wait();
            #endregion

            Console.WriteLine("the program done");
            Console.ReadKey();
        }
    }
}
