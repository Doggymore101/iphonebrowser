namespace iTunesDLLDebug
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lblCommonFiles = new System.Windows.Forms.Label();
            this.lblFoundPath = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "CommonFiles:";
            // 
            // lblCommonFiles
            // 
            this.lblCommonFiles.AutoSize = true;
            this.lblCommonFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommonFiles.Location = new System.Drawing.Point(124, 9);
            this.lblCommonFiles.Name = "lblCommonFiles";
            this.lblCommonFiles.Size = new System.Drawing.Size(45, 16);
            this.lblCommonFiles.TabIndex = 1;
            this.lblCommonFiles.Text = "label2";
            // 
            // lblFoundPath
            // 
            this.lblFoundPath.AutoSize = true;
            this.lblFoundPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFoundPath.Location = new System.Drawing.Point(12, 41);
            this.lblFoundPath.Name = "lblFoundPath";
            this.lblFoundPath.Size = new System.Drawing.Size(45, 16);
            this.lblFoundPath.TabIndex = 5;
            this.lblFoundPath.Text = "label2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(240, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Found iTunesMobileDevice.dll at:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 62);
            this.Controls.Add(this.lblFoundPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblCommonFiles);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.Text = "iTunesMobileDevice.dll Debug";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCommonFiles;
        private System.Windows.Forms.Label lblFoundPath;
        private System.Windows.Forms.Label label4;
    }
}

