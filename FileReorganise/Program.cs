using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReorganise
{
    class Program
    {
        static void Main(string[] args)
        {
            string srcDirectory = @"D:\Music\Reorganised-Temp\";
            string destDirectory = @"D:\Music\Reorganised\";

            string[] artists = Directory.GetDirectories(srcDirectory);

            foreach (string atristDir in artists)
            {
                string[] albums = Directory.GetDirectories(atristDir);
                foreach (string album in albums)
                {
                    string dest = string.Format("{0}{1}",destDirectory,album.Remove(0,srcDirectory.Length));
                    
                    // OGG
                    if(!Directory.Exists(string.Format(@"{0}\ogg\",dest)))
                        Directory.CreateDirectory(string.Format(@"{0}\ogg\",dest));
                    
                    // MP3
                    if (!Directory.Exists(string.Format(@"{0}\mp3\", dest)))
                        Directory.CreateDirectory(string.Format(@"{0}\mp3\", dest));

                    string[] tracks = Directory.GetFiles(album);

                    foreach (string track in tracks)
                    {
                        Console.WriteLine(track);
                        string trackName = track.Substring(track.LastIndexOf(@"\"));
                        try
                        {

                            if (track.ToLower().IndexOf(".mp3") == (track.Length - 4))
                                File.Copy(track, string.Format(@"{0}\mp3{1}", dest, trackName), true);
                            //Console.WriteLine(string.Format(@"{0}\mp3{1}", dest, trackName));

                            if (track.ToLower().IndexOf(".ogg") == (track.Length - 4))
                                File.Copy(track, string.Format(@"{0}\ogg{1}", dest, trackName), true);
                            //Console.WriteLine(string.Format(@"{0}\ogg{1}", dest, trackName));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("FAILED");
                        }
                            
                    }

                    Console.WriteLine(dest);
                }
            }
            Console.ReadLine();
        }
    }
}
