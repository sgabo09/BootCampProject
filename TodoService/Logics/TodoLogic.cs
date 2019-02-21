using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.OData;
using TodoService.Models;
using TodoService.Models.Enums;
using Guid = System.Guid;

namespace TodoService.Logics
{
    public class TodoLogic : ITodoInterface
    {
        private TodoContext _db = new TodoContext();
        private int _timeInterval = Convert.ToInt32(ConfigurationManager.AppSettings.GetValues("RecentTimeInterval"));


        public IQueryable<Todo> GetAllTodo()
        {
            return _db.Todos;
        }

        public Todo GetTodoById(Guid id)
        {
            return _db.Todos.Find(id);
        }

        public IEnumerable<Todo> GetTodosByCategory(CategoryEnum categoryId)
        {
            return _db.Todos.Where(c => categoryId == c.Category);
        }
        
        public List<TodoCategory> GetAllTodosByCategory()
        {
            var todoCategories = new List<TodoCategory>();

            foreach (CategoryEnum category in Enum.GetValues(typeof(CategoryEnum)))
            {
                todoCategories.Add(new TodoCategory{ Type = category});
            }

            foreach (var todo in _db.Todos)
            {
                foreach (var category in todoCategories)
                {
                    if (todo.Category == category.Type)
                    {
                        category.Issues.Add(todo);
                    }
                }                
            }

            return todoCategories;
        }

        public IEnumerable<Todo> GetRecentTodos()
        {
            var currentTime = DateTime.Now;
            return _db.Todos.Where(t => t.Status.Equals("Open") || DbFunctions.DiffMinutes(t.LastModified, currentTime) <= _timeInterval);
        }

        public IEnumerable<TreeNode> GetTodoTree()
        {
            var dictionary = new Dictionary<Guid, TreeNode>();
            var rootNodes = new List<TreeNode>();

            foreach (var todo in _db.Todos)
            {
                var treeNode = new TreeNode {Node = todo};

                dictionary.Add(todo.Id, treeNode);

                if (todo.ParentId == Guid.Empty)
                {
                    rootNodes.Add(treeNode);
                }

                foreach (var item in dictionary.Values)
                {
                    if (item.Node.ParentId == todo.Id)
                    {
                        treeNode.Children.Add(item);
                    }
                }
            }
            return rootNodes;
        }

        public bool PatchTodo(Guid id, Delta<Todo> todo)
        {
            var temp = _db.Todos.Find(id);
            if (temp is null)
            {
                return false;
            }
            
            temp.LastModified = DateTime.Now;
            todo.Patch(temp);
            _db.SaveChanges();

            return true;
        }

        public void PostTodo(Todo todo)
        {
            todo.Id = Guid.NewGuid();
            todo.CreationDate = DateTime.Now;
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
            _db.Todos.Remove(todo);
            _db.SaveChanges();

            return true;
        }
    }
}