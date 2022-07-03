using LabManager.Models;
using Microsoft.Data.Sqlite;
using LabManager.DataBase; //pq dataBaseconfig vem de outra pasta
using Dapper;

namespace LabManager.Repositories;

class ComputerRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
     
    public IEnumerable<Computer> GetAll()//devolve lista de computadores
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var computers = connection.Query<Computer>("SELECT * FROM Computers");
        connection.Close();

        return computers;
    }
            
    public void Save (Computer computer) //salva computer no bdd
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)", computer);
    }

    public Computer GetById (int id) //devolve computador pelo id
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var computer = connection.QuerySingle<Computer>("SELECT * FROM Computers WHERE id == @Id", new { Id = id });

        return computer;
    }

    public void Delete(int id) //deleta computador do bdd
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Computers WHERE id == @Id", new {Id = id});
    }

    public Computer Update (Computer computer) //atualiza computador no bdd
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Computers SET ram = @Ram, processor = @Processor  WHERE id == @Id", computer);
        
        return computer;
    }

    public bool ExistsById(int id) //confere se um computador existe no bdd pelo id
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
   
        bool result = connection.ExecuteScalar<bool>("SELECT count(id) FROM Computers WHERE id = @Id", new {Id = id});

        return result;
    }
}