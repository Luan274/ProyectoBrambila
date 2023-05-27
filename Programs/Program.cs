﻿using Newtonsoft.Json;
using static System.Console;
namespace Banco;
using FastJson = System.Text.Json.JsonSerializer;


partial class Program
{
    private static async Task Main(string[] args)
    {
        Clear();
        //await Serializadores.InyectarGerentes(1, 5, @"../Programs/JSON/Gerentes.json");
        // bool a = true;
        // for(int i = 56; i < 59; i++){
        // a = !(i % 5 == 0);
        // var apr = Usuario.AprovarUsuario(i, a);
        // Joins.JoinClientUser(i);
        // }
        Write("Ingresa tu usuario: ");
        string? user = ReadLine();
        Write("Ingresa tu contraseña: ");
        string? pass = ReadLine();
        var a = Validaciones.VerificarCredenciales(user, pass);
        var tipo = Validaciones.ObtenerTipoUsuario(a.user);
        WriteLine(tipo.tipo + " " + tipo.id);
        switch(tipo.tipo){
            case "Gerente":
            Joins.JoinGerEmpCliUser(tipo.id);
            break;
            case "Empleado":
            Joins.JoinEmpUser(tipo.id);
            break;
            case "Cliente":
            Joins.JoinClientUser(tipo.id);
            break;
        }

    }
}   