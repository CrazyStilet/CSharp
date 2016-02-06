using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log;

namespace Loger.Class
{
    class Game
    {
        public static void Start()
        {
            Console.WriteLine("Перед началом работы Логера, очистить его? (Y - Да \\ остальное - Нет)");
            string answer = Console.ReadLine();
            if((answer=="Y") || (answer=="y"))
            {
                Console.WriteLine("Перезаписываем лог файл");
                Log.Loger.Truncate();
            }
            
            List<Player> listPlayers = new List<Player>();
            int countPlayers = 0;
            #region Создание и сортировка колоды
            Cards koloda = new Cards();
            koloda.Create();
            koloda.Sort();
            #endregion
            #region Создание игроков и раздача им карт
            do
            {
                countPlayers = 0;
                Console.WriteLine("Введите количество игроков от 2 до 6:");
                try
                {
                    countPlayers = Convert.ToInt32(Console.ReadLine());
                }
                catch(Exception e)
                {
                    Log.Loger.Write(e);
                    Console.WriteLine(e.Message);
                }
                if(countPlayers == 5)
                {
                    string errorMessage = "36 карт на 5 поровну не делятся";
                    Log.Loger.Write(errorMessage);
                    Console.WriteLine(errorMessage);
                }
            } while((countPlayers > 6 || countPlayers < 2) || countPlayers == 5);
            for(int i = 0; i < countPlayers; i++)
            {
                listPlayers.Add(new Player());
                listPlayers[i].ListCards.AddRange(koloda.Razdacha(countPlayers));
            }
            #endregion
            #region Игра
            int winner = 0;
            do
            {
                int count = 0;
                while(listPlayers[winner].CountPlayerCards != 36 && count != 100)
                {
                    Console.Clear();
                    List<Cards> Table = new List<Cards>(countPlayers);
                    for(int i = 0; i < countPlayers; i++)
                    {
                        if(listPlayers[i].CountPlayerCards != 0)
                        {
                            Table.Add(listPlayers[i].ListCards[0]);
                            listPlayers[i].ListCards.RemoveAt(0);
                        }
                        else
                        {
                            Cards card = new Cards();
                            Table.Add(card);
                        }
                    }
                    for(int i = 0; i < countPlayers; i++)
                    {
                        if(listPlayers[i].CountPlayerCards != 0)
                        {
                            Console.WriteLine("{0} игрок {1} карт: ", i + 1, listPlayers[i].CountPlayerCards);
                            listPlayers[i].ShowPlayerCards();
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine("\nКарты на столе:");
                    for(int i = 0; i < Table.Count; i++)
                    {
                        if(Table[i].typesCard != 0)
                        {
                            Console.WriteLine("{0} {1}\t", Table[i].typesCard, Table[i].typesMast);
                        }
                    }
                    for(int i = countPlayers - 1; i >= 0; i--)
                    {
                        if(Table[i].typesCard != 0)
                        {
                            if(Table[i].typesCard >= Table[winner].typesCard)
                            {
                                winner = i;
                            }
                        }
                    }
                    Console.WriteLine("Победил игрок № {0}\n", winner + 1);
                    for(int i = 0; i < Table.Count; i++)
                    {
                        if(Table[i].typesCard != 0)
                        {
                            listPlayers[winner].ListCards.Add(Table[i]);
                        }
                    }
                    count++;
                }
                Console.ReadLine();
                //Loger
            } while(listPlayers[winner].CountPlayerCards != 36);
            #endregion
            #region Победа
            Console.WriteLine("Игрок № {0} - ПОБЕДИТЕЛЬ!!!", winner + 1);
            Console.ReadLine();
            #endregion
        }
    }
}
