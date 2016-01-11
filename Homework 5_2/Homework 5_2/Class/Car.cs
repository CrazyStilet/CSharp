using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_5_2.Cars;

namespace Homework_5_2.Class
{
    public class CarEventArgs : EventArgs
    {
        public string Message
        {
            set;
            get;
        }
    }
    abstract class Car
    {
        public static event EventHandler CarStop;
        string type;
        int maxSpeed;
        int way;
        int fuelDown;
        #region PROPERTIES
        public int Fuel
        {
            private set;
            get;
        }
        public double Oil
        {
            private set;
            get;
        }
        public int Temperature
        {
            private set;
            get;
        }
        #endregion
        public int Way
        {
            private set
            {
                way = value;
            }
            get
            {
                return way;
            }
        }
        public Car(string Type, int MaxSpeed, int fuel, int FuelDown, double oil)
        {
            type = Type;
            maxSpeed = MaxSpeed;
            Fuel = fuel;
            Oil = oil;
            way = 0;
            Temperature = 30;
            fuelDown = FuelDown;
        }
        public void Show()
        {
            string str = type + "\nтопливо: " + Fuel + " масло: "
                + Oil + " температура: " + Temperature +
                "\nПройденное расстояние: " + way + "\n";
            Console.WriteLine(str);
        }
        static public bool Ok()
        {
            if(SportCar.OK || LightCar.OK || Truck.OK || Bus.OK)
            {
                return true;
            }
            return false;
        }
        void FuelDown()
        {
            Random random = new Random();
            Fuel -= random.Next(5,fuelDown);
            if(Fuel<0)
            {
                Fuel = 0;
            }
        }
        void OilDown()
        {
            Random random = new Random();
            Oil -= random.NextDouble();
            if(Oil<0)
            {
                Oil = 0;
            }
        }
        void TemperatureUp()
        {
            Random random = new Random();
            Temperature += random.Next(5, 20);
        }
        public void Go(){
            if(Way>=500)
            {
                Way = 500;
                Finish();
                return;
            }
            else if(Fuel <= 0)
            {
                Fuel = 0;
                Stop();
                return;
            }
            else if(Oil <= 0)
            {
                Oil = 0;
                Stop();
                return;
            }
            else if(Temperature >= 110)
            {
                Stop();
                return;
            }

            Random random=new Random();
            way += random.Next(0, maxSpeed);

            FuelDown();
            OilDown();
            TemperatureUp();
        }
        public void Stop()
        {
            if(CarStop!=null)
            {
                CarEventArgs carEvent = new CarEventArgs();
                if(Fuel <= 0)
                {
                    carEvent.Message = "Закончился бензин";
                }
                else if(Oil <= 0)
                {
                    carEvent.Message = "Закончилось масло";
                }
                else if(Temperature >= 110)
                {
                    carEvent.Message = "Перегрев двигателя";
                }
                CarStop(this, carEvent);
            }
            Ok();
        }
        public void Finish()
        {
            if(CarStop!=null)
            {
                CarEventArgs carEvent = new CarEventArgs();
                carEvent.Message = "Машина доехала до финиша";
                CarStop(this, carEvent);
                Race.Finish = true;
            }
        }
        public void Stop(Object sender, EventArgs e)
        {
            Car car = (Car)sender;
            CarEventArgs cea = (CarEventArgs)e;

            if(car.type=="SportCar")
            {
                SportCar.OK = false;
            }
            else if(car.type=="LightCar")
            {
                LightCar.OK = false;
            }
            else if(car.type=="Truck")
            {
                Truck.OK = false;
            }
            else if(car.type=="Bus")
            {
                Bus.OK = false;
            }

            Console.WriteLine("Состояние машины: {0}, проехала {1} км",
                cea.Message, car.Way);
        }
        public void Finish(Object sender, EventArgs e  )
        {
            Car car = (Car)sender;
            CarEventArgs cea = (CarEventArgs)e;

            Console.WriteLine("Состояние машины: {0}, проехала {1} км",
               cea.Message, car.Way);
        }
    }
}
