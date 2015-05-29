using System.Collections.Generic;
using System;

namespace mainWindow
{
	public static class testClass
	{
		public static void show()
		{
			Gtk.Window window = new Gtk.Window ("TreeView Example");
			window.SetSizeRequest (500,200);
			window.Show ();
			Gtk.Image img = new Gtk.Image ();
			img.File = "/home/okg/caribbean_diplom.gif";
			window.Add (img);
			img.SizeRequest();
			img.Show ();
		}
	}
}

