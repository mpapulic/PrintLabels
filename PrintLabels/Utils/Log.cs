using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Common
{
    public class LogError
    {
        public LogError()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void LogException(Exception ex)
        {
            try
            {
                string Path;
                DateTime D;
                Path = Application.UserAppDataPath; // Application.StartupPath;
                D = DateTime.Now;
                Path = Path.TrimEnd('\\') + "\\" + D.Year.ToString() + D.Month.ToString().PadLeft(2, '0') + D.Day.ToString().PadLeft(2, '0') + ".err";
                System.IO.StreamWriter SW;
                System.IO.FileStream FS;
                FS = new System.IO.FileStream(Path, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                SW = new System.IO.StreamWriter(FS);
                SW.WriteLine();
                SW.WriteLine();
                SW.WriteLine("Date Occured: " + D.ToString());
                SW.WriteLine(ex.ToString());
                SW.WriteLine();
                SW.WriteLine("Message: " + ex.Message);
                SW.WriteLine("Stack: " + (new StackTrace()).ToString());
                SW.Close();
                FS.Close();
            }
            catch
            {

            }
        }

        public static void LogText(System.String Text)
        {
            try
            {
                string Path;
                DateTime D;
                Path = Application.UserAppDataPath; // Application.StartupPath;
                D = DateTime.Now;
                Path = Path.TrimEnd('\\') + "\\" + D.Year.ToString() + D.Month.ToString().PadLeft(2, '0') + D.Day.ToString().PadLeft(2, '0') + ".log";
                System.IO.StreamWriter SW;
                System.IO.FileStream FS;
                FS = new System.IO.FileStream(Path, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                SW = new System.IO.StreamWriter(FS);
                SW.WriteLine();
                SW.WriteLine("Date Occurred: " + D.ToString());
                SW.WriteLine(Text);
                SW.Close();
                FS.Close();
            }
            catch
            {

            }
        }

    }
}