using System;
using System.IO;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Waterplantinator_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		public Settings()
		{
			InitializeComponent();

			Assembly asm = Assembly.GetExecutingAssembly();
			FileInfo fi = new FileInfo(asm.Location);

			VersionLabel.Text = fi.LastWriteTime.ToShortDateString() + " V" + VersionTracking.CurrentVersion;
			ThemeSwitch.IsToggled = Application.Current.RequestedTheme == OSAppTheme.Dark;
			Client.ip = Preferences.Get("IP-adres", "192.168.0.45");
			ipEntry.Text = Client.ip;
			Client.port = Preferences.Get("IP-poort", 11000);
			ipPortEntry.Text = Client.port.ToString();
		}
		     
		private void BackButton_OnClicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}

        private void Switch_Toggled(object sender, ToggledEventArgs e)		
        {
			if(e.Value)
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
        private void SubmitButton_Clicked(object sender, EventArgs e)
        {
			Client.ip = ipEntry.Text;
			Preferences.Set("IP-adres", Client.ip);
			Client.port = Convert.ToInt32(ipPortEntry.Text);
			Preferences.Set("IP-poort", Client.port);
			DisplayAlert("Gegevens opgeslagen", "Uw gegevens zijn opgeslagen!", "OK");
        }
    }
}