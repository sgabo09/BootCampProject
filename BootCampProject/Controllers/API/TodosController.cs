﻿using System;
using System.Web.Http;
using System.Web.Http.OData;
using TodoService;
using TodoService.Logics;
using TodoService.Models;

namespace BootCampProject.Controllers.API
{
    public class TodosController : ApiController, ITodoInterface
    {
        private TodoLogic todoLogic = new TodoLogic();

        // GET: api/todos?{categoryId}
        public IHttpActionResult GetTodos()
        {
            return Ok(todoLogic.GetAllTodo());
        }

        // GET: api/todos/{id}
        public IHttpActionResult GetTodo(Guid id)
        { 
            return Ok(todoLogic.GetTodoById(id));
        }

        // GET: api/todos/category/{id}
        public IHttpActionResult GetTodosByCategory(int categoryId)
        {
            return Ok(todoLogic.GetTodosByCategory(categoryId));
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