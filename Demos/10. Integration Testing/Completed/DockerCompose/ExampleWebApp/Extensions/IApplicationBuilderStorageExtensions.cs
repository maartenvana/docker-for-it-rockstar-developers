using ExampleWebApp.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ExampleWebApp.Extensions
{
    public static class IApplicationBuilderStorageExtensions
    {
        public static IApplicationBuilder ApplyDataSeed(this IApplicationBuilder appBuilder)
        {
            Task.Run(async () =>
            {
                var context = appBuilder.ApplicationServices.GetRequiredService<ExampleContext>();

                var seedValue = "integration testing";

                if (!await context.Examples.AnyAsync(x => x.Value == seedValue))
                {
                    context.Examples.Add(new Storage.Entities.ExampleEntity
                    {
                        Value = seedValue
                    });

                    await context.SaveChangesAsync();
                }
            }).Wait();

            return appBuilder;
        }

        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder appBuilder)
        {
            var context = appBuilder.ApplicationServices.GetRequiredService<ExampleContext>();

            context.Database.Migrate();

            return appBuilder;
        }
    }
}