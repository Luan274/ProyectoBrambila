using Newtonsoft.Json;
using static System.Console;
namespace Banco;
using FastJson = System.Text.Json.JsonSerializer;


partial class Program
{
    private static async Task Main(string[] args)
    {
        Clear();
        await Serializadores.InyectarGerentes(1, 5, @"../Programs/JSON/Gerentes.json");

    }
}   