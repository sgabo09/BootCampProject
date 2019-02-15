using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.OData;
using TodoService.Models;

namespace TodoService.Logics
{
    public class TodoLogic
    {
        private TodoContext db = new TodoContext();

        public DbSet<Todo> GetAllTodo()
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

        public bool PatchTodo(Guid id, Delta<Todo> todo)
        {
            var temp = db.Todos.Find(id);
            if (temp is null)
            {
                return false;
            }
   
            todo.Patch(temp);
            db.SaveChanges();

            return true;
        }

        public void PostTodo(Todo todo)
        {
            todo.Id = Guid.NewGuid();
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