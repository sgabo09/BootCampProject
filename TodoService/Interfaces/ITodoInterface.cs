using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using TodoService.Models;

namespace TodoService
{
    public interface ITodoInterface
    {
        IQueryable<Todo> GetAllTodo();
        Todo GetTodoById(Guid id);
        bool DeleteTodo(Guid id);
    }
}
