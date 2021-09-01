using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Secure
{
    public class LogEntry
    {
        public string Dts { get; set; }
        public TransferType TransferType { get; set; }
        public Protocol Protocol { get; set; }
        public string FileName { get; set; }
        public string Destination { get; set; }
        public TransferResult TransferResult { get; set; }

        public override string ToString()
        {
            return $"{this.Dts}|{this.TransferType}|{this.Protocol}|{this.FileName}|{this.Destination}|{this.TransferResult}\r\n";

        }
    }

}
