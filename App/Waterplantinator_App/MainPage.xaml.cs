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
		public ObservableCollection<SensorData> sensorData { get; private set; }

		public MainPage()
		{
			InitializeComponent();
			BindingContext = this;

			sensorData = new ObservableCollection<SensorData>();

			SensorData data = new SensorData(60, 70, 23, 70, 20, DateTime.Now);
			sensorData.Add(data);
		}
	}
}
