using BootCampProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace BootCampProject.Controllers.API
{
    public class TodosController : ApiController
    {
        List<Todo> TodoList = new List<Todo>
        {
            new Todo("Todo1", 2, new DateTime(2019, 02, 13)),
            new Todo("Todo2", 3, new DateTime(2019, 03, 15), "Landing page","Developer Sándor","review", CategoryEnum.EPIC, 2)
        };

        //GET /api/todos
        public List<Todo> GetTodos()
        {
            return TodoList;
        }
    }
}
