using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;


namespace dotnet_parallel.ConcurrentCollections
{
    public class BlockingCollectionDemo
    {
        // the idea of blocking collection is it can block elements that we are consuming, and not let add elements
        // 1st parameter is collection
        // 2nd parameter is boundedCapacity defines how many elements we can have at most
        static BlockingCollection<int> messages = 
            new BlockingCollection<int>(new ConcurrentBag<int>(), 10);
        
        static CancellationTokenSource cts = new CancellationTokenSource();
        static Random random = new Random();

        public static void Test()
        {
            Task.Factory.StartNew(ProduceAndConsume, cts.Token);

            Console.ReadKey();
            cts.Cancel();
        }

        public static void ProduceAndConsume()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);
            try
            {
              Task.WaitAll(new[] { producer, consumer}, cts.Token);
            }
            catch(AggregateException ae)
            {
                // return true whatever happens
                ae.Handle(e => true);
            }
        }

        // getting items from the collection
        private static void RunConsumer()
        {
            // we don't have to manually remove items from the collection
            // because GetConsumingEnumerable() removes and returns elements out of the collection
            foreach (var item in messages.GetConsumingEnumerable())
           {
             cts.Token.ThrowIfCancellationRequested();
             Console.WriteLine($"-{item}\t");
                Thread.Sleep(random.Next(1000));
           }
        }

        // adding items to collection
        private static void RunProducer()
        {
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int i = random.Next(100);
                // if there is already 10 elements (boundedCapacity), this thread is going to block until it will have less than 10 elements 
                messages.Add(i);
                Console.WriteLine($"+{i}\t");
                Thread.Sleep(random.Next(100));
            }
        }
    }
}
