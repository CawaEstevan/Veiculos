using LojaCamisaGamer.Application.ViewModels;

namespace LojaCamisaGamer.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaViewModel>> GetAllAsync();
        Task<CategoriaViewModel?> GetByIdAsync(int id);
        Task<CategoriaViewModel?> GetByIdWithCamisasAsync(int id);
        Task<IEnumerable<CategoriaViewModel>> SearchAsync(string termo);
        Task<CategoriaViewModel> CreateAsync(CategoriaViewModel viewModel);
        Task UpdateAsync(CategoriaViewModel viewModel);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}