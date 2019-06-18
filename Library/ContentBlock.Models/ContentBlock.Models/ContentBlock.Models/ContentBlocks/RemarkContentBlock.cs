using ContentBlock.Models.ContentBlocks.Enum;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ContentBlock.Models.ContentBlocks
{
    public class RemarkContentBlock : BasicContentBlock
    {

        [DefaultValue(true)]
        [JsonProperty("hasBorder")]
        [BsonElement("hasBorder")]
        public bool HasBorder { get; set; }

        /// <summary>
        /// The URL of the remark's icon.
        /// </summary>
        [JsonProperty("iconUrl")]
        [BsonElement("iconUrl")]
        public string IconUrl { get; set; }

        /// <summary>
        /// The information this remark contains.
        /// </summary>
        [JsonProperty("content")]
        [BsonElement("content")]
        public string Content { get; set; }

        public RemarkContentBlock() : base(ContentBlockType.Remark)
        {
            HasBorder = true;
            this.Content = Content;
            this.IconUrl = IconUrl;
        }

        public RemarkContentBlock(string iconUrl, string content, bool hasBorders) : base(ContentBlockType.Remark)
        {
            this.HasBorder = hasBorders;
            this.IconUrl = iconUrl;
            this.Content = content;
        }


        public override string ToString()
        {
            return Content;
        }
    }
}
