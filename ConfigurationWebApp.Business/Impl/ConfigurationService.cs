using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigurationWebApp.Common;
using ConfigurationWebApp.Common.ViewModels;
using ConfigurationWebApp.Data;
using ConfigurationWebApp.Data.Entities;
using SharpRepository.Repository;

namespace ConfigurationWebApp.Business.Impl
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRepository _configurationRepository;
        public ConfigurationService(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        public bool DeleteConfiguration(int id)
        {
            return _configurationRepository.RemoveConfiguration(id).Result;
        }

        public IEnumerable<ConfigurationViewModel> GetAll()
        {
           return _configurationRepository.GetAllConfigurations().Result.Select(c=>c.ConvertToViewModel());
        }

        public ConfigurationViewModel InsertConfiguration(ConfigurationViewModel model)
        {
            return _configurationRepository.AddConfiguration(model.ConvertToEntity()).Result.ConvertToViewModel();
        }

        public ConfigurationViewModel UpdateConfiguration(ConfigurationViewModel model)
        {
            return _configurationRepository.UpdateConfiguration(model.ConvertToEntity()).Result.ConvertToViewModel();
        }
    }
}
