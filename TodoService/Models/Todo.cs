﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TodoService.Models
{
    public class Todo
    {
        [Key]
        public Guid Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public int Priority { get; set; }
        [StringLength(50)]
        public string Responsible { get; set; }
        public DateTime Deadline { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [Range(1,3)]
        public int Category { get; set; }
        public int ParentId { get; set; }
    }
}