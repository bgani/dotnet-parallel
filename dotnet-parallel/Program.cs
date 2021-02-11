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
            CancellingTasks.CancelableTasks();
            CancellingTasks.CompositeCancellationToken();
            CancellingTasks.MonitoringCancelation();
            #endregion

            Console.WriteLine("the program done");
            Console.ReadKey();
        }
    }
}
