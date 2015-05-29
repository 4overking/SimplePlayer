using System;
using Gtk;

namespace mainWindow
{
	class MainClass
	{

		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
			//PlayList <tagInfo> playlist = new PlayList<tagInfo>();
		}
	}
}
