using System;
using System.Collections.Generic;
using System.Text;
using Microfan.AutoDocumentation.Dll;
using ContentBlock.Models.Models;
using ContentBlock.Models.ContentBlocks;
using ContentBlock.Models.ContentBlocks.Enum;


namespace Document.Generator.App
{
    class Initializer
    {
        public FunctionContentBlock GetFunction()
        {
            return new FunctionContentBlock
            {
                FunctionID = 1989,
                InputRange = "1, 5, 10",
                FunctionName = "Change dev name",

            };
        }

        public ImageContentBlock GetImage()
        {
            return new ImageContentBlock
            {
                Content = "Image desc",
                Source = "ftp://path.to.src",
                HorizontalAlignment = AlignmentEnum.Center
            };
        }

        public PathContentBlock GetPath()
        {
            return new PathContentBlock
            {
                Route = "system.settings.functions.climate.moisture"
            };
        }

        public RemarkContentBlock GetRemark()
        {
            return new RemarkContentBlock
            {
                Content = "This is the remark's content.",
                IconUrl = "url.to.icon",
                HasBorder = true
            };
        }

        public TextContentBlock GetText()
        {
            return new TextContentBlock
            {
                Content = "This is the Text's text (?!?!?!?!)"
            };
        }
    }
}
