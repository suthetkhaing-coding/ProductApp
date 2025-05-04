using ProductApp.Models;

namespace ProductApp.Data
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetAllAsync();
        Task<ProductModel> GetByIdAsync(Guid id);
        Task AddAsync(ProductModel product);
        Task UpdateAsync(ProductModel product);
    }
}
