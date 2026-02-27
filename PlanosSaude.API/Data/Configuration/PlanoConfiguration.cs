using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanosSaude.API.Models;

namespace PlanosSaude.API.Data.Configurations;

public class PlanoConfiguration : IEntityTypeConfiguration<Plano>
{
    public void Configure(EntityTypeBuilder<Plano> builder)
    {
        builder.ToTable("planos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Codigo)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(p => p.Codigo)
            .IsUnique();

        builder.Property(p => p.CustoMensal)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Cobertura)
            .HasConversion<int>()
            .IsRequired();
    }
}