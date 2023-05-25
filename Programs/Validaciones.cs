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

    public static bool curpValida(string curp, string nombre, string apellidoP, string apellidoM, DateOnly fechaNac){
        
        nombre = Program.quitarAcentos(nombre);
        apellidoP = Program.quitarAcentos(apellidoP);
        apellidoM = Program.quitarAcentos(apellidoM);
        if (curp.Length != 18){
            return false;
        }
        if (!curp.StartsWith(apellidoP.ToUpper().Substring(0,2)) ||
            !curp.Contains(apellidoM.ToUpper().Substring(0, 1)) ||
            !curp.Contains(nombre.ToUpper().Substring(0, 1))){
                return false;
        }

        string curpFecha = curp.Substring(4, 6);
        string fechaNacimientoCurp = fechaNac.ToString("yyMMdd");
        if (curpFecha != fechaNacimientoCurp)
        {
            return false;
        }

        return true;
    }
}
