using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Secure
{
    public enum TransferType
    {
        Send,
        Receive,
        Failure
    }

    public enum Protocol
    {
        Sftp,
        Ftps,
        Ftp
    }

    public enum TransferResult
    {
        Success,
        Failure
    }

    public enum LogFileType
    {
        Custom,
        Standard
    }

    public enum OperationMode
    {
        GetFiles,
        PutFiles
    }
}
