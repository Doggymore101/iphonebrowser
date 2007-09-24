using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SCW_iPhonePNG;

namespace DisplayApplePNG
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                Image img = iPhonePNG.ImageFromFile(openFileDialog1.FileName);
                pictureBox1.Image = img;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }
    }
}