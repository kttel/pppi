using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace lab3_pppi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task task = new Task(CallMethod);
            task.Start();
            task.Wait();

            Thread.Sleep(2000);
            separate();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Task.WaitAll(delayFor4000(), delayFor7000(), delayFor2000());

            stopWatch.Stop();
            var elapsed = stopWatch.ElapsedMilliseconds;
            Console.WriteLine($"Elapsed: {elapsed} ms");
            separate();

            Task task2 = new Task(thirdExample);
            task2.Start();
            task2.Wait();

            Thread.Sleep(2000);
            Thread thread = new Thread(WorkThread);
            thread.Start();
            Console.WriteLine("Main thread quits.");
            thread.Join();
            separate();

            Thread.Sleep(2000);
            Thread first_thread = new Thread(new ThreadStart(threadMethod));
            Thread second_thread = new Thread(new ThreadStart(threadMethod));

            first_thread.Start();
            second_thread.Start();
            first_thread.Join();
            second_thread.Join();
            separate();

            Thread thread1 = new Thread(ForegroundMethod){};
            Console.WriteLine($"Thread1 is a background thread:  {thread1.IsBackground}");
            thread1.Start();
            Console.WriteLine("Main thread exited");

            Console.ReadLine();
        }
        static async void CallMethod()
        {
            string filePath = "D:\\lorem-ipsum.txt";
            const string name = "[CallMethod()]";

            Task<int> task = ReadFile(filePath);

            Console.WriteLine($"{name} Something");
            Console.WriteLine($"{name} Something else");

            int length = await task;
            Console.WriteLine($"{name} Total length: " + length);

            Console.WriteLine($"{name} After something");
            Console.WriteLine($"{name} After something else");
            
        }

        static async Task<int> ReadFile(string file)
        {
            int length = 0;
            const string name = "\t[ReadFile(string file)]";
            Console.WriteLine($"{name} File reading is stating");
            using (StreamReader reader = new StreamReader(file))
            {
                string s = await reader.ReadToEndAsync();

                length = s.Length;
            }
            Console.WriteLine($"{name} File reading is completed");
            return length;
        }

        static async Task delayFor4000()
        {
            await Task.Delay(4000);
            Console.WriteLine("delayFor4000 finished");
        }

        static async Task delayFor7000()
        {
            await Task.Delay(7000);
            Console.WriteLine("delayFor7000 finished");
        }

        static async Task delayFor2000()
        {
            await Task.Delay(2000);
            Console.WriteLine("delayFor2000 finished");
        }

        static async Task<string> MethodAsync(string x, int delay)
        {
            for (int i = 0; i < 3; i++)
            {
                ConsoleWriteLine($" {x}{i}");
                await Task.Delay(delay);
            }
            string result = new string(x[0], 4);
            ConsoleWriteLine($" {x} returns result {result}");
            return result;
        }

        static void ConsoleWriteLine(string str)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(
               $"{str}\tThread {threadId}");
        }
        static async void thirdExample()
        {
            var tasks = new List<Task<string>> {
                MethodAsync("A", 50),
                MethodAsync("B", 100), 
                MethodAsync("C", 20) 
            };
            while (tasks.Any())
            {
                Task<string> taskTerminated = await Task.WhenAny(tasks);
                ConsoleWriteLine($"Task terminated result {taskTerminated.Result}");
                Console.WriteLine();
                tasks.Remove(taskTerminated);
            }
            separate();

        }

        static void WorkThread()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("\tWorker thread is in progress!");
                Thread.Sleep(2000); 
            }
            Console.WriteLine("Worker thread quits.");
        }


        public static void threadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }

        static void ForegroundMethod()
        {
            Console.WriteLine("ForegroundMethod started");
            Thread thread2 = new Thread(BackgroundMethod)
            {
                IsBackground = true
            };
            thread2.Start();
            Thread.Sleep(3000);
            Console.WriteLine("ForegroundMethod exited");
        }
        static void BackgroundMethod()
        {
            Console.WriteLine("BackgroundMethod Started");
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine("\tbg thread is in progress!");
                Thread.Sleep(1000);
            }
            Console.WriteLine("BackgroundMethod Exited");
        }

        static void separate()
        {
            Console.WriteLine("\n<---------------------------->\n");
        }
    }
}
