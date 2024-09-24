using Catalog.API.DatabaseSettings.Interfaces;

namespace Catalog.API.DatabaseSettings
{
    public class CatalogDatabaseSettings : ICatalogDatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CollectionName { get; set; } = string.Empty;
    }
}
