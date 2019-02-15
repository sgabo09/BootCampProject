using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using TodoService.Models;

namespace TodoService
{
    public interface ITodoInterface
    {
        IHttpActionResult GetTodos();
        IHttpActionResult GetTodo(Guid id);
        IHttpActionResult PatchTodo(Guid id, [FromBody] Delta<Todo> todo);
        IHttpActionResult PostTodo([FromBody] Todo todo);
        IHttpActionResult DeleteTodo(Guid id);
    }
}
