using Catalog.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Repository
{
    public class CatalogRepository 
    {
        private readonly IMongoCollection<Product> _products;
        public CatalogRepository(IOptions<CatalogDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);

            _products = mongoDatabase.GetCollection<Product>(
                options.Value.CollectionName);
        }

        public async Task<List<Product>> GetAsync() =>
            await _products.Find(_ => true).ToListAsync();

        public async Task<Product?> GetByIdAsync(string id) =>
            await _products.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Product>> GetByCategoryAsync(string category) =>
            await _products.Find(x => x.Category == category).ToListAsync();

        public async Task CreateAsync(Product newProduct) =>
            await _products.InsertOneAsync(newProduct);

        public async Task UpdateAsync(string id, Product updatedProduct) =>
            await _products.ReplaceOneAsync(x => x.Id == id, updatedProduct);

        public async Task RemoveAsync(string id) =>
            await _products.DeleteOneAsync(x => x.Id == id);
    }
}
