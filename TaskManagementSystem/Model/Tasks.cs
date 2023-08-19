using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Model
{
  public class Tasks
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public TaskPriority Priority { get; set; }

        public string AssignId { get; set; }

        public string comment { get; set; }
    }

    public enum TaskStatus
    {
        Todo = 1,
        InProgress = 2,
        Done = 3
    }


    public enum TaskPriority
    {
        High = 1,
        Medium = 2,
        Low = 3
    }

}

