using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ConfigurationWebApp.Business;
using ConfigurationWebApp.Common.ViewModels;
using ConfigurationWebApp.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationWebApp.Controllers
{
    [Route("api/configuration")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;
        public ConfigurationController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpGet("all")]
        public IActionResult GetAllConfigurations()
        {
            return Ok( _configurationService.GetAll());
        }

        // GET: api/Configuration/5
        [HttpPost("add")]
        public IActionResult AddConfiguratio([FromBody] ConfigurationViewModel model )
        {
            return Ok(_configurationService.InsertConfiguration(model));
        }

        // POST: api/Configuration
        [HttpPost("update")]
        public IActionResult UpdateConfiguratio([FromBody] ConfigurationViewModel model)
        {
            return Ok(_configurationService.UpdateConfiguration(model));
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_configurationService.DeleteConfiguration(id));
        }
    }
}
