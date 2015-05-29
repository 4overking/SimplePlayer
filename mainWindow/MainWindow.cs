using System;
using Gtk;
namespace mainWindow
{

	public partial class MainWindow: Gtk.Window
	{

		//internal Player player;
		internal Player  player;
		public HScale hscale; 

		public MainWindow () : base (Gtk.WindowType.Toplevel)
		{
			Build ();
			player = new Player(this);

			hscale = this.hscale1;


		}
		//Событи при выходе и программы!!
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			player.saveConfig ();
			Application.Quit ();
			a.RetVal = true;
		}
		/**
		 * Обработка нажатых кнопок
		 *
		*/
		protected void OnButton12Clicked (object sender, EventArgs e)
		{
			player.playSong();
			label2.Text = player.nowPlayed ();
		}

		protected void OnButton10Clicked (object sender, EventArgs e)
		{
			player.Stop ();
		}

		protected void OnButton13Clicked (object sender, EventArgs e)
		{
			player.Next ();
		}

		protected void OnButton11Clicked (object sender, EventArgs e)
		{
			player.Previous ();
		}



		protected void OnButton14Clicked (object sender, EventArgs e)
		{
			player.openFiles ();
		}

		protected void OnHscale1FormatValue (object o, FormatValueArgs args)
		{
			Console.WriteLine (this.hscale1.Value);
		}

		protected void OnHscale1MoveSlider (object o, MoveSliderArgs args)
		{
			double pos = this.hscale1.Value;
			player.skipToPercent (pos);
		}

		protected void OnHscale1ValueChanged (object sender, EventArgs e)
		{
			sender.ToString ();
			Console.WriteLine ("Slider changed " + e.GetType ());

		}

		protected void OnButton1Clicked (object sender, EventArgs e)
		{
			player.clearPlaylist ();
		}


		protected void OnButton15Clicked (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected void OnButton16Clicked (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}
		//test button
		protected void OnButton19Clicked (object sender, EventArgs e)
		{
			//setSlider (80);
			//player.playlist.playSong (0);
			//player.config.playlist.testMeth ();
			Console.WriteLine ("I`m a trst button!");

			testClass.show ();
		}

		/**
		 * Методы для делегирования управления окном
		 **/
		public void setLbel(string text){
			this.label2.Text = text;
		}
		public void updateSlider(int pos){
			this.hscale1.Value = pos;

		}

		/*сделать как этого требуют правила изменение слайдеров и 
		 * ползунков через Adjustment
		 **/

		protected void OnTogglebutton1Clicked (object sender, EventArgs e)
		{
			/*if (this.togglebutton1.Active)
				playlist.Show ();
			else
				playlist.Hide ();*/
		}



		protected void OnButton2Clicked (object sender, EventArgs e)
		{
			player.openDir ();
		}
	}

}