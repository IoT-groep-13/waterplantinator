using System;
using System.Collections.Generic;
using System.Text;

namespace Waterplantinator_App
{
	public class SensorData
	{
		public int Humidity { get; set; }
		public int Light { get; set; }
		public int Temperature { get; set; }
		public int Watertank { get; set; }
		public int Soilmoist { get; set; }
		public DateTime Time { get; set; }

		public SensorData(int humidity, int light, int temperature, int watertank, int soilmoist, DateTime time)
		{
			this.Humidity = humidity;
			this.Light = light;
			this.Temperature = temperature;
			this.Watertank = watertank;
			this.Soilmoist = soilmoist;
			this.Time = time;
		}

	}
}
