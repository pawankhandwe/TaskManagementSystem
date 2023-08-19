using TaskManagementSystem.Model;

namespace TaskManagementSystem.Interface
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Tasks>> GetAllTasksAsync();
        Task<Tasks> GetTaskByIdAsync(int id);
        Task AddTaskAsync(Tasks task);
        Task AssignTaskAsync(Tasks task);
        Task UpdateTaskAsync(int id, Tasks updatedTask);
        Task DeleteTaskAsync(int id);
        Task<IEnumerable<Tasks>> SearchTasksAsync(string keywords);
        Task<IEnumerable<Tasks>> SortTasksByPriorityAsync();
        Task<IEnumerable<Tasks>> SortTasksByDueDateAsync();
    }
}
