using Mapster;
using LojaCamisaGamer.Application.Interfaces;
using LojaCamisaGamer.Application.ViewModels;
using LojaCamisaGamer.Domain.Entities;
using LojaCamisaGamer.Domain.Interfaces;

namespace LojaCamisaGamer.Application.Services
{
    public class CamisaService : ICamisaService
    {
        private readonly ICamisaRepository _repository;

        public CamisaService(ICamisaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CamisaViewModel>> GetAllAsync()
        {
            var camisas = await _repository.GetAllWithCategoriaAsync();
            return camisas.Select(c =>
            {
                var vm = c.Adapt<CamisaViewModel>();
                vm.CategoriaNome = c.Categoria?.Nome;
                return vm;
            });
        }

        public async Task<CamisaViewModel?> GetByIdAsync(int id)
        {
            var camisa = await _repository.GetByIdWithCategoriaAsync(id);
            if (camisa == null) return null;

            var viewModel = camisa.Adapt<CamisaViewModel>();
            viewModel.CategoriaNome = camisa.Categoria?.Nome;
            return viewModel;
        }

        public async Task<IEnumerable<CamisaViewModel>> GetByCategoriaIdAsync(int categoriaId)
        {
            var camisas = await _repository.GetByCategoriaIdAsync(categoriaId);
            return camisas.Adapt<IEnumerable<CamisaViewModel>>();
        }

        public async Task<IEnumerable<CamisaViewModel>> SearchAsync(string termo)
        {
            var camisas = await _repository.SearchAsync(termo);
            return camisas.Select(c =>
            {
                var vm = c.Adapt<CamisaViewModel>();
                vm.CategoriaNome = c.Categoria?.Nome;
                return vm;
            });
        }

        public async Task<CamisaViewModel> CreateAsync(CamisaViewModel viewModel)
        {
            var camisa = viewModel.Adapt<Camisa>();
            camisa.DataCadastro = DateTime.Now;
            
            var created = await _repository.AddAsync(camisa);
            return created.Adapt<CamisaViewModel>();
        }

        public async Task UpdateAsync(CamisaViewModel viewModel)
        {
            var camisa = viewModel.Adapt<Camisa>();
            await _repository.UpdateAsync(camisa);
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