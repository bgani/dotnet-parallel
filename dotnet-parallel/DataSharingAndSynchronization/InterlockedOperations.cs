using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.DataSharingAndSynchronization
{
    // interlocked class contains atomic operations on variables
    // atomic = cannot be interrupted
    public class InterlockedOperations
    {
        public class BankAccount
        {
            private int balance;

            public int Balance { 
                get => balance; 
                private set => balance = value; 
            }

            // Deposit and Withdraw operations are not atomic, e.g. 
            // +=
            // operation1: temp <- getBalance() + amount
            // operation2: setBalance(temp)
            public void Deposit(int amount)
            {
                Interlocked.Add(ref balance, amount);

                // Interlocked.Exchange() is used for thread safe assignment, it sets the value and gives back the original value
                // Interlocked.CompareExchange() compares two values for quality and, if equal then replaces 1st value

                // Interlocked.MemoryBarrier is a wrapper for Thread.MemoryBarrier
                // only required on memory systems that have weak memory ordering (e.g., Itanium)
                // prevents the CPU from reordering the instructions such that those before the barrier
                // execute after the Memory Barrier
                // Interlocked.MemoryBarrier();
            }

            public void Withdraw(int amount)
            {
                Interlocked.Add(ref balance, -amount);
            }
        }

        public static void Test()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            for (int i = 0; i < 10; ++i)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; ++j)
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
