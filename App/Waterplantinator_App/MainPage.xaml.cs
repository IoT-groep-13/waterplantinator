using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Waterplantinator_App
{
	public partial class MainPage : ContentPage
	{

		public SensorData SensorData { get; set; }

		public MainPage()
		{
			InitializeComponent();
			GetData();
			FrontSide.IsVisible = true;
			BackSide.IsVisible = false;

			BindingContext = this;
		}

		/// <summary>
		/// Gets data via connection or last data if no connection can be made
		/// </summary>
		private void GetData()
		{
			Client.OpenConnection();
			if (Client.connected)
			{
				SensorData = Client.Receive();
				Client.CloseConnection();
				ConnectionDot.Fill = Brush.LightGreen;
			}
			else
			{
				SensorData = Client.GetLastData();
				ConnectionDot.Fill = Brush.Red;
			}
		}


		private void RefreshIBtn_OnClicked(object sender, EventArgs e)
		{
			ConnectionDot.Fill = Brush.LightBlue;
			GetData();
		}

		private void SettingsIBtn_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new Settings());
		}

		private void WaterBtn_OnClicked(object sender, EventArgs e)
		{
			Client.OpenConnection();
			Client.GiveWater();
			Client.CloseConnection();
		}

		private async void BackSide_OnTapped(object sender, EventArgs e)
		{
			await Task.WhenAll(
			BackSide.RotateYTo(40, 300)
			);

			BackSide.IsVisible = false;
			FrontSide.IsVisible = true;
			FrontSide.RotationY = -90;

			await Task.WhenAll(
				FrontSide.RotateYTo(0, 500)
			);
		}

		private async void FrontSide_OnTapped(object sender, EventArgs e)
		{
			await Task.WhenAll(
				FrontSide.RotateYTo(90, 500)
			);

			FrontSide.IsVisible = false;
			BackSide.IsVisible = true;
			BackSide.RotationY = -40;

			await Task.WhenAll(
				BackSide.RotateYTo(0, 300)
			);
		}
	}
}
