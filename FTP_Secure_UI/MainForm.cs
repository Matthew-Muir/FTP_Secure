using Microsoft.SqlServer.Dts.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTP_Secure_UI
{
    public partial class MainForm : Form
    {
        private TaskHost taskHost;
        public MainForm(TaskHost taskHostValue)
        {
            InitializeComponent();
            taskHost = taskHostValue;

            #region Populate_Form_Fields

            textBox1.Text = taskHost.Properties["FtpHostName"].GetValue(taskHost).ToString();
            textBox2.Text = taskHost.Properties["FtpPortNumber"].GetValue(taskHost).ToString();
            textBox3.Text = taskHost.Properties["FtpUserName"].GetValue(taskHost).ToString();
            textBox4.Text = taskHost.Properties["FtpPassword"].GetValue(taskHost).ToString();
            textBox5.Text = taskHost.Properties["TlsHostCertificateFingerprint"].GetValue(taskHost).ToString();
            comboBox1.SelectedIndex = comboBox1.FindStringExact(taskHost.Properties["FtpOperationName"].GetValue(taskHost).ToString());
            textBox6.Text = taskHost.Properties["FtpLocalPath"].GetValue(taskHost).ToString();
            textBox7.Text = taskHost.Properties["FtpRemotePath"].GetValue(taskHost).ToString();
            comboBox2.SelectedIndex = comboBox2.FindStringExact(taskHost.Properties["FtpRemove"].GetValue(taskHost).ToString());
            comboBox3.SelectedIndex = comboBox3.FindStringExact(taskHost.Properties["FtpMode"].GetValue(taskHost).ToString());
            comboBox4.SelectedIndex = comboBox4.FindStringExact(taskHost.Properties["FtpSecure"].GetValue(taskHost).ToString());


            #endregion
        }


    }
}
