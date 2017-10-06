using MongoDB.Bson;
using MongoDB.Driver;

namespace Common
{
    public class MongoContext
    {
        private IMongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }

        private static MongoContext _mongoContext;
        private MongoContext()
        {

        }

        public static MongoContext Create()
        {
            if (_mongoContext == null)
            {
                string connectionString = @"mongodb://localhost:27017";
                string databaseName = "rabbitmq_demo";
                _mongoContext = new MongoContext();
                _mongoContext.Client = new MongoClient(connectionString);
                _mongoContext.Database = _mongoContext.Client.GetDatabase(databaseName);
            }
            return _mongoContext;
        }

        public bool CollectionExists(string collectionName)
        {
            bool exists = false;
            var filter = new BsonDocument("name", collectionName);
            var collections = _mongoContext.Database.ListCollections(new ListCollectionsOptions { Filter = filter });
            exists = collections.Any();
            return exists;
        }

        public IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            return _mongoContext.Database.GetCollection<BsonDocument>(collectionName);
        }
        public void CreateCollection(string collectionName, CreateCollectionOptions options)
        {
            _mongoContext.Database.CreateCollection(collectionName, options);
        }      

        public void DropCollection(string collectionName)
        {
            _mongoContext.Database.DropCollection(collectionName);
        }

        public void DeleteMany(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            _mongoContext.GetCollection(collectionName).DeleteMany(filter);
        }
    }
}
