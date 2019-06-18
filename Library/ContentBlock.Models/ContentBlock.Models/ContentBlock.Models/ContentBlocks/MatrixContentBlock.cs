using System;
using System.Collections.Generic;
using System.Text;

namespace ContentBlock.Models.ContentBlocks
{
    public class MatrixContentBlock : BasicContentBlock
    {
        
        /// <summary>
        /// Array that contains the headers of the matrix.
        /// In case of no header an empty string is placed.
        /// </summary>
        public string[] Headers { get; set; }

        /// <summary>
        /// The body of the matrix. A list that holds the ContentBlocks of each line.
        /// A content block represents one row. 
        /// Each of the elements of the content block represent a cell of the matrix.
        /// </summary>
        public List<ContentBlocksList> Body { get; set; }

        /// <summary>
        /// The footer of the matrix.
        /// </summary>
        public string Footer { get; set; }

        public bool HasBorders { get; set; }

        public override bool HasSubElements
        {
            get
            {
                if (Body.Count == 0)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// Default constructor. Sets the capacity of the Headers array to 40.
        /// Sets the HasBorders to True.
        /// </summary>
        public MatrixContentBlock():base(Enum.ContentBlockType.matrix)
        {
           
            this.Headers = new string[40];
            this.Body = new List<ContentBlocksList>();
            this.Footer = string.Empty;
            this.HasBorders = true;
        }

        public MatrixContentBlock(int numberOfColumns):base(Enum.ContentBlockType.matrix)
        {
           
            this.Headers = new string[numberOfColumns];
            this.Body = new List<ContentBlocksList>();
            this.Footer = string.Empty;
            this.HasBorders = true;
        }

        public BasicContentBlock GetElement(int row, int cell)
        {
            return this.Body[row].ContentBlocks[cell];
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("Matrix: \n");
            for (int i = 0; i < Body.Count; i++)
            {
                builder.Append($"Row {i + 1}: \n");
                for (int j = 0; j < Body[i].ContentBlocks.Count; j++)
                {
                    builder.Append($"BasicContentBlock {j + 1}: {Body[i].ContentBlocks[j].ToString()} \n");
                }
            }
            return builder.ToString();
        }
    }
}
