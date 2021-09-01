using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FTP_Secure
{
    public static class UpdateRecord
    {

        public static void AppendToEnd(string path, string text)
        {
            
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.Write(text);
            }
            
        }

        public static void UpdateLogFiles(bool xmlLogExists, bool flogExists, string ftpLogPath, string xmlLogName)
        {
            if (!flogExists && xmlLogExists)
            {

                CreateFlogFile.Create(ftpLogPath);
                List<LogEntry> logList = GenerateLogEntries.PopulateLogEntryList(PrepXMLFile.ReturnNav(ftpLogPath + xmlLogName), PrepXMLFile.ReturnXmlDoc(ftpLogPath + xmlLogName));
                foreach (var item in logList)
                {
                    UpdateRecord.AppendToEnd(CreateFlogFile.CurrentFormattedLogName(ftpLogPath), item.ToString());
                }
                File.Delete(ftpLogPath + xmlLogName);

            }

            //If the WinSCP log exists. Then proceed with XML information extraction and writing to F-log.
            if (xmlLogExists && flogExists)
            {
                List<LogEntry> logList = GenerateLogEntries.PopulateLogEntryList(PrepXMLFile.ReturnNav(ftpLogPath + xmlLogName), PrepXMLFile.ReturnXmlDoc(ftpLogPath + xmlLogName));
                foreach (var item in logList)
                {
                    UpdateRecord.AppendToEnd(CreateFlogFile.CurrentFormattedLogName(ftpLogPath), item.ToString());
                }
                File.Delete(ftpLogPath + xmlLogName);
            }
        }

    }
}
