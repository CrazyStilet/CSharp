using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_5_2.Cars;
using System.Threading;

namespace Homework_5_2.Class
{
    class Race
    {
        static bool finish = false;
        static public bool Finish
        {
            set
            {
                finish = value;
            }
            get
            {
                return finish;
            }
        }
        public static void Start()
        {
            Car[] cars ={
                          new SportCar(200,30,7,2),
                          new LightCar(150,50,10,5),
                          new Truck(120,100,15,10),
                          new Bus(120,75,12,7.5)
                      };

            Car.CarStop += new EventHandler(cars[0].Stop);

            while(!finish && Car.Ok())
            {
                Console.Clear();
                foreach(Car car in cars)
                {
                    car.Go();
                    car.Show();
                }
                Thread.Sleep(200);
            }
        }
    }
}
