using System;

namespace mainWindow
{
	public class MultiTab : Gtk.Box
	{
		public Gtk.Label Caption;
		Gtk.Image img = new Gtk.Image();
		public Gtk.ToolButton Close;
		public Gtk.Notebook _parent;

		public MultiTab ( string name )
		{
			CreateUI(name);
		}

		public MultiTab(string name, Gtk.Notebook parent)
		{
			_parent = parent;
			CreateUI(name);
			CreateHandlers();
		}

		void CreateUI(string name)
		{
			Caption = new Gtk.Label(name);
			Close = new Gtk.ToolButton(img,"");
			PackStart( Caption );
			PackStart( Close );
			ShowAll();
			Close.Hide();
		}

		void CreateHandlers()
		{
			Close.Clicked +=  delegate {
				_parent.RemovePage(_parent.CurrentPage);
			};
		}

		public bool Active;

	}    
}

