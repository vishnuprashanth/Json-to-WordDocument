using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace ContentBlock.Models.ContentBlocks
{
    public class ContentBlocksList
    {
        [JsonProperty("contentBlocks")]
        public List<BasicContentBlock> ContentBlocks { get; set; }

        public ContentBlocksList()
        {
            this.ContentBlocks = new List<BasicContentBlock>();
        }

        public ContentBlocksList(int capacity)
        {
            this.ContentBlocks = new List<BasicContentBlock>(capacity);
        }

        public override string ToString()
        {
            if (ContentBlocks.Count == 0)
            {
                return "no contents";
            }

            StringBuilder sb = new StringBuilder();
            foreach (BasicContentBlock element in ContentBlocks)
            {
                sb.AppendFormat("{0} \n", element.ToString());
            }
            return sb.ToString();
        }
    }
}
