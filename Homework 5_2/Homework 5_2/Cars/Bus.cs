﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_5_2.Class;

namespace Homework_5_2.Cars
{
    class Bus:Car
    {
        static string type = "Bus";
        static bool Ok = true;
        static public bool OK
        {
            set
            {
                Ok = value;
            }
            get
            {
                return Ok;
            }
        }
        public Bus(int MaxSpeed, int fuel, int FuelDown, double oil)
            : base(type, MaxSpeed, fuel, FuelDown, oil)
        {
        }
    }
}
