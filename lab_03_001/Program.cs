using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Lab3Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            // map and mutex for thread safety
            Mutex mutex = new Mutex();
            Dictionary<string, int> wcountsSingleThread = new Dictionary<string, int>();

            var filenames = new List<string> {
                "../../data/shakespeare_antony_cleopatra.txt",
                "../../data/shakespeare_hamlet.txt",
                "../../data/shakespeare_julius_caesar.txt",
                "../../data/shakespeare_king_lear.txt",
                "../../data/shakespeare_macbeth.txt",
                "../../data/shakespeare_merchant_of_venice.txt",
                "../../data/shakespeare_midsummer_nights_dream.txt",
                "../../data/shakespeare_much_ado.txt",
                "../../data/shakespeare_othello.txt",
                "../../data/shakespeare_romeo_and_juliet.txt",
           };

            Stopwatch stopWatch = new Stopwatch();
            TimeSpan time;
            double[] t = new double[2];

            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN SINGLE THREAD
            //=============================================================
            stopWatch.Start();
            foreach (string Name in filenames)                   // Initializing each thread.
            {
                HelperFunctions.CountCharacterWords(Name, mutex, wcountsSingleThread);
            }
            stopWatch.Stop();
            time = stopWatch.Elapsed;
            t[0] = time.TotalMilliseconds;
            stopWatch.Reset();

            List<Tuple<int, string>> singleThreadSorted = HelperFunctions.SortCharactersByWordcount(wcountsSingleThread);

            HelperFunctions.PrintListofTuples(singleThreadSorted);



            Console.WriteLine("SingleThread is Done!\n\n\n\n\n");
            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN MULTIPLE THREADS
            //=============================================================


            Dictionary<string, int> wcountsMultiThread = new Dictionary<string, int>();
            List<Thread> threads = new List<Thread>();
            stopWatch.Start();
            foreach (string Name in filenames)                   // Initializing each thread.
            {
                Thread thread = new Thread(() => HelperFunctions.CountCharacterWords(Name, mutex, wcountsMultiThread) );
                thread.Start();
                thread.Join();
                threads.Add(thread);
            }
            stopWatch.Stop();
            time = stopWatch.Elapsed;
            t[1] = time.TotalMilliseconds;
            stopWatch.Reset();


            double factor = t[0] / t[1];

            List<Tuple<int, string>> multiThreadSorted = HelperFunctions.SortCharactersByWordcount(wcountsMultiThread);

            HelperFunctions.PrintListofTuples(multiThreadSorted);


            Console.WriteLine("MultiThread is Done!\n\n");

            Console.WriteLine("Speed-Up Factor: {0}\n", factor);
        }
    }
}