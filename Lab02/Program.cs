using System.Text;

namespace Lab02;

// ЛР №2, вариант 3 «Месяц»:
// по номеру месяца определяем время года и количество дней.
internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        string[] monthNames =
        {
            "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
            "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };

        Console.Write("Введите номер месяца (1-12): ");
        if (!int.TryParse(Console.ReadLine(), out int month) || month < 1 || month > 12)
        {
            Console.WriteLine("Нужно ввести целое число от 1 до 12.");
            return;
        }

        // Время года
        string season;
        switch (month)
        {
            case 12:
            case 1:
            case 2:
                season = "зима";
                break;
            case 3:
            case 4:
            case 5:
                season = "весна";
                break;
            case 6:
            case 7:
            case 8:
                season = "лето";
                break;
            default: // остаются 9, 10 и 11
                season = "осень";
                break;
        }

        // Количество дней
        int days;
        switch (month)
        {
            case 2:
                // С февралём всё сложнее: нужно знать, високосный ли год
                Console.Write("Введите год (нужен для февраля): ");
                if (!int.TryParse(Console.ReadLine(), out int year) || year < 1 || year > 9999)
                {
                    Console.WriteLine("Год должен быть целым числом от 1 до 9999.");
                    return;
                }
                days = DateTime.IsLeapYear(year) ? 29 : 28;
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                days = 30;
                break;
            default:
                days = 31;
                break;
        }

        // "31 день", но "30 дней" - небольшая забота о русском языке
        string dayWord = days == 31 ? "день" : "дней";
        Console.WriteLine($"{monthNames[month - 1]} — {season}, {days} {dayWord}.");
    }
}
