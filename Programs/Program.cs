﻿using static System.Console;
namespace Banco;

partial class Program
{
    private static void Main(string[] args)
    {
        var resultAdd = Usuario.AddUsuario(nombre: "Andre", apellido:"Mejia", DoB:"27-08-2004");
            if(resultAdd.affected == 1)
            {
                WriteLine($"Added product successful with ID {resultAdd.UserId}.");
            }
            Usuario.ListUsuario(userIdToHighlight: new int?[] {(int)resultAdd.UserId});

    }
}   