using Microsoft.EntityFrameworkCore;
using LojaCamisaGamer.Domain.Entities;
using LojaCamisaGamer.Domain.Interfaces;
using LojaCamisaGamer.Infrastructure.Data;

namespace LojaCamisaGamer.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _context.Categorias
                .AsNoTracking()
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Categoria?> GetByIdAsync(int id)
        {
            return await _context.Categorias
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Categoria?> GetByIdWithCamisasAsync(int id)
        {
            return await _context.Categorias
                .Include(c => c.Camisas)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Categoria>> SearchAsync(string termo)
        {
            return await _context.Categorias
                .AsNoTracking()
                .Where(c => c.Nome.Contains(termo) || c.Descricao.Contains(termo))
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Categoria> AddAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task UpdateAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Categorias.AnyAsync(c => c.Id == id);
        }
    }
}