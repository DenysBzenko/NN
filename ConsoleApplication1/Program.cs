using System;
using System.Collections.Generic;

class RPNCalculator
{
    static bool IsOperator(string token)
    {
        return (token == "+" || token == "-" || token == "*" || token == "/" || token == "^");
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
            case "^":
                return 3;
            default:
                return 0;
        }
    }

    static void Main()
    {
        Console.WriteLine("Enter an expression: ");
        string input = Console.ReadLine();
        string[] tokens = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        string[] output = new string[tokens.Length];
        int outputIndex = 0;
        string[] stack = new string[tokens.Length];
        int stackIndex = 0;

        foreach (string token in tokens)
        {
            if (IsOperator(token))
            {
                while (stackIndex > 0 && IsOperator(stack[stackIndex - 1]) && GetPrecedence(stack[stackIndex - 1]) >= GetPrecedence(token))
                {
                    output[outputIndex++] = stack[--stackIndex];
                }
                stack[stackIndex++] = token;
            }
            else if (token == "(")
            {
                stack[stackIndex++] = token;
            }
            else if (token == ")")
            {
                while (stackIndex > 0 && stack[stackIndex - 1] != "(")
                {
                    output[outputIndex++] = stack[--stackIndex];
                }
                if (stackIndex > 0 && stack[stackIndex - 1] == "(")
                {
                    stackIndex--; // remove the "("
                }
            }
            else // number
            {
                output[outputIndex++] = token;
            }
        }

        while (stackIndex > 0)
        {
            output[outputIndex++] = stack[--stackIndex];
        }

        Array.Resize(ref output, outputIndex);

        Console.WriteLine("RPN: " + string.Join(" ", output));
        
            // Обчислення значення виразу з токенів в інверсній польській нотації
        double result = 0;
        double op1, op2;
        string op;

        for (int i = 0; i < outputIndex; i++)
        {
            if (double.TryParse(output[i], out op1))
            {
                // Якщо токен - число, додаємо його до стеку
                stack[stackIndex++] = output[i];
            }
            else
            {
                // Якщо токен оператор, вилучаємо два операнди зі стеку та застосовуємо оператор до них
                op = output[i];
                op2 = double.Parse(stack[--stackIndex]);
                op1 = double.Parse(stack[--stackIndex]);
                
                
                
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
                    case "^":
                        result = Math.Pow(op1, op2);
                        break;
                    default:
                        Console.WriteLine($"Невідомий оператор: {op}");
                        return;
                }

                // Додаємо результат до стеку
                stack[stackIndex++] = result.ToString();
            }
        }

        // Виводимо результат
        Console.WriteLine($"Результат: {result}");
    }
}