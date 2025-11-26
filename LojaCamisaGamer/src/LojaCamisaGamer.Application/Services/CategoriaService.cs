using Mapster;
using LojaCamisaGamer.Application.Interfaces;
using LojaCamisaGamer.Application.ViewModels;
using LojaCamisaGamer.Domain.Entities;
using LojaCamisaGamer.Domain.Interfaces;

namespace LojaCamisaGamer.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoriaViewModel>> GetAllAsync()
        {
            var categorias = await _repository.GetAllAsync();
            return categorias.Adapt<IEnumerable<CategoriaViewModel>>();
        }

        public async Task<CategoriaViewModel?> GetByIdAsync(int id)
        {
            var categoria = await _repository.GetByIdAsync(id);
            return categoria?.Adapt<CategoriaViewModel>();
        }

        public async Task<CategoriaViewModel?> GetByIdWithCamisasAsync(int id)
        {
            var categoria = await _repository.GetByIdWithCamisasAsync(id);
            if (categoria == null) return null;

            var viewModel = categoria.Adapt<CategoriaViewModel>();
            viewModel.QuantidadeCamisas = categoria.Camisas?.Count ?? 0;
            return viewModel;
        }

        public async Task<IEnumerable<CategoriaViewModel>> SearchAsync(string termo)
        {
            var categorias = await _repository.SearchAsync(termo);
            return categorias.Adapt<IEnumerable<CategoriaViewModel>>();
        }

        public async Task<CategoriaViewModel> CreateAsync(CategoriaViewModel viewModel)
        {
            var categoria = viewModel.Adapt<Categoria>();
            categoria.DataCriacao = DateTime.Now;
            
            var created = await _repository.AddAsync(categoria);
            return created.Adapt<CategoriaViewModel>();
        }

        public async Task UpdateAsync(CategoriaViewModel viewModel)
        {
            var categoria = viewModel.Adapt<Categoria>();
            await _repository.UpdateAsync(categoria);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}