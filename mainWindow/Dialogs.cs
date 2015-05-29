using System;
using Gtk;

namespace mainWindow
{
	public  class Dialogs
	{
		public Dialogs()
		{

		}
		public  string [] openFileDialog (){
			FileChooserDialog Fcd = new FileChooserDialog ("Открыть", null, FileChooserAction.Open);

			Fcd.AddButton(Stock.Cancel, ResponseType.Cancel);
			Fcd.AddButton(Stock.Open, ResponseType.Ok);
			Fcd.Filter = new FileFilter();
			Fcd.Filter.AddPattern("*.mp3");
			Fcd.SelectMultiple = true;
			ResponseType RetVal = (ResponseType)Fcd.Run();
			string[] data;
			if (RetVal == ResponseType.Ok) {
				data = Fcd.Filenames;
			} else {
				data = new string[0];
			}
			Fcd.Destroy();
			return data;
		}
		public  string  openDirDialog (){
			FileChooserDialog Fcd = new FileChooserDialog ("Открыть папку", null, FileChooserAction.SelectFolder);
			Fcd.AddButton(Stock.Cancel, ResponseType.Close);
			Fcd.AddButton(Stock.Open, ResponseType.Ok);

			ResponseType RetVal = (ResponseType)Fcd.Run();
			string dir;
			if (RetVal == ResponseType.Ok) {
				dir = Fcd.Filename;
			} else {
				dir = "";
			}
			Fcd.Destroy();
			return dir;
		}
	}
}

