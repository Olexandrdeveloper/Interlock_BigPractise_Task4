using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interlock_BigPractise_Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Intersection inter = new Intersection();
            Random rnd = new Random();
            int total = 8;

            Thread[] threads = new Thread[total];

            for (int i = 0; i < total; i++)
            {
                int id = i + 1;
                int from = rnd.Next(0, 4);
                int dir = rnd.Next(0, 3);

                threads[i] = new Thread(() =>
                {
                    Thread.Sleep(rnd.Next(100, 500));

                    Car car = new Car();
                    car.id = id;
                    car.from = from;
                    car.dir = dir;

                    inter.Go(car);
                });
            }

            foreach (var t in threads) t.Start();
            foreach (var t in threads) t.Join();
        }
    }
}
