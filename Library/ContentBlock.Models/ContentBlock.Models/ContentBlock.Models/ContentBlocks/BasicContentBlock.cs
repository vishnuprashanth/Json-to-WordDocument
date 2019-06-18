using ContentBlock.Models.ContentBlocks.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentBlock.Models.ContentBlocks
{

    public interface IBasicContentBlock
    {
        [BsonElement("type")]
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        ContentBlockType Type { get; }
    }

    public class BasicContentBlock : IBasicContentBlock
    {
        [BsonElement("type")]
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public ContentBlockType Type { get; }

        public BasicContentBlock(ContentBlockType type)
        {
            Type = type;
        }


        public virtual bool HasSubElements
        {
            get
            {
                if (this.Type == ContentBlockType.function || this.Type == ContentBlockType.matrix)
                    return true;
                return false;
            }
        }

        public override string ToString()
        {
            return $"{this.Type} ";
        }
    }
}
