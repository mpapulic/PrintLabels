using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintLabels.EntityClasses;

namespace PrintLabels.BLL
{
    public class LabelFormatBLL
    {
        private List<LabelFormat> _mLabelFormats = new List<LabelFormat>();
        public List<LabelFormat> GetLabelFormats()
        {
            if (_mLabelFormats.Count() == 0 )
            {

                _mLabelFormats = new List<LabelFormat>();
                _mLabelFormats.Add(new LabelFormat
                {   Id = 1,
                    Name = "A4",
                    Description = "A4 Sheet of 70.0 X 29.7mm address labels",
                    PageWidth = 210,
                    PageHeight = 297,
                    TopMargin = 0.0,
                    LeftMargin = 0.0,
                    LabelWidth = 70.0,
                    LabelHeight = 29.7,
                    VerticalPitch = 29.70,
                    HorizontalPitch = 70.0,
                    ColumnCount = 3,
                    RowCount = 10,
                    LabelPaddingTop = 7.0,
                    LabelPaddingLeft = 7.0
                });
                _mLabelFormats.Add(new LabelFormat
                {
                    Id = 2,
                    Name = "L7169",
                    Description = "A4 Sheet of 99.1 x 139mm BlockOut (tm) address labels",
                    PageWidth = 210,
                    PageHeight = 297,
                    TopMargin = 9.5,
                    LeftMargin = 4.6,
                    LabelWidth = 99.1,
                    LabelHeight = 139,
                    VerticalPitch = 139,
                    HorizontalPitch = 101.6,
                    ColumnCount = 2,
                    RowCount = 2,
                    LabelPaddingTop = 5.0,
                    LabelPaddingLeft = 8.0
                });           
            };
            return _mLabelFormats;
        }



        
        public LabelFormat GetLabelFormat(int idLabelFormat )
        {
            List<LabelFormat> listLabelFormat = null;
            LabelFormat emptyItem = new LabelFormat();
            listLabelFormat = GetLabelFormats();
            if (listLabelFormat.Count > 0)
            {
                foreach (LabelFormat item in listLabelFormat)
                {
                    if (item.Id == idLabelFormat)
                        return item;
                }
                return emptyItem;
            }

            else
                return emptyItem;
        }
    }
}
