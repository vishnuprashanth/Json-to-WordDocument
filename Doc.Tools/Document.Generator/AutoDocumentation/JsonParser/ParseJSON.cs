using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ContentBlock.Models.Models;
using ContentBlock.Models.ContentBlocks;
using ContentBlock.Models.ContentBlocks.Enum;
using ContentBlock.Models;



namespace Microfan.AutoDocumentation.Dll
{
    public class ParseJSON
    {
        private const string JSONFilePath = @"..\..\..\..\MicrofanAutoDocumentation\Sources\properJsonFile.json";
        //private const string JSONFilePath = @"..\..\..\..\MicrofanAutoDocumentation\Sources\TestproperJsonFile.json";



        private WriteToFile writer;
        // The following bool variable indicates if a static page in the document is before or after the main body of the doc.
       

        /// <summary>
        /// Gets the JSON file provided and parses every object to C# object. 
        /// Refactoring needed as it's pretty big for one method.
        /// </summary>
        /// <returns>A string that holds all the elements of the JSON file after parsing.</returns>
        /// 


        public string ParseJsonToObj()
        {
            // Initializations
       
            writer = new WriteToFile();


            // Gets the whole string of the file (root object).
            JObject jDocument = JObject.Parse(File.ReadAllText(JSONFilePath));

            // Gets the attribute's values of the above object (document).

            JObject JTag = (JObject)jDocument["Tags"];
            JValue jTitle = (JValue)JTag["title"];
            JValue jVersion = (JValue)JTag["version"];
            JValue jLanguage = (JValue)JTag["language"];
            JValue jType = (JValue)JTag["type"];
            JValue jHMI_Version = (JValue)JTag["HMI_Version"];

            // Create the C# Document instance and pass the title, version and HMI version from the JDocument.
            Document document = new Document
            {
                Title = (string)jTitle,
                Version = (string)jVersion,
                HMI_Version = (string)jHMI_Version,
               
            };
            
            if (Enum.TryParse<Language>(((string)jLanguage).ToLower(), out Language lan))
            {
                document.Language = lan;
            }

            if (Enum.TryParse<DocumentType>(((string)jType).ToLower(), out DocumentType res))
            {
                document.Type = res;
            }

            JArray jDocumentParrts = (JArray)jDocument["documentParts"];

            foreach (JObject jChapterLists in jDocumentParrts)
            {

                JArray ChapterList = (JArray)jChapterLists["chapters"]; //Get the Chapter

                foreach (JObject JPart in ChapterList)
                {
                    Chapter chapter = null;
                    chapter = new Chapter((string)JPart.GetValue("title")); //gets title
                    string Type = (string)JPart.GetValue("type");//gets the type of ContentBlocks

                    JArray chapterContentBlocks = (JArray)JPart.GetValue("contentBlocks");//Getting the contebt Block 
                    ContentBlocksList currentBlock = new ContentBlocksList();

                     foreach (JObject jElement in chapterContentBlocks) // passes the each elements to content block
                    {
                        BasicContentBlock element = ContentBlock_Parser.ParseContentBlock(jElement);
                        currentBlock.ContentBlocks.Add(element);
                    }

                    chapter.AddContents(currentBlock);
                    document.ChaptersList.Add(chapter);


                    // Need some codeWork to catch the chapters inside the chapter 
                    //JArray SubChapter = (JArray)JPart["chapters"]; //Get the Chapter

                    //foreach (JObject jChapter in SubChapter)//gets all chapters from chapter list in json
                    //{
                    //    Chapter Sub_Chapter = null;
                    //    Sub_Chapter = new Chapter((string)jChapter.GetValue("title")); //gets title
                    //    string Types = (string)jChapter.GetValue("type");//gets the type of ContentBlocks

                    //    JArray Sub_chapterContentBlocks = (JArray)jChapter.GetValue("contentBlocks");//Getting the contebt Block 
                    //    ContentBlocksList Sub_currentBlock = new ContentBlocksList();

                    //    foreach (JObject jElement in Sub_chapterContentBlocks) // passes the each elements to content block
                    //    {
                    //        BasicContentBlock element = ContentBlock_Parser.ParseContentBlock(jElement);
                    //        Sub_currentBlock.ContentBlocks.Add(element);

                    //        Sub_Chapter.AddContents(Sub_currentBlock);
                    //        document.ChaptersList.Add(Sub_Chapter);
                    //    }

                    //}

                }

            }
            // Call to WriteToFIle.cs for writing the elements to word.
            writer.ToFile(document);
            return "OK!";
        }

        
    }
}
