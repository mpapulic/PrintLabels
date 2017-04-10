using System;
using System.Xml.Serialization;

namespace Excel
{
    /// <summary>
    /// (c) 2014 Vienna, Dietmar Schoder
    /// 
    /// Code Project Open License (CPOL) 1.02
    /// 
    /// Deals with an Excel worksheet in an xlsx-file
    /// </summary>
    [Serializable()]
    [XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
    [XmlRoot("worksheet", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
    public class worksheet
    {

        /// <summary>
        /// Rows excel
        /// </summary>
        [XmlArray("sheetData")]
        [XmlArrayItem("row")]
        public Row[] Rows;

        /// <summary>
        ///  NumberOfColumns excel
        /// </summary>
        [XmlIgnore]
        public int NumberOfColumns; // Total number of columns in this worksheet

        /// <summary>
        ///         MaxColumnIndex excel
        /// </summary>
        public static int MaxColumnIndex = 0; // Temporary variable for import

        /// <summary>
        ///    worksheet excel
        /// </summary>
        public worksheet()
        {
        }

        /// <summary>
        ///   ExpandRows
        /// </summary>
        public void ExpandRows()
        {
            foreach (var row in Rows)
                row.ExpandCells(NumberOfColumns);
        }
    }
}
