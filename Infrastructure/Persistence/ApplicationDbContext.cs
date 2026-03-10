using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(builder =>
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome).IsRequired().HasMaxLength(120);
            builder.OwnsOne(u => u.Email, e =>
            {
                e.Property(x => x.Address).HasColumnName("Email").IsRequired();
            });
            builder.Property(u => u.Status).HasConversion<string>();
        });
    }
}