using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanosSaude.API.Models;

namespace PlanosSaude.API.Data.Configurations;

public class BeneficiarioConfiguration : IEntityTypeConfiguration<Beneficiario>
{
    public void Configure(EntityTypeBuilder<Beneficiario> builder)
    {
        builder.ToTable("beneficiarios");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(b => b.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.HasIndex(b => b.Cpf)
            .IsUnique();

        builder.Property(b => b.DataNascimento)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(b => b.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(b => b.Telefone)
            .HasMaxLength(20);

        builder.Property(b => b.IsAtivo)
            .IsRequired();

        builder.Property(b => b.DataCriacao)
            .IsRequired();
    }
}