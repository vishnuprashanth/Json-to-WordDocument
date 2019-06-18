using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{

    public interface IVersionEntity
    {
        [JsonProperty("product")]
        [BsonElement("product")]
        string Product { get; set; }

        [JsonProperty("device")]
        [BsonElement("device")]
        string Device { get; set; }

        [JsonProperty("version")]
        [BsonElement("version")]
        string Version { get; set; }
    }


    public class VersionEntity : IVersionEntity
    {

        [JsonProperty("product")]
        [BsonElement("product")]
        public string Product { get; set; }

        [JsonProperty("device")]
        [BsonElement("device")]
        public string Device { get; set; }

        [JsonProperty("version")]
        [BsonElement("version")]
        public string Version { get; set; }
    }
}
