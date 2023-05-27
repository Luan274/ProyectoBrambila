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

    public static (bool val, long? user) VerificarCredenciales(string? usuario, string? contraseña)
    {
        using (Bank db = new())
        {
            if (db.Usuarios is null) return (false, 0);

            var usuarioExistente = db.Usuarios.FirstOrDefault(u => u.Usuario1 == usuario && u.Contrasena == contraseña);
            if(usuarioExistente == null) return (false, 0);
            return (true, usuarioExistente.UserId);
        }
    }

    public static (string tipo, long id) ObtenerTipoUsuario(long? userId)
    {
        using (Bank db = new())
        {
            if (db.Empleados is null || db.Clientes is null || db.Gerentes is null) return ("Vacio", 0);
                
            var esCliente = db.Clientes.FirstOrDefault(c => c.UserId == userId);
            var esEmpleado = db.Empleados.FirstOrDefault(f => f.UsuarioId == userId);
            if(esEmpleado != null){
                var esGerente = db.Gerentes.FirstOrDefault(g => g.EmpleadoId == esEmpleado.Nomina);
                if (esGerente != null) return ("Gerente", esGerente.GerenteId);
            }

            if (esCliente != null) return ("Cliente", esCliente.ClienteId);
            if (esEmpleado != null) return ("Empleado", esEmpleado.Nomina);

            return ("Invalido", 0);
        }
    }

}
