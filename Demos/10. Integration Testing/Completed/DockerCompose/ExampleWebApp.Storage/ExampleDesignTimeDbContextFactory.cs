using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExampleWebApp.Storage
{
    public class ExampleDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExampleContext>
    {
        public ExampleContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExampleContext>();
            optionsBuilder.UseMySql("server=localhost;port=3307;database=SinanceDev;user=root;password=my-secret-pw;");

            return new ExampleContext(optionsBuilder.Options);
        }
    }
}