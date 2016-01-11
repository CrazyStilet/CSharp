using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_5.Class
{
    public class VehicleEventArgs : EventArgs
    {
        public string Message
        {
            get;
            set;
        }
    }
    class Vehicle
    {
        public event EventHandler CarStop;
        int speed;
        int way;

        public int Way
        {
            get
            {
                return way;
            }
        }
        #region Properties
        public int Fuel
        {
            get;
            set;
        }
        public double Oil
        {
            get;
            set;
        }
        public int Temperature
        {
            get;
            set;
        }
        #endregion
        public Vehicle()
        {
            speed = 100;
            way = 0;
            Fuel = 50;
            Oil = 4;
            Temperature = 30;
        }
        void FuelDown()
        {
            Random random = new Random();
            int rand = random.Next(5, 10);
            Fuel -= rand;
        }
        void OilDown()
        {
            Random random = new Random();
            double rand = random.NextDouble();
            Oil -= rand;
        }
        void TemperatureUp()
        {
            Random random = new Random();
            int rand = random.Next(5, 20);
            Temperature += rand;
        }
        public void Go()
        {
            for(int i = 0; i <= 10; i++)
            {
                way = speed * i;

                if(i!=0)
                {
                    FuelDown();

                    OilDown();

                    TemperatureUp();    
                }

                if(Fuel <= 0)
                {
                    OnStop();
                    break;
                }
                else if(Oil <= 0)
                {
                    OnStop();
                    break;
                }
                else if(Temperature >= 110)
                {
                    OnStop();
                    break;
                }
                else
                {
                    Show();
                }
            }
        }
        public void OnStop()
        {
            if(CarStop != null)
            {
                VehicleEventArgs vehicleArg = new VehicleEventArgs();
                if(Fuel<=0)
                {
                    vehicleArg.Message = "Закончился бензин";
                }
                else if(Oil<=0)
                {
                    vehicleArg.Message = "Закончилось масло";
                }
                else if(Temperature>=110)
                {
                    vehicleArg.Message = "Перегрев двигателя";
                }
                CarStop(this, vehicleArg);
            }
        }
        public void Show()
        {
            Console.WriteLine("Состояние машины! Едем, проехали {0} км", way);
        }
    }
}
