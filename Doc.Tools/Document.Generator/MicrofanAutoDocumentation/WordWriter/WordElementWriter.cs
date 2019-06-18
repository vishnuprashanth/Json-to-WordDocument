using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using Word = DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Linq;
using IS = SixLabors.ImageSharp;
using ContentBlock.Models.Models;
using ContentBlock.Models.ContentBlocks;
using ContentBlock.Models.ContentBlocks.Enum;
using Document.Generator.Dll.WordWriter.MarkDown;
using static ContentBlock.Models.ContentBlocks.TextContentBlock;

namespace Microfan.AutoDocumentation.Dll
{
    /// <summary>
    /// This class is responsible for appending every element to the document's body.
    /// Every method appends the element at the end of the body. Flexibility needed to provide the paragraph/run to be placed on.
    /// Past attempts to provide the paragraph were corrupting the file.
    /// </summary>
    public class WordElementWriter
    {
        public WordprocessingDocument Document { get; set; }
        private const string imagesPath = @"C:\Users\vishn_000\Desktop\Microfan1\microfan-intranet.argos-configurator\Doc.Tools\Document.Generator\MicrofanAutoDocumentation\Images";

        public WordElementWriter(WordprocessingDocument document)
        {
            this.Document = document;
        }

        /// <summary>
        /// This generic method is responsible for managing the printing of elements to the word file.
        /// </summary>
        /// <param name="element">The element to be printed.</param>
        /// <param name="paragraph">The paragraph in which the element will be placed. Not yet implemented.</param>
        public void AppendElement(BasicContentBlock element, Paragraph paragraph)
        {
            switch (element.Type)
            {
                case ContentBlock.Models.ContentBlocks.Enum.ContentBlockType.function:
                    AppendFunction(element, paragraph);
                    break;
                case ContentBlock.Models.ContentBlocks.Enum.ContentBlockType.image:
                    AppendImage(element, paragraph);
                    break;
                case ContentBlock.Models.ContentBlocks.Enum.ContentBlockType.matrix:
                    AppendMatrix(element, paragraph);
                    break;
                case ContentBlock.Models.ContentBlocks.Enum.ContentBlockType.path:
                    AppendPath(element, paragraph);
                    break;
                case ContentBlock.Models.ContentBlocks.Enum.ContentBlockType.Remark:
                    AppendRemark(element, paragraph);
                    break;
                case ContentBlock.Models.ContentBlocks.Enum.ContentBlockType.text:
                    AppendText(element, paragraph);
                    break;
                case ContentBlock.Models.ContentBlocks.Enum.ContentBlockType.StaticPage:
                    AppendImage(element, paragraph);
                    break;
                default:
                    Document.MainDocumentPart.Document.Body.Append(new Paragraph(new Run(new Word.Text(element.ToString()))));
                    break;
            }
        }

        /// <summary>
        /// Appends a remark element at the end of the document.
        /// </summary>
        /// <param name="element">The remark to be written as an Element.</param>
        /// <param name="paragraph">The paragraph where this remark will be written.</param>
        private void AppendRemark(BasicContentBlock element, Paragraph paragraph)
        {
            // The following code was copy-pasted from the OpenXml SDK Productivity Tool, with some small changes.
            // It creates a 1 row, 2 columns table, appends the remark image in the first cell and the text in the second.
            // Then appends the whole table at the end of the document.
            // Because the image and the table have standatd dimensions, we can supply the various properties with absolute values where possible.
            RemarkContentBlock remark = (RemarkContentBlock)element;

            Table table1 = new Table();

            TableProperties tableProperties1 = new TableProperties();


        //                new TableBorders(
        //    new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Dashed), Size = 24 },
        //    new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Dashed), Size = 24 },
        //    new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Dashed), Size = 24 },
        //    new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Dashed), Size = 24 },
        //    new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Dashed), Size = 24 },
        //    new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Dashed), Size = 24 }
        //));
        //    // Create a row.
        //    TableRow tr = new TableRow();

        //    // Create a cell.
        //    TableCell tc1 = new TableCell();

        //    // Specify the width property of the table cell.
        //    tc1.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));

        //    // Specify the table cell content.
        //    tc1.Append(new Paragraph(new Run(new Text(remark.Content))));

        //    // Append the table cell to the table row.
        //    tr.Append(tc1);


            TableStyle tableStyle1 = new TableStyle() { Val = "TableGrid" };
            TableWidth tableWidth1 = new TableWidth() { Width = "0", Type = TableWidthUnitValues.Auto };

            TableBorders tableBorders1 = new TableBorders();
            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };

            tableBorders1.Append(insideVerticalBorder1);
            TableLook tableLook1 = new TableLook() { Val = "04A0", FirstRow = true, LastRow = true, FirstColumn = true, LastColumn = true, NoHorizontalBand = false, NoVerticalBand = true };

            Justification justification1 = new Justification()
            {
                Val = JustificationValues.Center
            };

            tableProperties1.Append(tableStyle1);
            tableProperties1.Append(tableWidth1);
            tableProperties1.Append(tableBorders1);
            tableProperties1.Append(tableLook1);
            tableProperties1.Append(justification1);

            TableGrid tableGrid1 = new TableGrid();
            GridColumn gridColumn1 = new GridColumn() { Width = "846" };
            GridColumn gridColumn2 = new GridColumn() { Width = "8170" };

            tableGrid1.Append(gridColumn1);
            tableGrid1.Append(gridColumn2);

            TableRow tableRow1 = new TableRow() { RsidTableRowAddition = "00156C35", RsidTableRowProperties = "0092224D" };

            TableCell tableCell1 = new TableCell();

            TableCellProperties tableCellProperties1 = new TableCellProperties();
            TableCellWidth tableCellWidth1 = new TableCellWidth() { Width = "846", Type = TableWidthUnitValues.Dxa };

            TableCellBorders tableCellBorders1 = new TableCellBorders();
            BottomBorder bottomBorder1 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            LeftBorder leftBorder1 = new LeftBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            TopBorder topBorder1 = new TopBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableCellBorders1.Append(bottomBorder1);
            tableCellBorders1.Append(leftBorder1);
            tableCellBorders1.Append(topBorder1);

            tableCellProperties1.Append(tableCellWidth1);
            tableCellProperties1.Append(tableCellBorders1);

            Paragraph paragraph1 = new Paragraph() { RsidParagraphAddition = "00156C35", RsidParagraphProperties = "00156C35", RsidRunAdditionDefault = "00156C35" };

            Run run1 = new Run();

            RunProperties runProperties1 = new RunProperties();
            NoProof noProof1 = new NoProof();

            runProperties1.Append(noProof1);

            ImagePart imagePart = Document.MainDocumentPart.AddImagePart(ImagePartType.Jpeg);
            using (FileStream fs = new FileStream(Path.Combine(imagesPath, remark.IconUrl), FileMode.Open))
            {
                imagePart.FeedData(fs);
            }

            run1.Append(runProperties1);
            Drawing drawing1 = AddImageToBody(Document.MainDocumentPart.GetIdOfPart(imagePart), 361604L, 361604L);
            run1.Append(drawing1);

            paragraph1.Append(run1);

            tableCell1.Append(tableCellProperties1);
            tableCell1.Append(paragraph1);

            TableCell tableCell2 = new TableCell();

            TableCellProperties tableCellProperties2 = new TableCellProperties();
            TableCellWidth tableCellWidth2 = new TableCellWidth() { Width = "8170", Type = TableWidthUnitValues.Dxa };

            TableCellBorders tableCellBorders2 = new TableCellBorders();
            BottomBorder bottomBorder2 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            RightBorder rightBorder2 = new RightBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            TopBorder topBorder2 = new TopBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableCellProperties2.Append(tableCellWidth2);
            tableCellBorders2.Append(bottomBorder2);
            tableCellBorders2.Append(rightBorder2);
            tableCellBorders2.Append(topBorder2);
            tableCellProperties2.Append(tableCellBorders2);

            Paragraph paragraph2 = new Paragraph() { RsidParagraphAddition = "00156C35", RsidParagraphProperties = "00156C35", RsidRunAdditionDefault = "00156C35" };

            Run ContentRun = new Run();
            ContentRun.Append(new Word.Text(remark.Content));
            paragraph2.Append(ContentRun);

            //Run run2 = new Run();
            //Word.Text text1 = new Word.Text();
            //text1.Text = remark.Content;
            //run2.Append(text1);
            //paragraph2.Append(run2);

            tableCell2.Append(tableCellProperties2);
            tableCell2.Append(paragraph2);

            tableRow1.Append(tableCell1);
            tableRow1.Append(tableCell2);

            table1.Append(tableProperties1);
            table1.Append(tableGrid1);
            table1.Append(tableRow1);

            Document.MainDocumentPart.Document.Body.Append(new Paragraph(new Run(table1)));
            Document.Save();
        }

        /// <summary>
        /// Appends a path at the end of the document.
        /// </summary>
        /// <param name="element">The path to be written as an Element.</param>
        /// <param name="paragraph">The paragraph where this path will be written.</param>
        private void AppendPath(BasicContentBlock element, Paragraph paragraph)
        {
            PathContentBlock path = (PathContentBlock)element;

            // The Following code Runs The Path in Bold
            // Case 1

            //Paragraph para = new Paragraph();
            //RunProperties Path_rPr = new RunProperties(new Bold());
            //Run PathRun = new Run(Path_rPr);
            //PathRun.Append(new Word.Text("Path:" + path.ToString()));
            //para.Append(PathRun);

            //The following Code Runs the  path in normal 
            //Case 2 

            Run run = new Run(new Word.Text(path.ToString()));
            Paragraph para = new Paragraph(run);

            Document.MainDocumentPart.Document.Body.Append(para);
            Document.Save();
        }

        /// <summary>
        /// Appends a text at the end of the document.
        /// </summary>
        /// <param name="element">The text to be written as an Element.</param>
        /// <param name="paragraph">The paragraph where this path will be written.</param>
        /// 

        PatternMatcher Bold;
        PatternMatcher Italic;
        PatternMatcher Underline;
        PatternMatcher BulletList;
        private Run run;

        private void AppendText(BasicContentBlock element, Paragraph paragraph)
        {
            TextContentBlock Text = (TextContentBlock)element;

            ParagraphProperties pPr = new ParagraphProperties();
            RunProperties rPr = new RunProperties();

            Ranges<int> Tokens = new Ranges<int>();

            Italic = new PatternMatcher(ContentBlock.Models.ContentBlocks.Pattern.Italic);
            Italic.FindMatches(Text.Content, ref Tokens);

            Bold = new PatternMatcher(ContentBlock.Models.ContentBlocks.Pattern.Bold);
            Bold.FindMatches(Text.Content, ref Tokens);

            Underline = new PatternMatcher(ContentBlock.Models.ContentBlocks.Pattern.Underline);
            Underline.FindMatches(Text.Content, ref Tokens);

            BulletList = new PatternMatcher(Pattern.BulletList);
            BulletList.FindMatches(Text.Content, ref Tokens);



            if (!PatternsHaveMatches())
            {
                run = new Run();
                run.Append(new Text(Text.Content)
                {
                    Space = SpaceProcessingModeValues.Preserve
                });
                paragraph.Append(run);

            }

            else
            {
                int pos = 0;
                string buffer = "";

                while (pos < Text.Content.Length)
                {
                    if (!Tokens.ContainsValue(pos))
                    {
                        buffer += Text.Content.Substring(pos, 1);
                    }
                    else if (buffer.Length > 0)
                    {
                        run = new Run();

                        Bold.SetFlagFor(pos - 1);
                        Italic.SetFlagFor(pos - 1);
                        Underline.SetFlagFor(pos - 1);
                        BulletList.SetFlagFor(pos - 1);

                        // this is the place where the code should be Refactored to catch the pattern and pass it properly

                        if (Bold.Flag)
                        {
                            rPr.Append(new Bold() { Val = new OnOffValue(true) });
                        }
                        if (Italic.Flag)
                        {
                            rPr.Append(new Italic());
                        }
                        if (Underline.Flag)
                        {
                            rPr.Append(new Underline() { Val = DocumentFormat.OpenXml.Wordprocessing.UnderlineValues.Single });
                        }
                        if (BulletList.Flag)
                        {
                            rPr.Append(new NumberingFormat() { Val = NumberFormatValues.Bullet });
                        }



                        run.Append(new Text(buffer)
                        {
                            Space = SpaceProcessingModeValues.Preserve
                        });


                        //Need to do Some code changes to cath the text after Bold

                        //run.Append(rPr);   
                        paragraph.Append(run);

                        buffer = "";
                    }

                    pos++;
                };
            }


            ////Assign the properties to the paragraph and the run.
            //Paragraph para = new Paragraph()
            //{
            //    ParagraphProperties = pPr
            //};
            //Run runs = new Run(new Word.Text(Text.Content))
            //{
            //    RunProperties = rPr
            //};
            //paragraph.Append(runs);


            run.Append(rPr);

            Document.MainDocumentPart.Document.Body.Append(paragraph);

            Document.Save();


        }
        private bool PatternsHaveMatches()
        {
            return Bold.HasMatches() || Italic.HasMatches() || Underline.HasMatches() || BulletList.HasMatches();
        }


        /// <summary>
        /// Appends a function at the end of the document.
        /// </summary>
        /// <param name="element">The function to be written as an Element.</param>
        /// <param name="paragraph">The paragraph where this function will be written.</param>
        private void AppendFunction(BasicContentBlock element, Paragraph paragraph)
        {
            FunctionContentBlock function = (FunctionContentBlock)element;

            // Initializing the paragraph and the properties for input_range and title.
            Paragraph para = new Paragraph();
            RunProperties title_rPr = new RunProperties(new Bold());
            RunProperties FunctionID_rPr = new RunProperties(new Underline());
            RunProperties inputRange_rPr = new RunProperties(new Italic());

            Run titleRun = new Run(title_rPr);
            Run functionIdRun = new Run(FunctionID_rPr);
            Run inputRangeRun = new Run(inputRange_rPr);

            titleRun.Append(new Word.Text(function.FunctionName));
            titleRun.Append(new Break());
            functionIdRun.Append(new Word.Text("Function ID:" + function.FunctionID));
            functionIdRun.Append(new Break());
            inputRangeRun.Append(new Word.Text("Input Range:" + function.InputRange));
            inputRangeRun.Append(new Break());

            para.Append(titleRun);
            para.Append(functionIdRun);
            para.Append(inputRangeRun);

            Document.MainDocumentPart.Document.Body.Append(para);

            // Prints the inner elements of this function to the word file.
            foreach (var item in function.Content)
            {
                AppendElement(item, new Paragraph());
            }
            Document.Save();
        }

        private void AppendElement(ContentBlocksList item, Paragraph paragraph)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Appends a table at the end of the document.
        /// </summary>
        /// <param name="element">The table to be written as an Element.</param>
        /// <param name="paragraph">The paragraph where this table will be written.</param>
        private void AppendMatrix(BasicContentBlock element, Paragraph paragraph)
        {
            MatrixContentBlock matrix = (MatrixContentBlock)element;
            Table table = new Table();
            TableRow tr;

            //Initialize the table's borders and their properties and align it to the center of the page.
            TableProperties tableProperties = new TableProperties(new TableBorders(
                new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = 7 },
                new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = 7 },
                new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = 7 },
                new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = 7 },
                new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = 7 },
                new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = 7 }
                ), new Justification() { Val = JustificationValues.Center });
            table.Append(tableProperties);


            // Append the table headers and style them.
            if (matrix.Headers.Length > 0)
            {
                // Initialize, format and populate the header row.
                tr = new TableRow();
                TableCell tc = new TableCell();

                foreach (var header in matrix.Headers)
                {
                    tc = new TableCell();
                    tc.Append(new Paragraph(new Run(new Word.Text(header))));
                    TableCellProperties tcp = new TableCellProperties();

                    // Shading is the property needed to fill the background of a cell.
                    Shading shading = new Shading()
                    {
                        Color = "auto",
                        Fill = "0033A1",
                        Val = ShadingPatternValues.Clear
                    };
                    tcp.Append(shading);
                    tc.Append(tcp);
                    tr.Append(tc);
                }
                table.Append(tr);
            }
            // END of headers

            // Append the table body.
            for (int i = 0; i < matrix.Body.Count; i++)
            {
                tr = new TableRow();
                for (int j = 0; j < matrix.Body[i].ContentBlocks.Count; j++)
                {
                    TableCell tc = new TableCell();

                    // Get the element to be printed in this cell.
                    BasicContentBlock innerElement = matrix.Body[i].ContentBlocks[j];

                    // This if statement is here to distinguish between an image or a text to be placed in a cell.
                    // If it's neither an image nor a text defaults to the ToString() method of the specific element.
                    if (innerElement.Type == ContentBlock.Models.ContentBlocks.Enum.ContentBlockType.image)
                    {
                        ImageContentBlock eImage = (ImageContentBlock)innerElement;
                        ImagePart imagePart = Document.MainDocumentPart.AddImagePart(ImagePartType.Jpeg);
                        string imagePath = System.IO.Path.Combine(imagesPath, eImage.Source);

                        Dictionary<string, long> dictEmus = GetEmus(imagePath);

                        using (FileStream fs = new FileStream(imagePath, FileMode.Open))
                        {
                            imagePart.FeedData(fs);
                        }

                        // The following lines centers the image inside the table cell. Then it's assigned to a run and a paragraph.
                        Justification justification1 = new Justification() { Val = JustificationValues.Center };
                        ParagraphProperties paragraphProperties = new ParagraphProperties() { Justification = justification1 };
                        Paragraph paragraph1 = new Paragraph() { ParagraphProperties = paragraphProperties };
                        Run run = new Run();
                        run.Append(AddImageToBody(Document.MainDocumentPart.GetIdOfPart(imagePart), dictEmus["widthEmus"], dictEmus["heightEmus"]));
                        paragraph1.Append(run);

                        tc.Append(paragraph1);
                    }
                    else if (innerElement.Type == ContentBlock.Models.ContentBlocks.Enum.ContentBlockType.text)
                    {
                        TextContentBlock text = (TextContentBlock)innerElement;
                        RunProperties rPr = new RunProperties();
                        foreach (var fs in text.Font)
                        {
                            switch (fs)
                            {
                                case Pattern.Bold:
                                    rPr.Bold = new Bold();
                                    break;
                                case Pattern.Italic:
                                    rPr.Italic = new Italic();
                                    break;
                                case Pattern.Underline:
                                    rPr.Underline = new Underline();
                                    rPr.Underline.Val = UnderlineValues.Single;
                                    break;
                                default:
                                    break;
                            }
                        }
                        Run run = new Run();
                        run.RunProperties = rPr;
                        run.Append(new Word.Text(text.Content));
                        tc.Append(new Paragraph(run));
                    }
                    else
                        // If we omit  the Paragraph or the Run from the following statement, the Word file gets corrupted.
                        tc.Append(new Paragraph(new Run(new Word.Text(matrix.Body[i].ContentBlocks[j].ToString()))));

                    // Assume you want columns that are automatically sized.
                    //tc.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                    tr.Append(tc);
                }
                table.Append(tr);
            }
            // END of body.
            Document.MainDocumentPart.Document.Body.Append(new Paragraph(new Run(table)));
            Document.Save();
        }


        /// <summary>
        /// Appends an image or static page at the end of the document.
        /// </summary>
        /// <param name="element">The text to be written as an Element.</param>
        /// <param name="paragraph">The paragraph where this path will be written.</param>
        private void AppendImage(BasicContentBlock element, Paragraph paragraph)
        {


            MainDocumentPart mainPart = this.Document.MainDocumentPart;
            ImageContentBlock eImg;
            string imagePath = null;


            // The following 'if' statement is mandatory to distinguish between an image and a static page.
            // An image will be printed in its original dimensions if possible or resized to fit the width of the page (keeping its original ratio between X and Y dimensions).
            // A static page is treated like an image that is printed to a whole page.
            if (element.Type == ContentBlock.Models.ContentBlocks.Enum.ContentBlockType.image)
            {
                eImg = (ImageContentBlock)element;
                imagePath = System.IO.Path.Combine(imagesPath, eImg.Source);
            }
            else if (element.Type == ContentBlock.Models.ContentBlocks.Enum.ContentBlockType.StaticPage)
            {
                imagePath = System.IO.Path.Combine(imagesPath);
            }

            // Get the EMUs of this image's dimensions.
            Dictionary<string, long> emusDict = GetEmus(imagePath);

            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
            using (FileStream fs = new FileStream(imagePath, FileMode.Open))
            {
                imagePart.FeedData(fs);
            }

            // The following lines generate a paragraph and its properties and assign the alignment attibute to "center" .
            Justification justification = new Justification() { Val = JustificationValues.Center };
            ParagraphProperties paragraphProperties = new ParagraphProperties() { Justification = justification };
            Paragraph imageParagraph = new Paragraph() { ParagraphProperties = paragraphProperties };
            Run run = new Run();

            // This statement is here to distinguish between an image or a static page to be placed at the end of the file.
            if (element.Type == ContentBlock.Models.ContentBlocks.Enum.ContentBlockType.StaticPage)
            {
                run.Append(AddImageToBody(mainPart.GetIdOfPart(imagePart), emusDict["maxWidthEmus"], emusDict["maxHeightEmus"]));
            }
            else
            {
                run.Append(AddImageToBody(mainPart.GetIdOfPart(imagePart), emusDict["widthEmus"], emusDict["heightEmus"]));
            }
            imageParagraph.Append(run);
            Document.MainDocumentPart.Document.Body.Append(imageParagraph);
            Document.Save();

            ImageContentBlock image = (ImageContentBlock)element;

            Paragraph para = new Paragraph();
            Run contentRun = new Run();
            contentRun.Append(new Word.Text(image.Content));
            contentRun.Append(new Break());

            para.Append(contentRun);
            Document.MainDocumentPart.Document.Body.Append(para);

        }

        /// <summary>
        /// Gets the English Metric Units (EMUs) of the JPEG image found in the path provided.
        /// If image width is more than the width of a page, the image gets downsized keeping its original ratio of X and Y.
        /// </summary>
        /// <param name="imageFilePath">The path to the image.</param>
        /// <returns>A dictionary containing the widthEmus and heightEmus as well as their max values.</returns>
        private Dictionary<string, long> GetEmus(string imageFilePath)
        {
            Dictionary<string, long> dict = new Dictionary<string, long>();

            // Opens the image as a SixLabors.ImageSharp image and loads it to a var. 
            // The use of "var" here is mandatory as the compiler cries if the try to declare an IS.Image.
            FileStream fileStream = File.OpenRead(imageFilePath);
            var image = IS.Image.Load(fileStream);
            fileStream.Close();

            // The following piece of code gets the dimensions and resolution of the image, calculates its EMUs (English Metric Unit) and resizes the image if it's larger than the page.
            // The JPEG images so far use the perInch system and not perCm.
            // The current page width cm are 18.
            // The current page height cm are 27 - 2cm of the footer.
            int imgWidthPx = image.Width;
            int imgHeightPx = image.Height;
            double horzRezDpi = image.MetaData.HorizontalResolution;
            double vertRezDpi = image.MetaData.VerticalResolution;
            const int emusPerInch = 914400;
            const int emusPerCm = 360000;
            var widthEmus = (long)(imgWidthPx / horzRezDpi * emusPerInch);
            var heightEmus = (long)(imgHeightPx / vertRezDpi * emusPerInch);
            var maxWidthEmus = (long)(18 * emusPerCm);
            var maxHeightEmus = (long)(25 * emusPerCm);
            if (widthEmus > maxWidthEmus)
            {
                var ratio = (heightEmus * 1.0m) / widthEmus;
                widthEmus = maxWidthEmus;
                heightEmus = (long)(widthEmus * ratio);
            }

            dict["widthEmus"] = widthEmus;
            dict["heightEmus"] = heightEmus;
            dict["maxWidthEmus"] = maxWidthEmus;
            dict["maxHeightEmus"] = maxHeightEmus;

            return dict;
        }

        /// <summary>
        /// This method was copied from https://docs.microsoft.com/en-us/office/open-xml/how-to-insert-a-picture-into-a-word-processing-document.
        /// Small changes were made to make it work within the app. 
        /// No clue what it does exactly. Good luck in investigating...
        /// </summary>
        /// <param name="relationshipId">The relationshipId of this image.</param>
        /// <param name="X">The width of the image in EMUs.</param>
        /// <param name="Y">The height of the image in EMUs.</param>
        private Drawing AddImageToBody(string relationshipId, Int64 X, Int64 Y)
        {
            Drawing element =
             new Drawing(
                 new DW.Inline(
                     // Cx and Cy are the dimensions of the image.
                     new DW.Extent() { Cx = X, Cy = Y },
                     new DW.EffectExtent()
                     {
                         LeftEdge = 0L,
                         TopEdge = 0L,
                         RightEdge = 0L,
                         BottomEdge = 0L
                     },
                     new DW.DocProperties()
                     {
                         Id = (UInt32Value)1U,
                         Name = "Picture 1"
                     },
                     new DW.NonVisualGraphicFrameDrawingProperties(
                         new A.GraphicFrameLocks() { NoChangeAspect = true }),
                     new A.Graphic(
                         new A.GraphicData(
                             new PIC.Picture(
                                 new PIC.NonVisualPictureProperties(
                                     new PIC.NonVisualDrawingProperties()
                                     {
                                         Id = (UInt32Value)0U,
                                         Name = "New Bitmap Image"
                                     },
                                     new PIC.NonVisualPictureDrawingProperties()),
                                 new PIC.BlipFill(
                                     new A.Blip(
                                         new A.BlipExtensionList(
                                             new A.BlipExtension()
                                             {
                                                 Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                             })
                                     )
                                     {
                                         Embed = relationshipId,
                                         CompressionState =
                                         A.BlipCompressionValues.Print
                                     },
                                     new A.Stretch(
                                         new A.FillRectangle())),
                                 new PIC.ShapeProperties(
                                     new A.Transform2D(
                                         new A.Offset() { X = 0L, Y = 0L },
                                         // Cx and Cy are the dimensions of the image.
                                         // These values MUST be the same with the ones on top.
                                         new A.Extents() { Cx = X, Cy = Y }),
                                     new A.PresetGeometry(
                                         new A.AdjustValueList()
                                     )
                                     { Preset = A.ShapeTypeValues.Rectangle }))
                         )
                         { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                 )
                 {
                     DistanceFromTop = (UInt32Value)0U,
                     DistanceFromBottom = (UInt32Value)0U,
                     DistanceFromLeft = (UInt32Value)0U,
                     DistanceFromRight = (UInt32Value)0U,
                     EditId = "50D07946"
                 });
            return element;
        }
    }

}
