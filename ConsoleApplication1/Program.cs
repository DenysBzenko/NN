using System;
using System.Collections.Generic;

class Program {
    static void Main(string[] args) {
        // Зчитати вхідний рядок з консолі
        Console.Write("Введіть вираз: ");
        string input = Console.ReadLine();

        // Токенізація вхідного рядка
        string[] tokens = new string[input.Length];
        int tokenIndex = 0;
        string buffer = "";
        foreach (char c in input) {
            if (char.IsDigit(c)) {
                buffer += c;
            }
            else if (c == ' ') {
                if (buffer != "") {
                    tokens[tokenIndex++] = buffer;
                    buffer = "";
                }
            }
            else if (c == '+' || c == '-' || c == '*' || c == '/' || c == '(' || c == ')') {
                if (buffer != "") {
                    tokens[tokenIndex++] = buffer;
                    buffer = "";
                }
                tokens[tokenIndex++] = c.ToString();
            }
            else {
                Console.WriteLine($"Невідомий символ: {c}");
                return;
            }
        }
        if (buffer != "") {
            tokens[tokenIndex++] = buffer;
        }

        // Виведення токенів на екран
        Console.WriteLine("Токени:");
        for (int i = 0; i < tokenIndex; i++) {
            Console.WriteLine(tokens[i]);
        }
    }
}




