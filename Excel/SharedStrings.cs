using System;
using System.Xml;
using System.Xml.Serialization;

namespace Excel
{
    /// <summary>
    /// (c) 2014 Vienna, Dietmar Schoder
    /// 
    /// Code Project Open License (CPOL) 1.02
    /// 
    /// Handles a "shared strings XML-file" in an Excel xlsx-file
    /// </summary>
    [Serializable()]
    [XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
    [XmlRoot("sst", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
    public class sst
    {
        /// <summary>
        ///    uniqueCount excel
        /// </summary>
        [XmlAttribute]
        public string uniqueCount;
        /// <summary>
        /// count excel
        /// </summary>
        [XmlAttribute]
        public string count;

        /// <summary>
        /// si of sst
        /// </summary>
        [XmlElement("si")]
        public SharedString[] si;

        /// <summary>
        /// sst excel
        /// </summary>
        public sst()
        {
        }
    }
    /// <summary>
    ///   SharedString excel
    /// </summary>
    public class SharedString
    {
        /// <summary>
        /// t of sharedstring excel
        /// </summary>
        public string t;
    }
}
