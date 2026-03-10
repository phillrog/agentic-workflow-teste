using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Persistence.Repositories;

public class UsuarioRepository(ApplicationDbContext context) : IUsuarioRepository
{
    public async Task<Usuario?> GetByIdAsync(Guid id) => await context.Usuarios.FindAsync(id);
    
    public async Task<IEnumerable<Usuario>> GetAllAsync() => await context.Usuarios.ToListAsync();

    public async Task AddAsync(Usuario usuario)
    {
        await context.Usuarios.AddAsync(usuario);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        context.Usuarios.Update(usuario);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await GetByIdAsync(id);
        if (user != null)
        {
            context.Usuarios.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}