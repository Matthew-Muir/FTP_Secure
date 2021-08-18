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
using System.Globalization;

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
            numericUpDown1.Value = Int32.Parse(taskHost.Properties["FtpPortNumber"].GetValue(taskHost).ToString());
            textBox3.Text = taskHost.Properties["FtpUserName"].GetValue(taskHost).ToString();
            textBox4.Text = taskHost.Properties["FtpPassword"].GetValue(taskHost).ToString();
            textBox5.Text = taskHost.Properties["TlsHostCertificateFingerprint"].GetValue(taskHost).ToString();
            comboBox1.SelectedIndex = comboBox1.FindStringExact(taskHost.Properties["FtpOperationName"].GetValue(taskHost).ToString());
            textBox6.Text = taskHost.Properties["FtpLocalPath"].GetValue(taskHost).ToString();
            textBox7.Text = taskHost.Properties["FtpRemotePath"].GetValue(taskHost).ToString();
            comboBox2.SelectedIndex = comboBox2.FindStringExact(taskHost.Properties["FtpRemove"].GetValue(taskHost).ToString());
            comboBox3.SelectedIndex = comboBox3.FindStringExact(taskHost.Properties["FtpMode"].GetValue(taskHost).ToString());
            comboBox4.SelectedIndex = comboBox4.FindStringExact(taskHost.Properties["FtpSecure"].GetValue(taskHost).ToString());
            textBox2.Text = taskHost.Properties["FtpLogPath"].GetValue(taskHost).ToString();

            #endregion

            #region Populate_Tooltips

            toolTip1.SetToolTip(label1, "Ip address or URL");
            toolTip2.SetToolTip(label2, "Implicit 990, Explicit 21");
            toolTip3.SetToolTip(label3, "Login Credential to access server");
            toolTip4.SetToolTip(label4, "Login Credential to access server");
            toolTip5.SetToolTip(label5, "This value isn't always necessary depending on FTP server configuration.\nThis credential should be provided to you by server the admin.\nElse connect to server via WinSCP app and obtain credential from Session->Server/Protocol Info");
            toolTip6.SetToolTip(label6, "GetFiles (download files from server)\nPutFiles (Upload files to server)");
            toolTip7.SetToolTip(label7, "File location on local machine.\nE.G. C:\\folder\\myfile.txt\nThis field can accept wild card expression.\nRead help for more details.");
            toolTip8.SetToolTip(label8, "Path to destination on server\nE.G. / to access root \n/myfolder/ to access a particular directory");
            toolTip9.SetToolTip(label9, "Delete source files/directory after transfer?");
            toolTip10.SetToolTip(label10, "This setting will be determined by FTP server.\nBy default Passive");
            toolTip11.SetToolTip(label11, "Explicit mode usually uses the same port as Plain (unsecure) mode.\nImplicit mode requires dedicated port.\nImplicit mode cannot be run on the same port as TLS/SSL Explicit mode.\nImplicit mode cannot be run on the same port as plain (unsecure) communication.");
            toolTip12.SetToolTip(textBox2, "Output path for log files. Leave blank to turn off logging");

            #endregion
        }

        //Help button
        private void button1_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
        }

        //Browse button
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Select File",
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = openFileDialog1.FileName;
            }


        }

        //Done Button
        private void button2_Click(object sender, EventArgs e)
        {
            taskHost.Properties["FtpHostName"].SetValue(taskHost, textBox1.Text);
            taskHost.Properties["FtpPortNumber"].SetValue(taskHost, (int)numericUpDown1.Value);
            taskHost.Properties["FtpUserName"].SetValue(taskHost, textBox3.Text);
            taskHost.Properties["FtpPassword"].SetValue(taskHost, textBox4.Text);
            taskHost.Properties["TlsHostCertificateFingerprint"].SetValue(taskHost, textBox5.Text);
            taskHost.Properties["FtpOperationName"].SetValue(taskHost, comboBox1.Text);
            taskHost.Properties["FtpLocalPath"].SetValue(taskHost, textBox6.Text);
            taskHost.Properties["FtpRemotePath"].SetValue(taskHost, textBox7.Text);
            taskHost.Properties["FtpRemove"].SetValue(taskHost, Boolean.Parse(comboBox2.Text));
            taskHost.Properties["FtpMode"].SetValue(taskHost, comboBox3.Text);
            taskHost.Properties["FtpSecure"].SetValue(taskHost, comboBox4.Text);
            taskHost.Properties["FtpLogPath"].SetValue(taskHost, textBox2);
        }

        //Browse button - For setting log file path.
        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();

            if (browserDialog.ShowDialog() ==  DialogResult.OK)
            {
                textBox2.Text = browserDialog.SelectedPath;
            }
        }
    }
}
