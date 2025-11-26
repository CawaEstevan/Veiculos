using LojaCamisaGamer.Domain.Interfaces;
using LojaCamisaGamer.Infrastructure.Data;
using LojaCamisaGamer.Infrastructure.Repositories;

namespace LojaCamisaGamer.Infrastructure.Factories
{
    public class RepositoryFactory
    {
        private readonly AppDbContext _context;

        public RepositoryFactory(AppDbContext context)
        {
            _context = context;
        }

        public ICategoriaRepository CreateCategoriaRepository()
        {
            return new CategoriaRepository(_context);
        }

        public ICamisaRepository CreateCamisaRepository()
        {
            return new CamisaRepository(_context);
        }
    }
}