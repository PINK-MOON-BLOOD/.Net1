﻿using Paul1000.TaskPlanner.Domain.Models_.Enums;

namespace Paul1000.TaskPlanner.Domain.Models_
{
    public class WorkItem
    {
        // Properties
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Complexity Complexity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();

        public WorkItem Clone(){
            return new WorkItem { 
                Id = Guid.NewGuid(),
                CreationDate = this.CreationDate,
                DueDate = this.DueDate,
                Priority = this.Priority,   
                Complexity = this.Complexity,
                Title = this.Title,
                Description = this.Description,
                IsCompleted = this.IsCompleted
            };
        }

        // Constructor
        public override string ToString()
        {
            return $"{Title}: due {DueDate:dd.MM.yyyy}, {Priority.ToString().ToLower()} priority";
        }
    }
}
