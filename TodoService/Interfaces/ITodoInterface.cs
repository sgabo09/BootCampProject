using System;
using System.Collections.Generic;
using System.Linq;
using TodoService.Models;

namespace TodoService.Interfaces
{
    public interface ITodoInterface
    {
        IQueryable<Todo> GetAllTodo();
        Todo GetTodoById(Guid id);
        IEnumerable<Todo> GetTodosByCategory(Guid categoryId);
        List<Category> GetAllTodosByCategory();
        IEnumerable<Todo> GetRecentTodos();
        IEnumerable<TreeNode> GetTodoTree();
        List<Task> GetTasksByResponsible(string responsible);
        void PostTodo(Todo todo);
        void PatchTodo(Guid id, Todo todo);
        bool DeleteTodo(Guid id);
    }
}
