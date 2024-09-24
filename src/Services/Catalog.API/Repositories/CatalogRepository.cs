using Catalog.API.DatabaseSettings.Interfaces;
using Catalog.API.Models;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly IMongoCollection<Product> products;
        public CatalogRepository(ICatalogDatabaseSettings settings, IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);

            products = mongoDatabase.GetCollection<Product>(
                settings.CollectionName);
        }

        public async Task<List<Product>> GetAllAsync() =>
            await products.Find(_ => true).ToListAsync();

        public async Task<Product?> GetByIdAsync(string id) =>
            await products.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Product>> GetByCategoryAsync(string category) =>
            await products.Find(x => x.Category == category).ToListAsync();

        public async Task CreateAsync(Product newProduct) =>
            await products.InsertOneAsync(newProduct);

        public async Task UpdateAsync(string id, Product updatedProduct) =>
            await products.ReplaceOneAsync(x => x.Id == id, updatedProduct);

        public async Task RemoveAsync(string id) =>
            await products.DeleteOneAsync(x => x.Id == id);
    }
}
