/* **************************************************************
 * 
 * This code is provided as is, without any warranty.
 * The entire risk as to the quality, the performance of the code
 * for any particular purpose lies with the party using the code.
 * 
 * You may edit it and use into your programs, but if you do so,
 * remember to put this text into the credits window and into
 * documentation: 
 * "This program uses code developed by Jocker. http://jockersoft.no-ip.com"
 * and please drop me a line: jockersoft@altervista.org
 * 
 ****************************************************************/

using System;
using System.IO;
//using System.Collections;

namespace JockerSoft
{
	/// <summary>
	/// Reads the standard tags of an .ogg file
	/// </summary>
	public class OggReader
	{
		#region private properties
		//http://www.xiph.org/ogg/vorbis/doc/v-comment.html
		private string title = "";
		private string version = "";
		private string album = "";
		private string tracknumber = "";
		private string artist = "";
		private string performer = "";
		private string copyright = "";
		private string license = "";
		private string organization = "";
		private string description = "";
		private string genre = "";
		private string date = "";
		private string location = "";
		private string contact = "";
		private string iscr = "";
		//winamp
		private string comment = "";		

	//	private ArrayList other = new ArrayList();

		private System.Text.UTF8Encoding td = new System.Text.UTF8Encoding();

		#endregion				
		
		public OggReader(string FileName)
		{
			using(FileStream fs = new FileStream(FileName,FileMode.Open))
			{
				if (fs.Length == 0)
					throw (new Exception("File empty"));

				byte[] buffer = new byte[3];
				fs.Read(buffer,0,3);
				if (td.GetString(buffer).ToLower() != "ogg")
					throw (new Exception("Not Ogg file"));
				
				//					            v     o         
				byte[] end1 = { 0x01, 0x05, 0x76, 0x6F };
				//					  r     b     i     s
				byte[] end2 = { 0x72, 0x62, 0x69, 0x73 };

				buffer = new byte[4];
				do
				{
					//check if it's time to exit. It shouldn't be never used, but who knows!
					if (fs.Position == 2048 || fs.Position > fs.Length -10)
						break;

					//go back of 3 bytes
					fs.Position -= 3;
					//load the buffer with the next 4 bytes
					fs.Read(buffer, 0, 4);

					//check if it's time to exit
					if (buffer == end1)
					{
						fs.Read(buffer, 0, 4);
						if (buffer == end2)
							break;
					}

					//check if buffer contains a known tag
					switch (td.GetString(buffer).ToUpper())
					{
						case "TITL":
							//check that it really is the title tag (no "TitleOld" or others)
							if (fullTagName(fs) == "TITLE")
								//set the title property with the title tag
								this.title = this.readNext(fs);
							break;
						case "VERS":
							if (fullTagName(fs) == "VERSION")
								this.version = readNext(fs);
							break;
						case "ALBU":
							if (fullTagName(fs) == "ALBUM")
								this.album = readNext(fs);
							break;
						case "TRAC":
							if (fullTagName(fs) == "TRACKNUMBER")
								this.tracknumber = readNext(fs);
							break;
						case "ARTI":
							if (fullTagName(fs) == "ARTIST")
								this.artist = readNext(fs);
							break;
						case "PERF":
							if (fullTagName(fs) == "PERFORMER")
								this.performer = readNext(fs);
							break;
						case "COPY":
							if (fullTagName(fs) == "COPYRIGHT")
								this.copyright = readNext(fs);
							break;
						case "LICE":
							if (fullTagName(fs) == "LICENSE")
								this.license = readNext(fs);
							break;
						case "ORGA":
							if (fullTagName(fs) == "ORGANIZATION")
								this.organization = readNext(fs);
							break;
						case "DESC":
							if (fullTagName(fs) == "DESCRIPTION")
								this.description = readNext(fs);
							break;
						case "GENR":
							if (fullTagName(fs) == "GENRE")
								this.genre = readNext(fs);
							break;
						case "DATE":
							fs.Position++;  //avoid to read =
							this.date = readNext(fs);
							break;
						case "LOCA":
							if (fullTagName(fs) == "LOCATION")
								this.location = readNext(fs);
							break;							
						case "CONT":
							if (fullTagName(fs) == "CONTACT")
								this.contact = readNext(fs);
							break;
						case "ISCR":
							fs.Position++;  //avoid to read =
							this.iscr = readNext(fs);
							break;							
						case "COMM":
							if (fullTagName(fs) == "COMMENT")
								this.comment = readNext(fs);
							break;
							//					default:
							//						other.Add(fullTagName(fs) + "=" + readNext(fs));
							//						break;
					}
				}while (true);

		//		Console.WriteLine("done");
			}
		}


		/// <summary>
		/// reads the tag content
		/// </summary>
		/// <param name="fs">filestream of the ogg file (Position must be after the =)</param>
		/// <returns>the text of the current tag</returns>
		private string readNext(FileStream fs)
		{
			string text = "";

			byte[] byt = new byte[1];

			do
			{
				fs.Read(byt,0,1);
            if (byt[0] == 0x00) //the tag is finished: all tags are followed by 0? and one or more 00
				{
					text = text.Remove(text.Length-1, 1);		//remove the 0? byte
					break;
				}
				else if (byt[0] == 0x01) //0x01 is at the end of all tags. no more tags in the file
					break;

				text += td.GetString(byt);
			} while (true);
			return text;
		}


		/// <summary>
		/// reads the full name of the tag
		/// </summary>
		/// <param name="fs">FileStream of the ogg file (position must be 4 chars after the start of the tag name)</param>
		/// <returns>the full name of the tag</returns>
		private string fullTagName(FileStream fs)
		{
			string tag = "";
			fs.Seek(-4, SeekOrigin.Current);		//go where the tag name starts

			byte[] byt = new byte[1];

			do
			{
				fs.Read(byt,0,1);
				if (byt[0] == 0x3D)//an = always follows tha tag name. //hex: 3D    decimal: 61    string: =
					break;
				tag += td.GetString(byt);
			} while(true);
			return tag;
		}


		#region public properties
		public string Title
		{
			get { return this.title; }
		}
		public string Version
		{
			get { return this.version; }
		}
		public string Album
		{
			get { return this.album; }
		}
		public string TrackNumber
		{
			get { return this.tracknumber; }
		}
		public string Artist
		{
			get { return this.artist; }
		}
		public string Performer
		{
			get { return this.performer; }
		}
		public string Copyright
		{
			get { return this.copyright; }
		}
		public string License
		{
			get { return this.license; }
		}
		public string Organization
		{
			get { return this.organization; }
		}
		public string Description
		{
			get { return this.description; }
		}
		public string Genre
		{
			get { return this.genre; }
		}
		public string Date
		{
			get { return this.date; }
		}
		public string Location
		{
			get { return this.location; }
		}
		public string Contact
		{
			get { return this.contact; }
		}
		public string Iscr
		{
			get { return this.iscr; }
		}
		public string Comment
		{
			get { return this.comment; }
		}
		#endregion

	}
}
