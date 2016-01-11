using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_5.Class;

namespace Homework_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicle vehicle = new Vehicle();
            Car car = new Car();
            vehicle.CarStop += new EventHandler(car.OnStop);
            vehicle.Go();
            Console.WriteLine("Завершение программы");
            Console.ReadKey();
        }
    }
}
