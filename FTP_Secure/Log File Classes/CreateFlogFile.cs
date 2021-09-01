using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Secure
{
    public static class CreateFlogFile
    {
        public static void Create(string directoryPath)
        {
            var fullPath = CurrentFormattedLogName(directoryPath);
            using (File.Create(fullPath))
            { }
            UpdateRecord.AppendToEnd(fullPath, ProvideColumnNamesAndStruc());
        }

        public static string CurrentStandardLogName(string path)
        {
            return path + "Standard_FTP_Log_" + DateTime.Now.ToString("yyyyMM") + ".txt";
        }

        public static string CurrentFormattedLogName(string path)
        {
            return path + "Formatted_FTP_Log_" + DateTime.Now.ToString("yyyyMM") + ".txt";
        }

        private static string ProvideColumnNamesAndStruc()
        {
            return "DTS|TransferType|Protocol|Filename|Destination|Result\r\n";
        }
    }
}
