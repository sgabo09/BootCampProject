using BootCampProject.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace BootCampProject.Controllers.API
{
    public class TodosController : ApiController
    {
        static List<Todo> TodoList = new List<Todo>
        {
            new Todo
            {
                Name = "Todo1",
                Priority = 2,
                Deadline = new DateTime(2019, 02, 13)
            },
            new Todo
            {
                Name = "Todo2",
                Priority = 3,
                Deadline = new DateTime(2019, 03, 15),
                Description = "Landing page",
                Responsible = "Developer Sándor",
                Status = "review",
                Category = CategoryEnum.EPIC,
                ParentId = 2
            },
            new Todo
            {
                Name = "Todo3",
                Priority = 1,
                Deadline = new DateTime(2019, 03, 15),
                Description = "Add to cart does not work",
                Responsible = "Frontend Ferenc",
                Status = "reopen",
                Category = CategoryEnum.BUG,
                ParentId = 1
            }
        };

        private Todo GetTodoById(Guid id)
        {
            var todo = TodoList.Find(item => item.Id == id);
            if (todo is null)
            {
                throw new ArgumentException("Given ID not found!");
            }
            else
            {
                return todo;
            }
        }

        //GET /api/todos
        public List<Todo> GetTodos()
        {
            return TodoList;
        }

        //GET /api/todos/{id}
        public Todo GetTodo(Guid id)
        {
            return GetTodoById(id);
        }

        //POST api/todos
        public IHttpActionResult CreateTodo([FromBody] Todo todo)
        {
            if (String.IsNullOrEmpty(todo.Name) || todo.Priority <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            TodoList.Add(todo);

            return Ok("Todo created!");
        }

        //PATCH /api/todos/{id}
        public IHttpActionResult PatchTodo(Guid id, [FromBody] Todo todo)
        {
            var old = GetTodoById(id);
            old.Name = todo.Name;
            old.Priority = todo.Priority;
            old.Deadline = todo.Deadline;
            old.Description = todo.Description;
            old.Responsible = todo.Responsible;
            old.Status = todo.Status;
            old.Category = todo.Category;
            old.ParentId = todo.ParentId;

            return Ok("Todo updated!");
        }

        //DELETE /api/todos/{id}
        public IHttpActionResult DeleteTodo(Guid id)
        {
            TodoList.Remove(GetTodoById(id));

            return Ok("Todo deleted!");
        }
    }
}