using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using app.Models;

namespace app.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;
        public string Created { get; set; }

        public override string ToString()
        {
            return $"\n Id: {Id}\n Name: {Name}\n Status: {Status}\n";

        }

    }
}