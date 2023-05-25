using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using static System.Console;
namespace Banco;

partial class Program
{
    public static string quitarAcentos(string palabra)
    {
        string palabraNormalizada = palabra.Normalize(NormalizationForm.FormD);
        StringBuilder sb = new StringBuilder();

        foreach (char c in palabraNormalizada)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

}