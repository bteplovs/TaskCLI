using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using app.Models;
using Microsoft.Data.Sqlite;
using SQLitePCL;

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

        IEnumerable<TaskItem>? ITaskService.GetAllTasks()
        {

            var taskItems = new List<TaskItem>();

            try
            {
                _connection.Open();

                var sql = "SELECT * FROM TaskItem";

                using var command = new SqliteCommand(sql, _connection);

                using var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        if (Enum.TryParse(reader.GetString(2), true, out TaskItemStatus result)) {}

                        var taskItem = new TaskItem
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Status = result,
                            Created = reader.GetString(3)

                        };
                        taskItems.Add(taskItem);
                    }
                }
                else
                {
                    Console.WriteLine("No Tasks");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }

            return taskItems;

        }

        TaskItem? ITaskService.GetTaskById(int id)
        {

            TaskItem? taskItemToReturn = null;

            try
            {
                _connection.Open();

                var sql = "SELECT * FROM TaskItem WHERE Id = @id";

                using var command = new SqliteCommand(sql, _connection);
                command.Parameters.AddWithValue("@id", id);

                using var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (Enum.TryParse(reader.GetString(2), true, out TaskItemStatus result)) {}

                        var taskItem = new TaskItem
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Status = result,
                            Created = reader.GetString(3)

                        };
                        taskItemToReturn = taskItem;
                    }
                }
                else
                {
                    Console.WriteLine($"No task with id {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }

            return taskItemToReturn;
        }

        void ITaskService.AddTask(TaskItem task)
        {
            try
            {
                _connection.Open();

                var sql = "INSERT INTO TaskItems (Name, Status, Created)" + 
                            "VALUES (@Name, @Status, @Created)";
                
                using var command = new SqliteCommand(sql, _connection);

                command.Parameters.AddWithValue("@Name", task.Name);
                command.Parameters.AddWithValue("@Status", task.Status.ToString());
                command.Parameters.AddWithValue("@Created", task.Created);

                var rowInserted = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex}");
            }
            finally
            {
                _connection.Close();
            }
        }

        void ITaskService.UpdateTask(int id, TaskItemStatus status)
        {
            try
            {
                _connection.Open();

                var sql = "UPDATE TaskItems SET Status = @status WHERE id = @id";

                using var command = new SqliteCommand(sql, _connection);

                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@id", id);

                var rowInserted = command.ExecuteNonQuery();
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex}");
            }
            finally
            {
                _connection.Close();
            }
        }

        void ITaskService.DeleteTask(int id)
        {
            try
            {
                _connection.Open();

                var sql = "DELETE FROM TaskItem WHERE id = @id";

                using var command = new SqliteCommand(sql, _connection);
                
                command.Parameters.AddWithValue("@id", id);

                var rowDeleted = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured ${ex}");
            }
            finally
            {
                _connection.Close();                
            }
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