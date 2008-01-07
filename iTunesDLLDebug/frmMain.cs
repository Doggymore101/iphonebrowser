using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iTunesDLLDebug
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblCommonFiles.Text = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);

            string addpath = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) + @"\Apple\Mobile Device Support\bin";
            if (File.Exists(addpath + @"\iTunesMobileDevice.dll"))
                lblFoundPath.Text = addpath;
            else {
                addpath = @"C:\Program Files\Apple\Mobile Device Support\bin";
                if (File.Exists(addpath + @"\iTunesMobileDevice.dll"))
                    lblFoundPath.Text = addpath;
            }
        }
    }
}