using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_parallel.DataSharingAndSynchronization
{
    public class SpinLockinigAndLockRecursion
    {
        public class BankAccount
        {
            private int balance;

            public int Balance
            {
                get => balance;
                private set => balance = value;
            }

            // Deposit and Withdraw operations are not atomic, e.g. 
            // +=
            // operation1: temp <- getBalance() + amount
            // operation2: setBalance(temp)
            public void Deposit(int amount)
            {
                balance += amount;
            }

            public void Withdraw(int amount)
            {
                balance -= amount;
            }
        }

        public static void TestSpinLock()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            // spinning avoid overhead of resheduling
            // useful if you expect the wait time to be very short

            SpinLock sl = new SpinLock();

            // owner tracking keeps a record of which thread acquired it to improve debugging

            for (int i = 0; i < 10; ++i)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; ++j)
                    {
                        bool lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (lockTaken) sl.Exit();
                        }
                        
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; ++j)
                    {
                        bool lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTaken) sl.Exit();
                        }
                    }
                }));
            }



            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
        }

        public static void TestLockRecutsion()
        {
            LockRecursion(5);
        }

        // true = exception, false = deadlock
        static SpinLock sl = new SpinLock(true);

        // if we already took a lock on something then taking another lock might not be possible
        private static void LockRecursion(int x)
        {
            // lock recursion is being able to take the same lock multiple times
            bool lockTaken = false;
           
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch(LockRecursionException ex)
            {
                Console.WriteLine("Exception" + ex);
            }
            finally
            {
                if(lockTaken)
                {
                    Console.WriteLine($"Took a lock, x = {x}");
                    LockRecursion(x-1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }

        }
    }
}
