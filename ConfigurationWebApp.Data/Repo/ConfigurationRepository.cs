using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationWebApp.Common;
using ConfigurationWebApp.Data.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ConfigurationWebApp.Data.Repo
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly ConfigurationContext _context = null;

        public ConfigurationRepository(IOptions<Settings> settings)
        {
            _context = new ConfigurationContext(settings);
        }

        public async Task<IEnumerable<ConfigurationEntitiy>> GetAllConfigurations()
        {
            try
            {
                return await _context.Configurations
                    .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ConfigurationEntitiy> GetConfiguration(int id)
        {
            try
            {

                return await _context.Configurations
                    .Find(config => config.ID == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public async Task<ConfigurationEntitiy> AddConfiguration(ConfigurationEntitiy item)
        {
            try
            {
                int lastId = _context.Configurations.AsQueryable().Max(c=>c.ID).Value;
                item.ID = lastId+1;
                await _context.Configurations.InsertOneAsync(item);
                return _context.Configurations.Find(c => c.ID == item.ID).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> RemoveConfiguration(int id)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Configurations.DeleteOneAsync(
                        Builders<ConfigurationEntitiy>.Filter.Eq("ID", id));

                return actionResult.IsAcknowledged
                       && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ConfigurationEntitiy> UpdateConfiguration(ConfigurationEntitiy item)
        {
          
            try
            {

                await _context.Configurations.ReplaceOneAsync(
                    doc => doc.ID == item.ID, item);
                return GetConfiguration(item.ID.ToInt32()).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> RemoveAllConfigurations()
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Configurations.DeleteManyAsync(new BsonDocument());
                return actionResult.IsAcknowledged
                       && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
