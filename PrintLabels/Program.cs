using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintLabels.Utils;
using static System.Console;
using Common;
using System.IO;

namespace PrintLabels
{
    class Program
    {
        static void Main(string[] args)
         {
            DateTime startTimeObrade = DateTime.Now;
            try
            {


                LabelHandler mojDoc = new LabelHandler();
                string filePrefix = @"c:\temp\Nalepnice\";

                if (!Directory.Exists(filePrefix))
                {
                    Directory.CreateDirectory(filePrefix);
                }



                string excelFile = @"c:\temp\SdPress\2017-03-15_All_Tamara_Coupons.xlsx";

                int numberPagePerPDF = 10;
                int numberLabelsPerPage = 30;
                mojDoc.PrintLabels(filePrefix, numberPagePerPDF, numberLabelsPerPage, excelFile);
            }
            catch (Exception err)
            {

                LogError.LogException(err);
            }


            DateTime stopTimeObrade = DateTime.Now;

            WriteLine($" Ukupno trajanje obrade: {stopTimeObrade - startTimeObrade} sec. ");
            WriteLine("END END END !!!");
            WriteLine("press any key ... ");
            ReadLine();
        }
    }
}
