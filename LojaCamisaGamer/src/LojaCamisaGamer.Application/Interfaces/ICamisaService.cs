using LojaCamisaGamer.Application.ViewModels;

namespace LojaCamisaGamer.Application.Interfaces
{
    public interface ICamisaService
    {
        Task<IEnumerable<CamisaViewModel>> GetAllAsync();
        Task<CamisaViewModel?> GetByIdAsync(int id);
        Task<IEnumerable<CamisaViewModel>> GetByCategoriaIdAsync(int categoriaId);
        Task<IEnumerable<CamisaViewModel>> SearchAsync(string termo);
        Task<CamisaViewModel> CreateAsync(CamisaViewModel viewModel);
        Task UpdateAsync(CamisaViewModel viewModel);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
