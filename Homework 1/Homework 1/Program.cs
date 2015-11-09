using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1
{
    class Program
    {
        static void H1()
        {
            Console.WriteLine("\n\n\t\t***Диапазон целых чисел от А до B***\n\n");
            int firstNumber, secondNumber;
            do
            {
                do
                {
                    Console.WriteLine("Введите первое положительное число: ");
                    firstNumber = int.Parse(Console.ReadLine());
                    if(firstNumber < 0)
                    {
                        Console.WriteLine("Введено отрицательное число!");
                        continue;
                    }
                    break;
                } while(true);
                do
                {
                    Console.WriteLine("Введите второе положительное число: ");
                    secondNumber = int.Parse(Console.ReadLine());
                    if(secondNumber < 0)
                    {
                        Console.WriteLine("Введено отрицательно число!");
                        continue;
                    }
                    break;
                } while(true);
                if(firstNumber > secondNumber)
                {
                    Console.WriteLine("Первое число больше второго");
                    continue;
                }
                break;
            } while(true);
            Console.WriteLine();
            for(int i = firstNumber; i <= secondNumber; i++)
            {
                for(int j = 0; j < i; j++)
                {
                    Console.Write("{0}", i);
                }
                Console.WriteLine();
            }
        }

        static void H2()
        {
            Console.WriteLine("\n\n\t\t***Вывод строк алфавита***\n\n");
            Console.Write("Введите количество строк: ");
            int row = Convert.ToInt32(Console.ReadLine());
            char symbol;
            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    symbol = Convert.ToChar(i + j + 65);
                    Console.Write(symbol + " ");
                }
                Console.WriteLine();
            }
        }

        static void H3()
        {
            Console.Write("\n\n\t\t***Определение число перевертышь***\n\n");
            Console.Write("Введите число: ");
            int number = Convert.ToInt32(Console.ReadLine()), count = 0, firstNumber = number, secondNumber = number;
            bool bNumber = true;
            while(secondNumber % 10 != 0)
            {
                secondNumber /= 10;
                count++;
            }
            secondNumber = number;
            for(int i = 0; i <= count / 2; i++)
            {
                secondNumber %= 10;
                for(int j = i; j + 1 < count; j++)
                {
                    firstNumber /= 10;
                }
                firstNumber %= 10;
                if(firstNumber == secondNumber)
                {
                    firstNumber = number;
                    secondNumber = number;
                    for(int k = 0; k < i + 1; k++)
                    {
                        secondNumber /= 10;
                    }
                    continue;
                }
                else
                {
                    bNumber = false;
                    break;
                }
            }
            if(bNumber)
            {
                Console.WriteLine("Число является числом перевертышем");    
            }
            else
            {
                Console.WriteLine("Число не является числом перевертышем");
            }
        }

        static void H4()
        {
            Console.Write("\n\n\t\t***Проверка на интервал 'a'-'f' и x кратно 8***\n\n");
            Console.Write("Введите букву: ");
            char symbol = Convert.ToChar(Console.ReadLine());
            if(symbol >= 'a')
            {
                if(symbol <= 'f')
                {
                    Console.Write("Введен символ из допустимого интервала ");
                    if(symbol % 8 == 0)
                    {
                        Console.WriteLine("{0} кратно 8", symbol);
                    }
                    else
                    {
                        Console.WriteLine("{0} не кратно 8", symbol);
                    }
                }
                else
                {
                    Console.WriteLine("Введен символ из недопустимого интервала");
                }
            }
            else
            {
                Console.WriteLine("Введен символ из недопустимого интервала");
            }
        }

        static void H5()
        {
            Console.Write("\n\n\t***Проверка на попадание в список значений 1,7,9,22***\n\n");
            Console.Write("Введите число: ");
            int number = Convert.ToInt32(Console.ReadLine());
            switch(number)
            {
            case 1:
            case 7:
            case 9:
            case 22:
                Console.WriteLine("Число входит в список");
                break;
            default:
                Console.WriteLine("Число не входит в список");
                break;
            }
        }

        static void H6()
        {
            Console.Write("\n\n\t\t***Вывод значений выражения в интервале***\n\n");
            Console.WriteLine("Выражение Х + 0,5");
            Console.WriteLine("Интервал -37...-22,5");
            Console.WriteLine("Количество значений 12");
            for(double i = -22.5; i >= -37; i += ((-37 + 22.5) / 12))
            {
                Console.WriteLine(i + 0.5);
            }
        }

        static void H7()
        {
            Console.Write("\n\n\t***Определение суммы наименьшей и наибольшей цифры в числе***\n\n");
            Console.Write("Введите число: ");
            int num = Convert.ToInt32(Console.ReadLine()), count = 0, tempNum = num, minNum = 9, maxNum = 0;
            while(tempNum % 10 != 0)
            {
                tempNum /= 10;
                count++;
            }

            tempNum = num;
            for(int i = 0; i < count; i++)
            {
                if(minNum > tempNum % 10)
                {
                    minNum = tempNum % 10;
                }
                if(maxNum < tempNum % 10)
                {
                    maxNum = tempNum % 10;
                }
                tempNum /= 10;
            }
            Console.WriteLine("Сумма наибольшей {0} и наименьшей {1} цифры в числе {2} равна {3}", maxNum, minNum, num, minNum + maxNum);
        }

        static void H8()
        {
            Console.WriteLine("\n\n\t\t***Рассчитать сумму чисел 100 Фибоначчи***\n\n");
            ulong numFib = 1, current = 0, lastNumFib = 0;
            for(int i = 0; i < 100; i++)
            {
                numFib += lastNumFib;
                lastNumFib = current;
                current= numFib;
            }
            Console.WriteLine("Число Фибоначчи 100 - {0}", numFib);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\t\tДомашняя работа студента Олега Орлова по С# №1");
            int numTask;
            do
            {
                Console.Write("\n\nВведите номер задания от 1 до 8\n0 для выхода: ");
                numTask = Convert.ToInt32(Console.ReadLine());
                switch(numTask)
                {
                case 1:
                    H1();
                    break;
                case 2:
                    H2();
                    break;
                case 3:
                    H3();
                    break;
                case 4:
                    H4();
                    break;
                case 5:
                    H5();
                    break;
                case 6:
                    H6();
                    break;
                case 7:
                    H7();
                    break;
                case 8:
                    H8();
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Неверно введен номер задания");
                    break;
                }
            } while(numTask != 0);
        }
    }
}