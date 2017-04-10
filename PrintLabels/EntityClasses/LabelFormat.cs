using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintLabels.EntityClasses
{
    public class LabelFormat
    {
    public int Id {get; set;}
    public string Name {get;set;}
    public string Description {get;set;}
    public double PageWidth { get; set; }
    public double PageHeight { get; set; }
    public double TopMargin { get; set; }
    public double LeftMargin { get; set; }
    public double LabelWidth { get; set; }
    public double LabelHeight { get; set; }
    public double LabelPaddingLeft { get; set; }
    public double LabelPaddingRight { get; set; }
    public double LabelPaddingTop { get; set; }
    public double LabelPaddingBottom { get; set; }
    public double VerticalPitch { get; set; }
    public double HorizontalPitch { get; set; }
    public int ColumnCount { get; set; }
    public int RowCount { get; set; }
    }

}
