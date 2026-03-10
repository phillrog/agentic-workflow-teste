using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(120)
            .HasColumnType("varchar(120)");

        // Configuração do Value Object Email como uma coluna simples (Conversion)
        builder.Property(x => x.Email)
            .HasConversion(
                v => v.Address,
                v => Domain.ValueObjects.Email.Create(v).Value)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");

        builder.Property(x => x.Status)
            .IsRequired();
    }
}