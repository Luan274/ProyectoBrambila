namespace Banco;
using static System.Console;


class Generators
{
    public static (bool sucess, long id) GenerarCliente(string fechaNac, string curp, string nombre, string apellidop, string apellidom ){
        var user = Usuario.AddUsuario(DoB: fechaNac, nombre: nombre, apellido: apellidop);
        if(user.affected == 0) return (false, 0);
        DateOnly d;
        if(!DateOnly.TryParse(fechaNac, out d)) return (false, 0);
        if(!Validaciones.curpValida(curp, nombre, apellidop, apellidom, d)) return (false, 0);
        var cliente = Cliente.addCliente(user.UserId, fechaNac, curp, DateTime.Now);
        if(cliente.affected == 0) return (false, 0);
        return (true, cliente.clienteId);
    }
}