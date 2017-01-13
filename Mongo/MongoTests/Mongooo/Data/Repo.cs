using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongooo.Data
{
    internal class Repo
    {
        private Dictionary<string, IMongoCollection<BsonDocument>> _collectionCache;
        private static IMongoDatabase Database;

        public Repo()
        {
            _collectionCache = new Dictionary<string, IMongoCollection<BsonDocument>>();
            Database = MongoContext.Database;
        }

        public void GetCollection(string collectionName)
        {
            bool created;
            var collection = GetCollection(collectionName, out created);            
        }

        protected IMongoCollection<BsonDocument> GetCollection(string collectionName, out bool created)
        {
            IMongoCollection<BsonDocument> collection;
            created = false;
            if (_collectionCache.ContainsKey(collectionName))
                return _collectionCache[collectionName];
            else
            {
                bool exist = CollectionExists(collectionName);
                collection = Database.GetCollection<BsonDocument>(collectionName);

                created = !exist;

                _collectionCache.Add(collectionName, collection);

                return collection;
            }
        }

        public bool CollectionExists(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            //filter by collection name
            var collections = Database.ListCollections(new ListCollectionsOptions { Filter = filter });
            //check for existence
            return (collections.ToList()).Any();
        }
    }
    
}
