namespace Banco;
using FastJson = System.Text.Json.JsonSerializer;
using static System.Console;

class Serializadores{
    public static async Task InyectarClientes(int n, int m, string jsonPath){

        using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
        {
            // Deserialize The entire Object
            List<Person>? loadedPeople =
            await FastJson.DeserializeAsync(utf8Json: jsonLoad, returnType: typeof(List<Person>)) as List<Person>;
            if (loadedPeople is not null)
            {
            Clear();
            WriteLine($"{"CId", -3} | {"UId", -3} | {"Nombre", -10} | {"Apellido", -10} | {"DoB", -10} | {"Curp", -18}  ");

                for(int i = n; i < m; i++){
                    DateTime d;
                    if(!DateTime.TryParse(loadedPeople[i].dob, out d)) break;
                    string curp = Program.GenerarCURP(loadedPeople[i].first_name ?? string.Empty, loadedPeople[i].middle_name ?? string.Empty, loadedPeople[i].last_name ?? string.Empty, d);
                    var gen = Generators.GenerarCliente(loadedPeople[i].dob ?? string.Empty, curp, loadedPeople[i].first_name ?? string.Empty, loadedPeople[i].middle_name ?? string.Empty,
                            loadedPeople[i].last_name ?? string.Empty );
                    if(gen.sucess) Joins.JoinClientUser(gen.id);
                }

            }
    }
}

    public static async Task InyectarEmpleados(int n, int m, string jsonPath){

        using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
        {
            // Deserialize The entire Object
            List<Person>? loadedPeople =
            await FastJson.DeserializeAsync(utf8Json: jsonLoad, returnType: typeof(List<Person>)) as List<Person>;
            if (loadedPeople is not null)
            {
            Clear();
            WriteLine($"{"EId", -3} | {"UId", -3} | {"Nombre", -10} | {"Apellido", -10} | {"username", -25} | {"Contrasena", -15} | {"Entrada", -10} ");

                for(int i = n; i < m; i++){

                    var gen = Generators.GenerarEmpleado(loadedPeople[i].first_name ?? string.Empty, loadedPeople[i].last_name ?? string.Empty,
                            loadedPeople[i].fec_entrada ?? string.Empty, loadedPeople[i].dob ?? string.Empty);
                    if(gen.sucess) Joins.JoinEmpUser(gen.id);
                }

            }
        }
    }

    public static async Task InyectarGerentes(int n, int m, string jsonPath){

        using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
        {
            // Deserialize The entire Object
            List<Person>? loadedPeople =
            await FastJson.DeserializeAsync(utf8Json: jsonLoad, returnType: typeof(List<Person>)) as List<Person>;
            if (loadedPeople is not null)
            {
            WriteLine($"{"EId", -3} | {"UId", -3} | {"CId", -3} | {"GID", -3} | {"Nombre", -10} | {"Apellido", -10} | {"username", -25} | {"Contrasena", -15} | {"Entrada", -10} | {"DoB", -10} | {"CURP", 18} ");

                for(int i = n; i < m; i++){
                    DateTime d;
                    if(!DateTime.TryParse(loadedPeople[i].dob, out d)) break;
                    string curp = Program.GenerarCURP(loadedPeople[i].first_name ?? string.Empty, loadedPeople[i].middle_name ?? string.Empty, loadedPeople[i].last_name ?? string.Empty, d);
                    
                    var gen = Generators.GenerarGerente(loadedPeople[i].first_name ?? string.Empty, curp, loadedPeople[i].last_name ?? string.Empty, loadedPeople[i].middle_name ?? string.Empty,
                            loadedPeople[i].fec_entrada ?? string.Empty, loadedPeople[i].dob ?? string.Empty);
                    if(gen.sucess) Joins.JoinGerEmpCliUser(gen.id);
                }

            }
        }
    }
}