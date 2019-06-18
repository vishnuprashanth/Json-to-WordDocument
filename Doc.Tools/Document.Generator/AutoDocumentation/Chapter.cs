using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using ContentBlock.Models.Models;
using ContentBlock.Models.ContentBlocks;

namespace Microfan.AutoDocumentation.Dll
{
    public class Chapter
    {
        public string Title { get; set; }
        
        /// <summary>
        /// The content blocks of this chapter.
        /// </summary>

        public List<Chapter> Chapters { get; set; }
        public List<ContentBlocksList> ChapterContents { get; set; }


        public Chapter()
        {
            this.Title = "";
            this.ChapterContents = new List<ContentBlocksList>();
        }

        public Chapter(string title)
        {
            this.Title = title;
            this.ChapterContents = new List<ContentBlocksList>();
        }

        public void AddContents(ContentBlocksList inputList)
        {
            this.ChapterContents.Add(inputList);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ContentBlocksList item in ChapterContents)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }

        public static explicit operator Chapter(JToken v)
        {
            throw new NotImplementedException();
        }
    }

    
}
