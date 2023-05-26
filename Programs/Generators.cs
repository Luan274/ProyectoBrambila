namespace Banco;
using static System.Console;


class Generators
{
    public static (bool sucess, long id) GenerarCliente(string fechaNac, string curp, string nombre, string apellidop, string apellidom ){
        var user = Usuario.AddUsuario(nombre: nombre, apellido: apellidop);
        if(user.affected == 0) return (false, 0);
        DateOnly d;
        if(!DateOnly.TryParse(fechaNac, out d)) return (false, 0);
        if(!Validaciones.curpValida(curp, nombre, apellidop, apellidom, d)) return (false, 0);
        var cliente = Cliente.addCliente(user.UserId, fechaNac, curp, DateTime.Now);
        if(cliente.affected == 0) return (false, 0);
        return (true, cliente.clienteId);
    }

    public static (bool sucess, long id) GenerarEmpleado(string nombre, string apellido, string fecEntrada, string fechaNac){
        var user = Usuario.AddUsuario(nombre: nombre, apellido: apellido, fechaNac);
        if(user.affected == 0) return (false, 0);
        var emp = Empleado.addEmpleado(fecEntrada, user.UserId);
        if(emp.affected == 0) return (false, 0);
        return (true, emp.nomina);
    }

    public static (bool sucess, long id) GenerarGerente(string nombre, string curp, string apellidop, string apellidom , string fecEntrada, string fechaNac){
        var user = Usuario.AddUsuario(nombre: nombre, apellido: apellidop, fechaNac);
        if(user.affected == 0) return (false, 0);
        DateOnly d;
        if(!DateOnly.TryParse(fechaNac, out d)) return (false, 0);
        if(!Validaciones.curpValida(curp, nombre, apellidom, apellidop, d)) return (false, 0);
        var cliente = Cliente.addCliente(user.UserId, fechaNac, curp, DateTime.Now);
        if(cliente.affected == 0) return (false, 0);
        var emp = Empleado.addEmpleado(fecEntrada, user.UserId);
        if(emp.affected == 0) return (false, 0);
        var ger = Gerente.add(emp.nomina, cliente.clienteId);
        if(ger.affected == 0) return (false, 0);
        return (true, ger.gerenteID);
    }
}