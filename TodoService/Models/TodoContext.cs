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

        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TodoContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Filter("IsDeleted", (Todo t) => (t.IsDeleted), false);
        }
    }
}