using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByIdAsync(int id) => 
        await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<IEnumerable<Usuario>> GetAllAsync() => 
        await _context.Usuarios.ToListAsync();

    public async Task AddAsync(Usuario usuario) => 
        await _context.Usuarios.AddAsync(usuario);

    public void Update(Usuario usuario) => 
        _context.Usuarios.Update(usuario);

    public void Delete(Usuario usuario) => 
        _context.Usuarios.Remove(usuario);

    public async Task SaveChangesAsync() => 
        await _context.SaveChangesAsync();
}