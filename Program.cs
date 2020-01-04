using System;
using System.Threading;

namespace LabWork3_Final
{
    class Program
    {
        //Метод, который выводит на экран содержимое массива
        //в форматированном виде
        static void ShowArray(int[] massive, string message)
        {
            //Если размер массива меньше 500 элементов, его содержимое выводится на экран
            if (massive.Length <= 500) 
            {
                Console.WriteLine(message);
                for (int i = 0; i < massive.Length; i++)
                {
                    //после вывода десяти элементов массива - перенос на новую строку
                    if (i > 0 && i % 10 == 0) Console.Write("\n");

                    //Добавляем разделители и табуляцию, чтобы красиво выглядело
                    Console.Write("|" + massive[i] + "|\t"); 
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\nПрограмма выполняется. Массивы слишком велики для вывода на экран!");
            }
        }

        //Метод заполнения двух массивов случайными числами
        //В диапазоне 0..MaxValue
        static void Randomize (ref int[] massiveA, ref int[] massiveB, int MaxValue)
        {
            Random rnd = new Random();
            for (int i = 0; i < massiveA.Length; i++) massiveA[i] = rnd.Next(0, MaxValue);
            
            //Ждём 500мсек, чтобы при заполнении второго массива 
            //изменилось системное время. Иначе значения массивов будут одинаковыми
            
            Thread.Sleep(500); 
            for (int i = 0; i < massiveB.Length; i++) massiveB[i] = rnd.Next(0, MaxValue);
        }

        //Метод сортировки массива пузырьком
        //На входе - неупорядоченный массив
        //На выходе - отсортированный!
        static int[] BubbleSort(int[] massive)
        {
            for (int k = 0; k < massive.Length; k++)
            {
                for (int i = 0; i < massive.Length - 1; i++)
                {
                    // Последовательно сравниваем элементы 
                    // Больший элемент постепенно движется к концу массива

                    if (massive[i] > massive[i + 1]) 
                    {
                        //Временная переменная для обмена большего элемента с меньшим 
                        int Temp;   
                        Temp = massive[i + 1]; 
                        massive[i + 1] = massive[i];
                        massive[i] = Temp;
                    }
                }
            }
            return massive;
        }

        static int[] LinearSearch (int[] massiveA, int[] massiveB)
        {
            //Для хранения значения повторяющегося элемента
            int Temp = 0;
            //Для определения позиции, куда записать значение в итоговом массиве C

            //Для определения индекса записи значения в массив C
            int Counter = 0; 
            int[] massiveC = new int[0];

            //для поиска повторяющихся элементов, последовательно обходим все элементы
            for (int i = 0; i < massiveA.Length - 1; i++) 
            {
                //если следующий элемент равен предыдущему
                // и не равен предыдщуму найденному значению
                if (massiveA[i] == massiveA[i + 1] && massiveA[i] != Temp) 
                {
                    //Записываем его в переменную Temp 
                    Temp = massiveA[i];                                   
                                        
                    for (int k = 0; k < massiveB.Length; k++)              
                    {
                        //Для каждого повторяющегося элемента проверяем его наличие во втором массиве
                        if (massiveB[k] == Temp)
                        {
                            //Если совпадение найдено, увеличиваем размер массива C на единицу
                            Array.Resize(ref massiveC, massiveC.Length + 1);
                            massiveC[Counter] = Temp; 
                            Counter++;  //Увеличиваем счётчик на единицу
                           //Поскольку нам надо записать только одно значение
                           //дальнейший поиск не целесообразен. Выходим из цикла
                            break;      
                        }               
                    }
                }
            }
            return massiveC;
        }
        static void Main(string[] args)
        {
            // Лабораторная работа № 3 "Обработка одномерных массивов"
            // Вариант 11
            // Найти: Повторяющиеся элементы массива А, которые есть в массиве B

            //Размерность массивов определяется аргументом коммандной строки
            // Если аргумент не задан (равен нулю), по умолчанию 100

            //Описание программы:

            Console.WriteLine();
            Console.WriteLine("Лабораторная работа 3 по теме \"Обработка одномерных массивов\"");
            Console.WriteLine("по предмету \"Программирование на языках высокого уровня\"");
            Console.WriteLine("\nСоздаются два массива заданной размерности (по умолчанию 100)");
            Console.WriteLine("Массивы сортируются Пузырьком. Далее происходит поиск элементов");
            Console.WriteLine("которые повторяются в первом массиве и встречаются во втором");
            Console.WriteLine("Значения, отвечающие данным условиям, записываются в третий массив.");
            Console.WriteLine("\nВыполнил студент: Карандашёв Роман\n");
            Console.WriteLine("Исходный код: https://github.com/romwhite82/LabWork3_IVT");
            Console.WriteLine("\nИспользование программы: LabWork3.exe [размер массива] [значение максимального элемента]\n");


            int n=100; //размерность массива
            int MaxValue=16384; //максимальное значение ячейки массива

            if (args.Length == 0) //размерность определяется первым аргументом коммандной строки
            //если аргументов нет, размерность по умолчанию равен 100
            //а максимальное значение ячейки 2^14 
            { n = 100; MaxValue = 16384; }            
            
            if (args.Length == 1)
            {
                try //Обработка исключения. Если первый аргумент командной строки не int
                { n = Int32.Parse(args[0]);
                    
                } 
                catch { Console.WriteLine("Неправильный аргумент!"); //Происходит выход из программы с ошибкой
                Environment.Exit(1);
                }
                finally { n = Int32.Parse(args[0]); MaxValue = 16384; }
            }

            if (args.Length==2)
            {
                try
                {
                    n = Int32.Parse(args[0]);
                    MaxValue = Int32.Parse(args[1]);
                }
                catch {
                    Console.WriteLine("Ошибка аргументов!");
                    Environment.Exit(1);
                            }
                finally  { n = Int32.Parse(args[0]); MaxValue = Int32.Parse(args[1]); }
            }

            Console.WriteLine("Размерность массивов A и B: {0}", n);
            Console.WriteLine("Максимальное значение элемента массивов: {0}\n", MaxValue);

            int[] massiveA = new int[n];
            int[] massiveB = new int[n];


            // В этот массив будут записываться повторяющиеся элементы
            // Поскольку неизвестно, сколько будет этих элементов,
            // Придётся каким-то образом превратить этот массив
            // в динамический))

            int[] massiveC = new int[0];

            //Передаём методу значения массивов по ссылке
            //Чтобы преобразования происходили с областью памяти
            //на которую ссылается перемена массива, а не с копией

            Randomize(ref massiveA, ref massiveB, MaxValue);

            //Выводим массивы A и B на экран, с использованием метода ShowArray
            ShowArray(massiveA, "Массив А до сортировки:");
            ShowArray(massiveB, "\nМассив B до сортировки");

            //Засекаем время выполнения программы
            int t1 = Environment.TickCount;
            
            //Сортируем массивы и пытаемся вывести их на экран
            
            massiveA = BubbleSort(massiveA);
            ShowArray(massiveA, "\nМассив A после сортировки: ");
                        
            massiveB = BubbleSort(massiveB);
            ShowArray(massiveB, "\nМассив B после сортировки: ");

            //Ищем элементы, отвечающие условию задания с помощью
            //метода LinearSearch. Присваиваем результат massiveC
            massiveC = LinearSearch(massiveA, massiveB);


            //Отсановка замера времени. Сохраняем состояние
            int t2 = Environment.TickCount;
            int TotalTime = t2 - t1;
           
            if (massiveC.Length == 0)
            {
                Console.WriteLine("\nЭлементов, отвечающих условию задания нет.");
            }
            else
                { ShowArray(massiveC, "\nЭлементы, повторяющиеся в массиве A и встречющиеся в массиве B:"); }

            //Если время выполнения программы меньше 10000 мсек, 
            //выводим значение в миллисекундах, если больше - в секундах

            if (TotalTime < 10000) 
            { Console.WriteLine("\nВремя выполнения программы: {0} мсек", TotalTime); }
        else 
            { Console.WriteLine("\nВремя выполнения программы: {0} сек", TotalTime / 1000); }
            
        Console.WriteLine("");
        Console.WriteLine("Для выхода из программы нажмите любую клавишу");
        Console.ReadKey();

        }
    }
}


