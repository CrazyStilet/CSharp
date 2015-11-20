using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_2
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Задание 1. Строки
                1. Дана строка текста, в которой слова разделены пробелами. Необходимо:
                    - определить количество слов в строке;
                    - найти самое длинное слово и его порядковый номер;
                    - вычислить количество разных слов в строке.*/
            Console.WriteLine("Введите строку:");
            string str = Console.ReadLine();
            string[] array = str.Split(' ');
            string longWord = array[0];
            int numberWord = 0;
            Console.WriteLine("Количество слов в строке = {0}",array.Length);

            for(int i = 0; i < array.Length; i++)
            {
                if(longWord.Length<array[i].Length)
                {
                    longWord = array[i];
                    numberWord = i;
                }
            }
            Console.WriteLine("Самое длинное слово: {0}\nЕго порядковый номер: {1}", longWord, numberWord + 1);

            int count = 0, unique = 0;
            for(int i = 0; i < array.Length; i++)
            {
                for(int j = 1; j < array.Length - i; j++)
                {
                    if(array[i] == array[i + j])
                    {
                        break;
                    }
                    count++;
                }
                if(count == array.Length - i - 1)
                {
                    unique++;
                }
                count = 0;
            }
            Console.WriteLine("Разных слов в строке: {0}", unique);
            
            /*Задание 2. Массивы
                1.	Напишите программу, которая сортирует строки рваного массива по количеству элементов в строке.*/
            Console.WriteLine("Не отсортированный рваный массив:");
            Random rand = new Random();
            int size = rand.Next(1, 10);
            int[][] mas = new int[size][];
            for(int i = 0; i < size; i++)
            {
                mas[i] = new int[rand.Next(1, 5)];
                for(int j = 0; j < mas[i].Length; j++)
                {
                    mas[i][j]=rand.Next(1,10);
                    Console.Write("{0} ", mas[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Отсортированный рваный массив:");
            for(int k = 0; k < size; k++)
            {
                for(int i = 1; i < size; i++)
                {
                    if(mas[i - 1].Length > mas[i].Length)
                    {
                        int[] temp = new int[mas[i - 1].Length];
                        temp = mas[i - 1];
                        mas[i - 1] = new int[mas[i].Length];
                        mas[i - 1] = mas[i];
                        mas[i] = temp;
                    }
                }
            }
            
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < mas[i].Length; j++)
                {
                    Console.Write("{0} ", mas[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}
