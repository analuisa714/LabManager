using Microsoft.Data.Sqlite;

namespace LabManager.DataBase;

class DatabaseSetup
{

    public DatabaseSetup()
    {
        CreateComputerTable();
        CreateLabTable();
    }

    private void CreateComputerTable()
    {
        var connection = new SqliteConnection ("Data Source=database.db");
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

    }
}