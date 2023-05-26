namespace Banco;

class Joins{
    public static void JoinClientUser(long ID)
    {
        using(Bank db = new())
        {
            // Join every product to list of categories
            var queryJoin = db.Clientes!.Join(
                inner: db.Usuarios!,
                outerKeySelector: cliente => cliente.UserId, // ON Categories.Id = Products.Id
                innerKeySelector: usuario => usuario.UserId,
                resultSelector: (c, u) => new { c.ClienteId  , u.UserId, u.Nombre, u.Apellido, u.Usuario1, u.Contrasena, c.FechaDeNac, c.Curp, c.Comportamiento, c.Aprovado, c.Saldo, c.HoraLogin, c.IntentosFallidos}
            ).Where(item => item.ClienteId == ID).FirstOrDefault();
            

            // foreach(var c in  queryJoin)
            // {
                var c = queryJoin;
                Console.WriteLine($"{c.ClienteId, -3} | {c.UserId, -3} | {c.Nombre, -10} | {c.Apellido, -10} | {c.FechaDeNac, -10} | {c.Curp, -18} ");
            //}
        }
    }

    public static void JoinEmpUser(long ID)
    {
        using(Bank db = new())
        {
            // Join every product to list of categories
            var queryJoin = db.Empleados!.Join(
                inner: db.Usuarios!,
                outerKeySelector: empleado => empleado.UsuarioId, // ON Categories.Id = Products.Id
                innerKeySelector: usuario => usuario.UserId,
                resultSelector: (e, u) => new { e.Nomina, u.UserId, u.Nombre, u.Apellido, u.Usuario1, u.Contrasena, e.FecEntrada}
            ).Where(item => item.Nomina == ID).FirstOrDefault();
            

            // foreach(var c in  queryJoin)
            // {
                var c = queryJoin;
                Console.WriteLine($"{c.Nomina, -3} | {c.UserId, -3} | {c.Nombre, -10} | {c.Apellido, -10} | {c.Usuario1, -25} | {c.Contrasena, -15} | {c.FecEntrada, -10} | ");
            //}
        }
    }

        public static void JoinGerEmpCliUser(long ID)
    {
        using(Bank db = new())
        {
            // Join every product to list of categories
        var queryJoin = db.Empleados!
            .Join(
                inner: db.Usuarios!,
                outerKeySelector: empleado => empleado.UsuarioId,
                innerKeySelector: usuario => usuario.UserId,
                resultSelector: (e, u) => new { e.Nomina, u.UserId, u.Nombre, u.Apellido, u.Usuario1, u.Contrasena, e.FecEntrada }
            )
            .Join(
                inner: db.Clientes!,
                outerKeySelector: empleadoUsuario => empleadoUsuario.UserId,
                innerKeySelector: cliente => cliente.UserId,
                resultSelector: (eu, c) => new { eu.Nomina, eu.UserId, eu.Nombre, eu.Apellido, eu.Usuario1, eu.Contrasena, eu.FecEntrada, c.ClienteId, c.FechaDeNac, c.Curp,  }
            )
            .Join(
                inner: db.Gerentes!,
                outerKeySelector: empleadoUsuarioCliente => empleadoUsuarioCliente.ClienteId,
                innerKeySelector: gerente => gerente.ClienteId,
                resultSelector: (euc, g) => new { euc.Nomina, euc.UserId, euc.Nombre, euc.Apellido, euc.Usuario1, euc.Contrasena, euc.FecEntrada, euc.ClienteId, euc.FechaDeNac, euc.Curp, g.GerenteId}
            )
            .Where(item => item.GerenteId == ID)
            .FirstOrDefault();

            

            // foreach(var c in  queryJoin)
            // {
                Console.WriteLine($"{queryJoin.Nomina, -3} | {queryJoin.UserId, -3} | {queryJoin.ClienteId, -3} | {queryJoin.GerenteId} | {queryJoin.Nombre, -10} | {queryJoin.Apellido, -10} | {queryJoin.Usuario1, -25} | {queryJoin.Contrasena, -15} | {queryJoin.FecEntrada, -10} | {queryJoin.FechaDeNac} | {queryJoin.Curp} | ");
            //}
        }
    }
}