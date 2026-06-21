using app.Models;

namespace app.Services

{
    public interface ITaskService
    {
        IEnumerable<TaskItem> GetAllTasks();
        TaskItem GetTaskById(int id);
        void AddTask(TaskItem task);
        void UpdateTask(TaskItem task);
        void DeleteTask(int id);
    }
}