using System;
using System.Collections.Generic;
using System.Text;
using ConfigurationWebApp.Common.ViewModels;
using ConfigurationWebApp.Data.Entities;
using MongoDB.Bson;

namespace ConfigurationWebApp.Common
{
    public static class ConvertingExtensions
    {
        public static ConfigurationViewModel ConvertToViewModel(this ConfigurationEntitiy config)
        {
            ConfigurationViewModel viewModel = new ConfigurationViewModel();
            viewModel.Name = config.Name;
            viewModel.ApplicationName = config.ApplicationName;
            viewModel.ID = config.ID.ToInt32();
            viewModel._id = config._id.ToString();
            viewModel.IsActive = config.IsActive;
            viewModel.Type = config.Type;
            viewModel.Value = config.Value;
            return viewModel;
        }

        public static ConfigurationEntitiy ConvertToEntity(this ConfigurationViewModel viewModel)
        {
            ConfigurationEntitiy config = new ConfigurationEntitiy();
            config.Name = viewModel.Name;
            config.ApplicationName = viewModel.ApplicationName;
            config.ID = viewModel.ID;
            config._id = !string.IsNullOrEmpty( viewModel._id )? ObjectId.Parse(viewModel._id) : ObjectId.Empty;
            config.IsActive = viewModel.IsActive;
            config.Type = viewModel.Type;
            config.Value = viewModel.Value;
            return config;
        }
    }
}
