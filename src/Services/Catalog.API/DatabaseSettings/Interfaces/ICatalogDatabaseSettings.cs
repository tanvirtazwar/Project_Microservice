namespace Catalog.API.DatabaseSettings.Interfaces
{
    public interface ICatalogDatabaseSettings
    {
        string ConnectionString { get; set; } 

        string DatabaseName { get; set; } 

        string CollectionName { get; set; } 
    }
}
