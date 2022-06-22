using LabManager.DataBase;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class LabRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public LabRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Lab> GetAll() //devolve lista de labs
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var labs = connection.Query<Lab>("SELECT * FROM Lab").ToList();

        return labs;
    }

    public void Save(Lab lab) //salva lab no bdd
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Lab VALUES(@Id, @Number, @Name, @Block)", lab);
    }

    public Lab GetById(int id) //devolve lab pelo id
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
    
        var lab = connection.QuerySingle<Lab>("SELECT * FROM Lab WHERE id_lab == @Id", new { Id = id });

        return lab;
    }

    public Lab Update(Lab lab) //atualiza lab no bdd
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Lab SET number = @Number, name = @Name, block = @Block  WHERE id_lab == @id", lab);
        
        return GetById(lab.Id);
    }

    public void Delete(int id) //deleta um lab do bdd
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Lab WHERE id_lab == @Id", new {Id = id});
    }

    public bool ExistsById(int id)  //confere se um lab existe no bdd pelo id
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        
        bool result = connection.ExecuteScalar<bool>("SELECT count(id_lab) FROM Lab WHERE id_lab = $Id", new {Id = id});

        return result;
    }

}