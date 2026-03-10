using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).HasMaxLength(120).IsRequired();
            
            // Mapeamento de Value Object
            entity.OwnsOne(e => e.Email, email =>
            {
                email.Property(p => p.Address)
                     .HasColumnName("Email")
                     .HasMaxLength(200)
                     .IsRequired();
            });

            entity.Property(e => e.Status).IsRequired();
        });
    }
}