using System.Text;

namespace Lab05;

// ЛР №5, вариант 3 «Замена»:
// заменяем в строке подстроку a на подстроку b и считаем, сколько раз это сделали.
internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.Write("Введите текст: ");
        string text = Console.ReadLine() ?? "";
        if (text.Length == 0)
        {
            Console.WriteLine("Строка пустая, менять нечего.");
            return;
        }

        Console.Write("Что заменить: ");
        string oldPart = Console.ReadLine() ?? "";
        if (oldPart.Length == 0)
        {
            // Пустую подстроку "найти" можно где угодно, так что такой случай запрещаем
            Console.WriteLine("Искомая подстрока не должна быть пустой.");
            return;
        }

        Console.Write("На что заменить: ");
        string newPart = Console.ReadLine() ?? "";

        string result = ReplaceAndCount(text, oldPart, newPart, out int count);

        Console.WriteLine("Результат: " + result);
        Console.WriteLine($"Сделано замен: {count}");
    }

    // Заменяет все вхождения oldPart на newPart и отдаёт количество замен через count.
    // Регистр учитывается: "Мама" и "мама" - это разные подстроки.
    static string ReplaceAndCount(string text, string oldPart, string newPart, out int count)
    {
        count = 0;
        var builder = new StringBuilder();
        int position = 0; // с этого места ищем дальше

        while (true)
        {
            int found = text.IndexOf(oldPart, position, StringComparison.Ordinal);

            if (found < 0)
            {
                // Больше вхождений нет - дописываем хвост и выходим
                builder.Append(text, position, text.Length - position);
                break;
            }

            // Кусок до найденного места копируем как есть, потом вставляем замену
            builder.Append(text, position, found - position);
            builder.Append(newPart);

            position = found + oldPart.Length;
            count++;
        }

        return builder.ToString();
    }
}
