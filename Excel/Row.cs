using System.Xml.Serialization;

namespace Excel
{
    /// <summary>
    /// (c) 2014 Vienna, Dietmar Schoder
    /// 
    /// Code Project Open License (CPOL) 1.02
    /// 
    /// Deals with an Excel row
    /// </summary>
    public class Row
    {
        /// <summary>
        ///  FilledCells
        /// </summary>
        [XmlElement("c")]
        public Cell[] FilledCells;
        /// <summary>
        ///   Cells of row
        /// </summary>
        [XmlIgnore]
        public Cell[] Cells;


        /// <summary>
        ///   ExpandCells of row
        /// </summary>
        /// <param name="NumberOfColumns"></param>
        public void ExpandCells(int NumberOfColumns)
        {
            Cells = new Cell[NumberOfColumns];
            foreach (var cell in FilledCells)
                Cells[cell.ColumnIndex] = cell;
            FilledCells = null;
        }
    }
}
