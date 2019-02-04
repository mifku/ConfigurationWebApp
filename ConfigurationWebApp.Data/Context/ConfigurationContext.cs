using System;
using System.Collections.Generic;
using System.Text;
using ConfigurationWebApp.Data.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ConfigurationWebApp.Common;


namespace ConfigurationWebApp.Data
{
    public class ConfigurationContext
    {
        private readonly IMongoDatabase _database = null;

        public ConfigurationContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<ConfigurationEntitiy> Configurations
        {
            get
            {
                return _database.GetCollection<ConfigurationEntitiy>("Configurations");
            }
        }
    }
}
