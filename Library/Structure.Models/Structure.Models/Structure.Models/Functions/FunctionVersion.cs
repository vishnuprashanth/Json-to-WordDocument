using Models.Models.Functions;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Models.Models.Config.functionConfig
{
    [BsonIgnoreExtraElements]
    public class FunctionVersion : VersionEntity
    {
        [JsonProperty(PropertyName = "functions")]
        [BsonElement("functions")]
        public List<FunctionModel> Functions { get; set; }

        public FunctionVersion()
        {
            Functions = new List<FunctionModel>();
        }

    }
}
