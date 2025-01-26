using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GreenLebanon.Taxi.Infrastructure.Data
{
    // Why is This Needed?
    // The EF Core tools(dotnet ef migrations add, dotnet ef database update) run outside the application's runtime and cannot rely on dependency injection.
    // This factory provides a fallback mechanism for the tools to resolve the DbContext.
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();

            optionsBuilder.UseSqlServer("Data Source=HARB\\SQLEXPRESS;Initial Catalog=GreenLebanonWasmDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
