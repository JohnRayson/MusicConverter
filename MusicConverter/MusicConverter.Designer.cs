namespace MusicConverter
{
    partial class MusicConverter
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
            if (disposing && (components != null))
            {
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
            this.OpenSourceFolderBtn = new System.Windows.Forms.Button();
            this.OpenDestinationFolderBtn = new System.Windows.Forms.Button();
            this.SourceFolderLbl = new System.Windows.Forms.Label();
            this.DestinationFolderLbl = new System.Windows.Forms.Label();
            this.StartBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenSourceFolderBtn
            // 
            this.OpenSourceFolderBtn.Location = new System.Drawing.Point(12, 12);
            this.OpenSourceFolderBtn.Name = "OpenSourceFolderBtn";
            this.OpenSourceFolderBtn.Size = new System.Drawing.Size(154, 40);
            this.OpenSourceFolderBtn.TabIndex = 0;
            this.OpenSourceFolderBtn.Text = "Source Folder";
            this.OpenSourceFolderBtn.UseVisualStyleBackColor = true;
            this.OpenSourceFolderBtn.Click += new System.EventHandler(this.OpenSourceFolderBtn_Click);
            // 
            // OpenDestinationFolderBtn
            // 
            this.OpenDestinationFolderBtn.Location = new System.Drawing.Point(13, 58);
            this.OpenDestinationFolderBtn.Name = "OpenDestinationFolderBtn";
            this.OpenDestinationFolderBtn.Size = new System.Drawing.Size(154, 40);
            this.OpenDestinationFolderBtn.TabIndex = 1;
            this.OpenDestinationFolderBtn.Text = "Destination Folder";
            this.OpenDestinationFolderBtn.UseVisualStyleBackColor = true;
            this.OpenDestinationFolderBtn.Click += new System.EventHandler(this.OpenDestinationFolderBtn_Click);
            // 
            // SourceFolderLbl
            // 
            this.SourceFolderLbl.AutoSize = true;
            this.SourceFolderLbl.Location = new System.Drawing.Point(176, 26);
            this.SourceFolderLbl.Name = "SourceFolderLbl";
            this.SourceFolderLbl.Size = new System.Drawing.Size(161, 13);
            this.SourceFolderLbl.TabIndex = 2;
            this.SourceFolderLbl.Text = "<- Select folder to get music from";
            // 
            // DestinationFolderLbl
            // 
            this.DestinationFolderLbl.AutoSize = true;
            this.DestinationFolderLbl.Location = new System.Drawing.Point(176, 72);
            this.DestinationFolderLbl.Name = "DestinationFolderLbl";
            this.DestinationFolderLbl.Size = new System.Drawing.Size(276, 13);
            this.DestinationFolderLbl.TabIndex = 3;
            this.DestinationFolderLbl.Text = "<- Set destination folder, this should be the base directory";
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(13, 104);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(153, 52);
            this.StartBtn.TabIndex = 4;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // MusicConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 521);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.DestinationFolderLbl);
            this.Controls.Add(this.SourceFolderLbl);
            this.Controls.Add(this.OpenDestinationFolderBtn);
            this.Controls.Add(this.OpenSourceFolderBtn);
            this.Name = "MusicConverter";
            this.Text = "Music Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenSourceFolderBtn;
        private System.Windows.Forms.Button OpenDestinationFolderBtn;
        private System.Windows.Forms.Label SourceFolderLbl;
        private System.Windows.Forms.Label DestinationFolderLbl;
        private System.Windows.Forms.Button StartBtn;
    }
}

