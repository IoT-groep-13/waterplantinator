using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Xamarin.Essentials;

namespace Waterplantinator_App
{
	class Client
	{

		public static string ip = "192.168.0.45";
		public static int port = 11000;
		private static Socket client;

		private static SensorData data = new SensorData(0, 0, 0, 0, 0, "", "");
		public static bool connected;

		/// <summary>
		/// Opens connection to server
		/// </summary>
		public static void OpenConnection()
		{
			connected = false;
			IPAddress ipAddress = IPAddress.Parse(ip);
			IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

			client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			client.SendTimeout = 1500;
			try
			{
				client.Connect(remoteEP);
				connected = true;
				Console.WriteLine("Connected: {0}", client.Connected);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		/// <summary>
		/// Sends message to Arduino
		/// </summary>
		/// <param name="message">Message to send</param>
		private static void Send(string message)
		{
			byte[] msg = Encoding.ASCII.GetBytes(message);
			try
			{
				client.Send(msg);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}


		/// <summary>
		/// Requests all sensor data from Server/Arduino
		/// </summary>
		/// <returns>Filled in SensorData class</returns>
		public static SensorData Receive()
		{
			byte[] bytes = new byte[1024];

			try
			{
				Send("<HUMIDITY>");
				int bytesRec = client.Receive(bytes);
				data.Humidity = Convert.ToInt32(Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1]);

				Send("<LIGHT>");
				bytesRec = client.Receive(bytes);
				data.Light = Convert.ToInt32(Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1]);

				Send("<TEMPERATURE>");
				bytesRec = client.Receive(bytes);
				data.Temperature = Convert.ToInt32(Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1]);

				Send("<WATERTANK>");
				bytesRec = client.Receive(bytes);
				data.Watertank = Convert.ToInt32(Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1]);

				Send("<SOILMOIST>");
				bytesRec = client.Receive(bytes);
				data.Soilmoist = Convert.ToInt32(Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1]);
				data.Time = DateTime.Now.ToLongTimeString();
				data.Date = DateTime.Now.ToShortDateString();
				SaveLastData();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			return data;
		}

		/// <summary>
		/// Saves last data in preferences to get it if connection is lost
		/// </summary>
		private static void SaveLastData()
		{
			Preferences.Set("humidity", data.Humidity);
			Preferences.Set("light", data.Light);
			Preferences.Set("temperature", data.Temperature);
			Preferences.Set("watertank", data.Watertank);
			Preferences.Set("soilmoist", data.Soilmoist);
			Preferences.Set("time", data.Time);
			Preferences.Set("date", data.Date);
		}

		/// <summary>
		/// Gets last data in preferences if there is no connection
		/// </summary>
		/// <returns>SensorData from saved preferences</returns>
		public static SensorData GetLastData()
		{
			data.Humidity = Preferences.Get("humidity", 0);
			data.Light = Preferences.Get("light", 0);
			data.Temperature = Preferences.Get("temperature", 0);
			data.Watertank = Preferences.Get("watertank", 0);
			data.Soilmoist = Preferences.Get("soilmoist", 0);
			data.Time = Preferences.Get("time", "");
			data.Date = Preferences.Get("date", "");

			return data;
		}

		/// <summary>
		/// Requests Arduino to activate the pump
		/// </summary>
		/// <returns>Returned string from Aruduino</returns>
		public static string GiveWater()
		{
			byte[] bytes = new byte[1024];
			Send("<WATER>");
			int bytesRec = client.Receive(bytes);
			return Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1];
		}

		/// <summary>
		/// Closes connection
		/// </summary>
		public static void CloseConnection()
		{
			if (client.Connected)
			{
				client.Shutdown(SocketShutdown.Both);
				client.Close();
			}
		}
	}
}
