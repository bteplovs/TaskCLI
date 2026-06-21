using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using app.Models;
using Microsoft.Data.Sqlite;

namespace app.Services
{
    public class TaskService : ITaskService
    {
        private SqliteConnection _connection;

        public TaskService(SqliteConnection connection)
        {
            _connection = connection;
            this.InitializeDatabase();
        }

        IEnumerable<TaskItem> ITaskService.GetAllTasks()
        {
            throw new NotImplementedException();
        }

        TaskItem ITaskService.GetTaskById(int id)
        {
            throw new NotImplementedException();
        }

        void ITaskService.AddTask(TaskItem task)
        {
            throw new NotImplementedException();
        }

        void ITaskService.UpdateTask(TaskItem task)
        {
            throw new NotImplementedException();
        }

        void ITaskService.DeleteTask(int id)
        {
            throw new NotImplementedException();
        }

        public void InitializeDatabase()
        {
            try
            {
                _connection.Open();
                Console.WriteLine("Connection to SQLite database established successfully.");

                var createTaskTable = _connection.CreateCommand();
                createTaskTable.CommandText = @"
                    CREATE TABLE IF NOT EXISTS TaskItem (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Description TEXT NOT NULL,
                        Status TEXT NOT NULL,
                        Created TEXT DEFAULT CURRENT_TIMESTAMP
                    );
                ";
                createTaskTable.ExecuteNonQuery();
                Console.WriteLine("TaskItem table created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                _connection.Close();
                Console.WriteLine("Connection to SQLite database closed.");
            }
            
        }
    }
}