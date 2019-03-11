using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using TodoService.Filters;
using TodoService.Interfaces;
using TodoService.Logics;
using TodoService.Models;

namespace BootCampProject.Controllers.API
{
    public class TodosController : ApiController
    {
        private ITodoInterface todoLogic;
        private IssueLogic issueLogic = new IssueLogic();

        public TodosController(ITodoInterface logic)
        {
            this.todoLogic = logic;
        }

        // GET: api/todos/
        [LoggingFilter(isQuery:true, isBody:true, isHeader:true)]
        public IHttpActionResult GetTodos()
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
        public IHttpActionResult GetTasks(string responsible)
        {
            return Ok(todoLogic.GetTasksByResponsible(responsible));
        }

        [Route("api/todos/git")]
        public IHttpActionResult GetGitIssues()
        {
            return Ok(issueLogic.GetIssuesFromGitRepository());
        }

        // PATCH: api/todos{id}
        public IHttpActionResult PatchTodo(Guid id, [FromBody]Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
               todoLogic.PatchTodo(id, todo);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DBConcurrencyException)
            {
                throw;
            }

            return Ok("Todo updated successfully.");

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