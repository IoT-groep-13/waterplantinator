using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Waterplantinator_App.Annotations;

namespace Waterplantinator_App
{

	public class SensorData : INotifyPropertyChanged
	{
		private int _Humidity;
		public int Humidity
		{
			get { return _Humidity; }
			set
			{
				_Humidity = value;
				OnPropertyChanged();
			}
		}
		private int _Light;
		public int Light
		{
			get { return _Light; }
			set
			{
				_Light = value;
				OnPropertyChanged();
			}
		}
		private int _Temperature;
		public int Temperature
		{
			get { return _Temperature; }
			set
			{
				_Temperature = value;
				OnPropertyChanged();
			}
		}
		private int _Watertank;
		public int Watertank
		{
			get { return _Watertank; }
			set
			{
				_Watertank = value;
				OnPropertyChanged();
			}
		}
		private int _Soilmoist;
		public int Soilmoist
		{
			get { return _Soilmoist; }
			set
			{
				_Soilmoist = value;
				OnPropertyChanged();
			}
		}
		private string _Time;
		public string Time
		{
			get { return _Time; }
			set
			{
				_Time = value;
				OnPropertyChanged();
			}
		}
		private string _Date;
		public string Date
		{
			get { return _Date; }
			set
			{
				_Date = value;
				OnPropertyChanged();
			}
		}

		public SensorData(int humidity, int light, int temperature, int watertank, int soilmoist, string date, string time)
		{
			this.Humidity = humidity;
			this.Light = light;
			this.Temperature = temperature;
			this.Watertank = watertank;
			this.Soilmoist = soilmoist;
			this.Date = date;
			this.Time = time;
			OnPropertyChanged();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
