using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EntityFramework.DynamicFilters;

namespace TodoService.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext() : base("TodoContext")
        {
        }

        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TodoContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Filter("IsDeleted", (Todo t) => (t.IsDeleted), false);
        }
    }
}