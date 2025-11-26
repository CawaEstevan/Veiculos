using Microsoft.EntityFrameworkCore;
using LojaCamisaGamer.Domain.Entities;
using LojaCamisaGamer.Domain.Interfaces;
using LojaCamisaGamer.Infrastructure.Data;

namespace LojaCamisaGamer.Infrastructure.Repositories
{
    public class CamisaRepository : ICamisaRepository
    {
        private readonly AppDbContext _context;

        public CamisaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Camisa>> GetAllAsync()
        {
            return await _context.Camisas
                .AsNoTracking()
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Camisa>> GetAllWithCategoriaAsync()
        {
            return await _context.Camisas
                .Include(c => c.Categoria)
                .AsNoTracking()
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Camisa?> GetByIdAsync(int id)
        {
            return await _context.Camisas
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Camisa?> GetByIdWithCategoriaAsync(int id)
        {
            return await _context.Camisas
                .Include(c => c.Categoria)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Camisa>> GetByCategoriaIdAsync(int categoriaId)
        {
            return await _context.Camisas
                .AsNoTracking()
                .Where(c => c.CategoriaId == categoriaId)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Camisa>> SearchAsync(string termo)
        {
            return await _context.Camisas
                .Include(c => c.Categoria)
                .AsNoTracking()
                .Where(c => c.Nome.Contains(termo) || 
                           c.Descricao.Contains(termo) ||
                           c.Categoria.Nome.Contains(termo))
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Camisa> AddAsync(Camisa camisa)
        {
            await _context.Camisas.AddAsync(camisa);
            await _context.SaveChangesAsync();
            return camisa;
        }

        public async Task UpdateAsync(Camisa camisa)
        {
            _context.Camisas.Update(camisa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var camisa = await _context.Camisas.FindAsync(id);
            if (camisa != null)
            {
                _context.Camisas.Remove(camisa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Camisas.AnyAsync(c => c.Id == id);
        }
    }
}