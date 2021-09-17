using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureFTP.Get_Latest_File
{
    public static class GetLatestFile
    {
        public static string ReturnMostRecentWrittenFile(string directoryPath)
        {
            directoryPath = directoryPath.Replace("L@TEST", "");
            var fileList = Directory.GetFiles(directoryPath);
            Dictionary<string, DateTime> fileInfoDic = new Dictionary<string, DateTime>();

            for (int i = 0; i < fileList.Length; i++)
            {
                fileInfoDic.Add(fileList[i], File.GetLastWriteTimeUtc(fileList[i]));
            }

            return fileInfoDic.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

        }
    }
}
