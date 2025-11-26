using LojaCamisaGamer.Domain.Entities;

namespace LojaCamisaGamer.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<Categoria?> GetByIdAsync(int id);
        Task<Categoria?> GetByIdWithCamisasAsync(int id);
        Task<IEnumerable<Categoria>> SearchAsync(string termo);
        Task<Categoria> AddAsync(Categoria categoria);
        Task UpdateAsync(Categoria categoria);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}