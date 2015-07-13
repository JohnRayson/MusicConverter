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
            this.progressGrid = new System.Windows.Forms.DataGridView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progress_artist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progress_album = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progress_track = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progress_title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progress_year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.progressGrid)).BeginInit();
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
            // progressGrid
            // 
            this.progressGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.progressGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.progress_artist,
            this.progress_album,
            this.progress_track,
            this.progress_title,
            this.progress_year});
            this.progressGrid.Location = new System.Drawing.Point(13, 182);
            this.progressGrid.Name = "progressGrid";
            this.progressGrid.Size = new System.Drawing.Size(787, 327);
            this.progressGrid.TabIndex = 5;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(179, 104);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(621, 52);
            this.progressBar.TabIndex = 6;
            // 
            // progress_artist
            // 
            this.progress_artist.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.progress_artist.HeaderText = "Artist";
            this.progress_artist.Name = "progress_artist";
            // 
            // progress_album
            // 
            this.progress_album.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.progress_album.HeaderText = "Album";
            this.progress_album.Name = "progress_album";
            // 
            // progress_track
            // 
            this.progress_track.HeaderText = "#";
            this.progress_track.Name = "progress_track";
            this.progress_track.Width = 25;
            // 
            // progress_title
            // 
            this.progress_title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.progress_title.HeaderText = "Title";
            this.progress_title.Name = "progress_title";
            // 
            // progress_year
            // 
            this.progress_year.HeaderText = "Year";
            this.progress_year.Name = "progress_year";
            this.progress_year.Width = 40;
            // 
            // MusicConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 521);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.progressGrid);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.DestinationFolderLbl);
            this.Controls.Add(this.SourceFolderLbl);
            this.Controls.Add(this.OpenDestinationFolderBtn);
            this.Controls.Add(this.OpenSourceFolderBtn);
            this.Name = "MusicConverter";
            this.Text = "Music Converter";
            ((System.ComponentModel.ISupportInitialize)(this.progressGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenSourceFolderBtn;
        private System.Windows.Forms.Button OpenDestinationFolderBtn;
        private System.Windows.Forms.Label SourceFolderLbl;
        private System.Windows.Forms.Label DestinationFolderLbl;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.DataGridView progressGrid;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn progress_artist;
        private System.Windows.Forms.DataGridViewTextBoxColumn progress_album;
        private System.Windows.Forms.DataGridViewTextBoxColumn progress_track;
        private System.Windows.Forms.DataGridViewTextBoxColumn progress_title;
        private System.Windows.Forms.DataGridViewTextBoxColumn progress_year;
    }
}

