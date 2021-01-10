using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Waterplantinator_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		private string _Ip;
		public string Ip
		{
			get { return _Ip; }
			set
			{
				_Ip = value;
				OnPropertyChanged();
			}
		}

		private int _Port;
		public int Port
		{
			get { return _Port; }
			set
			{
				_Port = value;
				OnPropertyChanged();
			}
		}
		public Settings()
		{
			InitializeComponent();

			Assembly asm = Assembly.GetExecutingAssembly();
			FileInfo fi = new FileInfo(asm.Location);

			VersionLabel.Text = fi.LastWriteTime.ToShortDateString() + " V" + VersionTracking.CurrentVersion;
			ThemeSwitch.IsToggled = Application.Current.RequestedTheme == OSAppTheme.Dark;

			Ip = Preferences.Get("IP-adres", "192.168.0.45");
			Port = Preferences.Get("IP-poort", 11000);
			OnPropertyChanged();

			BindingContext = this;
		}

		/// <summary>
		/// Back button click event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BackButton_OnClicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}

		/// <summary>
		/// Toggles switch button to change theme
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Switch_Toggled(object sender, ToggledEventArgs e)
		{
			if (e.Value)
			{
				Application.Current.UserAppTheme = OSAppTheme.Dark;
				Preferences.Set("theme", "Dark");
			}
			else
			{
				Application.Current.UserAppTheme = OSAppTheme.Light;
				Preferences.Set("theme", "Light");
			}
		}

		/// <summary>
		/// On submit button click saves all data when validation is passed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SubmitButton_Clicked(object sender, EventArgs e)
		{
			ipPortEntry.BackgroundColor = ipEntry.BackgroundColor = Color.Transparent;
			if (ipEntry.Text.Split('.').Length == 4 && IPAddress.TryParse(ipEntry.Text, out _) && !String.IsNullOrWhiteSpace(ipEntry.Text))
			{
				Client.ip = ipEntry.Text;
				Preferences.Set("IP-adres", Client.ip);

				if (int.TryParse(ipPortEntry.Text, out _) && !String.IsNullOrWhiteSpace(ipPortEntry.Text))
				{
					Client.port = Convert.ToInt32(ipPortEntry.Text);
					Preferences.Set("IP-poort", Client.port);
					DisplayAlert("Gegevens opgeslagen", "Uw gegevens zijn opgeslagen!", "OK");
				}
				else
				{
					ipPortEntry.BackgroundColor = Color.Red;
					DisplayAlert("Niet opgeslagen", "Het ingevulde poort voldoet niet aan de eisen.", "OK");
				}
			}
			else
			{
				ipEntry.BackgroundColor = Color.Red;
				DisplayAlert("Niet opgeslagen", "Het ingevulde ip-adres voldoet niet aan de eisen.", "OK");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}