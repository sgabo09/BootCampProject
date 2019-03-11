using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TodoService.Interfaces;
using TodoService.Models;
using Guid = System.Guid;

namespace TodoService.Logics
{
    public class TodoLogic : ITodoInterface
    {
        private TodoContext _db { get; set; }
        private int _timeInterval = 0;

        public TodoLogic()
        {
            _db = new TodoContext();
            int.TryParse(ConfigurationManager.AppSettings["RecentTimeInterval"], out _timeInterval);
        }

        //public TodoLogic(TodoContext context)
        //{
        //    _db = context;
        //    int.TryParse(ConfigurationManager.AppSettings["RecentTimeInterval"], out _timeInterval);
        //}

        public IQueryable<Todo> GetAllTodo()
        {
            return _db.Todos;
        }

        public Todo GetTodoById(Guid id)
        {
            return _db.Todos.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Todo> GetTodosByCategory(Guid categoryId)
        {
            return _db.Todos.Where(c => categoryId == c.CategoryId);
        }
        
        public List<Category> GetAllTodosByCategory()
        {
            return _db.Categories.Include("Todos").ToList();
        }

        public IEnumerable<Todo> GetRecentTodos()
        {
            DateTime recentInterval = DateTime.Now.AddMinutes(_timeInterval * -1);

            return _db.Todos.Where(t => t.Status.Equals("Open") || recentInterval <= t.LastModified);
        }

        public IEnumerable<TreeNode> GetTodoTree()
        {
            var rootNodes = new List<TreeNode>();
            var treeNodes = _db.Todos
                .Select(x => new TreeNode() { Node = x })
                .ToList();
            var dictionary = treeNodes.GroupBy(x => x.Node.ParentId).ToDictionary(x => x.Key, x => x.ToList());

            foreach (var todo in treeNodes)
            {
                if (todo.Node.ParentId == Guid.Empty)
                {
                    rootNodes.Add(todo);
                }

                if (dictionary.ContainsKey(todo.Node.Id))
                todo.Children = dictionary[todo.Node.Id];
            }

            return rootNodes;
        }

        public List<Task> GetTasksByResponsible(string responsible)
        {
            var param1 = new SqlParameter("@Responsible", responsible);

            return _db.Database.SqlQuery<Task>("EXEC GetTasksByResponsible @Responsible", param1).ToList();
        }

        public void PatchTodo(Guid id, Todo todo)
        {
            //var temp = _db.Todos.Find(id);
            //if (temp is null)
            //{
            //    throw new KeyNotFoundException();
            //}
            todo.Id = id;
            todo.LastModified = DateTime.Now;
            _db.Entry(_db.Todos.Find(id)).CurrentValues.SetValues(todo);
            try
            {
                _db.SaveChanges();
            }
            catch (DBConcurrencyException exception)
            {
                throw exception;
            }
        }

        public void PostTodo(Todo todo)
        {
            todo.Id = Guid.NewGuid();
            todo.CreationDate = DateTime.Now;
            todo.IsDeleted = false;
            _db.Todos.Add(todo);
            _db.SaveChanges();
        }

        public bool DeleteTodo(Guid id)
        {
            var todo = _db.Todos.Find(id);
            if (todo is null)
            {
                return false;
            }
            todo.IsDeleted = true;
            _db.SaveChanges();

            return true;
        }
    }
}