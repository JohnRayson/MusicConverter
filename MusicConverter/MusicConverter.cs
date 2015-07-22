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
        private int totalTracks = 0;
        private Config config = new Config();
        private string exeLoc, ffmpegLoc;
        BackgroundWorker worker = new BackgroundWorker();

        public MusicConverter()
        {
            InitializeComponent();
            
            exeLoc = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf(@"\"));
            ffmpegLoc = string.Format(@"{0}\ffmpeg\ffmpeg.exe", exeLoc);

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(Worker_Progress);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_Completed);
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

                    totalTracks = 0;
                    progressBar.Maximum = 100;
                    progressBar.Step = 1;
                    progressBar.Value = 0;

                    worker.RunWorkerAsync(config);
                }
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            Config config = (Config)e.Argument;

            string destFolder = config._destDir;
            string[] tracks = Directory.GetFiles(config._srcDir, "*.mp3", SearchOption.AllDirectories);

            worker.ReportProgress(tracks.Length, new TrackInfo());

            StringBuilder sql, sqlCols, sqlVals;
            string trackName;
            int fileNum = 0;

            foreach (string track in tracks)
            {
                fileNum++;
                // reset
                destFolder = config._destDir;

                trackName = track.Substring(track.LastIndexOf(@"\")+1);
                trackName = trackName.Remove(trackName.Length - 4);

                string fileInfo, trimKey, trimVal;
                string[] lines;
                
                try
                {
                    // read tags
                    var startInfo = new ProcessStartInfo();
                    startInfo.FileName = ffmpegLoc;
                    startInfo.Arguments = string.Format(" -i \"{0}\" -f ffmetadata ", track);
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;

                    using (Process ffmpegProc = Process.Start(startInfo))
                    {
                        ffmpegProc.WaitForExit();
                        fileInfo = ffmpegProc.StandardOutput.ReadToEnd();
                        // I'm expecting this to fail as ffmpeg reports an error unless you specify an output file - but theres no need, we just want to read fgrom the "screen"
                        if(fileInfo == "")
                            fileInfo = ffmpegProc.StandardError.ReadToEnd();
                    }

                    lines = fileInfo.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    TrackInfo trackInfo = new TrackInfo();

                    foreach(string line in lines)
                    {
                        trimKey = line.TrimStart();
                        if (trimKey.IndexOf(':') > 0)
                            trimVal = line.Split(new char[] { ':' })[1].Trim();
                        else
                            trimVal = "";

                        // some tracks have multiple metadata "Prince - Purple Rain" - so only set them if its not empty
                        if (trimVal == "")
                            continue;

                        if (trimKey.IndexOf("publisher") == 0)
                            trackInfo._publisher = trimVal;
                        if (trimKey.IndexOf("track") == 0)
                            trackInfo._track = trimVal;
                        if (trimKey.IndexOf("album") == 0 && trimKey.IndexOf("album_artist") != 0)
                            trackInfo._album = trimVal;
                        if (trimKey.IndexOf("title") == 0)
                            trackInfo._title = trimVal;
                        if (trimKey.IndexOf("genre") == 0)
                            trackInfo._genre = trimVal;
                        if (trimKey.IndexOf("composer") == 0)
                            trackInfo._composer = trimVal;
                        if (trimKey.IndexOf("artist") == 0)
                            trackInfo._artist = trimVal;
                        if (trimKey.IndexOf("date") == 0)
                            trackInfo._year = trimVal;
                    }

                    // correct some of the settings / missing values
                    while (trackInfo._track.Length < 2)
                        trackInfo._track = string.Format("0{0}", trackInfo._track);

                    if (trackInfo._artist == "")
                        trackInfo._artist = "Unknown Artist";
                    if (trackInfo._album == "")
                        trackInfo._album = "Unknown Album";
                    if (trackInfo._title == "")
                        trackInfo._title = "Unknown Track";

                    destFolder = string.Format(@"{0}\{1}\{2}\", destFolder, trackInfo._artist, trackInfo._album);

                    trackName = string.Format("{0} - {1}", trackInfo._track, trackInfo._title);

                    // update the UI
                    worker.ReportProgress(fileNum, trackInfo);

                    // index it
                    string connStr = string.Format(@"Data Source={0}\musicLibrary.sqlite; Version=3;", exeLoc);
                    using (SQLiteConnection conn = new SQLiteConnection(connStr))
                    {
                        conn.Open();
                        sql = new StringBuilder();
                        // insert them to generate ids
                        sql.AppendFormat(" INSERT INTO Album(name) SELECT '{0}' WHERE NOT EXISTS(SELECT 1 FROM Album WHERE name = '{0}'); ", trackInfo._album);
                        sql.AppendFormat(" INSERT INTO Artist(name) SELECT '{0}' WHERE NOT EXISTS(SELECT 1 FROM Artist WHERE name = '{0}'); ", trackInfo._artist);
                        sql.AppendFormat(" INSERT INTO Composer(name) SELECT '{0}' WHERE NOT EXISTS(SELECT 1 FROM Composer WHERE name = '{0}'); ", trackInfo._composer);
                        sql.AppendFormat(" INSERT INTO Genre(name) SELECT '{0}' WHERE NOT EXISTS(SELECT 1 FROM Genre WHERE name = '{0}'); ", trackInfo._genre);
                        sql.AppendFormat(" INSERT INTO Publisher(name) SELECT '{0}' WHERE NOT EXISTS(SELECT 1 FROM Publisher WHERE name = '{0}'); ", trackInfo._publisher);
                        
                        // select back the ids
                        sql.AppendFormat(" SELECT [id] FROM Album WHERE [name] = '{0}'; ", trackInfo._album);
                        sql.AppendFormat(" SELECT [id] FROM Artist WHERE [name] = '{0}'; ", trackInfo._artist);
                        sql.AppendFormat(" SELECT [id] FROM Composer WHERE [name] = '{0}'; ", trackInfo._composer);
                        sql.AppendFormat(" SELECT [id] FROM Genre WHERE [name] = '{0}'; ", trackInfo._genre);
                        sql.AppendFormat(" SELECT [id] FROM Publisher WHERE [name] = '{0}'; ", trackInfo._publisher);


                        SQLiteCommand cmd = new SQLiteCommand(sql.ToString(), conn);
                        SQLiteDataReader reader = cmd.ExecuteReader();

                        sqlCols = new StringBuilder();
                        sqlVals = new StringBuilder();

                        // album
                        if (reader.HasRows)
                        {
                            reader.Read();
                            sqlCols.Append(",[album]");
                            sqlVals.Append(",@album");
                            cmd.Parameters.AddWithValue("@album",reader["id"].ToString());
                        }
                        reader.NextResult();
                        // artist
                        if (reader.HasRows)
                        {
                            reader.Read();
                            sqlCols.Append(",[artist]");
                            sqlVals.Append(",@artist");
                            cmd.Parameters.AddWithValue("@artist", reader["id"].ToString());
                        }
                        reader.NextResult();
                        // composer
                        if (reader.HasRows)
                        {
                            reader.Read();
                            sqlCols.Append(",[composer]");
                            sqlVals.Append(",@composer");
                            cmd.Parameters.AddWithValue("@composer", reader["id"].ToString());
                        }
                        reader.NextResult();
                        // genre
                        if (reader.HasRows)
                        {
                            reader.Read();
                            sqlCols.Append(",[genre]");
                            sqlVals.Append(",@genre");
                            cmd.Parameters.AddWithValue("@genre", reader["id"].ToString());
                        }
                        reader.NextResult();
                        // publisher
                        if (reader.HasRows)
                        {
                            reader.Read();
                            sqlCols.Append(",[publisher]");
                            sqlVals.Append(",@publisher");
                            cmd.Parameters.AddWithValue("@publisher", reader["id"].ToString());
                        }

                        reader.Close();

                        sql = new StringBuilder();
                        sql.AppendFormat(" INSERT INTO track ([title],[year],[track],[location]{0}) VALUES ('{1}','{2}','{3}','{4}'{5}); ", sqlCols, trackInfo._title, trackInfo._year, trackInfo._track, destFolder, sqlVals);
                        cmd.CommandText = sql.ToString();
                        cmd.ExecuteNonQuery();
                    }


                    // MP3
                    if (!Directory.Exists(string.Format(@"{0}mp3\", destFolder)))
                        Directory.CreateDirectory(string.Format(@"{0}mp3\", destFolder));

                    File.Copy(track, string.Format(@"{0}mp3\{1}.mp3", destFolder, trackName), true);

                    // OGG
                    if (!Directory.Exists(string.Format(@"{0}ogg\", destFolder)))
                        Directory.CreateDirectory(string.Format(@"{0}ogg\", destFolder));

                    /* TURNED OFF FOR SPEED DURING TESTING
                    
                    // Convert to OGG
                    startInfo = new ProcessStartInfo();
                    startInfo.FileName = ffmpegLoc;
                    startInfo.Arguments = string.Format(" -i \"{0}\" \"{1}\"", track, string.Format(@"{0}ogg\{1}.ogg", destFolder, trackName));
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;

                    using (Process ffmpegProc = Process.Start(startInfo))
                    {
                        ffmpegProc.WaitForExit();
                    }
                      
                    */
                    
                }
                catch (Exception ex)
                {
                    var stop = "";
                }

            }
               
        }
        private void Worker_Progress(object sender, ProgressChangedEventArgs e)
        {
            TrackInfo trackInfo = (TrackInfo)e.UserState;

            if (totalTracks == 0)
            {
                progressBar.Value = 0;
                totalTracks = e.ProgressPercentage;
                TracksIndexLbl.Text = string.Format("Tracks Indexed: 0/{0}", totalTracks);
            }
            else
            {
                int precentComplete = ((100 / totalTracks) * e.ProgressPercentage);
                progressBar.Value = precentComplete;
                TracksIndexLbl.Text = string.Format("Tracks Indexed: {0}/{1}", e.ProgressPercentage, totalTracks);
            }

            progressGrid.Rows.Add(trackInfo._artist, trackInfo._album, trackInfo._track, trackInfo._title, trackInfo._year);

        }
        private void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Value = 100;

            MessageBox.Show("All done!");
        }

        
    }

    public class Config
    {
        public string _srcDir = null;
        public string _destDir = null;

    }

    public class TrackInfo
    {
        public string _publisher;
        public string _track;
        public string _album;
        public string _title;
        public string _genre;
        public string _composer;
        public string _artist;
        public string _year;
    }
}
