using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Common
{
    public interface IRawDataRepository
    {
        void Create(int siteID, int assetID);
        void Insert(int siteID, int assetID, string measurementLocationID, DateTime eventDate, double value);

        IEnumerable<BsonDocument> Get(int siteID, int assetID, string measurementLocationID, double value, DateTime startDate, DateTime endDate);
        IEnumerable<BsonDocument> Get(int siteID, int assetID, string measurementLocationID,
            DateTime startDate, DateTime endDate);

        long GetCount(int siteID, int assetID, string measurementLocationID,
            DateTime startDate, DateTime endDate);
        BsonDocument Get(int siteID, int assetID, string objectID);

        void Drop(int siteID, int assetID);
    }
}
