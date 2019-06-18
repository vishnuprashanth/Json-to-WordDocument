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
    public class ImageContentBlock : BasicContentBlock
    {
        [JsonProperty("imageUrl")]
        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }


        /// <summary>
        /// Optional. The description that follows this image.
        /// </summary>
        [JsonProperty("content")]
        [BsonElement("content")]
        public string Content { get; set; }


        /// <summary>
        /// The horizontal alignment of the image.
        /// </summary>
        [JsonProperty("horizontalAlignment")]
        [BsonElement("horizontalAlignment")]
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public AlignmentEnum HorizontalAlignment { get; set; }

        
        public string Source { get; set; }

        public ImageContentBlock() : base(ContentBlockType.image)
        {
            HorizontalAlignment = AlignmentEnum.Center;
            this.Content = string.Empty;
            this.Source = "default.jpg";

        }

        public override string ToString()
        {
            return ToString();
        }

    }
}
