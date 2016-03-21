using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Statistics.Class;
using System.IO;

namespace Statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Phone> PhoneList = new List<Phone>();
            CreatePhoneList(PhoneList);
            MainMenu(PhoneList);
        }
        private static List<Phone> CreatePhoneList(List<Phone>PhoneList)
        {
            try
            {
                using(FileStream file=new FileStream("PhoneBase.txt",FileMode.Open,FileAccess.Read))
                {
                    using(StreamReader strR = new StreamReader(file, Encoding.Default))
                    {
                        // Чтение Базы данных телефонов из файла
                        int number;
                        double limitMoney;
                        string[] strM = strR.ReadToEnd().Split('\n');
                        for(int i = 0; i < strM.Length; i++)
                        {
                            if (strM[i] == "")
                            {
                                break;
                            }
                            string[] str = strM[i].Split(' ');
                            number = Convert.ToInt32(str[0]);
                            limitMoney = Convert.ToDouble(str[1]);
                            PhoneList.Add(new Phone(number, limitMoney));
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                if(PhoneList.Count==0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Хотите создать и заполнить базу? (Y - да \\ остальное - Нет)");
                    Console.ResetColor();
                    string answer = Console.ReadLine();
                    if((answer == "Y") || (answer == "y"))
                    {
                        CreateManuallyPhoneList(PhoneList);
                    }
                }
                else
                {
                    Console.WriteLine("Для продолжения нажмите Enter");
                    Console.ReadLine();
                }
            }
            return PhoneList;
        }
        private static List<Phone> CreateManuallyPhoneList(List<Phone> PhoneList)
        {
            Console.Clear();
            string answer = "y";
            do
            {
                int numPhone = 1;
                double limitMoney;
                try
                {
                    Console.WriteLine("Введите {0}-й номер телефона:", PhoneList.Count + 1);
                    numPhone = Convert.ToInt32(Console.ReadLine());
                    if(numPhone <= 0 || numPhone > 9999999)
                    {
                        throw new Exception("Неверный формат номера телефона");
                    }
                    if(PhoneList.Count != 0)
                    {
                        foreach(Phone Phone in PhoneList)
                        {
                            if(Phone.CountNumber == numPhone)
                            {
                                throw new Exception("Такой номер уже существует");
                            }
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                    continue;
                }
                do
                {
                    try
                    {
                        Console.WriteLine("Введите лимит средств для этого номер:");
                        limitMoney = Convert.ToDouble(Console.ReadLine());
                        if(limitMoney < 0)
                        {
                            throw new Exception("Лимит вредств не может быть отрицательным");
                        }
                        PhoneList.Add(new Phone(numPhone, limitMoney));
                        Console.WriteLine("Добавить еще телефон? (Y - Да\\ остальне - Нет)");
                        answer = Console.ReadLine();
                    }
                    catch(Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                        continue;
                    }
                    break;
                } while(true);
            } while((answer == "y") || (answer == "Y"));

            using (FileStream file=new FileStream("PhoneBase.txt",FileMode.Create,FileAccess.Write))
            {
                using(StreamWriter strW=new StreamWriter(file,Encoding.Default))
                {
                    foreach(Phone phone in PhoneList)
                    {
                        strW.WriteLine(phone.WritePhone());
                    }
                }
            }
            return PhoneList;
        }
        private static void MainMenu(List<Phone>PhoneList)
        {
            int menu = 0;
            Console.Clear();
            do
            {
                if(PhoneList.Count == 0)
                {
                    Console.WriteLine("База телефонов пуста, хотите создать и заполнить её вручную (Y - Да \\ остальное - Нет)");
                    string answer = Console.ReadLine();
                    if((answer == "Y") || (answer == "y"))
                    {
                        CreateManuallyPhoneList(PhoneList);
                    }
                    else
                    {
                        Console.WriteLine("Дальнейшая работа с программой невозможна!\nНажмите Enter для выхода!");
                        Console.ReadLine();
                        return;
                    }
                }
                Console.WriteLine("Выберите телефон:");
                
                for(int i = 0; i < PhoneList.Count; i++)
                {
                    PhoneList[i].Show(i);
                }
                Console.WriteLine("№ {0} Статистика", PhoneList.Count + 1);
                Console.WriteLine("№ {0} Выход", PhoneList.Count + 2);
                Console.Write("Введите пункт меню: ");
                try
                {
                    menu = Convert.ToInt32(Console.ReadLine());
                    if(menu > PhoneList.Count + 2 || menu < 1)
                    {
                        throw new Exception("Нет такого пункта меню");
                    }
                    if(menu <= PhoneList.Count)
                    {
                        CallMenu(PhoneList, menu);
                    }
                    if(menu == PhoneList.Count + 1)
                    {
                        // Статистика
                        StatisticMenu(PhoneList);
                    }
                    if(menu == PhoneList.Count + 2)
                    {
                        // Выход
                        Console.WriteLine("Выход");
                        using(FileStream file = new FileStream("PhoneBase.txt", FileMode.Create, FileAccess.Write))
                        {
                            using(StreamWriter strW = new StreamWriter(file, Encoding.Default))
                            {
                                foreach(Phone phone in PhoneList)
                                {
                                    strW.WriteLine(phone.WritePhone());
                                }
                            }
                        }
                        break;
                    }
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Повторите ввод");
                    Console.ResetColor();
                }
            } while(true);
        }
        private static void StatisticMenu(List<Phone>PhoneList)
        {
            int menu = 0;
            Console.Clear();
            do
            {
                Console.WriteLine("Меню статистики");
                Console.WriteLine("№ 1 Показать звонки для всех телефонов");
                Console.WriteLine("№ 2 Показать звонки по указанному телефону");
                Console.WriteLine("№ 3 Показать сумму стоимости/продолжительности звонков по всем телефонам");
                Console.WriteLine("№ 4 Показать сумму стоимости/продолжительности звонков по указаному телефону");
                Console.WriteLine("№ 5 Показать среднюю стоимость одного звонка по всем телефонам");
                Console.WriteLine("№ 6 Показать среднюю стоимость одного звонка по указанному телеофну");
                Console.WriteLine("№ 7 Показать дорогой/дешевый звонок и номер, на который был осуществлен звонок, среди всех телефонов");
                Console.WriteLine("№ 8 Показато дорогой/дешевый звонок и номер, на который был осуществлен звонок, по выбранному телефону");
                Console.WriteLine("№ 9 Показать телефона, с которого было осуществлено наибольшее/наименьшее звонков");
                Console.WriteLine("№ 10 Показать телефон, с которого было осуществленно звонков на самую наибольшую/наименьшую стоимость");
                Console.WriteLine("№ 11 Вернуться в главное меню");
                Console.Write("Введите пункт меню: ");
                try
                {
                    menu = Convert.ToInt32(Console.ReadLine());
                    if(menu < 1 || menu > 11)
                    {
                        throw new Exception("Нет такого пункта меню");
                    }
                    if(menu == 1)
                    {
                        // Показать звонки для всех телефонов
                        Console.Clear();
                        Console.WriteLine("Звонки для всех телефонов:");
                        foreach(Phone phones in PhoneList)
                        {
                            phones.ShowCalls();
                        }
                        Console.Write("Для продолжения нажмите Enter:");
                        Console.ReadLine();
                    }
                    if(menu == 2)
                    {
                        // Показать звонки по указанному телефону
                        Console.Clear();
                        Console.WriteLine("Выберите телефон:");
                
                        for(int i = 0; i < PhoneList.Count; i++)
                        {
                            PhoneList[i].Show(i);
                        }
                        try
                        {
                            int choose = Convert.ToInt32(Console.ReadLine());
                            if(choose < 1 || choose > PhoneList.Count)
                            {
                                throw new Exception("Нет такого номера в списке");
                            }
                            else
                            {
                                PhoneList[choose - 1].ShowCalls();
                            }
                        }
                        catch(Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(e.Message);
                            Console.ResetColor();
                        }
                        Console.Write("Для продолжения нажмите Enter:");
                        Console.ReadLine();
                    }
                    if(menu == 3)
                    {
                        // Показать сумму стоимости/продолжительности звонков по всем телефонам
                        Console.Clear();
                        Console.WriteLine("Сумма стоимости/продолжительности звонков по всем телефонам:");
                        foreach(Phone phones in PhoneList)
                        {
                            phones.ShowCastAndDuration();
                        }
                        Console.Write("Нажмите Enter для продолжения:");
                        Console.ReadLine();
                    }
                    if(menu == 4)
                    {
                        // Показать сумму стоимости/продолжительности звонков по указаному телефону
                        Console.Clear();
                        Console.WriteLine("Выберите телефон:");

                        for(int i = 0; i < PhoneList.Count; i++)
                        {
                            PhoneList[i].Show(i);
                        }
                        try
                        {
                            int choose = Convert.ToInt32(Console.ReadLine());
                            if(choose < 1 || choose > PhoneList.Count)
                            {
                                throw new Exception("Нет такого номера в списке");
                            }
                            else
                            {
                                PhoneList[choose - 1].ShowCastAndDuration();
                            }
                        }
                        catch(Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(e.Message);
                            Console.ResetColor();
                        }
                        Console.Write("Для продолжения нажмите Enter:");
                        Console.ReadLine();
                    }
                    if(menu == 5)
                    {
                        // Показать среднюю стоимость одного звонка по всем телефонам
                        Console.Clear();
                        Console.Write("Средняя стоимость одного звонка по всем телефонам: ");
                        double averageCast = 0;
                        foreach(Phone phones in PhoneList)
                        {
                            if(averageCast==0)
                            {
                                averageCast = phones.AverageCostCalls;
                            }
                            else
                            {
                                averageCast += phones.AverageCostCalls;
                                averageCast /= 2;
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(averageCast);
                        Console.ResetColor();
                        Console.Write("Нажмите Enter для продолжения:");
                        Console.ReadLine();
                    }
                    if(menu == 6)
                    {
                        // Показать среднюю стоимость одного звонка по указанному телеофну
                        Console.Clear();
                        Console.WriteLine("Выберите телефон:");

                        for(int i = 0; i < PhoneList.Count; i++)
                        {
                            PhoneList[i].Show(i);
                        }
                        try
                        {
                            int choose = Convert.ToInt32(Console.ReadLine());
                            if(choose < 1 || choose > PhoneList.Count)
                            {
                                throw new Exception("Нет такого номера в списке");
                            }
                            else
                            {
                                Console.WriteLine("Сердняя стоимость одного звонка по указанному телефону: {0}", PhoneList[choose - 1].AverageCostCalls);
                            }
                        }
                        catch(Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(e.Message);
                            Console.ResetColor();
                        }
                        Console.Write("Нажмите Enter для продолжения:");
                        Console.ReadLine();
                    }
                    if(menu == 7)
                    {
                        // Показать дорогой/дешевый звонок и номер, на который был осуществлен звонок, среди всех телефонов
                        Console.Clear();
                        Console.WriteLine("Тут должен быть какой-то код... но пока его нет(");
                        Console.Write("Нажмите Enter для продолжения:");
                        Console.ReadLine();
                    }
                    if(menu == 8)
                    {
                        // Показато дорогой/дешевый звонок и номер, на который был осуществлен звонок, по выбранному телефону
                        Console.Clear();
                        Console.WriteLine("Тут должен быть какой-то код... но пока его нет(");
                        Console.Write("Нажмите Enter для продолжения:");
                        Console.ReadLine();
                    }
                    if(menu == 9)
                    {
                        // Показать телефона, с которого было осуществлено наибольшее/наименьшее звонков
                        Console.Clear();
                        Console.WriteLine("Тут должен быть какой-то код... но пока его нет(");
                        Console.Write("Нажмите Enter для продолжения:");
                        Console.ReadLine();
                    }
                    if(menu == 10)
                    {
                        // Показать телефон, с которого было осуществленно звонков на самую наибольшую/наименьшую стоимость
                        Console.Clear();
                        Console.WriteLine("Тут должен быть какой-то код... но пока его нет(");
                        Console.Write("Нажмите Enter для продолжения:");
                        Console.ReadLine();
                    }
                    if(menu == 11)
                    {
                        // Вернуться в главное меню
                        Console.WriteLine("Возврат в главное меню");
                        break;
                    }
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Повторите ввод");
                    Console.ResetColor();
                }
            } while(true);
        }
        private static void CallMenu(List<Phone> list,int chooseNumber)
        {
            Console.Clear();
           
            int menu = 0;
            do
            {
                Console.WriteLine("Ваш номер: {0}, Доступные средства: {1}", list[chooseNumber - 1].CountNumber, list[chooseNumber - 1].LimitMoney);
                Console.WriteLine("№ 1 Позвонить");
                Console.WriteLine("№ 2 Вернуться в главное меню");
                try
                {
                    menu = Convert.ToInt32(Console.ReadLine());
                    if(menu < 1 || menu > 2)
                    {
                        throw new Exception("Нет такого пункта меню");
                    }
                    if(menu == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Ваш номер: {0}, Доступные средства: {1}", list[chooseNumber - 1].CountNumber, list[chooseNumber - 1].LimitMoney);
                        do
                        {
                            Console.WriteLine("Выберите номер на который надо позвонить из списка:");
                            for(int i = 0; i < list.Count; i++)
                            {
                                if(i == chooseNumber - 1)
                                {
                                    continue;
                                }
                                else
                                {
                                    list[i].Show(i);
                                }
                            }
                            Console.WriteLine("№ {0} - Возврат к предыдущему меню", list.Count + 1);
                            Console.WriteLine("Ваш выбор: ");
                            try
                            {
                                int choose = Convert.ToInt32(Console.ReadLine());
                                Random random = new Random();
                                int timeCall = random.Next(-360, 3600);
                                double CastCall = (double)timeCall / 100;
                                if(choose == list.Count + 1)
                                {
                                    break;
                                }
                                else if(choose <= 0 || choose == chooseNumber || choose > list.Count)
                                {
                                    throw new Exception("Нет такого пункта меню");
                                }
                                else if(list[chooseNumber - 1].LimitMoney <= 0)
                                {
                                    Bad_Call badCall = new Bad_Call(0, "Недостаточно средств для звонка", list[chooseNumber - 1].CountNumber);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(badCall);
                                    Console.ResetColor();
                                    badCall.WriteBadCall();
                                    break;
                                }
                                else if(timeCall <= 0)
                                {
                                    Bad_Call badCall = new Bad_Call(1, "Звонок не удался", list[chooseNumber - 1].CountNumber);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(badCall);
                                    Console.ResetColor();
                                    badCall.WriteBadCall();
                                    break;
                                }
                                else if(CastCall > list[chooseNumber - 1].LimitMoney)
                                {
                                    CastCall = list[chooseNumber - 1].LimitMoney;
                                    timeCall = 100 * (int)CastCall;
                                }
                                Console.WriteLine("Успешный звонок на номер: {0}, Продолжительность: {1} секунд, Стоимость: {2}$", list[choose - 1].CountNumber, timeCall, CastCall);
                                list[chooseNumber - 1].LimitMoney -= CastCall;
                                list[chooseNumber - 1].ListCall.Add(new Call(list[choose - 1].CountNumber, timeCall, CastCall));
                                // Записать звонок в файл
                                string path = "PhoneBase\\" + Convert.ToString(list[chooseNumber - 1].CountNumber) + ".txt";
                                using(FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write))
                                {
                                    using(StreamWriter strW = new StreamWriter(file, Encoding.Default))
                                    {
                                        strW.WriteLine(list[chooseNumber - 1].ListCall[list[chooseNumber - 1].ListCall.Count - 1].WriteCall());
                                    }
                                }
                                break;
                            }
                            catch(Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(e.Message);
                                Console.ResetColor();
                            }
                        } while(true);
                    }
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Повторите ввод");
                    Console.ResetColor();
                }
            } while(menu != 2);
        }
    }
}
