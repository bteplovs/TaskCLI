using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using app.Models;
using app.Services;
using Microsoft.VisualBasic;

namespace app.Controllers
{
    public class TaskCli
    {
        private ITaskService _taskService;
        public TaskCli(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public void RunAsync()
        {
            Console.WriteLine("== TASK MANAGER ==");
            Console.WriteLine();

            while (true)
            {
                PrintMenu();
                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        ListAllTasks();
                        break;
                    case "2":
                        ListAllTasksByStatus(TaskItemStatus.Done);
                        break;
                    case "3":
                        ListAllTasksByStatus(TaskItemStatus.Todo);
                        break;
                    case "4":
                        ListAllTasksByStatus(TaskItemStatus.InProgress);
                        break;
                    case "5":
                        UpdateTaskStatus();
                        break;
                    case "6":
                        AddTask();
                        break;
                    case "7":
                        DeleteTask();
                        break;
                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Unknown option. Try again");
                        break;
                }
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("== What would you like to do? ==");
            Console.WriteLine( "1) List all tasks");
            Console.WriteLine( "2) All tasks done");
            Console.WriteLine( "3) All tasks todo");
            Console.WriteLine( "4) All tasks in progress");
            Console.WriteLine( "5) Update task status");
            Console.WriteLine( "6) Add task");
            Console.WriteLine( "7) Delete task");
            Console.WriteLine( "0) Quit");
            Console.Write("> ");
        }

        private void ListAllTasks()
        {
            Console.WriteLine("== All Tasks ==");
            var tasks = _taskService.GetAllTasks();
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks");
            }

            foreach (TaskItem taskItem in tasks)
            {
                Console.WriteLine(taskItem);
            }

        }

        private void ListAllTasksByStatus(TaskItemStatus status)
        {
            Console.WriteLine($"== All {status} Tasks");
            var tasks = _taskService.GetAllTasksByStatus(status);
            if (tasks.Count == 0)
            {
                Console.WriteLine($"No tasks with status: {status}");
            }

            foreach (TaskItem taskItem in tasks)
            {
                Console.WriteLine(taskItem);
            }
        }

        private TaskItem? UpdateTaskStatus()
        {
            Console.WriteLine($"== Select a task to change status ==");
            var tasks = _taskService.GetAllTasks();
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks");
            }

            foreach (TaskItem taskItem in tasks)
            {
                Console.WriteLine(taskItem);
            }

            Console.Write("Change the task status (Todo, InProgress, Done) using <taskId> <new status>. Or leave blank to cancel: ");
            
            while (true)
            {
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Cancelled.");
                    return null;
                }

                var inputAsList = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (inputAsList.Length != 2)
                {
                    Console.WriteLine("Please enter: <taskId> <Todo|InProgress|Done>");
                    continue;
                }

                if (int.TryParse(inputAsList[0], out int id))
                {
                    if (Enum.TryParse(inputAsList[1], true, out TaskItemStatus status))
                    {
                        _taskService.UpdateTask(id, status);
                        return null; 
                    }
                    Console.WriteLine($"Invalid status '{inputAsList[1]}'. Try again or leave blank to cancel: ");
                    Console.Write("> ");
                } 
                else
                {
                    Console.WriteLine($"No Task found with id '{id}'. Try again or leave blank to cancel: ");
                    Console.Write("> ");
                }
            }
        }

        private TaskItem? DeleteTask()
        {
            Console.WriteLine($"== Select a task to delete ==");
            var tasks = _taskService.GetAllTasks();
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks");
            }

            foreach (TaskItem taskItem in tasks)
            {
                Console.WriteLine(taskItem);
            }

            Console.Write("Delete the task by: <taskId>. Or leave blank to cancel: ");
            
            while (true)
            {
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Cancelled.");
                    return null;
                }

                if (int.TryParse(input, out int id))
                {
                    _taskService.DeleteTask(id);
                    Console.WriteLine($"Deleted task with id: {id}");
                    return null;
                }

                Console.WriteLine($"No Task found with id '{id}'. Try again or leave blank to cancel.");
                Console.Write("> "); 
            }
        }

        private TaskItem? AddTask()
        {
            while (true)
            {
                Console.Write("Name your task or leave blank to cancel: ");
                var name = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Cancelled.");
                    return null;
                }

                var newTask = new TaskItem
                {
                    Name = name,
                    Status = TaskItemStatus.Todo,
                    Created = DateTime.Now.ToString("yyyy-MM-dd")
                };

                _taskService.AddTask(newTask);
                Console.WriteLine($"Added task: {name}");
                return null;

            }
        }
    }
}