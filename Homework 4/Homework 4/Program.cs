using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Homework_4
{
    class House
    {
        public static IPart[] parts = { new Basement(), new Walls(), new Walls(), new Walls(), new Walls(), new Door(), new Window(), new Window(), new Window(), new Window(), new Roof()};

        public static bool done = false;
        public static void showProgress()
        {

            for(int i = 0; i <= parts.Length; i++)
            {
                if(i == parts.Length)
                {
                    done = true;
                    return;
                }
                if(parts[i].getStatus() != 100)
                {
                    break;
                }
            }
            foreach(IPart part in parts)
            {
                part.showStatus();
            }
        }
    }
    class Basement : Part
    {
        public Basement() : base("Фундамент")
        {
        }
    }
    class Walls : Part
    {
        public Walls() : base("Стена")
        {
        }
    }
    class Door : Part
    {
        public Door() : base("Дверь")
        {
        }
    }
    class Window : Part
    {
        public Window():base("Окно")
        {
        }
    }
    class Roof : Part
    {
        public Roof() : base("Крыша")
        {
        }
    }
    class Worker : IWorker
    {
        public void working(ref IPart[] parts)
        {
            Console.WriteLine("Строительство");
            for(int i = 0; i <= House.parts.Length; i++)
            {
                if(i == House.parts.Length)
                {
                    House.done = true;
                    return;
                }
                if(House.parts[i].getStatus() != 100)
                {
                    break;
                }
            }
            foreach(IPart part in House.parts)
            {
                while(part.getStatus() < 100)
                {
                    part.build();
                    part.showStatus();
                }
            }

        }
    }
    class TeamLeader
    {
        public void Report()
        {
            if(!House.done)
            {
                House.showProgress();
            }
            else
            {
                Console.WriteLine("Дом построен!");
            }
        }
    }
    public interface IWorker
    {
        void working(ref IPart[]parts);
    }
    public interface IPart
    {
        void build();
        void showStatus();
        void setSpeed(int speed);
        int getStatus();
    }

    class Part:IPart
    {
        public int Progress
        {
            private set;
            get;
        }
        public int Speed
        {
            private set;
            get;
        }
        public string Type
        {
            private set;
            get;
        }
        public int Count{
            private set;
            get;
        }
        public bool Done
        {
            private set;
            get;
        }
        public int getStatus()
        {
            return Progress;
        }
        public Part(string type)
        {
            Done = false;
            Type = type;
        }
        public Part(string type, int count){
            Done=false;
            Type=type;
            Count = count;
        }
        public void setSpeed(int speed)
        {
            Speed = speed;
        }
        public void build()
        {
            int numRand;
            Random rand = new Random();
            numRand = rand.Next(1, 10);
            setSpeed(numRand);

            if(Progress==100)
            {
                return;
            }
            Progress += Speed;
        }
        public void showStatus()
        {
            if(Done)
            {
                Console.WriteLine(Type + " - стройка завершена!");
            }
            if(Progress > 100)
            {
                Progress = 100;
            }
            Console.WriteLine(Type + " готов(а) на " + Progress + "%");
            Thread.Sleep(200);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Worker Builder1 = new Worker();
            TeamLeader Superintendent = new TeamLeader();

            string select;
            do
            {
                Console.WriteLine("\n1 - Проверить статус постройки дома\n"
                    + "2 - Строить\n"
                    + "0 - Выход");
                select = Console.ReadLine();
                switch(select)
                {
                case "1":
                    Superintendent.Report();
                    break;
                case "2":
                    if(House.done)
                    {
                        Console.WriteLine("Дом построен!");
                    }
                    while(!House.done)
                    {
                        Builder1.working(ref House.parts);
                    }
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Неизвесная команда");
                    break;
                }
            } while(select != "0");
            
        }
    }
}
