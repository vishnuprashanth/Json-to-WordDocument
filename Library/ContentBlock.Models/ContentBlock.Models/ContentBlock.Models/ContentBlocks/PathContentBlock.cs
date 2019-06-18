using ContentBlock.Models.ContentBlocks.Enum;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentBlock.Models.ContentBlocks
{
    public class PathContentBlock : BasicContentBlock
    {
        /// <summary>
        /// Route holds the intermediate steps to reach a specific functionality.
        /// </summary>
        [JsonProperty("route")]
        [BsonElement("route")]
        public string Route { get; set; }

        public PathContentBlock() : base(ContentBlockType.path)
        {
            this.Route = null;

        }
        public PathContentBlock(string steps) : base(ContentBlockType.path)
        {
            this.Route = null;
        }

        public override string ToString()
        {


            //return this.Route.Replace(".", "->");
            return "Path: " + this.Route.Replace(".", "->");
        }


    }
}
