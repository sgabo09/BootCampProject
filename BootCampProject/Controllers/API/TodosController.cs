using System;
using System.Web.Http;
using BootCampProject.Models;

namespace BootCampProject.Controllers.API
{
    public class TodosController : ApiController
    {
        private TodoContext _db = new TodoContext();

        // GET: api/todos
        public IHttpActionResult GetTodos()
        {
            return Ok(_db.Todos);
        }

        // GET: api/todos/{id}
        public IHttpActionResult GetTodo(Guid id)
        {
            var todo = _db.Todos.Find(id);
            if (todo is null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // PATCH: api/todos{id}
        public IHttpActionResult PatchTodo(Guid id, [FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            todo.Id = id;
            _db.Entry(_db.Todos.Find(id)).CurrentValues.SetValues(todo);
            _db.SaveChanges();

            return Ok("Todo updated successfully.");
        }

        // POST: api/todos
        public IHttpActionResult PostTodo([FromBody] Todo todo)
        {
            todo.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Todos.Add(todo);
            _db.SaveChanges();

            return Ok("Todo added successfully.");
        }

        // DELETE: api/Todos/{id}
        public IHttpActionResult DeleteTodo(Guid id)
        {
            var todo = _db.Todos.Find(id);
            if (todo is null)
            {
                return NotFound();
            }

            _db.Todos.Remove(todo);
            _db.SaveChanges();

            return Ok("Todo deleted successfully");
        }
    }
}