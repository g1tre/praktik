                                                                                  using System.Globalization;
using System.Text;

namespace Lab01;

// ЛР №1, вариант 3 «Температура»:
// переводим температуру из градусов Цельсия в Фаренгейты и Кельвины.
internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.Write("Введите температуру в °C: ");
        double celsius = ReadDouble();

        // Ниже абсолютного нуля температура не бывает, поэтому сразу проверим
        if (celsius < -273.15)
        {
            Console.WriteLine("Так не бывает: ниже -273,15 °C температуры не существует.");
            return;
        }

        // Формулы из школьного учебника физики
        double fahrenheit = celsius * 9.0 / 5.0 + 32;
        double kelvin = celsius + 273.15;

        Console.WriteLine();
        Console.WriteLine($"{celsius:F2} °C = {fahrenheit:F2} °F");
        Console.WriteLine($"{celsius:F2} °C = {kelvin:F2} K");
    }

    // Читает число с клавиатуры. Принимает и запятую, и точку,
    // а если ввели ерунду - просто спрашивает ещё раз.
    static double ReadDouble()
    {
        while (true)
        {
            string text = (Console.ReadLine() ?? "").Trim().Replace(',', '.');

            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                return value;
            }

            Console.Write("Это не число, попробуйте ещё раз: ");
        }
    }
}
