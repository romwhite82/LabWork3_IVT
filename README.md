# Лабораторная работа № 3 по теме "Обработка одномерных массивов"

## по дисциплине "Программирование на языках высокого уровня" для студентов специальности 220100 (ВМКСС)

### Вариант 11


#### Задание:

- Создать два массива A[N] и B[N]
- Инициализировать массивы случайными числами в диапазоне от 0 до 2^16
- Провести сортировку массивов алгоритмом "Пузырька"
- Найти элементы, которые повторяются в массиве A, но также встречаются и в массиве B
- Элементы, отвечающие предыдущему условию, записать в массив C в одном экземпляре
- Выполнить программу для N = 100, 1000, 5000, 10000, 15000 
- Замерить время выполнения программы
- При выполнении программы при N=100, вывести на экран содержимое массивов для проверки правильности работы программы

*В ходе тестирования программы было установлено, что при размерности массива 100 элементов, заполненного элементами от 0..16384, появление элементов, отвечающих условию задания маловероятно, было принято решение добавить в качестве параметра величину максимального элемента массива (принимается в качестве второго аргумента коммандной строки). Если значения находятся в диапазоне 0..500, элементы, отвечающие условию задания, встречаются в количестве нескольких штук*

#### Использование программы
LabWork3.exe [размерность массивов] [максимальный размер элемента массива]

Значения по умолчанию: Размерность массива 100;
                       Максимальный размер элемента массива: 16384;

#### Особенности устройства
1. В программе используются статичные методы, принадлежащие к тому же классу, что и главный метод программы:

* Randomize
  * Заполняет элементы массивов случайными числами, не превышающими MaxValue (по умолчанию 16384);
  * После заполнения первого массива, вызывается метод Threading.Sleep(500), поскольку генерация случайных чисел
происходит с использованием показания системных часов, велика вероятность одиновых значений в обоих массивах

* ShowArray
Принимает в качестве аргумента массив []int и текстовую строку
  * Если размерность массива больше 500, его содержимое на экран не выводится;
  * Если размерность массива меньше или равно 500, его содержимое выводится на экран;
Вывод содержимого массива в форматированном виде: после вывода 10 значений происходит перевод картеки на новую строку
Возвращаемое значение - void

* BubbleSort
Принимает в качестве аргумента массив []int, производит сортировку и возвращает сортированный массив []int

* LinearSearch
Ищет элементы, удовлетворяющие условию задания - которые повторяются в массиве A и встречаются в массиве B
Удовлетворяющие условию элементы записываются в массив С

2. В начале выполнения главного метода программа проверяет аргументы командной строки:
- если аргументов нет - размерность массива 100;
- в целях исключения возможных ошибок, проводится проверка аргумента командной строки на преобразование в int;
- если преобразование успешно - размерность массива n;
- если преобразование не удалось - программа завершается с ошибкой;

3. Поскольку значения, отвечающие условиям задания, должны быть записаны в массив, придётся делать его динамическим:
Изначально создаётся massiveC[0] с нулевой размерностью.
Если в ходе выполнения программы находится значение, отвечающее условиям, размер массива увеличивается на единицу и происходит
запись элемента:
```Array.Resize(ref massiveC, massiveC.Length + 1)```
