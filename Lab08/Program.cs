using System.Text;

namespace Lab08;

// ЛР №8, вариант 3 «Сотрудники»:
// базовый класс Employee и три наследника - Manager, Developer, Designer.
// У каждого своя формула зарплаты, а вызываем мы их одинаково (полиморфизм).
internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Один список, в котором лежат сотрудники разных видов
        var staff = new List<Employee>
        {
            new Manager("Алексей", 80000, teamSize: 5),
            new Developer("Мария", 90000, overtimeHours: 10),
            new Designer("Олег", 60000, projectsDone: 3)
        };

        decimal total = 0;
        foreach (Employee employee in staff)
        {
            // Какую именно формулу применить, C# решает сам по типу объекта
            Console.WriteLine(employee.Info());
            total += employee.CalculateSalary();
        }

        Console.WriteLine($"Итого фонд зарплат: {total:F2} руб.");
    }
}

/// <summary>Общий предок всех сотрудников.</summary>
public abstract class Employee
{
    public string Name { get; }
    public decimal BaseSalary { get; }

    // Название должности каждый наследник задаёт сам
    public abstract string Position { get; }

    protected Employee(string name, decimal baseSalary)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Имя не может быть пустым.");
        }
        if (baseSalary < 0)
        {
            throw new ArgumentException("Оклад не может быть отрицательным.");
        }

        Name = name;
        BaseSalary = baseSalary;
    }

    /// <summary>Считает итоговую зарплату. У каждой должности свои правила.</summary>
    public abstract decimal CalculateSalary();

    /// <summary>Короткая строка с информацией о сотруднике.</summary>
    public virtual string Info()
    {
        return $"{Position} {Name}: {CalculateSalary():F2} руб.";
    }
}

/// <summary>Менеджер: оклад плюс надбавка за каждого человека в команде.</summary>
public class Manager : Employee
{
    private const decimal BonusPerPerson = 3000m;

    public int TeamSize { get; }

    public override string Position => "Менеджер";

    public Manager(string name, decimal baseSalary, int teamSize) : base(name, baseSalary)
    {
        if (teamSize < 0)
        {
            throw new ArgumentException("Размер команды не может быть отрицательным.");
        }
        TeamSize = teamSize;
    }

    public override decimal CalculateSalary() => BaseSalary + BonusPerPerson * TeamSize;
}

/// <summary>Разработчик: оклад плюс оплата сверхурочных часов.</summary>
public class Developer : Employee
{
    private const decimal PayPerOvertimeHour = 1200m;

    public int OvertimeHours { get; }

    public override string Position => "Разработчик";

    public Developer(string name, decimal baseSalary, int overtimeHours) : base(name, baseSalary)
    {
        if (overtimeHours < 0)
        {
            throw new ArgumentException("Количество часов не может быть отрицательным.");
        }
        OvertimeHours = overtimeHours;
    }

    public override decimal CalculateSalary() => BaseSalary + PayPerOvertimeHour * OvertimeHours;
}

/// <summary>Дизайнер: оклад плюс премия за каждый сданный проект.</summary>
public class Designer : Employee
{
    private const decimal BonusPerProject = 4000m;

    public int ProjectsDone { get; }

    public override string Position => "Дизайнер";

    public Designer(string name, decimal baseSalary, int projectsDone) : base(name, baseSalary)
    {
        if (projectsDone < 0)
        {
            throw new ArgumentException("Количество проектов не может быть отрицательным.");
        }
        ProjectsDone = projectsDone;
    }

    public override decimal CalculateSalary() => BaseSalary + BonusPerProject * ProjectsDone;
}
