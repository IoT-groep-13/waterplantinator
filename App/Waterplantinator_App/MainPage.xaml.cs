﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
			getSavedTheme();

			BindingContext = this;
		}

		/// <summary>
		/// Gets saved theme and sets it
		/// </summary>
		private void getSavedTheme()
		{
			string currentTheme = Application.Current.RequestedTheme == OSAppTheme.Light ? "Light" : "Dark";
			Application.Current.UserAppTheme = Preferences.Get("theme", currentTheme) == "Light" ? OSAppTheme.Light : OSAppTheme.Dark;
		}

		/// <summary>
		/// Gets data via connection or last data if no connection can be made
		/// </summary>
		private void GetData()
		{
			Client.OpenConnection();
			if (Client.connected)
			{
				waterBtn.IsEnabled = true;
				SensorData = Client.Receive();
				Client.CloseConnection();
				ConnectionDot.Fill = Brush.LightGreen;
			}
			else
			{
				waterBtn.IsEnabled = false;
				SensorData = Client.GetLastData();
				ConnectionDot.Fill = Brush.Red;
			}
		}

		/// <summary>
		/// Refresh button gets new data from server/Arduino
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RefreshIBtn_OnClicked(object sender, EventArgs e)
		{
			GetData();
		}

		/// <summary>
		/// Pushes new Modal with settings page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SettingsIBtn_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new Settings());
		}

		/// <summary>
		/// Opens connection, gives water command, closes connection
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WaterBtn_OnClicked(object sender, EventArgs e)
		{
			Client.OpenConnection();
			Client.GiveWater();
			Client.CloseConnection();
		}

		/// <summary>
		/// Tap on back of card to turn.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Tap on back of card to turn.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
