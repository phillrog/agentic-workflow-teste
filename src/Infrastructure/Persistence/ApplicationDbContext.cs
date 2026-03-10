using Microsoft.EntityFrameworkCore;
using Domain.Entities; // Ajuste conforme seu namespace real
using Domain.Common;

namespace Infrastructure.Persistence;

/// <summary>
/// Implementação do DbContext utilizando Clean Architecture.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Exemplo de DbSet
    // public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas as configurações de IEntityTypeConfiguration no assembly de Infrastructure
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Lógica para atualizar datas de auditoria ou disparar Domain Events aqui
        return await base.SaveChangesAsync(cancellationToken);
    }
}