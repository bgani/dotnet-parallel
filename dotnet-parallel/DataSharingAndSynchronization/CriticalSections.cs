using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.DataSharingAndSynchronization
{
    public class CriticalSections
    {
        public class BankAccount 
        {
            // to control synchronization we can use lock
            // only one thread can lock the padlock
            // hence only one thread can proceed the operation
            public object padlock = new object();
            public int Balance { get; private set; }


            // Deposit and Withdraw operations are not atomic, e.g. 
            // +=
            // operation1: temp <- getBalance() + amount
            // if we won't use lock something might happen between opertion1 and opertion2, which can mess up the result
            // operation2: setBalance(temp)
            // if we won't use lock, the value of the balance will be different on each execution of the program
            public void Deposit(int amount)
            {
                lock (padlock)
                {
                  Balance += amount;
                }
            }

            public void Withdraw(int amount)
            {
                lock (padlock)
                {
                    Balance -= amount;
                }
            }
        }

        public static void Test()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            for(int i = 0; i < 10; ++i)
            {
                tasks.Add(Task.Factory.StartNew(() => 
                { 
                   for(int j = 0; j < 1000; ++j)
                   {
                        ba.Deposit(100);
                   }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; ++j)
                    {
                        ba.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
        }
    }
}
