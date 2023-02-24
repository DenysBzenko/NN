using System;

class Program
{
    static void Main(string[] args)
    {
        // Зчитати вхідний рядок з консолі
        Console.Write("Введіть вираз: ");
        string input = Console.ReadLine();

        // Переведення в PRN
        string[] output = new string[input.Length];
        int outputIndex = 0;
        string[] operators = new string[input.Length];
        int operatorIndex = 0;
        string buffer = "";

        // Цикл по всім символам рядка
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                // Якщо символ - цифра, додаємо його до буфера
                buffer += c;
            }
            else if (c == ' ')
            {
                // Якщо символ - пробіл, перевіряємо, чи був попередній символ число
                if (buffer != "")
                {
                    output[outputIndex++] = buffer;
                    buffer = "";
                }
            }
            else if (c == '+' || c == '-' || c == '*' || c == '/')
            {
                // Якщо символ - оператор, перевіряємо, чи був попередній символ число
                if (buffer != "")
                {
                    output[outputIndex++] = buffer;
                    buffer = "";
                }

                // Додаємо оператор до стеку
                operators[operatorIndex++] = c.ToString();
            }
            else
            {
                // Якщо символ невідомий, видаємо повідомлення про помилку
                Console.WriteLine($"Невідомий символ: {c}");
                return;
            }
        }

        if (buffer != "")
        {
            output[outputIndex++] = buffer;
        }

        // Витягуємо залишки операторів зі стеку та додаємо їх до вихідної черги
        while (operatorIndex > 0)
        {
            output[outputIndex++] = operators[--operatorIndex];
        }

        // Обчислення значення виразу з токенів в інверсній польській нотації
        double result = 0;
        double op1, op2;
        string op;

        for (int i = 0; i < outputIndex; i++)
        {
            if (double.TryParse(output[i], out op1))
            {
                // Якщо токен - число, додаємо його до стеку
                operators[operatorIndex++] = output[i];
            }
            else
            {
                // Якщо токен - оператор, вилучаємо два операнди зі стеку та застосовуємо оператор до них
                op = output[i];
                op2 = double.Parse(operators[--operatorIndex]);
                op1 = double.Parse(operators[--operatorIndex]);

                switch (op)
                {
                    case "+":
                        result = op1 + op2;
                        break;
                    case "-":
                        result = op1 - op2;
                        break;
                    case "*":
                        result = op1 * op2;
                        break;
                    case "/":
                        result = op1 / op2;
                        break;
                    default:
                        Console.WriteLine($"Невідомий оператор: {op}");
                        return;
                }

                // Додаємо результат до стеку
                operators[operatorIndex++] = result.ToString();
            }
        }

        // Виводимо результат
        Console.WriteLine($"Результат: {result}");
    }
}