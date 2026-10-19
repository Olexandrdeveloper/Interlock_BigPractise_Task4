using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interlock_BigPractise_Task4
{
    internal class Intersection
    {
        private object locker = new object();
        private bool[] quads = new bool[4];

        public void Go(Car car)
        {
            List<int> needed = GetQuads(car.from, car.dir);

            lock (locker)
            {
                while (true)
                {
                    bool ok = true;
                    foreach (int q in needed)
                    {
                        if (quads[q])
                        {
                            ok = false;
                            break;
                        }
                    }

                    if (ok)
                    {
                        foreach (int q in needed)
                            quads[q] = true;

                        break;
                    }

                    Monitor.Wait(locker);
                }

                Console.WriteLine("Авто #" + car.id + " на перехресті");
            }

            Thread.Sleep(500);

            lock (locker)
            {
                foreach (int q in needed) quads[q] = false;

                Console.WriteLine("Авто #" + car.id + " проїхало");
                Monitor.PulseAll(locker);
            }
        }

        private List<int> GetQuads(int from, int dir)
        {
            List<int> list = new List<int>();
            if (dir == 0)
                list.Add(from);
            else if (dir == 1)
            {
                list.Add(from);
                list.Add((from + 1) % 4);
            }
            else
            {
                list.Add(from);
                list.Add((from + 1) % 4);
                list.Add((from + 2) % 4);
            }
            return list;
        }
    }
}
