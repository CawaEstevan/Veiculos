using LojaCamisaGamer.Domain.Entities;

namespace LojaCamisaGamer.Domain.Interfaces
{
    public interface ICamisaRepository
    {
        Task<IEnumerable<Camisa>> GetAllAsync();
        Task<IEnumerable<Camisa>> GetAllWithCategoriaAsync();
        Task<Camisa?> GetByIdAsync(int id);
        Task<Camisa?> GetByIdWithCategoriaAsync(int id);
        Task<IEnumerable<Camisa>> GetByCategoriaIdAsync(int categoriaId);
        Task<IEnumerable<Camisa>> SearchAsync(string termo);
        Task<Camisa> AddAsync(Camisa camisa);
        Task UpdateAsync(Camisa camisa);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}