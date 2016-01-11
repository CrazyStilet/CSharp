using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_5.Class
{
    class Car
    {
        public void OnStop(Object sender, EventArgs e)
        {
            Vehicle vehicle = (Vehicle)sender;
            VehicleEventArgs vea = (VehicleEventArgs)e;

            Console.WriteLine("Состояние машины: {0}, проехали {1} км",
                vea.Message, vehicle.Way);
        }
    }
}
