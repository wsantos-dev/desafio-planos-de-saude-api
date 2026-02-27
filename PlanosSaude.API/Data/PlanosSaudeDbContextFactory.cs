using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PlanosSaude.API.Data;

public class PlanosSaudeDbContextFactory : IDesignTimeDbContextFactory<PlanosSaudeDbContext>
{
    public PlanosSaudeDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var builder = new DbContextOptionsBuilder<PlanosSaudeDbContext>();
        var connectionString = configuration.GetConnectionString("PlanosSaudeConnection");

        builder.UseNpgsql(connectionString);

        return new PlanosSaudeDbContext(builder.Options);
    }
}