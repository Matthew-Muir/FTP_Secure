using Microsoft.SqlServer.Dts.Runtime.Design;
using Microsoft.SqlServer.Dts.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//PublicKeyToken=0dcba816e36a4a48
namespace FTP_Secure_UI
{
    public class FTP_Secure_UI : IDtsTaskUI
    {
        private TaskHost taskHostValue;
        public void Delete(System.Windows.Forms.IWin32Window parentWindow)
        {
            
        }

        public ContainerControl GetView()
        {
            return new MainForm(taskHostValue);
        }

        public void Initialize(TaskHost taskHost, IServiceProvider serviceProvider)
        {
            taskHostValue = taskHost;
        }

        public void New(System.Windows.Forms.IWin32Window parentWindow)
        {

        }
    }
}
