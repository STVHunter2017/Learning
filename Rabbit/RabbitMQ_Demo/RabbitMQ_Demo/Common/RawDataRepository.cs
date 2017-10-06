using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Common
{
    public class RawDataRepository : IRawDataRepository
    {
        private MongoContext _mongoContext;
        private readonly int _maxDocuments = 10000;
        private readonly long _maxSize = 10000 * 100;
        public RawDataRepository()
        {
            _mongoContext = MongoContext.Create();
        }
        public void Create(int siteID, int assetID)
        {
            string collectionName = GetCollectionName(siteID, assetID);
            var options = new CreateCollectionOptions
            {
                Capped = true,
                MaxDocuments = _maxDocuments,
                MaxSize = _maxSize
            };
            bool exists = _mongoContext.CollectionExists(collectionName);
            if (!exists)
            {
                _mongoContext.DropCollection(collectionName);
                _mongoContext.CreateCollection(collectionName, options);
                IMongoCollection<BsonDocument> collection = _mongoContext.GetCollection(collectionName);
                var indexKeysDefiniton = Builders<BsonDocument>.IndexKeys.Combine(
                            Builders<BsonDocument>.IndexKeys.Ascending("meas_loc_id"));
                collection.Indexes.CreateOne(indexKeysDefiniton, new CreateIndexOptions()
                {
                    Unique = false,
                    Sparse = true
                });
            }
        }
        public void Drop(int siteID, int assetID)
        {
            string collectionName = GetCollectionName(siteID, assetID);
            _mongoContext.DropCollection(collectionName);
        }

        public void Insert(int siteID, int assetID, string measurementLocationID, DateTime eventDate, double value)
        {
            string collectionName = GetCollectionName(siteID, assetID);
            BsonDocument insertDocument = GetInsertBsonDocument(measurementLocationID, eventDate, value);
            _mongoContext.GetCollection(collectionName).InsertOne(insertDocument);
        }

        public IEnumerable<BsonDocument> Get(int siteID, int assetID, string measurementLocationID, double value, 
            DateTime startDate, DateTime endDate)
        {            
            string collectionName = GetCollectionName(siteID, assetID);
            var project = new BsonDocument
                {
                    { "meas_loc_id", 1 },
                    { "event_date", 1 },
                    { "value", 1 }
                };
            var match = new BsonDocument
                {
                    { "meas_loc_id", measurementLocationID },
                    { "event_date", new BsonDocument { { "$gte", startDate }, {"$lte", endDate } } },
                    { "value", new BsonDocument { { "$gt", value } } }
                };
           
            IEnumerable<BsonDocument> results = _mongoContext.GetCollection(collectionName)
                    .Find(match).Project(project).ToEnumerable<BsonDocument>();
            
            return results;
        }
        public IEnumerable<BsonDocument> Get(int siteID, int assetID, string measurementLocationID, DateTime startDate, DateTime endDate)
        {
            string collectionName = GetCollectionName(siteID, assetID);
            var project = new BsonDocument
                {
                    { "meas_loc_id", 1 },                  
                    { "event_date", 1 },
                    { "value", 1 }
                };
            var match = new BsonDocument
                {
                    { "meas_loc_id", measurementLocationID },                    
                    { "event_date", new BsonDocument { { "$gte", startDate }, {"$lte", endDate } } }
                };
            
            IEnumerable<BsonDocument> results = _mongoContext.GetCollection(collectionName)
                    .Find(match).Project(project).ToEnumerable<BsonDocument>();
            return results;
        }

        public long GetCount(int siteID, int assetID, string measurementLocationID, DateTime startDate, DateTime endDate)
        {
            string collectionName = GetCollectionName(siteID, assetID);
            var match = new BsonDocument
                {
                    { "meas_loc_id", measurementLocationID },                    
                    { "event_date", new BsonDocument { { "$gte", startDate }, {"$lt", endDate } } }
                };
            long numOfRecords = _mongoContext.GetCollection(collectionName)
                    .Find(match).Count();
            return numOfRecords;
        }

        public BsonDocument Get(int siteID, int assetID, string objectID)
        {
            string collectionName = GetCollectionName(siteID, assetID);
            var match = new BsonDocument
                {
                    { "_id", new BsonObjectId(new ObjectId(objectID)) }
                };
            return _mongoContext.GetCollection(collectionName).Find(match)?.First();
        }
        #region private methods
        private string GetCollectionName(int siteID, int assetID)
        {
            return string.Format("site_{0}_asset_{1}_raw", siteID, assetID);
        }
        private BsonDocument GetInsertBsonDocument(string measurementLocationID, DateTime eventDate, double value)
        {                  
            BsonDocument document = new BsonDocument();
            document.Add("meas_loc_id", measurementLocationID);
            document.Add("event_date", new BsonDateTime(eventDate));            
            document.Add("value", value);

            return document;
        }
        #endregion
    }
}
