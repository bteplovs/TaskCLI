using System;
using Microsoft.Data.Sqlite;
using app.Services;
using app.Controllers;

namespace SqliteConsoleApp
{
    class Program
    {
        private const string ConnectionString = "Data Source=appdata.db";

        static void Main(string[] args)
        {
            var connection = new SqliteConnection(ConnectionString);

            var taskService = new TaskService(connection);
            var cli = new TaskCli(taskService);
            cli.RunAsync();
        }
    }
}
