namespace Banco;
using static System.Console;

partial class Program
{
    public static void SectionTitle(string title)
    {
        ConsoleColor previouscolor = ForegroundColor;
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine("*");
        WriteLine($"* {title}");
        WriteLine("*");
        ForegroundColor = previouscolor;
    }

    public static void Fail(string message)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.Red;
        WriteLine($"Fail > {message}");
        ForegroundColor = previousColor;
    }

    public static void Info(string message)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine($"Info > {message}");
        ForegroundColor = previousColor;
    }

    public static string GenerarCURP(string nombre, string apellidoPaterno, string apellidoMaterno, DateTime fechaNacimiento)
{
    // Obtener las iniciales del nombre y apellidos
    string primeraLetraNombre = nombre.Substring(0, 1).ToUpper();
    string primeraLetraApellidoPaterno = apellidoPaterno.Substring(0, 2).ToUpper();
    string primeraLetraApellidoMaterno = apellidoMaterno.Substring(0, 1).ToUpper();

    // Obtener la fecha de nacimiento en formato YYMMdd
    string fechaNacimientoCurp = fechaNacimiento.ToString("yyMMdd");

    // Generar dos letras aleatorias para el primer carácter alfanumérico del CURP
    Random random = new Random();
    char[] caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    string caracteresAleatorios = string.Empty;
    for (int i = 0; i < 8; i++)
    {
        caracteresAleatorios += caracteres[random.Next(caracteres.Length)];
    }

    // Generar los demás caracteres alfanuméricos del CURP basados en el nombre, apellidos y fecha de nacimiento
    string curp = $"{primeraLetraApellidoPaterno}{primeraLetraApellidoMaterno}{primeraLetraNombre}{fechaNacimientoCurp}{caracteresAleatorios}";

    return curp.ToUpper();
}

}

//GenerarCliente()
    class Person
    {
        public string? first_name { get; set; }
        public string? middle_name { get; set; }
        public string? last_name { get; set; }
        public string? dob { get; set; }
        
    };