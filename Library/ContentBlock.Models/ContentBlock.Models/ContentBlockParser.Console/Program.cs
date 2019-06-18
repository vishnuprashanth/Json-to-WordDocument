using ContentBlock.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace ContentBlockParser.Console
{
    class Program
    {


        static void Main(string[] args)
        {
            //This function always needs a json string including and contentBlocks[] with contentBlocks (check the TestJsonContentBlock.json file).
            var json = JObject.Parse(File.ReadAllText("../../../TestJsonContentBlock.json"));
            var aa = ContentBlock_Parser.ParseContentBlocksFromJson(JsonConvert.SerializeObject(json));
            foreach (var block in aa)
            {
                System.Console.Write(JsonConvert.SerializeObject(block, Formatting.Indented));
                System.Console.WriteLine(" ");

            }
            System.Console.ReadKey();

        }
    }
}
