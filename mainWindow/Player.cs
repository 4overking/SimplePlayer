using System;
using System.Collections.Generic;

using System.Windows.Forms;
using Gtk;


namespace mainWindow
{
	public class Player:Play
	{
		public bool isPlay = false;
		public Dialogs dialogs;
		MainWindow window;
		//public PlayListWindow playlist ;
		public Player (MainWindow win) : base (win)
		{
			isPlay = true;
			dialogs = new Dialogs();
			window = win;

			//playlist = new PlayListWindow (config);
			//автообновление слайдера прогресса 
			startAutoUpdateSlider ();

			//восстановление позиции ползунка
			updateMainSlider();
			//если был плейлист
		}

		public void playSong()
		{
			int sng = playlist.config.currPlay;

			//обновление лейбы Назания песни
			updateMainSlider ();
			playSong(sng);
		}
		public void Stop(){
			this.stopTimer ();
			stopSong ();
			updateMainSlider ();
		}

		public void Next(){
			nextSong ();
			updateMainSlider ();
		}
		public void Previous(){
			prevSong ();
			updateMainSlider ();
		}	
		public void openDir(){

			var data = dialogs.openDirDialog ();
			if (data != "") {
				string[] songs = mediaData.openDir (data);
				foreach (string sng in songs) {
					tagInfo tg = getTagInfo (sng);
					playlist.config.playlist.Add(tg);
				}
				//config.songs.AddRange (songs);
				//сделать нормально

}
		}
		public void openFiles(){
			var data = dialogs.openFileDialog ();
			if (data.Length>0) {
				foreach (string sng in data) {
					tagInfo tg = getTagInfo (sng);
					playlist.config.playlist.Add(tg);
				}
			}
		}
		public void skipToPercent(double pos){
			skipToPostition (pos);
		}

		uint progressTag = 0;

		public void startAutoUpdateSlider()
		{
			if (progressTag == 0) {
				progressTag = GLib.Timeout.Add (500, new GLib.TimeoutHandler (progressTimer));
			}

		}
		bool progressTimer ()
		{
			//Console.WriteLine ("timer tick");
			updateMainSlider ();
			return true;
		}
		private void updateMainSlider()
		{
			Action <int> updateSlr = window.updateSlider;
			updateSlr (getPositinPers ());
		}
		private void stopTimer(){
			GLib.Source.Remove(progressTag);
			
		}
		public string nowPlayed(){
			return Now ();
		}
		public void saveConfig(){
			playlist.config.currStatePosition = getPosition ();
			configManager.save (playlist.config);
		}

	}
}
