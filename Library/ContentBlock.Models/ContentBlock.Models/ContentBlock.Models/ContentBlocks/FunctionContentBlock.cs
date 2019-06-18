using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentBlock.Models.ContentBlocks
{
    public class FunctionContentBlock:BasicContentBlock
    {
        /// <summary>
        /// The ID of this function.
        /// </summary>
        /// 
        [JsonProperty("functionid")]
        public int FunctionID { get; set; }

        /// <summary>
        /// The title of this function.
        /// </summary>
        /// 
        [JsonProperty("title")]
        public string FunctionName { get; set; } // Function name


        /// <summary>
        /// The input range for this function. It is a string as it can take many different types of values (ints, bools, descrete)
        /// </summary>
        /// 
        [JsonProperty("inputRange")]
        public string InputRange { get; set; }

        /// <summary>
        /// Contains all the subblocks of this function (images, text, chapters etc).
        /// </summary>
        public List<BasicContentBlock> Content { get; set; }
        /// <summary>
        /// Default constructor. Sets the FinctionID to 0.
        /// </summary>

        public FunctionContentBlock() : base(Enum.ContentBlockType.function)
        {
            this.FunctionID = int.MaxValue;
            this.FunctionName = string.Empty;
            this.InputRange = string.Empty;
            this.Content = new List<BasicContentBlock>();
        }

        public FunctionContentBlock(int funcID, string title, List<BasicContentBlock> content) : base(Enum.ContentBlockType.function)
        {

            this.FunctionID = funcID;
            this.FunctionName = title;
            this.InputRange = string.Empty;
            this.Content = content;
        }
        public override bool HasSubElements
        {
            get
            {
                if (this.Content.Count == 0)
                    return false;
                return true;
            }
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(@"Title: {0}, inputRange: {1}, functionid:{2},contentBlocks: {3} ", FunctionName, InputRange, FunctionID, Content.ToString()).Replace(".", "->");
        }
    }
}

