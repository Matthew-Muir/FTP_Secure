using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Secure
{
    /// <summary>
    /// Contains method to remove the xmlns from the WinSCP
    /// log file.
    /// </summary>
    public static class XmlnsRemove
    {
        public static void Remove(string fullFilePath)
        {
            var str = File.ReadAllText(fullFilePath);

            str = str.Replace("xmlns=\"http://winscp.net/schema/session/1.0\"", "");

            File.WriteAllText(fullFilePath, str);

        }
    }
}
