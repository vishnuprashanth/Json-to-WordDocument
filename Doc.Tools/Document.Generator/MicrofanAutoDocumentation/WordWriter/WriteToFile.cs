using System;
using DocumentFormat.OpenXml.Packaging;
using Word = DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Xml.Linq;
using OpenXmlPowerTools;
using System.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using ContentBlock.Models.Models;
using ContentBlock.Models.ContentBlocks;
namespace Microfan.AutoDocumentation.Dll
{
    public class WriteToFile
    {
        private const string wordFilePath = @"..\..\..\..\MicrofanAutoDocumentation\Final Documents\JsonToWord.docx";
        private const string templateFilePath = @"..\..\..\..\MicrofanAutoDocumentation\Templates\ManualTemplate.docx";
        private static WordElementWriter wordWriter;
        /// <summary>
        /// This method gets a C# Document instance and prints every element of it to word.
        /// </summary>
        /// <param name="oDocument">The instance of Document.cs.</param>
        /// 
        public void ToFile(Document oDocument)
        {
            // Delete the previous file in this dir and copy the new template there.
            if (File.Exists(wordFilePath))  
                File.Delete(wordFilePath);
            File.Copy(templateFilePath, wordFilePath, true);
            // Open the JsonToWord.docx in edit mode.
            using (WordprocessingDocument wpd = WordprocessingDocument.Open(wordFilePath, true))
            {
                // Gets the body of the document and initializes the WordElementWriter.
                MainDocumentPart mainPart = wpd.MainDocumentPart;
                Body body = mainPart.Document.Body;
                wordWriter = new WordElementWriter(wpd);
                // Start style definitions
                // These lines are mandatory as they initialize the various style parts in the doc. The program crashes without these.
                StyleDefinitionsPart sdp = wpd.MainDocumentPart.StyleDefinitionsPart;
                if (sdp == null)
                {
                    sdp = wpd.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
                    Styles root = new Styles();
                    root.Save(sdp);
                }
                StylesWithEffectsPart sep = wpd.MainDocumentPart.StylesWithEffectsPart;
                if (sep == null)
                {
                    sep = wpd.MainDocumentPart.AddNewPart<StylesWithEffectsPart>();
                    Styles root = new Styles();
                    root.Save(sep);
                }
                // End of style definitions.
                // Call to method for replacing the title and the version on the cover page.
                this.ReplaceCover(wpd, oDocument);

                // Prints the front page with Title, MAnual version,HMI_Version,date of creation etc.
                Paragraph p = body.AppendChild(new Paragraph());
                Run runTitle = p.AppendChild(new Run(new Word.Text(oDocument.Title)));
                p.Append(new Run(new Break()));
                Run runType = p.AppendChild(new Run(new Word.Text(oDocument.Type.ToString() + " Manual")));
                p.Append(new Run(new Break()));
                Run runVersion = p.AppendChild(new Run(new Word.Text(" Manual Version : " + oDocument.Version)));
                p.Append(new Run(new Break()));
                Run runHMI_Version = p.AppendChild(new Run(new Word.Text("HMI version :" + oDocument.HMI_Version)));
                p.Append(new Run(new Break()));
                Run runDate = p.AppendChild(new Run(new Word.Text("Date : " + oDocument.DateCreated)));
                p.Append(new Run(new Break()));
                // The following 2 lines align the text of the previous paragraph to the center of the page.
                p.ParagraphProperties = new ParagraphProperties(new Justification());
                p.ParagraphProperties.Justification.Val = JustificationValues.Center;

                // The following line breaks to a new page.
                body.Append(new Paragraph(new Run(new Break() { Type = BreakValues.Page })));

                // Inserts a TOC that includes headings through level 4.
                // This is the only piece of code that makes use of TocAdder project.
                XElement firstPara = wpd
                    .MainDocumentPart
                    .GetXDocument()
                    .Descendants(W.p)
                    .Skip(2)
                    .FirstOrDefault();
                TocAdder.AddToc(wpd, firstPara,
                    @"TOC \o '1-4' \h \z \u", null, null);
                // Appends a page break.
                body.Append(new Paragraph(new Run(new Break() { Type = BreakValues.Page })));
                // Prints the elements.
                foreach (Chapter chapter in oDocument.ChaptersList)
                {
                    // Get the chapter's title, place it to the doc and format it to Heading1.
                    Paragraph chapterTitle = body.AppendChild(new Paragraph(new Run(new Word.Text(chapter.Title))));
                    chapterTitle.ParagraphProperties = new ParagraphProperties(new ParagraphStyleId());
                    chapterTitle.ParagraphProperties.ParagraphStyleId.Val = "Heading1";
                    // This paragraph is the one not used in the methods of WordElementWriter.
                    // It is here to serve as a future placeholder of elements, so the methods of WordElementWriter return a paragraph with the printed element.
                    Paragraph paragraph;
                    foreach (var block in chapter.ChapterContents)
                    {
                        foreach (BasicContentBlock element in block.ContentBlocks)
                        {
                            paragraph = new Paragraph();
                            wordWriter.AppendElement(element, paragraph);
                            body.Append(new Paragraph(new Run(new Break())));
                        }
                    }
                }

                wpd.Save();
            }
        }
        /// <summary>
        /// Replaces the default title of the document with the title from the JSON file.
        /// </summary>
        /// <param name="documentXml">The XML document instance.</param>
        /// <param name="documentObject">The C# document instance.</param>
        private void ReplaceCover(WordprocessingDocument documentXml, Document documentObject)
        {
            // Next line gets all the runs of the document (as an IEnumerable) that contain the strings "Document title", {{title}} etc.
            IEnumerable<Run> runs = documentXml.MainDocumentPart.Document.Body.Descendants<Run>()
                .Where(element => element.InnerText.Contains("[Document title]")
                || element.InnerText.Contains("[Document subtitle]")
                || element.InnerText.Contains("{{TITLE}}")
                || element.InnerText.Contains("{{VERSION}}")
                || element.InnerText.Contains("[Course title]"));

            if (runs != null)
            {
                foreach (Run run in runs)
                {
                    string innerText = null;
                    switch (run.InnerText)
                    {
                        case "[Document title]":
                            innerText = run.InnerText.Replace("[Document title]", documentObject.Title);
                            break;
                        case "{{TITLE}}":
                            innerText = run.InnerText.Replace("{{TITLE}}", documentObject.Title);
                            break;
                        case "[Document subtitle]":
                            innerText = run.InnerText.Replace("[Document subtitle]", documentObject.Version);
                            break;
                        case "{{VERSION}}":
                            innerText = run.InnerText.Replace("{{VERSION}}", documentObject.Version);
                            break;
                        case "[Course title]":
                            innerText = run.InnerText.Replace("[Course title]", documentObject.Type.ToString());
                            break;
                    }
                    run.RemoveAllChildren<Word.Text>();
                    run.Append(new Word.Text(innerText));
                }
            }
        }

    }
}