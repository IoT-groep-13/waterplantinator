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

		public string Time { get; set; }
		public string Date { get; set; }
		public SensorData sensorData { get; set; }

		public MainPage()
		{
			InitializeComponent();
			BindingContext = this;

			Client.OpenConnection();
			sensorData = Client.Receive();
			Time = sensorData.Time;
			Date = sensorData.Date;
			Client.CloseConnection();
			FrontSide.IsVisible = true;
			BackSide.IsVisible = false;
		}

		private void RefreshIBtn_OnClicked(object sender, EventArgs e)
		{
			Client.OpenConnection();
			sensorData = Client.Receive();
			Time = sensorData.Time;
			Date = sensorData.Date;
			Client.CloseConnection();
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

		private void BackSide_OnTapped(object sender, EventArgs e)
		{
			FrontSide.IsVisible = true;
			BackSide.IsVisible = false;
		}

		private void FrontSide_OnTapped(object sender, EventArgs e)
		{
			FrontSide.IsVisible = false;
			BackSide.IsVisible = true;
		}
	}
}
