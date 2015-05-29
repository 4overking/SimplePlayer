using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
namespace mainWindow
{
	public static class mediaData
	{
		//Поместить общесистемный список где-то!
		public static Config config; 
		public static string [] openDir (string dir){
			List<string> files = new List <string>();

			string[] MultipleFilters = config.fileFilter.Split('|');
			foreach (string FileFilter in MultipleFilters) {
				try
				{
					files.AddRange(Directory.GetFiles (dir, FileFilter));
				}
				catch {
				}
			}
			string[] incDirectories;
			try {
				incDirectories= Directory.GetDirectories (dir);
			}
			catch{
				return  null;
			}
			foreach (var incDir in incDirectories) {
				var incFiles = openDir (incDir);

				if (!(incFiles == null))
					foreach (var incFile in incFiles) {
						files.Add (incFile);
					}
			}
			return files.ToArray();
		}
		public static void openFiles(string [] files){

		}

	}

}

