using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ProductService.MongoDbConfig
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Product> productCollection;

        public MongoDbService(IOptions<MongoDbSettings> productDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                productDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                productDatabaseSettings.Value.DatabaseName);

            productCollection = mongoDatabase.GetCollection<Product>(
                productDatabaseSettings.Value.CollectionName);
        }


        public async Task<List<Product>> GetAllAsync()
        {
            List<Product> products = await productCollection.Find(_ => true).ToListAsync();
            return products;
        }

        public async Task<ReplaceOneResult> UpdateAsync(Product product)
        {
            var result=await productCollection.ReplaceOneAsync(filter=>filter.id.Equals(product.id), product);
            return result;
        }


        public async Task CreateAsync(Product product)
        {
           await productCollection.InsertOneAsync(product);
        }

        public async Task<Product?> GetAsync(string id) {       
            return await productCollection.Find(x => x.id.Equals(id)).FirstOrDefaultAsync();        
        }

        public async Task<List<Product>> GetByNameAsync(string name)
        {
            var filter = new BsonDocument { { "name", new BsonDocument { { "$regex", name }, { "$options", "i" } } } };
            var product = await productCollection.Find(filter).ToListAsync();
            return product.ToList();
        }




    }
}
