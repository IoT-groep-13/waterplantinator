using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			//TODO: dark/light theme button
			//OSAppTheme currentTheme = Application.Current.RequestedTheme;
			//Application.Current.UserAppTheme = OSAppTheme.Dark;
			//Application.Current.UserAppTheme = OSAppTheme.Light;
		}
	}
}