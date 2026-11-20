using System.Text;

namespace Lab04;

// ЛР №4, вариант 3 «Подсчёт»:
// считаем положительные, отрицательные и нулевые элементы
// и меняем местами минимальный и максимальный элементы массива.
internal class Program
{
    const int Size = 10;

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Заполняем массив случайными числами от -10 до 10
        var random = new Random();
        int[] array = new int[Size];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(-10, 11);
        }

        Console.WriteLine("Исходный массив: " + string.Join(" ", array));

        // Считаем, сколько чисел каждого вида
        int positive = 0, negative = 0, zeros = 0;
        foreach (int x in array)
        {
            if (x > 0) positive++;
            else if (x < 0) negative++;
            else zeros++;
        }
        Console.WriteLine($"Положительных: {positive}, отрицательных: {negative}, нулей: {zeros}");

        // Ищем индексы минимума и максимума.
        // Если одинаковых значений несколько, берём самое первое.
        int minIndex = 0;
        int maxIndex = 0;
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] < array[minIndex]) minIndex = i;
            if (array[i] > array[maxIndex]) maxIndex = i;
        }
        Console.WriteLine($"Минимум: {array[minIndex]} (индекс {minIndex}), " +
                          $"максимум: {array[maxIndex]} (индекс {maxIndex})");

        // Меняем их местами. Через кортеж не нужна временная переменная.
        (array[minIndex], array[maxIndex]) = (array[maxIndex], array[minIndex]);

        Console.WriteLine("Массив после обмена: " + string.Join(" ", array));
    }
}
