using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootCampProject.Models
{
    public class Todo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string Responsible { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        public CategoryEnum Category { get; set; }
        public int ParentId { get; set; }

        public Todo(
            string name,
            int priority,
            DateTime deadline,
            string description = "",
            string responsible = "",
            string status = "",
            CategoryEnum category = CategoryEnum.TASK,
            int parentId = 1)
        {
            Name = name;
            Priority = priority;
            Deadline = deadline;
            Description = description;
            Responsible = responsible;
            Status = status;
            Category = category;
            ParentId = parentId;
        }
    }
}