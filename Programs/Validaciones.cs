using System.Text.RegularExpressions;
namespace Banco;

class Validaciones
{
    public static bool ContieneNumeros(string cadena)
    {
        Regex regex = new Regex(@"\d"); // Expresión regular que busca dígitos numéricos
        MatchCollection matches = regex.Matches(cadena);
        return matches.Count == 0;
    }


}
