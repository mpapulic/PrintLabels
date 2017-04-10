using System;
using System.IO;
using System.Collections.Generic;
using PdfSharp.Pdf;
using PrintLabels.EntityClasses;
using PrintLabels.BLL;
using PrintLabels.Utils;
using System.Net.Http;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using Excel;
using Common;


namespace PrintLabels.Utils
{
    public class LabelHandler
    {
        public void PrintLabels(string Path, int pagePerPDF, int labelsPerPage, string excelFile )
        {
            // Get all adresses and order by characters 
            // List<string> listAddresses = GetAllAddresses(excelFile).OrderBy(q => q).ToList();

            List<string> listAddresses = GetAllAddresses(excelFile).ToList();

            // postavljanje prve stranice
            int iPage = 1;
            bool dalje = true;
            do
            {
                // preuzimanje  seta adresa
                List<string> nalepnice = listAddresses.Skip((iPage-1)* pagePerPDF * labelsPerPage).Take(pagePerPDF*labelsPerPage).ToList();

                if (nalepnice.Count <= 0)
                {
                    dalje = false;
                    break;

                }

                LabelFormatBLL labelFormatBLL = new LabelFormatBLL();
                PdfLabelUtil pdfItem = new PdfLabelUtil();
                int labelId = 1;
                if (labelFormatBLL.GetLabelFormat(labelId).Id == labelId)
                {
                    Stream stream = pdfItem.GeneratePdfLabels(nalepnice, labelFormatBLL.GetLabelFormat(labelId), labelId);
                    PdfDocument pdfDoc = new PdfDocument();
                    if (stream != null)
                    {
                        byte[] streamBytes = PdfLabelUtil.ReadFully(stream);
                        File.WriteAllBytes(Path + iPage.ToString().PadLeft(3, '0') + ".pdf", streamBytes);
                    }
                    else
                        Console.WriteLine("Something is wrong");
                        LogError.LogText("Something is wrong");

                }
                iPage++;
            } while (dalje);

        }






        public List<string> GetAllAddresses(string pathExcelFile)
        {
            List<string> list = new List<string>();
            List<LabelAddress> listaKupaca = new List<LabelAddress>();
            foreach (var worksheet in Workbook.Worksheets(pathExcelFile))
            {
                string ime = "";
                string prezime = "";
                string grad = "";
                string adresa = "";
                string ptt = "";
                foreach (var row in worksheet.Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        /*
                         0 - CLIENT ID	
                         1 - NAME 	
                         2 - SURNAME	
                         3 - SEX	
                         4 - BIRTHDAY	
                         5 - ADD1	
                         6 - CITY	
                         7 - ZIP

                        */
                        switch (cell.ColumnIndex)
                        {
                            case 0:
                                break;
                            case 1:
                                ime = cell.Text.ToString();
                                break;
                            case 2:
                                prezime = cell.Text.ToString();
                                break;
                            case 4:
                                break;
                            case 5:
                                adresa = cell.Text.ToString().ToUpper();
                                break;
                            case 6:
                                grad = cell.Text.ToString().ToUpper();
                                break;
                            case 7:
                                ptt = cell.Text.ToString();
                                break;
                            default:
                                break;
                        }
                    }

                    listaKupaca.Add(new LabelAddress { Ime = ime, Prezime = prezime, Grad = grad, Adresa = adresa, PTT = ptt });


                }
            }

            //ovde ce ici order

            List<LabelAddress> listaOrder = GetDataOrder(listaKupaca);

           // foreach (var item in listaKupaca)
            foreach (var item in listaOrder)

                list.Add(item.rbr.ToString() + "-" +item.Ime + " " + item.Prezime + Environment.NewLine + item.Adresa + Environment.NewLine + item.PTT + " " + item.Grad);

            return list;
        }


        public static List<LabelAddress> GetDataOrder(List<LabelAddress> dataAll )
        {



            // var sortdata = dataAll.OrderBy(x => x.Prezime).OrderBy(c => c.Ime).ToList();
               var sortdata = dataAll.OrderBy(x => x.Prezime).ToList();

           // var sortdata = dataAll.OrderBy(x => x.Grad).ToList();

            int broj = 0;
            foreach (var ljudi in sortdata)
            {
                broj = broj + 1;
                ljudi.rbr = broj;
                //Console.WriteLine("Amount is {0} and type is {1}", money.amount, money.type);
            }

            // int broj = sortdata.Length;

            return sortdata;
        }


        public static DateTime DateFromExcelFormat(string ExcelCellValue)
        {
            return DateTime.FromOADate(Convert.ToDouble(ExcelCellValue));
        }
        public class LabelAddress
        {

            public int rbr { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Adresa { get; set; }
            public string PTT { get; set; }
            public string Grad { get; set; }
        }


    }

}
