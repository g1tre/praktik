using System.Text;

namespace Lab06;

// ЛР №6, вариант 3 «Простые числа»:
// три функции IsPrime, NextPrime и PrimeRange + небольшое меню.
// Глобальных переменных нет: всё, что нужно, передаётся через параметры.
internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            ShowMenu();
            int choice = ReadInt("Ваш выбор", 0, 3);

            if (choice == 0)
            {
                Console.WriteLine("До свидания!");
                return;
            }

            switch (choice)
            {
                case 1:
                    int number = ReadInt("Введите число", 1, 1_000_000_000);
                    Console.WriteLine(IsPrime(number)
                        ? $"Число {number} — простое."
                        : $"Число {number} — составное (или единица).");
                    break;

                case 2:
                    int start = ReadInt("Введите число", 1, 1_000_000_000);
                    Console.WriteLine($"Следующее простое после {start}: {NextPrime(start)}");
                    break;

                case 3:
                    int rangeStart = ReadInt("Начало диапазона", 1, 1_000_000);
                    int rangeEnd = ReadInt("Конец диапазона", rangeStart, 1_000_000);
                    PrintPrimes(PrimeRange(rangeStart, rangeEnd), rangeStart, rangeEnd);
                    break;
            }

            Console.WriteLine();
        }
    }

    // ----------------------- Ввод -----------------------

    static void ShowMenu()
    {
        Console.WriteLine("=== Простые числа ===");
        Console.WriteLine("1 - Проверить, простое ли число");
        Console.WriteLine("2 - Найти следующее простое");
        Console.WriteLine("3 - Вывести простые из диапазона");
        Console.WriteLine("0 - Выход");
    }

    /// <summary>
    /// Спрашивает у пользователя целое число и не отстаёт, пока оно не окажется в нужных границах.
    /// </summary>
    /// <param name="prompt">Что показать перед вводом.</param>
    /// <param name="min">Наименьшее допустимое значение.</param>
    /// <param name="max">Наибольшее допустимое значение.</param>
    static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write($"{prompt} ({min}..{max}): ");

            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine("Не подходит, попробуйте ещё раз.");
        }
    }

    // --------------------- Обработка ---------------------

    /// <summary>Проверяет, является ли число простым.</summary>
    /// <param name="n">Проверяемое число.</param>
    /// <returns>true, если число простое.</returns>
    static bool IsPrime(int n)
    {
        if (n < 2) return false;
        if (n % 2 == 0) return n == 2; // из чётных простое только 2

        // Дальше проверяем только нечётные делители до корня из n
        for (int d = 3; (long)d * d <= n; d += 2)
        {
            if (n % d == 0) return false;
        }

        return true;
    }

    /// <summary>Находит ближайшее простое число, которое строго больше n.</summary>
    /// <param name="n">Число, после которого ищем.</param>
    static int NextPrime(int n)
    {
        int candidate = n + 1;
        while (!IsPrime(candidate))
        {
            candidate++;
        }

        return candidate;
    }

    /// <summary>Возвращает список всех простых чисел от first до last включительно.</summary>
    /// <param name="first">Начало диапазона.</param>
    /// <param name="last">Конец диапазона.</param>
    static List<int> PrimeRange(int first, int last)
    {
        var primes = new List<int>();

        for (int i = first; i <= last; i++)
        {
            if (IsPrime(i))
            {
                primes.Add(i);
            }
        }

        return primes;
    }

    // ---------------------- Вывод ----------------------

    static void PrintPrimes(List<int> primes, int first, int last)
    {
        if (primes.Count == 0)
        {
            Console.WriteLine($"В диапазоне от {first} до {last} простых чисел нет.");
            return;
        }

        Console.WriteLine($"Простые числа от {first} до {last} ({primes.Count} шт.):");
        Console.WriteLine(string.Join(" ", primes));
    }
}
