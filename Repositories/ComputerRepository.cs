using LabManager.Models;
using Microsoft.Data.Sqlite;
using LabManager.DataBase; //pq dataBaseconfig vem de outra pasta

namespace LabManager.Repositories;

class ComputerRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Computer> GetAll () //devolver lista de comptadores
    {
        var computers = new List<Computer>();

        var connection = new SqliteConnection ("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers"; //* seleciona todas as colunas da tabela, não todos os computadores

        var reader = command.ExecuteReader();

        while(reader.Read()) //preenche com os computadores que tão no bdd
        //não precisa de select no bdd
        {
            var id = reader.GetInt32(0);
            var ram = reader.GetString(1);
            var processor = reader.GetString(2);
            var computer = new Computer(id, ram, processor);
            computers.Add(computer);
        }

        connection.Close();

        return computers;
    }

    public void Save (Computer computer) //salva computer no bdd
    {
        var connection = new SqliteConnection (_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor)";
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);

        command.ExecuteNonQuery();
        connection.Close();
    }
}