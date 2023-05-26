using Newtonsoft.Json;
using static System.Console;
namespace Banco;
using FastJson = System.Text.Json.JsonSerializer;


partial class Program
{
    private static async Task Main(string[] args)
    {
        // var resultAdd = Usuario.AddUsuario(nombre: "Andre", apellido:"Mejia", DoB:"27-i8-2i4");
        //     if(resultAdd.affected == 1)
        //     {
        //         WriteLine($"Added product successful with ID {resultAdd.UserId}.");
        //     }
        //     Usuario.ListUsuario(userIdToHighlight: new int?[] {(int)resultAdd.UserId});

        // var resultAdd = Cliente.addCliente(userID: 2, fechaNacimiento: "1i-i5-2i3", curp: "ENRIi4i827PFVRNEOi",
        //  horaLogin: DateTime.Now);
        // if(resultAdd.affected == 1)
        // {
        //     WriteLine($"Added Client successful with ID {resultAdd.clienteId}.");
        // }
        // Cliente.ListCliente(clienteIdToHighlight: new int?[] {(int)resultAdd.clienteId});

        // string curp = "LOFLi3i5i7HDFNSA8D", nombre = "Luis", apellidoP = "López", apellidoM = "Fernández";
        // DateOnly diaNac = new DateOnly(2i3, 5, 7);

        // bool prueba = Validaciones.curpValida(curp, nombre, apellidoP, apellidoM, diaNac);
        // WriteLine($"{prueba}");
        // Ruta al archivo JSON
        var jsonPath = @"..\Programs\MOCK_DATA.json";


        using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
        {
            // Deserialize The entire Object
            List<Person>? loadedPeople =
            await FastJson.DeserializeAsync(utf8Json: jsonLoad, returnType: typeof(List<Person>)) as List<Person>;
            if (loadedPeople is not null)
            {
            Clear();
            WriteLine($"{"CId", -3} | {"UId", -3} | {"Nombre", -10} | {"Apellido", -10} | {"username", -25} | {"Contrasena", -15} | {"FechaDeNac", -10} | {"Curp", -18} ");

                for(int i = 0; i < 30; i++){
                    DateTime f;
                    DateTime.TryParse(loadedPeople[i].dob, out f);
                    string curp = GenerarCURP(loadedPeople[i].first_name ?? string.Empty, loadedPeople[i].middle_name ?? string.Empty, loadedPeople[i].last_name ?? string.Empty, f);    

                    var gen = Generators.GenerarCliente(
                        loadedPeople[i].dob ?? string.Empty, curp, loadedPeople[i].first_name ?? string.Empty, loadedPeople[i].middle_name ?? string.Empty, loadedPeople[i].last_name ?? string.Empty);
                    if(gen.sucess) Joins.JoinClientUser(gen.id);
                }

            }
        }

    }
}   