using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanosSaude.API.Models;

namespace PlanosSaude.API.Data.Configurations;

public class ContratacaoConfiguration : IEntityTypeConfiguration<Contratacao>
{
    public void Configure(EntityTypeBuilder<Contratacao> builder)
    {
        builder.ToTable("contratacoes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(c => c.DataInicio)
            .IsRequired();

        builder.HasOne(c => c.Beneficiario)
            .WithMany(b => b.Contratacoes)
            .HasForeignKey(c => c.BeneficiarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Plano)
            .WithMany(p => p.Contratacoes)
            .HasForeignKey(c => c.PlanoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}