using System.Text;

namespace Lab07;

// ЛР №7, вариант 3 «BankAccount»:
// класс банковского счёта с пополнением, снятием и переводом.
internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var ivan = new BankAccount("40817-001", "Иван Петров", 1000);
        var anna = new BankAccount("40817-002", "Анна Соколова", 100);

        Console.WriteLine(ivan);
        Console.WriteLine(anna);
        Console.WriteLine();

        try
        {
            Console.WriteLine("Пополняем счёт Ивана на 500...");
            ivan.Deposit(500);
            Console.WriteLine(ivan);

            Console.WriteLine("Снимаем 200...");
            ivan.Withdraw(200);
            Console.WriteLine(ivan);

            Console.WriteLine("Переводим Анне 300...");
            ivan.TransferTo(anna, 300);
            Console.WriteLine(ivan);
            Console.WriteLine(anna);

            // А вот это уже не получится: денег не хватает
            Console.WriteLine("Пробуем снять 5000...");
            ivan.Withdraw(5000);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Неверные данные: " + ex.Message);
        }
    }
}

/// <summary>Банковский счёт: номер, владелец и баланс.</summary>
public class BankAccount
{
    // Поля закрыты, снаружи с ними можно работать только через свойства и методы
    private string _number = "";
    private string _owner = "";
    private decimal _balance;

    /// <summary>Создаёт счёт.</summary>
    /// <param name="number">Номер счёта.</param>
    /// <param name="owner">ФИО владельца.</param>
    /// <param name="startBalance">Начальный баланс (по умолчанию 0).</param>
    public BankAccount(string number, string owner, decimal startBalance = 0)
    {
        Number = number;
        Owner = owner;

        if (startBalance < 0)
        {
            throw new ArgumentException("Начальный баланс не может быть отрицательным.");
        }
        _balance = startBalance;
    }

    public string Number
    {
        get => _number;
        private set // номер после создания менять нельзя
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Номер счёта не может быть пустым.");
            }
            _number = value;
        }
    }

    public string Owner
    {
        get => _owner;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Имя владельца не может быть пустым.");
            }
            _owner = value;
        }
    }

    // Баланс можно только читать: менять его надо через Deposit/Withdraw/TransferTo
    public decimal Balance => _balance;

    /// <summary>Кладёт деньги на счёт.</summary>
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сумма пополнения должна быть больше нуля.");
        }
        _balance += amount;
    }

    /// <summary>Снимает деньги со счёта. Если денег не хватает - бросает исключение.</summary>
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сумма снятия должна быть больше нуля.");
        }
        if (amount > _balance)
        {
            throw new InvalidOperationException(
                $"Недостаточно средств: на счёте {_balance:F2}, а нужно {amount:F2}.");
        }
        _balance -= amount;
    }

    /// <summary>Переводит деньги на другой счёт.</summary>
    public void TransferTo(BankAccount target, decimal amount)
    {
        if (target == null)
        {
            throw new ArgumentException("Не указан счёт получателя.");
        }
        if (ReferenceEquals(target, this))
        {
            throw new ArgumentException("Нельзя перевести деньги самому себе.");
        }

        // Сначала снимаем: если денег не хватит, исключение вылетит
        // до того, как получатель что-то получит
        Withdraw(amount);
        target.Deposit(amount);
    }

    public override string ToString()
    {
        return $"Счёт {Number}, владелец: {Owner}, баланс: {Balance:F2} руб.";
    }
}
