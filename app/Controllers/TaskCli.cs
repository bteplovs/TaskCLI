using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
                        # method
                        break;
                    case "2":

                }
            }
            

        }

    }
}