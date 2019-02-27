using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http.OData;
using TodoService.Models;
using Guid = System.Guid;

namespace TodoService.Logics
{
    public class TodoLogic : ITodoInterface
    {
        private TodoContext _db = new TodoContext();
        private int _timeInterval = 0;

        public TodoLogic()
        { 
            int.TryParse(ConfigurationManager.AppSettings["RecentTimeInterval"], out _timeInterval);
        }

        public IQueryable<Todo> GetAllTodo()
        {
            return _db.Todos;
        }

        public Todo GetTodoById(Guid id)
        {
            return _db.Todos.Find(id);
        }

        public IEnumerable<Todo> GetTodosByCategory(Guid categoryId)
        {
            return _db.Todos.Where(c => categoryId == c.CategoryId);
        }
        
        public Dictionary<string, List<Todo>> GetAllTodosByCategory()
        {
            return _db.Todos.Include("Category").GroupBy(t => t.Category).ToDictionary(g => g.Key.Name, g => g.ToList());
        }

        public IEnumerable<Todo> GetRecentTodos()
        {
            DateTime recentInterval = DateTime.Now.AddMinutes(_timeInterval * -1);

            return _db.Todos.Where(t => t.Status.Equals("Open") || recentInterval <= t.LastModified);
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

        public List<Task> GetTasksByResponsible(string responsible)
        {
            var param1 = new SqlParameter("@Responsible", responsible);

            return _db.Database.SqlQuery<Task>("EXEC GetTasksByResponsible @Responsible", param1).ToList();
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