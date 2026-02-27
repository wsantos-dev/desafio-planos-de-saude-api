using Microsoft.EntityFrameworkCore;
using PlanosSaude.API.Models;

namespace PlanosSaude.API.Data
{
    public class PlanosSaudeDbContext : DbContext
    {
        public PlanosSaudeDbContext(DbContextOptions<PlanosSaudeDbContext> options)
         : base(options)
        {
        }

        public DbSet<Beneficiario> Beneficiarios => Set<Beneficiario>();
        public DbSet<Plano> Planos => Set<Plano>();
        public DbSet<Contratacao> Contratacoes => Set<Contratacao>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlanosSaudeDbContext).Assembly);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.IsOwned())
                    continue;

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(
                        property.GetColumnName().ToSnakeCase()
                    );
                }
            }
        }
    }
}