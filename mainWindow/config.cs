using System;
using System.IO;
using System.Xml.Serialization; 
using System.Collections.Generic;
namespace mainWindow
{
	public  class Config
	{
		public Config()
		{

		}
		public string fileFilter = "*.mp3|*.wav|*.flac";
		public string currState = "stop";
		public double currStatePosition = 0;
		private int _currPlay = 0;
		public bool showPlayList = false;
		//расширим наш список!!!
		//public List <tagInfo> playlist__ = new List<tagInfo>();

		public List <tagInfo> playlist = new List<tagInfo> ();

		public int currPlay 
		{
			get{ return _currPlay;}
			set 
			{

				if (value > playlist.Count-1) {
					_currPlay = playlist.Count-1;
				} else if (value < 0) {
					_currPlay = 0;
				} else {
					_currPlay = value;

				}
			}
		}

		private float _volume = 1;
		public float volume { 
			get { return _volume;}
			set
			{
				if (value > 1) {
					_volume = 1;
				} else if (value < 0) {
					_volume = 0;
				} else {
					_volume = value;

				}
			}
		}
	}

	//Класс для загрузки/выгрузки настроек пограммы
	public static class configManager 
	{
		private static string configName = "config.xml";
		public static void save (Config config)
		{
			using (Stream writer = new FileStream(configName, FileMode.Create))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Config));
				serializer.Serialize(writer, config);
			}
		}
		public static Config load ()
		{
			if (File.Exists (configName)) {
				using (Stream stream = new FileStream (configName, FileMode.Open)) {
					XmlSerializer serializer = new XmlSerializer (typeof(Config));

					// в тут же созданную копию класса iniSettings под именем iniSet
					Config config = (Config)serializer.Deserialize (stream);
					mediaData.config = config;
					return config;

				}
			} else {

				Config defConfig = new Config ();
				return defConfig;
			}

		}

	}
	class PlayList {

		public bool active = false;
		public Gtk.ListStore songs;
		public PlayList(){
			songs = new Gtk.ListStore (typeof(tagInfo));
		}
		public void Add (tagInfo song){
			songs.AppendValues (song);
		}
		public void Remove(tagInfo value)
		{
			//songs.Remove(value);

		}
		/*public int Count { 
			get {return songs.Co;} 
		}*/

	}


}

