using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("running program");
        }

    }
}