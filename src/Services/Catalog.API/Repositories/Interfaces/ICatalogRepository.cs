using Catalog.API.Models;

namespace Catalog.API.Repositories.Interfaces
{
    public interface ICatalogRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(string id);
        Task<List<Product>> GetByCategoryAsync(string category);
        Task CreateAsync(Product newProduct);
        Task UpdateAsync(string id, Product updatedProduct);
        Task RemoveAsync(string id);
    }
}
