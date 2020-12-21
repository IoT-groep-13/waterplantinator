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

			//TODO: dark/light theme button
			//OSAppTheme currentTheme = Application.Current.RequestedTheme;
			//Application.Current.UserAppTheme = OSAppTheme.Dark;
			//Application.Current.UserAppTheme = OSAppTheme.Light;
		}

        private void LightButton_Clicked(object sender, EventArgs e)
        {
			Application.Current.UserAppTheme = OSAppTheme.Light;
		}

		private void DarkButton_Clicked(object sender, EventArgs e)
		{
			Application.Current.UserAppTheme = OSAppTheme.Dark;
		}

		private void BackButton_OnClicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}
	}
}