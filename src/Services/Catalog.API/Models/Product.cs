using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.API.Models
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; } = null!;
        [BsonElement("description")]
        public string Description { get; set; } = null!;
        [BsonElement("category")]
        public string Category { get; set; } = null!;
        [BsonElement("summary")]
        public string Summary { get; set; } = null!;
        [BsonElement("imageFile")] 
        public string ImageFile { get; set; } = null!;
        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}
