using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Statistics.Class
{
    class Phone
    {
        int countNumber;
        public int CountNumber
        {
            get { return countNumber; }
            set { countNumber = value; }
        }
        List<Call> listCall = new List<Call>();
        internal List<Call> ListCall
        {
            get { return listCall; }
            set { listCall = value; }
        }
        double averageCostCalls; // Средняя стоимость звонков
        public double AverageCostCalls
        {
            get { return averageCostCalls; }
            set { averageCostCalls = value; }
        }
        double limitMoney;
        public double LimitMoney
        {
            get { return limitMoney; }
            set { limitMoney = value; }
        }
        public Phone() { }
        public Phone(int number,double limit)
        {
            countNumber = number;
            limitMoney = limit;
            string path = "PhoneBase\\" + Convert.ToString(countNumber) + ".txt";
            try
            {
                using(FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using(StreamReader strR = new StreamReader(file, Encoding.Default))
                    {
                        // Чтение звонков из файла
                        int numberPhone;
                        int timeCall;
                        double castCall;
                        string[] strM = strR.ReadToEnd().Split('\n');
                        for(int i = 0; i < strM.Length; i++)
                        {
                            if (strM[i]=="")
                            {
                                break;
                            }
                            string[] str = strM[i].Split(' ');
                            numberPhone = Convert.ToInt32(str[0]);
                            timeCall = Convert.ToInt32(str[1]);
                            castCall = Convert.ToDouble(str[2]);
                            if(averageCostCalls==0)
                            {
                                averageCostCalls += castCall;    
                            }
                            else
                            {
                                averageCostCalls += castCall;
                                averageCostCalls /= 2;
                            }
                            listCall.Add(new Call(numberPhone, timeCall, castCall));
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Для продолжения нажмите Enter");
                Console.ReadLine();
            }
        }
        public void ShowCalls()
        {
            Console.WriteLine("№ {0}", countNumber);
            foreach(Call calls in listCall)
            {
                Console.WriteLine(calls);
            }
            Console.WriteLine();
        }
        public void Show(int i)
        {
            Console.WriteLine("№ {0} - {1}", i + 1, countNumber);
        }
        public string WritePhone()
        {
            string str = countNumber + " " + limitMoney;
            return str;
        }
        public void ShowCastAndDuration()
        {
            Console.WriteLine("№ {0}", countNumber);
            int timeCall=0;
            double castCall=0;
            foreach(Call calls in listCall)
            {
                timeCall+=calls.TimeCall;
                castCall += calls.CastCall;
            }
            Console.WriteLine("Стоимость всех звонков: {0}\nПродолжительность всех звонков: {1}", castCall, timeCall);
            Console.WriteLine();
        }
    }
}
