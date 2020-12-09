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
		//public static string data2;

		public ObservableCollection<SensorData> sensorData { get; private set; }

		public MainPage()
		{
			InitializeComponent();
			BindingContext = this;
			sensorData = new ObservableCollection<SensorData>();
			SensorData data;
			try
			{
				Client.OpenConnection();
				data = Client.Receive();
				Client.CloseConnection();
			}
			catch
			{
				data = new SensorData(60, 70, 23, 70, 20, DateTime.Now);
			}

			sensorData.Add(data);
		}

		private void RefreshIBtn_OnClicked(object sender, EventArgs e)
		{
			Client.OpenConnection();
			SensorData data = Client.Receive();
			Client.CloseConnection();

			sensorData.Add(data);
		}

		private void SettingsIBtn_OnClicked(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void WaterBtn_OnClicked(object sender, EventArgs e)
		{
			Client.OpenConnection();
			Client.GiveWater();
			Client.CloseConnection();
		}
	}
}
