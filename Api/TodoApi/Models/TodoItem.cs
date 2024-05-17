using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
<<<<<<< HEAD
<<<<<<< HEAD
    public Guid Id { get; set; }
    public required string Task { get; set; }
    public required string Deadline { get; set; }
    public required string Details { get; set; }
    public bool IsComplete { get; set; }
    public IEnumerable<TodoItem>? SubTasks { get; set; }
=======
    public Guid? Id { get; set; }
    public string? Task { get; set; }
    public string? Deadline { get; set; }
    public string? Details { get; set; }
    public bool? IsComplete { get; set; }
    public string? Secret { get; set; }
    public List<TodoItem>? SubTasks { get; set; }


>>>>>>> 6314c53 (Add Tests)
}
=======
    public class TodoItem
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public required string Task { get; set; }

        public required string Deadline { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(500)]
        public required string Details { get; set; }

        public bool IsComplete { get; set; }

        private List<TodoItem> subTasks = new List<TodoItem>();

        public List<TodoItem> SubTasks
        {
            get => subTasks;
            set
            {
                if (value != null)
                {
                    foreach (var subTask in value)
                    {
                        if (subTask.SubTasks != null && subTask.SubTasks.Any())
                        {
                            throw new InvalidOperationException("Subtasks cannot have their own subtasks.");
                        }
                    }
                    subTasks = value;
                }
                else
                {
                    subTasks = new List<TodoItem>();
                }
            }
        }
    }
}
>>>>>>> 9542e40 (Added additional tests)
