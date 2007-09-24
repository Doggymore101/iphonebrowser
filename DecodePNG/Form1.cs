using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SCW_iPhonePNG;

namespace DecodePNG
{
    public partial class Form1 : Form
    {
        private int indent = 0;

        public Form1() {
            InitializeComponent();
        }

        string spaces(int ct) {
            return new string(' ', ct);
        }

        void wo(string s) {
            tbOut.Text += spaces(indent)+ s + Environment.NewLine;
        }
        void ind() {
            indent += 5;
        }

        void oud() {
            indent -= 5;
        }

        string decodeColorType(IHDRChunk ic) {
            List<string> ans = new List<string>();

            if (ic.HasPalette)
                ans.Add("palette");
            if (ic.HasColor)
                ans.Add("color");
            if (ic.HasAlpha)
                ans.Add("alpha");

            return String.Join(",", ans.ToArray());
        }

        void dIHDR(PNGChunk c) {
            IHDRChunk ic = new IHDRChunk(c);

            ind();

            wo("Width: " + ic.Width);
            wo("Height: " + ic.Height);
            wo("BitDepth: " + ic.BitDepth);
            wo("ColorType: " + ic.ColorType + " " + decodeColorType(ic));

            oud();
        }

        void DoDecodePNG(string fp) {
            tbOut.Text = "";

            List<PNGChunk> chunks = iPhonePNG.ReadPNG(fp);

            foreach (PNGChunk c in chunks) {
                wo(c.Signature + " Len(" + c.Length + ") [" + c.CRC.ToString("x") + "]");
                switch (c.Signature) {
                    case "IHDR":
                        dIHDR(c);
                        break;
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                string fp = openFileDialog1.FileName;
                Text = Path.GetFileName(fp) + " - DecodePNG";

                DoDecodePNG(fp);
            }
        }
    }
}