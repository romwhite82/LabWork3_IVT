using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork3_Final
{
    class Program
    {
        //Метод, который выводит на экран содержимое массива
        //в форматированном виде
        static void ShowArray(int[] massive, string message)
        {
            if (massive.Length <= 500)
            {
                Console.WriteLine(message);
                for (int i = 0; i < massive.Length; i++)
                {
                    if (i > 0 && i % 10 == 0) Console.Write("\n");
                    Console.Write("|" + massive[i] + "|\t");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\nПрограмма выполняется. Массивы слишком велики для вывода на экран!");
            }
        }

        static int[] BubbleSort(int[] massive)
        {
            for (int k = 0; k < massive.Length; k++)
            {
                for (int i = 0; i < massive.Length - 1; i++)
                {
                    if (massive[i] > massive[i + 1]) // Последовательно сравниваем элементы 
                    {                                                  // Больший элемент постепенно движется к концу массива
                        int Temp;   //Временная переменная для обмена большего элемента с меньшим 
                        Temp = massive[i + 1];
                        massive[i + 1] = massive[i];
                        massive[i] = Temp;
                    }
                }
            }
            return massive;
        }
        static void Main(string[] args)
        {
            // Лабораторная работа № 3 "Обработка одномерных массивов"
            // Вариант 11
            // Найти: Повторяющиеся элементы массива А, которые есть в массиве B

            //Размерность массивов определяется аргументом коммандной строки
            // Если аргумент не задан (равен нулю), по умолчанию 100

            Console.WriteLine("Лабораторная работа 3 по теме \"Обработка одномерных массивов\"");
            Console.WriteLine("по предмету \"Программирование на языках высокого уровня\"");
            Console.WriteLine("Создаются два массива заданной размерности (по умолчанию 100)");
            Console.WriteLine("Массивы сортируются Пузырьком. Далее происходит поиск элементов");
            Console.WriteLine("которые повторяются в первом массиве и встречаются во втором");
            Console.WriteLine("Значения, отвечающие данным условиям, записываются в третий массив.");
            Console.WriteLine("\nВыполнил студент: Карандашёв Роман\n");
            Console.WriteLine("Использование программы: LabWork3.exe [размер массива]\n");


            int n;
            if (args.Length == 0)
            { n = 100; }
            else
            {
                try
                { n = Int32.Parse(args[0]); }
                catch { Console.WriteLine("Неправильный аргумент!");
                Environment.Exit(1);
                }
                finally { n = Int32.Parse(args[0]); }
                                
            }
            Console.WriteLine("Размерность массивов A и B: {0}", n);

            int[] massiveA = new int[n];
            int[] massiveB = new int[n];

            int[] massiveC = new int[0]; // В этот массив будут записываться повторяющиеся элементы
                                         // Поскольку неизвестно, сколько будет этих элементов,
                                         // Придётся каким-то образом превратить этот массив
                                         // в динамический))

            //Дальше нам необходимо инициализировать массивы A и B случайными числами 
            //В диапазоне от 0 до 2^16, то есть до 16384

            int MaxValue = 16384;
            Random rnd = new Random();
            for (int i = 0; i < massiveA.Length; i++) massiveA[i] = rnd.Next(0, MaxValue);

            //Выводим массив A на экран, с использованием метода ShowArray
            ShowArray(massiveA, "Массив А до сортировки:");

            //Теперь нужно выполнить ту же операцию для второго массива
            //Однако если мы её выполним сразу, он получит ровно те же значения
            //Поскольку генерация случайных чисел производится на основании показания
            //системных часов. Нужно подождать какое-то время. Для этого:

            Thread.Sleep(500); // Ждём 500 мсек, а затем инициализируем второй массив
            for (int i = 0; i < massiveB.Length; i++) massiveB[i] = rnd.Next(0, MaxValue);

            //Выводим массив B на экран с использованием метода ShowArray
            ShowArray(massiveB, "\nМассив B до сортировки");

            int t1 = Environment.TickCount; //Засекаем время выполнения программы
            
            //Теперь для поиска повторяющихся элементов в массиве A, его нужно сортировать Пузырьком
            //Для этого используем метод BubbleSort
            massiveA = BubbleSort(massiveA);
            // Выведем его на экран с помощью метода ShowArray

            ShowArray(massiveA, "\nМассив A после сортировки: ");

            //Повторяем сортировку и вывод на экран для массива B
            massiveB = BubbleSort(massiveB);
            ShowArray(massiveB, "\nМассив B после сортировки: ");

            //После выполнения предыдущих блоков, массив A и B являются упорядоченными
            // Теперь нужно найти в массиве А повторяющиеся значения, то есть следующий элемент
            // должен быть равным предыдущему

            int Temp = 0; //Для хранения значения повторяющегося элемента
            int Counter = 0; //Для определения позиции, куда записать значение в итоговом массиве C

            for (int i = 0; i < massiveA.Length - 1; i++)
            {
                if (massiveA[i] == massiveA[i + 1] && massiveA[i] != Temp)
                {
                    Temp = massiveA[i];
                    for (int k = 0; k < massiveB.Length; k++)
                    {
                        if (massiveB[k] == Temp)
                        {
                            Array.Resize(ref massiveC, massiveC.Length + 1);
                            massiveC[Counter] = Temp;
                            Counter++;
                            break;
                        }
                    }
                }
            }

            int t2 = Environment.TickCount;//Отсановка замера времени. Сохраняем состояние
            int TotalTime = t2 - t1;
           
            if (massiveC.Length == 0)
            {
                Console.WriteLine("\nЭлементов, отвечающих условию задания нет.");
            }
            else
                { ShowArray(massiveC, "\nЭлементы, повторяющиеся в массиве A и встречющиеся в массиве B:"); }

        if (TotalTime < 1000) { Console.WriteLine("\nВремя выполнения программы: {0} мсек", TotalTime); }
        else { Console.WriteLine("\nВремя выполнения программы: {0} сек", TotalTime / 1000); }
            
        Console.WriteLine("");
         Console.WriteLine("Для выхода из программы нажмите любую клавишу");
         Console.ReadKey();


        }
    }
}


