using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(120);
            
            // Value Object Mapping
            entity.OwnsOne(e => e.Email, email =>
            {
                email.Property(p => p.Address)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(200);
            });

            entity.Property(e => e.Status).IsRequired();
        });
    }
}