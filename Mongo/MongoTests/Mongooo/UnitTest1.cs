using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mongooo.Data;

namespace Mongooo
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string _mongoDbName;

        public UnitTest1()
        {
            _mongoDbName = _mongoDbName + "STV";
            MongoContext.SetDbName(_mongoDbName);
        }


        private Repo repo;

        [TestInitialize]
        public void Setup()
        {
            this.repo = new Repo();
        }

        [TestMethod]
        public void CanConnectToMongo()
        {
            this.repo.GetCollection("STV1");

        }
    }
}
