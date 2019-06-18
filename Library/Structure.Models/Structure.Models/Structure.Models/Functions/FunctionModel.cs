using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Models.Models.Functions
{
    [BsonIgnoreExtraElements]
    public class FunctionModel
    {

        //made nullable int

        [BsonElement("functionId")]
        [JsonProperty(PropertyName = "functionId")]
        public string FunctionId { get; set; }

        [BsonElement("functionNumber")]
        [JsonProperty(PropertyName = "functionNumber")]
        public int? FunctionNumber { get; set; }

        [BsonElement("HelpTextId")]
        [JsonProperty(PropertyName = "helpTextId")]
        public string HelpTextId { get; set; }

        [BsonElement("groupReference")]
        [JsonProperty(PropertyName = "groupReference")]
        public string GroupReference { get; set; }

        [BsonElement("category")]
        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [BsonElement("description")]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [BsonElement("settingOrMeasurement")]
        [JsonProperty(PropertyName = "settingOrMeasurement")]
        public string SettingOrMeasurement { get; set; }

        [BsonElement("conversion")]
        [JsonProperty(PropertyName = "conversion")]
        public string Conversion { get; set; }

        [BsonElement("minValue")]
        [JsonProperty(PropertyName = "minValue")]
        public int? MinValue { get; set; }

        [BsonElement("maxValue")]
        [JsonProperty(PropertyName = "maxValue")]
        public int? MaxValue { get; set; }

        [BsonElement("defaultValue")]
        [JsonProperty(PropertyName = "defaultValue")]
        public int? DefaultValue { get; set; }

        [BsonElement("functionAttributes")]
        [JsonProperty(PropertyName = "functionAttributes")]
        public List<dynamic> FunctionAttributes { get; set; }


        public FunctionModel()
        {
            FunctionAttributes = new List<dynamic>();
            HelpTextId = "";
        }





    }
}
