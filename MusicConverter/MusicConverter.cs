using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MusicConverter
{
    public partial class MusicConverter : Form
    {
        private Config config = new Config();
        private string exeLoc;
        BackgroundWorker worker = new BackgroundWorker();

        public MusicConverter()
        {
            InitializeComponent();
            
            exeLoc = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf(@"\"));

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(Worker_Progress);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_Completed);

            string connStr = string.Format(@"Data Source={0}\musicLibrary.SQLiteDB;", exeLoc);

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Genre (name) VALUES ('Test')", conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {

                    }
                }
            }
        }

        private void OpenSourceFolderBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = @"D:\Development\SamplesForMusicConvertor\";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                SourceFolderLbl.Text = fbd.SelectedPath;
                config._srcDir = fbd.SelectedPath;
            }
        }

        private void OpenDestinationFolderBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = @"D:\Development\SamplesForMusicConvertor\";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                DestinationFolderLbl.Text = fbd.SelectedPath;
                config._destDir = fbd.SelectedPath;
            }
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (config._srcDir != null && config._destDir != null)
            {
                if (!worker.IsBusy)
                {
                    worker.RunWorkerAsync(config);
                }
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Config config = (Config)e.Argument;
            
 
            string dest = config._destDir;
            string[] tracks = Directory.GetFiles(config._srcDir, "*.mp3", SearchOption.AllDirectories);
            

            string folderPath, trackName, destFolder, convertSrc, convertDest;

            foreach (string track in tracks)
            {
                trackName = track.Substring(track.LastIndexOf(@"\")+1);
                
                folderPath = track.Substring(config._srcDir.Length);
                folderPath = folderPath.Remove(folderPath.Length - trackName.Length);

                trackName = trackName.Remove(trackName.Length - 4);

                destFolder = string.Format("{0}{1}", dest, folderPath);
                try
                {
                    // OGG
                    if (!Directory.Exists(string.Format(@"{0}ogg\", destFolder)))
                        Directory.CreateDirectory(string.Format(@"{0}ogg\", destFolder));

                    // MP3
                    if (!Directory.Exists(string.Format(@"{0}mp3\", destFolder)))
                        Directory.CreateDirectory(string.Format(@"{0}mp3\", destFolder));

                    convertSrc = string.Format(@"{0}mp3\{1}.mp3", destFolder, trackName);
                    convertDest = string.Format(@"{0}ogg\{1}.ogg", destFolder, trackName);

                    // just copy it to the MP3 folder
                    if (track.ToLower().IndexOf(".mp3") == (track.Length - 4))
                        File.Copy(track, convertSrc, true);

                    // Convert to OGG
                    var startInfo = new ProcessStartInfo();
                    startInfo.FileName = string.Format(@"{0}\sox\sox.exe",exeLoc);
                    startInfo.Arguments = string.Format("\"{0}\" \"{1}\"", convertSrc, convertDest);
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;

                    using (Process soxProc = Process.Start(startInfo))
                    {
                        soxProc.WaitForExit();
                    }
                    // this needs to somehow work out the artist etc
                    JockerSoft.OggReader trackInfo = new JockerSoft.OggReader(convertDest);

                }
                catch (Exception ex)
                {
                    var stop = "";
                }

            }
               
        }
        private void Worker_Progress(object sender, ProgressChangedEventArgs e)
        {
        }
        private void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        
    }

    public class Config
    {
        public string _srcDir = null;
        public string _destDir = null;

    }
}
