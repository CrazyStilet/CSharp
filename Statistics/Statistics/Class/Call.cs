using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Class
{
    class Call
    {
        int numberPhone;
        public int NumberPhone
        {
            get { return numberPhone; }
            set { numberPhone = value; }
        }
        int timeCall;
        public int TimeCall
        {
            get { return timeCall; }
            set { timeCall = value; }
        }
        double castCall;
        public double CastCall
        {
            get { return castCall; }
            set { castCall = value; }
        }
        public Call() { }
        public Call(int number,int time,double cast)
        {
            numberPhone = number;
            timeCall = time;
            castCall = cast;
        }

        public override string ToString()
        {
            return String.Format("№ Телефона: {0} - Продолжительность: {1} секунд - Стоимость: {2}", numberPhone, timeCall, castCall);
        }
        public void Show(int i)
        {
            Console.WriteLine("№ {0} - {1}", i + 1, numberPhone);
        }
        public void Show()
        {

        }
        public string WriteCall()
        {
            string str = numberPhone + " " + timeCall + " " + castCall;
            return str;
        }
    }
}
