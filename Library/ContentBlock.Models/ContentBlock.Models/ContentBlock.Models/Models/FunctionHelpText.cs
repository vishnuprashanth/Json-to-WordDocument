using ContentBlock.Models.ContentBlocks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ContentBlock.Models.Models
{
    [BsonIgnoreExtraElements]
    public class FunctionHelpText
    {
        [BsonElement("product")]
        [JsonProperty("product")]
        public string Product { get; set; }

        [BsonElement("device")]
        [JsonProperty("device")]
        public string Device { get; set; }

        [BsonElement("version")]
        [JsonProperty("version")]
        public string Version { get; set; }

        [BsonElement("functionId")]
        [JsonProperty("functionId")]
        public string FunctionId { get; set; }

        [BsonElement("functionNumber")]
        [JsonProperty("functionNumber")]
        public string FunctionNumber { get; set; }

        [BsonElement("contentBlocks")]
        [JsonProperty("contentBlocks")]
        public List<BasicContentBlock> ContentBlocks { get; set; }

        public FunctionHelpText()
        {
            ContentBlocks = new List<BasicContentBlock>();
        }
    }
}
