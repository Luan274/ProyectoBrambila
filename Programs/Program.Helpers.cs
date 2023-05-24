namespace Banco;
using static System.Console;

partial class Program
{
    static void SectionTitle(string title)
    {
        ConsoleColor previouscolor = ForegroundColor;
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine("*");
        WriteLine($"* {title}");
        WriteLine("*");
        ForegroundColor = previouscolor;
    }

    static void Fail(string message)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.Red;
        WriteLine($"Fail > {message}");
        ForegroundColor = previousColor;
    }

    static void Info(string message)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine($"Info > {message}");
        ForegroundColor = previousColor;
    }
}