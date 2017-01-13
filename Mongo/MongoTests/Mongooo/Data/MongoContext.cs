using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Mongooo.Data
{
    public class MongoContext
    {
        private static IMongoClient _client;
        private static IMongoDatabase _database;
        private static string _dbName = "";

        public static IMongoDatabase Database
        {
            get
            {
                Setup();
                return _database;
            }
        }

        public static void Setup()
        {
            if (_client == null || _database == null)
            {
                _client = new MongoClient();

                //string dbName = ConfigurationManagerUtil.GetAppSetting<string>("MongoDBConfig:DbName", "pds", false);
                //string environment = ConfigurationManagerUtil.GetAppSetting<string>("Environment", "prod", false);
                //if (_dbName == "")
                //{
                //    _dbName = dbName + "_" + environment;
                //}

                _database = _client.GetDatabase(_dbName);

            }
        }

        // this is used to override the default db name for testing purposes
        public static void SetDbName(string name)
        {
            _dbName = name;
        }
    }
}
