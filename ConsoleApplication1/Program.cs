using System;
using System.Collections.Generic;

partial class Program
{
    static void Main(string[] args)
    {
        // Зчитати вхідний рядок з консолі
        Console.Write("Введіть вираз: ");
        string input = Console.ReadLine();

        // Токенізація вхідного рядка
        string[] tokens = new string[input.Length];
        int tokenIndex = 0;
        string buffer = "";
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                buffer += c;
            }
            else if (c == ' ')
            {
                if (buffer != "")
                {
                    tokens[tokenIndex++] = buffer;
                    buffer = "";
                }
            }
            else if (c == '+' || c == '-' || c == '*' || c == '/' || c == '(' || c == ')')
            {
                if (buffer != "")
                {
                    tokens[tokenIndex++] = buffer;
                    buffer = "";
                }

                tokens[tokenIndex++] = c.ToString();
            }
            else
            {
                Console.WriteLine($"Невідомий символ: {c}");
                return;
            }
        }

        if (buffer != "")
        {
            tokens[tokenIndex++] = buffer;
        }

        RPN(tokens);
    }

    public static void RPN(string[] tokens)
    {
        Stack<string> operatorStack = new Stack<string>();
        List<string> output = new List<string>();
        // Проходження по токенам
        foreach (string token in tokens)
        {
            // Перевіряємо, чи є поточний токен оператором
            if (IsOperator(token))
            {
                // Якщо поточний оператор має менший або рівний пріорітет в порівнянні з тим, що зберігається в стеку, то викидаємо його зі стеку та додаємо до вихідного списку
                while (operatorStack.Count > 0 && IsOperator(operatorStack.Peek()) &&
                       GetPrecedence(token) <= GetPrecedence(operatorStack.Peek()))
                {
                    output.Add(operatorStack.Pop());
                }

                // Додаємо поточний оператор в стек
                operatorStack.Push(token);

            }
            else if (token == "(") //Якщо токен є відкриваючою дужкою, то він також потрапляє в стек operatorStack.
            {
                operatorStack.Push(token);
            }
            else if (token == ")") //Якщо токен є закриваючою дужкою, тоді потрібно вивести всі оператори від останньої відкриваючої дужки до першого оператора на вершині стеку operatorStack.
            {
                while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                {
                    output.Add(operatorStack.Pop());
                }

                if (operatorStack.Count == 0) //Якщо в стеку operatorStack немає відкриваючої дужки, то виводиться повідомлення про помилку та метод RPN завершується.
               

                {
                    Console.WriteLine("Помилка: не знайдено відкриваючої дужки");
                    return;
                }

                operatorStack.Pop();
            }
            else
            {
                output.Add(token);
            }
        }

        while (operatorStack.Count > 0)
        {
            if (operatorStack.Peek() == "(") ////Якщо в стеку operatorStack немає закриваючої дужки, то виводиться повідомлення про помилку та метод RPN завершується.
            {
                Console.WriteLine("Помилка: не знайдено закриваючої дужки");
                return;
            }

            output.Add(operatorStack.Pop());
        }

        // Виведення RPN на екран
        Console.WriteLine("RPN (зворотня польська нотація):");
        foreach (string token in output)
        {
            Console.Write(token + " ");
        }
    }

    static bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/";
    }

    static int GetPrecedence(string token)
    {
        switch (token)
        {
            case "+":
            case "-":
                return 1;
            case "*":
            case "/":
                return 2;
            default:
                return 0;
        }
    }
}


        
        