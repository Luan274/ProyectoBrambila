﻿using static System.Console;
namespace Banco;

partial class Program
{
    private static void Main(string[] args)
    {
        // var resultAdd = Usuario.AddUsuario(nombre: "Andre", apellido:"Mejia", DoB:"27-08-2004");
        //     if(resultAdd.affected == 1)
        //     {
        //         WriteLine($"Added product successful with ID {resultAdd.UserId}.");
        //     }
        //     Usuario.ListUsuario(userIdToHighlight: new int?[] {(int)resultAdd.UserId});
        // var resultAdd = Cliente.addCliente(userID: 4, fechaNacimiento: "13-07-2004", curp: "MAGL040827PFVRNEO0",
        //  horaLogin: DateTime.Now);
        //     if(resultAdd.affected == 1)
        //     {
        //         WriteLine($"Added Client successful with ID {resultAdd.clienteId}.");
        //     }
            Cliente.ListCliente(clienteIdToHighlight: new int?[] {(int)1});

    }
}   