using System;
using System.ComponentModel.DataAnnotations;

namespace TodoService.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }
        public string Name { get; set; }
    }
}