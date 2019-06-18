using System.Collections.Generic;
using System.Text;


//namespace Microfan.AutoDocumentation.Dll.Elements
//{
//    interface IMyBaseCell
//    {

//    }


//    /// <summary>
//    /// The body of the matrix. A list that holds the ContentBlocks of each line.
//    /// A content block represents one row. 
//    /// Each of the elements of the content block represent a cell of the matrix.
//    /// Table cells can span across more than one column or row by using colspan
//    /// </summary>
//    class MyBaseCell : IMyBaseCell
//    {
//        public int Colspan { get; set; }

//       // public List<ContentBlockList> Contents { get; set; }
//    }

//    class MyCell : MyBaseCell
//    {
        
//    }

//    class HeaderCell : MyBaseCell
//    {
//        public string Title { get; set; }
//    }

//    class MyRow
//    {
//        public List<IMyBaseCell> Cells { get; set; }
//    }


//    class MyMatrix : Element
//    {
//        /// <summary>
//        /// Array that contains the headers of the matrix.
//        /// In case of no header an empty string is placed.
//        /// </summary>
//        public List<MyRow> Header { get; set; }


//        /// <summary>
//        /// The body of the matrix. 
//        /// Body represents  row. 
//        /// </summary>
//        public List<MyRow> Body { get; set; }


//        /// <summary>
//        /// The footer of the matrix.
//        /// </summary>
//        public List<MyRow> Footer { get; set; }


//        public bool HasBorders { get; set; }

//        public override bool HasSubElements
//        {
//            get
//            {
//                if (Body.Count == 0)
//                    return false;
//                return true;
//            }
//        }


//        public MyMatrix()
//        {
//            this.Type = ContentBlockType.MyMatrix;
//            this.Header = new List<MyRow>();
//            this.Body = new List<MyRow>();
//            this.Footer = new List<MyRow>();
//            this.HasBorders = true;
//        }

  
       

//        public override string ToString()
//        {
//            StringBuilder builder = new StringBuilder("MyMatrix: \n");
//            for (int i = 0; i < Body.Count; i++)
//            {
//                builder.Append($"Row {i + 1 }: \n");
//                for (int j = 0; j < Body[i].Cells.Count; j++)
//                {
//                    builder.Append(value: $"Element{j + i}:{Body[i].Cells[j].ToString()}\n");
//                }
//            }
//            return base.ToString();
//        }



//    }
//}
