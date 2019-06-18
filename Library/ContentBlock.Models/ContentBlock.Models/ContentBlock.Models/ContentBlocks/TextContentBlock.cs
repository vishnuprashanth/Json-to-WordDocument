using ContentBlock.Models.ContentBlocks.Enum;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace ContentBlock.Models.ContentBlocks
{
    public class TextContentBlock : BasicContentBlock
    {
        [JsonProperty("content")]
        [BsonElement("content")]
        public string Content { get; set; }

        [JsonProperty("font")]
        [BsonElement("font")]
        public List<Pattern> Font { get; set; }

        public TextContentBlock() : base(ContentBlockType.text)
        {
            this.Font = new List<Pattern>();
            this.Content = string.Empty;
        }
        public TextContentBlock(string content) : base(ContentBlockType.text)
        {
            this.Content = content;
            this.Font = new List<Pattern>();
        }
        public TextContentBlock(string content, List<Pattern> font) : base(ContentBlockType.text)
        {
            this.Content = content;
            this.Font = font;
        }
        public TextContentBlock(string content, Pattern font) : base(ContentBlockType.text)
        {
            this.Content = content;
            this.Font = new List<Pattern>();
            this.Font.Add(font);
        }

        public override string ToString()
        { 
            return Content;
        }
    }





    public enum Pattern
    {
        Bold,                     // ** Hi **
        Asterisk,
        Italic,                  // ` Hi `         
        Underline,              //_ Hi _  
        BulletList,
        heading1
    }
   
}


    
  

    

