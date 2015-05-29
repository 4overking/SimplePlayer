using System.Collections.Generic;
using System;


namespace mainWindow
{
	public class PlayListWindow 
	{
		Gtk.TreeView tree = new Gtk.TreeView ();
		public Gtk.ListStore musicListStore = new Gtk.ListStore (
			typeof (string), typeof (string), 
			typeof (string), typeof (string),
			typeof (int),
			typeof (string), typeof (string), 
			typeof (string), typeof (string));
		public Config config;

		public PlayListWindow (MainWindow win, Config cfg)
		{
			//musicListStore = sngs;
			// Create a Window
			config = cfg;
			foreach (tagInfo song in config.playlist) {
				addSongs (song);
			}


			Gtk.Window window = new Gtk.Window ("TreeView Example");
			window.SetSizeRequest (500,200);

			// Create our TreeView

			tree.Selection.Changed+= new EventHandler (change);
			
			// Add our tree to the window
			window.Add (tree);
			tree.Model = musicListStore;
			tree.HeadersVisible = true;
			// Create a column for the artist name
			Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn ();
			artistColumn.Title = "Artist";

			// Create the text cell that will display the artist name
			Gtk.CellRendererText artistNameCell = new Gtk.CellRendererText ();

			// Add the cell to the column
			artistColumn.PackStart (artistNameCell, true);

			// Create a column for the song title
			Gtk.TreeViewColumn songColumn = new Gtk.TreeViewColumn ();
			songColumn.Title = "Song Title";

			// Do the same for the song title column
			Gtk.CellRendererText songTitleCell = new Gtk.CellRendererText ();
			songTitleCell.Foreground = "red";
			songTitleCell.Background="black";
			songTitleCell.CellBackground = "green";
			songColumn.PackStart (songTitleCell, true);

			Gtk.TreeViewColumn timeColumn = new Gtk.TreeViewColumn ();
			timeColumn.Title = "time";
			timeColumn.MaxWidth = 50;
			songColumn.MinWidth = 150;
			// Add the columns to the TreeView
			tree.AppendColumn (artistColumn);
			tree.AppendColumn (songColumn);
			tree.AppendColumn (timeColumn);

			// Tell the Cell Renderers which items in the model to display
			artistColumn.AddAttribute (artistNameCell, "text", 0);
			songColumn.AddAttribute (songTitleCell, "text", 1);
			timeColumn.AddAttribute (songTitleCell, "text", 2);

			// Create a model that will hold two strings - Artist Name and Song Title
			// Assign the model to the TreeView


			// Show the window and everything on it
			window.ShowAll ();
		}
		public void change(object sender, object e ){
			Console.WriteLine ("click");
		}
		private void addSongs(tagInfo song){
			musicListStore.AppendValues (
				song.Title, song.Genre, 
				song.Artist, song.Album,
				song.BitRate, 
				song.Type, song.Composer, 
				song.Duration, song.Path);
		}
		public int Count{
			 
			get
			{
				int ii = 0;


				return ii;
			}
		}



	}

	//Расширим List для наших нужд
	/*public class PlayList<tagInfo>: List<tagInfo>{

		public Gtk.ListStore musicListStore3 = new Gtk.ListStore (
			typeof (string), typeof (string), 
			typeof (string), typeof (string),
			typeof (int),
			typeof (string), typeof (string), 
			typeof (string), typeof (string)
		);public Gtk.ListStore musicListStore = new Gtk.ListStore (typeof(tagInfo));

		private void addToMusicStore (tagInfo song)
		{
	
			/*musicListStore.AppendValues (
				song.Title, song.Genre, 
				song.Artist, song.Album,
				song.BitRate, 
				song.Type, song.Composer, 
				song.Duration, song.Path);

		*/
			

		
		/*
		public PlayList(){
			Console.WriteLine ("construct");
		}
		//PlayListWindow playlist;
		public void Add(tagInfo value)
		{
			base.Add(value);
			musicListStore.AppendValues (value);
			//addToMusicStore ((mainWindow.tagInfo)value);
			Console.WriteLine("add the song");
			//plstW.fillPlaylist ();

		}
			public void Remove(tagInfo value)
			{
				base.Remove(value);

			}
		public int Count { 
			get {return base.Count;} 
		}
		public void Clear (){
			base.Clear ();
		}
		public void test(){
			Console.WriteLine ("test meth");
		}
	}*/
}