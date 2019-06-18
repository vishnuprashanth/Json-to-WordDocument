using System;
using Microfan.AutoDocumentation.Dll;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using System.Diagnostics;

namespace Document.Generator.App
{
    class Program
    {
        private static ParseJSON parser = new ParseJSON();
        private static WriteToFile writer = new WriteToFile();

        static void Main(string[] args)
        {
            Console.Write(parser.ParseJsonToObj());
            Console.ReadKey();
            //Process p = new Process();
            //p.StartInfo.FileName = @"..\..\..\..\Final Documents\JsonToWord.docx";
            //p.Start();
        }
    }
}
