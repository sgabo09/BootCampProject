using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoService.Models.Enums;

namespace TodoService.Models
{
    public class TodoCategory
    {
        public CategoryEnum Type { get; set; }
        public List<Todo> Issues { get; set; }

        public TodoCategory()
        {
            Issues = new List<Todo>();
        }
    }
}