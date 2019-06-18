using ContentBlock.Models.ContentBlocks;
using ContentBlock.Models.ContentBlocks.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace ContentBlock.Models
{
    public static class ContentBlock_Parser
    {
        //Deserializes the JSON to a.NET object

        public static List<BasicContentBlock> ParseContentBlocksFromJson(string listContentBlockJson)
        {

            var contentBlockList = new List<BasicContentBlock>();
            dynamic jObject = JsonConvert.DeserializeObject(listContentBlockJson);
            var startPosition = jObject;
            if (listContentBlockJson.Contains("contentBlocks"))
            {
                startPosition = jObject["contentBlocks"];
            }


            foreach (var contentBlock in startPosition)
            {
                contentBlockList.Add(ParseContentBlock(contentBlock));
            }

            return contentBlockList;
        }

        public static BasicContentBlock ParseContentBlock(dynamic contentBlock)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            if (contentBlock != null)
            {
                try
                {
                    Enum.TryParse<ContentBlockType>(contentBlock.type.Value.ToString(), out ContentBlockType contentBlockType);

                    switch (contentBlockType)
                    {
                        case ContentBlockType.text:
                            return JsonConvert.DeserializeObject<TextContentBlock>(JsonConvert.SerializeObject(contentBlock, settings));
                        case ContentBlockType.image:
                            if (contentBlock.HorizontalAlignment == "")
                            {
                                contentBlock.HorizontalAlignment = "Center";
                            }
                            return JsonConvert.DeserializeObject<ImageContentBlock>(JsonConvert.SerializeObject(contentBlock, settings));
                        case ContentBlockType.Remark:
                            return JsonConvert.DeserializeObject<RemarkContentBlock>(JsonConvert.SerializeObject(contentBlock, settings));
                        case ContentBlockType.function:
                            var list = new List<BasicContentBlock>();
                            foreach (var content in contentBlock["contentBlocks"])
                            {
                                list.Add(ParseContentBlock(content));
                            }
                            var function = new FunctionContentBlock() { FunctionID = contentBlock["functionid"], FunctionName = contentBlock["title"], InputRange = contentBlock["inputRange"], Content = list };
                            return function;
                        case ContentBlockType.path:
                            return JsonConvert.DeserializeObject<PathContentBlock>(JsonConvert.SerializeObject(contentBlock, settings));
                        default:
                            return null;
                    }

                }
                catch (Exception)
                {
                    return null;
                }
            }


            return null;

        }
    }
}
