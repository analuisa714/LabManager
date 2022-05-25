using Microsoft.Data.Sqlite;
using LabManager.DataBase;
using LabManager.Repositories; //importar a classe 

var databaseConfig = new DatabaseConfig(); //objeto. não precisa ficar criando nas classes

var databaseSetup = new DatabaseSetup(databaseConfig);

var computerRepository = new ComputerRepository(databaseConfig); //criar a váriavel 

//Routing
var modelName = args[0];
var modelAction = args[1];

if (modelName == "Computer")
{
        if (modelAction == "List")
        {
                Console.WriteLine("Computer List");
                foreach (var computer in computerRepository.GetAll())
                {
                        Console.WriteLine(
                                "{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor
                                );
                }
        }

        if (modelAction == "New") 
        {
                var id = Convert.ToInt32(args [2]);
                var ram = args [3];
                var processor = args [4];

                var connection = new SqliteConnection (databaseConfig.ConnectionString);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor)";
                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$ram", ram);
                command.Parameters.AddWithValue("$processor", processor);

                command.ExecuteNonQuery();
                connection.Close();
        }
}