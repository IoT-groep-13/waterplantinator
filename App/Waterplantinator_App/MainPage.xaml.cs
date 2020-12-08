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


			Client.OpenConnection();
			SensorData data = Client.Receive();
			Client.CloseConnection();

			//SensorData data = new SensorData(60, 70, 23, 70, 20, DateTime.Now);
			sensorData.Add(data);
		}

		private void Refresh_OnClicked(object sender, EventArgs e)
		{
			Client.OpenConnection();
			SensorData data = Client.Receive();
			Client.CloseConnection();

			sensorData.Add(data);
		}

		private void Settings_OnClicked(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
