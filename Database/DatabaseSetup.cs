using Microsoft.Data.Sqlite;

namespace LabManager.DataBase;

class DatabaseSetup
{

    private readonly DatabaseConfig _databaseConfig; //atributo com _ indica q é privado. conveção

    public DatabaseSetup(DatabaseConfig databaseConfig) //variavel
    {
        _databaseConfig = databaseConfig; //ñ tem mais conflito de nome
        CreateComputerTable();
        CreateLabTable();
    }

    private void CreateComputerTable()
    {
        var connection = new SqliteConnection (_databaseConfig.ConnectionString); //objeto
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Computers(
                        id int null primary key,
                        ram varchar (100) not null,
                        processor varchar (100) not null
                );
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }

    private void CreateLabTable()
    {
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Lab(
            id int not null primary key,
            number int not null,
            name varchar(100) not null,
            block varchar(100) not null
            );
        ";
        command.ExecuteNonQuery();
        connection.Close();
    }   
}