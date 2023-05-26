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
                Console.WriteLine($"{c.ClienteId, -3} | {c.UserId, -3} | {c.Nombre, -10} | {c.Apellido, -10} | {c.Usuario1, -25} | {c.Contrasena, -15} | {c.FechaDeNac, -10} | {c.Curp, -18} ");
            //}
        }
    }
}