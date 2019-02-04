using ConfigurationWebApp.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationWebApp.Business
{
    public interface IConfigurationService
    {
        IEnumerable<ConfigurationViewModel> GetAll();
        ConfigurationViewModel UpdateConfiguration(ConfigurationViewModel model);
        bool DeleteConfiguration(int id);
        ConfigurationViewModel InsertConfiguration(ConfigurationViewModel model);

    }
}
