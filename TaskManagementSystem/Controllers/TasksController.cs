using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Interface;
using TaskManagementSystem.Model;

namespace TaskManagementSystem.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository; // You'll need to create the TaskRepository

        private readonly UserManager<ApplicationUser> _userManager;

        public TasksController(ITaskRepository taskRepository, UserManager<ApplicationUser> userManager)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetTaskById(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<Tasks>> AddTask(Tasks task)
        {
            if (task == null)
            {
                return BadRequest("Invalid task data.");
            }

            await _taskRepository.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Tasks updatedTask)
        {
            await _taskRepository.UpdateTaskAsync(id, updatedTask);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskRepository.DeleteTaskAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddCommentToTask(int id, [FromBody] string commentContent)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            task.comment = commentContent;

            await _taskRepository.UpdateTaskAsync(id, task);

            return Ok("Comment added successfully.");
        }


        [HttpPost("Assign/{taskId}/{userName}")]
        public async Task<IActionResult> AssignTask(int taskId, string userName)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId); 
            var user = await _userManager.FindByNameAsync(userName);
            

            if (task == null || user == null)
            {
                return NotFound();
            }

            task.AssignId = user.Id;
            //_context.Update(task);
            //await _context.SaveChangesAsync();

            return Ok("Task assigned successfully.");
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Tasks>>> SearchTasks(string keywords)
        {
            var tasks = await _taskRepository.SearchTasksAsync(keywords);
            return Ok(tasks);
        }

        [HttpGet("SortByPriority")]
        public async Task<ActionResult<IEnumerable<Tasks>>> SortTasksByPriority()
        {
            var tasks = await _taskRepository.SortTasksByPriorityAsync();
            return Ok(tasks);
        }

        [HttpGet("SortByDueDate")]
        public async Task<ActionResult<IEnumerable<Tasks>>> SortTasksByDueDate()
        {
            var tasks = await _taskRepository.SortTasksByDueDateAsync();
            return Ok(tasks);
        }
    }
}
