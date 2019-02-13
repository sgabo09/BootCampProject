using BootCampProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace BootCampProject.Controllers.API
{
    public class TodosController : ApiController
    {
        static List<Todo> TodoList = new List<Todo>
        {
            new Todo("Todo1", 2, new DateTime(2019, 02, 13)),
            new Todo("Todo2", 3, new DateTime(2019, 03, 15), "Landing page","Developer Sándor","review", CategoryEnum.EPIC, 2),
            new Todo("Todo3", 5, new DateTime(2019, 02, 12), "404", "Frontend Ferenc","review", CategoryEnum.BUG, 1)
        };

        //GET /api/todos
        public List<Todo> GetTodos()
        {
            return TodoList;
        }

        //GET /api/todos/{id}
        public Todo GetTodo(int id)
        {
            try
            {
                return TodoList.ElementAt(id);
            }
            catch(ArgumentOutOfRangeException)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        //POST api/todos
        public void CreateTodo([FromBody] Todo todo)
        {
            if (todo.Name == null || todo.Priority <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            TodoList.Add(todo);
        }

        //PATCH /api/todos/{id}
        public void PatchTodo(int id, [FromBody] Todo todo)
        {
            var old = TodoList.ElementAt(id);
            old.Name = todo.Name;
            old.Priority = todo.Priority;
            old.Deadline = todo.Deadline;
            old.Description = todo.Description;
            old.Responsible = todo.Responsible;
            old.Status = todo.Status;
            old.Category = todo.Category;
            old.ParentId = todo.ParentId;
        }

        //DELETE /api/todos/{id}
        public void DeleteTodo(int id)
        {
            TodoList.RemoveAt(id);
        }
    }
}
