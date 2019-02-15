using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BootCampProject.Models;

namespace BootCampProject.DAL
{
    public class TodoContext : DbContext
    {
        public TodoContext() : base("TodoDB")
        {

        }

        public DbSet<Todo> Todos { get; set; }

    }
}