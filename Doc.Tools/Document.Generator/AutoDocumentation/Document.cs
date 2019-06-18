using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ContentBlock.Models.Models;
using ContentBlock.Models.ContentBlocks;
using System.Linq;

namespace Microfan.AutoDocumentation.Dll
{
    public class Document
    {
        /// <summary>
        /// The title of this manual.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The version of this manual.
        /// </summary>
        public string Version { get; set; }

        ///<summary>
        ///The version of the HMI
        ///</summary>
        public string HMI_Version { get; set; }

        /// <summary>
        /// The language of the manual.
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public Language Language { get; set; }

        /// <summary>
        /// The type of the manual, installer or user.
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DocumentType Type { get; set; }




        /// <summary>
        /// The list of chapters of the manual.
        /// </summary>
        public List<Chapter> ChaptersList { get; set; }


        
        /// <summary>
        /// The creation date of the document. Only getter is needed as the date of creation is the current date.
        /// </summary>
        public string DateCreated
        {
            get
            {
                return DateTime.Now.Date.ToShortDateString();
            }
        }

        /// <summary>
        /// Deafault constructor. THe default Version is 0 and the default language is english.
        /// </summary>
        public Document()
        {
            this.Title = "";
            this.Version = "0";
            this.Language = Language.EN;
            this.Type = new DocumentType();
            this.ChaptersList = new List<Chapter>();
        }

        public override string ToString()
        {
            return (ChaptersList.ToString());
        }

    }

    public enum Language
    {
        EN,
        FR,
        NL,
        DE,
        GR
    }

    public enum DocumentType
    {
        User,
        installer
    }
}
