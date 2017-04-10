using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PrintLabels.EntityClasses;
using static System.Console;



namespace PrintLabels.Utils
{
    public class PdfLabelUtil
    {
        public Stream GeneratePdfLabels(List<string> Addresses , LabelFormat lf , int QtyEachLabel )
        {
            Stream generatePdfLabels = new MemoryStream();
            // The label sheet is basically a table and each cell is a single label
            // Format related
            int CellsPerPage = lf.RowCount * lf.ColumnCount;
            int CellsThisPage = 0;
            //XRect ContentRectangle = new XRect();       // A single cell content rectangle. This is the rectangle that can be used for contents and accounts for margins and padding.
            XSize ContentSize = new XSize();            // Size of content area inside a cell.
            double ContentLeftPos;        // left edge of current content area.
            double ContentTopPos;         // Top edge of current content area

            // Layout related
            XColor StrokeColor = XColors.DarkBlue;
            XColor FillColor = XColors.DarkBlue;
            XPen Pen  = new XPen(StrokeColor, 0.1);
            XBrush Brush = new XSolidBrush(FillColor);
            XGraphics Gfx ;
            XGraphicsPath Path = new XGraphicsPath();

            //int LoopTemp = 0;         // Counts each itteration. Used with QtyEachLabel
            int CurrentColumn = 1;
            int CurrentRow = 1;
            PdfDocument Doc = new PdfDocument();
            PdfPage page = new PdfPage();
            //AddPage(Doc, page, lf);
            Doc.AddPage(page);
            Gfx = XGraphics.FromPdfPage(page);

            // Ensure that at least 1 of each label is printed.
            if (QtyEachLabel < 1)
                QtyEachLabel = 1;

            // Define the content area size
            ContentSize = new XSize(XUnit.FromMillimeter(lf.LabelWidth - lf.LabelPaddingLeft - lf.LabelPaddingRight).Point,
                                 XUnit.FromMillimeter(lf.LabelHeight - lf.LabelPaddingTop - lf.LabelPaddingBottom).Point);

            if(Addresses!= null)
            {

                if (Addresses.Count > 0)
                // We actually have addresses to output.
                {
                    WriteLine();
                    foreach (string Address in Addresses)
                    {
                        // Once for each address
                        for (int LoopTemp = 0; LoopTemp < QtyEachLabel; LoopTemp++)
                        // Once for each copy of this address.
                        {
                            WriteLine($"Obradjuje se : {Address} ");
                            //WriteLine($"Nalepnica: {CellsThisPage} od :{CellsPerPage} ");
                            if (CellsThisPage == CellsPerPage)
                            {
                                //AddPage(Doc, page, lf);
                                //Gfx = XGraphics.FromPdfPage(page);
                                page = Doc.AddPage();
                                Gfx = XGraphics.FromPdfPage(page);
                                CellsThisPage = 0;
                            }
                            // This pages worth of cells are filled up. Create a new page


                            // Calculate which row and column we are working on.
                            CurrentColumn = (CellsThisPage + 1) % lf.ColumnCount;
                            double a = (CellsThisPage + 1) / lf.ColumnCount;
                            CurrentRow = (int)Math.Truncate(a);
                            //WriteLine($"Tekuci red: {CurrentRow} tekuca kolona :{CurrentColumn} ");


                            if (CurrentColumn == 0)
                            {
                                // This occurs when you are working on the last column of the row.
                                // This affects the count for column and row
                                CurrentColumn = lf.ColumnCount;
                            }
                            else
                                // We are not viewing the last column so this number will be decremented by one.
                                CurrentRow = CurrentRow + 1;



                            // Calculate the left position of the current cell.
                            ContentLeftPos = ((CurrentColumn - 1) * lf.HorizontalPitch) + lf.LeftMargin + lf.LabelPaddingLeft;


                            // Calculate the top position of the current cell.
                            ContentTopPos = ((CurrentRow - 1) * lf.VerticalPitch) + lf.TopMargin + lf.LabelPaddingTop;
                            //WriteLine($"Leva pozicija: {ContentLeftPos} Pozicija od vrha :{ContentTopPos} Velicina sadrzaja : {ContentSize} ");


                            // Define the content rectangle.
                            XPoint xpoint1 = new XPoint(XUnit.FromMillimeter(ContentLeftPos).Point,XUnit.FromMillimeter(ContentTopPos).Point );

                            XRect ContentRectangle =  new XRect( xpoint1, ContentSize) ;


                            Path = new XGraphicsPath();


                            // Add the address string to the page.
                            Path.AddString(Address, new XFontFamily("Arial"),
                                       XFontStyle.Regular,
                                       9,
                                       ContentRectangle,
                                       XStringFormats.TopLeft);


                            Gfx.DrawPath(Pen, Brush, Path);


                            // Increment the cell count
                            CellsThisPage = CellsThisPage + 1;
                        }
                    }
                    Doc.Save(generatePdfLabels, false);
                }
            }
            return generatePdfLabels;
        }

        private void AddPage(PdfDocument Doc ,
                        PdfPage Page ,
                        LabelFormat lf )
        {
            Page = Doc.AddPage();
            Page.Width = XUnit.FromMillimeter(lf.PageWidth);
            Page.Height = XUnit.FromMillimeter(lf.PageHeight);

        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
