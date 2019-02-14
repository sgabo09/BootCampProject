﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootCampProject.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string Responsible { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        public CategoryEnum Category { get; set; }
        public int ParentId { get; set; }

        public Todo()
        {
            Id = Guid.NewGuid();
            Name = "Default";
            Description = "None";
            Priority = 1;
            Responsible = "Not assigned";
            Deadline = DateTime.Now.AddDays(1);
            Status = "to-do";
            Category = CategoryEnum.TASK;
            ParentId = 1;
        }
    }
}