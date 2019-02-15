using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BootCampProject.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext() : base("TodoContext")
        {
        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TodoContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}