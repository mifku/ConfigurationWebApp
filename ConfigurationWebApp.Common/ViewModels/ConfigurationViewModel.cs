using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationWebApp.Common.ViewModels
{
    public class ConfigurationViewModel
    {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string _id { get; set; }

        [JsonProperty("id" , NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("type",NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("isActive", NullValueHandling = NullValueHandling.Ignore)]
        public int IsActive { get; set; }

        [JsonProperty("applicationName", NullValueHandling = NullValueHandling.Ignore)]
        public string ApplicationName { get; set; }
    }
}
