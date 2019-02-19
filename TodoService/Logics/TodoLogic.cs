using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.OData;
using TodoService.Models;

namespace TodoService.Logics
{
    public class TodoLogic : ITodoInterface
    {
        private TodoContext db = new TodoContext();

        public IQueryable<Todo> GetAllTodo()
        {
            return db.Todos;
        }

        public Todo GetTodoById(Guid id)
        {
            return db.Todos.Find(id);
        }

        public IEnumerable<Todo> GetTodosByCategory(int categoryId)
        {
            return db.Todos.Where(c => categoryId == c.Category);
        }
        
        public IQueryable<IGrouping<int, Todo>> GetAllTodosByCategory()
        {
            return db.Todos.GroupBy(c => c.Category);
        }

        public IEnumerable<Todo> GetRecentTodos()
        {
            return db.Todos.Where(t => t.Status.Equals("Open") || DbFunctions.DiffMinutes(t.LastModified, DateTime.Now) <= 120);
        }

        public bool PatchTodo(Guid id, Delta<Todo> todo)
        {
            var temp = db.Todos.Find(id);
            if (temp is null)
            {
                return false;
            }
            
            temp.LastModified = DateTime.Now;
            todo.Patch(temp);
            db.SaveChanges();

            return true;
        }

        public void PostTodo(Todo todo)
        {
            todo.Id = Guid.NewGuid();
            todo.LastModified = DateTime.Now;
            db.Todos.Add(todo);
            db.SaveChanges();
        }

        public bool DeleteTodo(Guid id)
        {
            var todo = db.Todos.Find(id);
            if (todo is null)
            {
                return false;
            }
            db.Todos.Remove(todo);
            db.SaveChanges();

            return true;
        }
    }
}