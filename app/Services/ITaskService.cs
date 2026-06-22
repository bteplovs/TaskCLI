using app.Models;

namespace app.Services

{
    public interface ITaskService
    {
        IEnumerable<TaskItem>? GetAllTasks();
        IEnumerable<TaskItem>? GetAllTasksByStatus(TaskItemStatus status);
        TaskItem? GetTaskById(int id);
        void AddTask(TaskItem task);
        void UpdateTask(int id, TaskItemStatus status);
        void DeleteTask(int id);
    }
}