using ExampleWebApp.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExampleWebApp.Storage
{
    public class ExampleContext : DbContext
    {
        public DbSet<ExampleEntity> Examples { get; set; }

        public ExampleContext(DbContextOptions<ExampleContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}