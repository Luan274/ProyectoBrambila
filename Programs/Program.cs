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
        // Cliente.ListCliente(clienteIdToHighlight: new int?[] {(int)resultAdd.clienteId});
        Usuario.ListUsuario();
        WriteLine();
        Cliente.ListCliente();
        WriteLine();
        // var resultAdd = Empleado.addEmpleado(fecEntrada: "20/05/2023", Usuario: 1);
        // if(resultAdd.affected == 1){
        //     WriteLine($"Added employee succesful wiht nomina {resultAdd.nomina}");
        // }
        // Empleado.ListEmpleado(IdToHighlight: new int?[] {(int)resultAdd.nomina});

        var resultAdd = Boleto.add(cliente: 1, fecha: "25-05-2023");
        if(resultAdd.affected == 1){
            WriteLine($"Added Ticket succesful wiht # {resultAdd.ticket}");
        }
        Boleto.List(IdToHighlight: new int?[] {(int)resultAdd.ticket});

    }
}   