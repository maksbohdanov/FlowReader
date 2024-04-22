using FlowReader.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FlowReader.DataAccess.Persistence
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser> // TODO roles
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<TodoItem> TodoItems { get; set; }

        //public DbSet<TodoList> TodoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
