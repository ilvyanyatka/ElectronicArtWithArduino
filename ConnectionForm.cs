using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using FolderSelect;
using Sharer;

namespace ElectronicArt1
{
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
        }

        private void cbPort_DropDown(object sender, EventArgs e)
        {
            

        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            lbError.Text = "";
            cbPort.Items.Clear();
            cbPort.Items.AddRange(SharerConnection.GetSerialPortNames());
            if (cbPort.Items.Count > 0)
            {
                cbPort.SelectedIndex = 0;
            }
            else {
                lbError.Text = "Error: Arduino not found";
            }

            tbImageFolder.Text = Application.StartupPath + "\\images";
            ArduinoSettings.ImageFolder = tbImageFolder.Text;
            
            Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // create images folder
                if (!Directory.Exists(ArduinoSettings.ImageFolder))
                {
                    // Try to create the directory.
                    Directory.CreateDirectory(ArduinoSettings.ImageFolder);
                }
                
                Cursor = Cursors.WaitCursor;
                lbError.Text = "";
                if (ArduinoSettings.Connection != null)
                {
                    ArduinoSettings.Connection.Disconnect();
                }

                ArduinoSettings.COMPort = cbPort.Text;
                ArduinoSettings.Connection = new SharerConnection(ArduinoSettings.COMPort, ArduinoSettings.Baud);

                //ArduinoSettings.Connection.UserDataReceived += _connection_UserDataReceived;

                ArduinoSettings.Connection.Connect();
                ArtForm artForm = new ArtForm();
                artForm.TopLevel = true;
                artForm.ShowDialog();



            }
            catch (Exception ex)
            {
                lbError.Text = ex.Message;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderSelectDialog folderDialog = new FolderSelectDialog();
            folderDialog.InitialDirectory = Application.StartupPath;
            folderDialog.Title = "Folder for images";
            folderDialog.ShowDialog();
            tbImageFolder.Text = folderDialog.FileName;
            ArduinoSettings.ImageFolder = folderDialog.FileName;
        }
    }
}
