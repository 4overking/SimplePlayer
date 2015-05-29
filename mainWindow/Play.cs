using System;
using Un4seen.Bass;
using System.Runtime.InteropServices;
using System.IO;
using Un4seen.Bass.AddOn.Tags;

namespace mainWindow
{
	public class Play
	{
		internal PlayListWindow  playlist;
		public Play (MainWindow win)
		{
			BassNet.Registration("overking@inbox.ru", "2X2417151729148");
			var pluginDirHandle = Bass.BASS_PluginLoadDirectory(pluginDirPath);
			Bass.BASS_Init (-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
			syncProcEndStream = new SYNCPROC(endPlay);

			Config config = configManager.load();
			playlist = new PlayListWindow (win, config);
			if (config.currState == "play") {
				this.playSong (playlist.config.currPlay, playlist.config.currStatePosition);
			}
		}


		int stream = 0;
		public int Stream { 
			get { return stream; } 
			set 
			{ 
				Bass.BASS_ChannelStop (stream);
				stream = value; 
			}
		}

		private string _pluginDirPath = Directory.GetCurrentDirectory();
		private string pluginDirPath 
		{
			get
			{
				return _pluginDirPath + "/linux64/"; //get os and get path
			}

		}

		SYNCPROC syncProcEndStream;
		int handleSynchronizationEndStream; 
		private static GCHandle _hGCFile;


		public void  playSong(int number, double position=0)
		{	
			playlist.config.currState = "play";
			if (playlist.config.playlist.Count > 0) {
				playlist.config.currPlay = number;
				string file = playlist.config.playlist[playlist.config.currPlay].Path;
				Console.WriteLine (file);
				Stream = Bass.BASS_StreamCreateFile (file, 0, 0, BASSFlag.BASS_DEFAULT);
				if (Stream != 0) {
					setPosition (position);
					Bass.BASS_ChannelPlay (stream, false);

				} else {
					Console.WriteLine ("Stream error: {0}", Bass.BASS_ErrorGetCode ());
				}
				handleSynchronizationEndStream = Bass.BASS_ChannelSetSync (Stream, BASSSync.BASS_SYNC_END, 0, syncProcEndStream, IntPtr.Zero);
			}
		}
		protected void endPlay(int handle, int channel, int data, IntPtr user)
		{
			playlist.config.currPlay++;
			this.playSong (playlist.config.currPlay);
			Console.WriteLine ("nextSong");

		}
		public bool stopSong()
		{
			playlist.config.currState = "stop";
			Console.WriteLine (playlist.Count+"---count");
			return Bass.BASS_ChannelStop(Stream);

		}
		public bool Pause()
		{
			return Bass.BASS_ChannelPause(Stream);
		}
		public bool Volume(float value)
		{

			return Bass.BASS_ChannelSetAttribute(Stream,BASSAttribute.BASS_ATTRIB_VOL,value/100);
		}

		public string Now() 
		{
			tagInfo file = playlist.config.playlist[playlist.config.currPlay];
			string songInfo = file.Artist + " - " + file.Title + " :: "
				+ file.Duration + " : "	+ file.Type+ " : "
				+ file.BitRate+"kbps";
			return songInfo;
		}
		public bool isPaused() 
		{
			return Bass.BASS_ChannelIsActive(Stream) == BASSActive.BASS_ACTIVE_PAUSED;
		}
		public void volumeUp (){
			playlist.config.volume = (playlist.config.volume + Convert.ToSingle (0.05));
			Bass.BASS_SetVolume (playlist.config.volume);
		}
		public void volumeDown(){
			playlist.config.volume = (playlist.config.volume - Convert.ToSingle (0.05));
			Bass.BASS_SetVolume (playlist.config.volume);
		}
		public void nextSong(){
			playlist.config.currPlay++;
			this.playSong (playlist.config.currPlay);

		}
		public void prevSong(){
			playlist.config.currPlay--;
			playSong (playlist.config.currPlay);
		}
		public double getPosition (){
			long pos = Bass.BASS_ChannelGetPosition (Stream);
			return Bass.BASS_ChannelBytes2Seconds( Stream, pos);
		}
		public int getPositinPers(){
			long currPos = Bass.BASS_ChannelGetPosition (Stream);
			long len = Bass.BASS_ChannelGetLength(Stream, BASSMode.BASS_POS_BYTES);
			return (int)((currPos* 100) / len);
			 
		}
		public void setPosition (double pos){
			long position = Bass.BASS_ChannelSeconds2Bytes (Stream, pos);
			Bass.BASS_ChannelSetPosition (Stream, position);
		}
		public void skipToPostition(double percent){
			long len = Bass.BASS_ChannelGetLength(Stream, BASSMode.BASS_POS_BYTES);
			long bytesPos = (len * (long)percent) / 100;
			Bass.BASS_ChannelSetPosition (Stream, bytesPos);
		}
		public void clearPlaylist(){
			playlist.config.playlist.Clear ();
		}

		public tagInfo getTagInfo (string path)
		{
			tagInfo file = new tagInfo();

			TAG_INFO fileTags = BassTags.BASS_TAG_GetFromFile (path, true, true);

			file.Title = fileTags.title;
			file.Genre = fileTags.genre;
			file.Artist = fileTags.artist;
			file.Album = fileTags.album;
			file.BitRate = fileTags.bitrate;
			file.Composer = fileTags.composer;
			int totalSeconds = (int)fileTags.duration;
			int seconds = totalSeconds % 60;
			int minutes = totalSeconds / 60;
			file.Duration = minutes + ":" + seconds;
			file.Path = fileTags.filename;

			return file;

		}
	}
}

