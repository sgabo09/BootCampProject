using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.OData;
using TodoService.Filters;
using TodoService.Logics;
using TodoService.Models;

namespace BootCampProject.Controllers.API
{
    public class TodosController : ApiController
    {
        private TodoLogic todoLogic = new TodoLogic();

        // GET: api/todos/?name
        [LoggingFilter(isQuery:true, isBody:true, isHeader:true)]
        public IHttpActionResult GetTodos([FromUri] string name)
        {
            return Ok(todoLogic.GetAllTodo());
        }

        // GET: api/todos/{id}
        public IHttpActionResult GetTodo(Guid id)
        {
            var todo = todoLogic.GetTodoById(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [Route("api/todos/category")]
        [HttpGet]
        [LoggingFilter]
        public IHttpActionResult GetTodosCategory()
        {
            return Ok(todoLogic.GetAllTodosByCategory());
        }

        [Route("api/todos/category/{categoryId}")]
        [HttpGet]
        public IHttpActionResult GetTodosByCategory(Guid categoryId)
        {
            return Ok(todoLogic.GetTodosByCategory(categoryId));
        }

        [Route("api/todos/recent")]
        [HttpGet]
        public IHttpActionResult GetRecentTodos()
        {
            return Ok(todoLogic.GetRecentTodos());
        }

        [Route("api/todos/tree")]
        [HttpGet]
        public IHttpActionResult GetTodoTree()
        {
            return Ok(todoLogic.GetTodoTree());
        }

        [Route("api/todos/responsibility/{responsible}")]
        [HttpGet]
        public List<Task> GetTasks(string responsible)
        {
            return todoLogic.GetTasksByResponsible(responsible);
        }

        // PATCH: api/todos{id}
        public IHttpActionResult PatchTodo(Guid id, [FromBody] Delta<Todo> todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isPatched = todoLogic.PatchTodo(id, todo);

            return isPatched ? (IHttpActionResult) Ok("Todo updated successfully.") : NotFound();
        }

        // POST: api/todos
        [LoggingFilter]
        public IHttpActionResult PostTodo([FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            todoLogic.PostTodo(todo);
            return Ok("Todo added successfully.");
        }

        // DELETE: api/Todos/{id}
        public IHttpActionResult DeleteTodo(Guid id)
        {
            var isDeleted = todoLogic.DeleteTodo(id);
            return isDeleted ? (IHttpActionResult) Ok("Todo deleted successfully") : NotFound();
        }
    }
}