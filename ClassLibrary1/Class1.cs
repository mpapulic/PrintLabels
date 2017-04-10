using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using Excel;



namespace ExcelPodaci
{
    class Program
    {
        static void Main(string[] args)
        {

            using (StreamWriter sw = File.AppendText(@"C:\Temp\Log.txt"))
            {
                // PRVI PRIMER CITANJA IZ EXCEL FAJLA
                sw.WriteLine("---------------------------------------------------");
                sw.WriteLine("-----------   PRVI PRIMER ČITANJA ----------------");
                sw.WriteLine("---------------------------------------------------");
                string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 12.0;", @"C:\Users\Milan Papulic\Downloads\2017-03-28_Clients for Tamara.xls");
                var fileName = @"C:\Users\Milan Papulic\Downloads\2017-03-28_Clients for Tamara.xls";
                var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fileName);

                var adapter = new OleDbDataAdapter("SELECT CLIENTID,NAME,SURNAME,BIRTHDAY  FROM [All_Coupons$]", connectionString);
                var ds = new DataSet();
                adapter.Fill(ds, "adapter");

                DataTable data = ds.Tables["adapter"];
                var dataS = ds.Tables["adapter"].AsEnumerable();
                foreach (var item in dataS)
                {
                    sw.WriteLine(item.Field<string>("NAME") + " - " + item.Field<string>("SURNAME") + " - " + item.Field<DateTime>("BIRTHDAY"));
                }

                // DRUGI PRIMER CITANJA IZ EXCEL FAJLA
                sw.WriteLine(" ");
                sw.WriteLine(" ");
                sw.WriteLine("---------------------------------------------------");
                sw.WriteLine("-----------   DRUGI PRIMER ČITANJA ----------------");
                sw.WriteLine("---------------------------------------------------");
                foreach (var worksheet in Workbook.Worksheets(@"C:\Users\Milan Papulic\Downloads\Clients for Tamara.xlsx"))
                {
                    sw.WriteLine(worksheet.ToString());
                    sw.WriteLine(worksheet.NumberOfColumns.ToString());
                    sw.WriteLine(worksheet.MaxColumnIndex.ToString());
                    foreach (var row in worksheet.Rows)
                    {
                        foreach (var cell in row.Cells)
                        {
                            switch (cell.ColumnIndex)
                            {
                                case 0:
                                    sw.Write(cell.Value.ToString() + " - ");
                                    break;
                                case 4:
                                    sw.Write(DateFromExcelFormat(cell.Value).ToString() + " - ");
                                    break;
                                default:
                                    sw.Write(cell.Text.ToString() + " - ");
                                    break;
                            }
                        }
                        sw.WriteLine("  ");
                    }
                }
            }
        }
        public static DateTime DateFromExcelFormat(string ExcelCellValue)
        {
            return DateTime.FromOADate(Convert.ToDouble(ExcelCellValue));
        }
    }
}
