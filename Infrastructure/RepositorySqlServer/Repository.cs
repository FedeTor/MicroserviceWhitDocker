using Domain.Domain.Interfaces.IRepositorySqlServer;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.RepositorySqlServer
{
    public class Repository : IRepository
    {
        private readonly DbContextSqlServer _context;

        public Repository(DbContextSqlServer context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return _context.Products.ToList();
        }

        public async Task<Product> GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public async Task Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
