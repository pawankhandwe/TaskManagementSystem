
    using TaskManagementSystem.Context;
    using TaskManagementSystem.Interface;
    using TaskManagementSystem.Model;
    using Microsoft.EntityFrameworkCore;
    namespace TaskManagementSystem.Repository
    {
        public class TaskRepository : ITaskRepository
        {
            private readonly ApplicationDbContext _dbContext;

            public TaskRepository(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<IEnumerable<Tasks>> GetAllTasksAsync()
            {
                return await _dbContext.Tasks.ToListAsync();
            }

            public async Task<Tasks> GetTaskByIdAsync(int id)
            {
                return await _dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == id);
            }

            public async Task AddTaskAsync(Tasks task)
            {
                _dbContext.Tasks.Add(task);
                await _dbContext.SaveChangesAsync();
            }
        public async Task AssignTaskAsync(Tasks task)
        {
            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateTaskAsync(int id, Tasks updatedTask)
            {
                var existingTask = await _dbContext.Tasks.FindAsync(id);
                if (existingTask != null)
                {
                    existingTask.Title = updatedTask.Title;
                    existingTask.Description = updatedTask.Description;
                    existingTask.Status = updatedTask.Status;
                    existingTask.DueDate = updatedTask.DueDate;
                    existingTask.Priority = updatedTask.Priority;

                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Task not found.", nameof(id));
                }
            }

            public async Task DeleteTaskAsync(int id)
            {
                var task = await _dbContext.Tasks.FindAsync(id);
                if (task != null)
                {
                    _dbContext.Tasks.Remove(task);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Task not found.", nameof(id));
                }
            }
        public async Task<IEnumerable<Tasks>> SearchTasksAsync(string keywords)
        {
            return await _dbContext.Tasks
                .Where(task => task.Title.Contains(keywords) || task.Description.Contains(keywords))
                .ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> SortTasksByPriorityAsync()
        {
            return await _dbContext.Tasks
                .OrderBy(task => task.Priority)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> SortTasksByDueDateAsync()
        {
            return await _dbContext.Tasks
                .OrderBy(task => task.DueDate)
                .ToListAsync();
        }
    }
    }


