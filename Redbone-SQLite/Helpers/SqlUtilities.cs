using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace Redbone.Helpers;

public static class SqlUtilities
{
    public static void InitializeDatabase(string databaseFileName)
    {
        string currentPath = Directory.GetCurrentDirectory();
        string filePath = @$"{currentPath}\{databaseFileName}";

        if (!File.Exists(filePath))
        {
            SQLiteConnection.CreateFile(filePath);
        }

        string connectionString = $"Data Source={filePath}";
        var connection = new SqliteConnection(connectionString);

        connection.Open();

        if (!TableExists(connectionString,"post") || !TableExists(connectionString, "image"))
        {
            string query = File.ReadAllText(@$"{currentPath}\Database.sql");

            var command = new SqliteCommand(query, connection);
            command.ExecuteNonQuery();
        }
    }

    private static bool TableExists(string connectionString, string tableName)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        string query = $"SELECT count(name) FROM sqlite_master WHERE type='table' AND name='{tableName}';";
        using var command = new SqliteCommand(query, connection);
        object result = command.ExecuteScalar();
	
        connection.Close();
	
        return Convert.ToInt32(result) > 0;
    }
}