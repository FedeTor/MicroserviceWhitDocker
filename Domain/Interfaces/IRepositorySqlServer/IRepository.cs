using Domain.Entities;

namespace Domain.Domain.Interfaces.IRepositorySqlServer
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }
}
