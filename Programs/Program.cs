using static System.Console;
namespace Banco;

partial class Program
{
    private static void Main(string[] args)
    {
        var resultAdd = Usuario.AddUsuario(nombre: "Luis", apellido:"Martinez", DoB:"27-08-2004");
            if(resultAdd.affected == 1)
            {
                WriteLine($"Added product successful with ID {resultAdd.UserId}.");
            }
            Usuario.ListUsuario(productIdToHighlight: new int?[] {(int)resultAdd.UserId});

    }
}   