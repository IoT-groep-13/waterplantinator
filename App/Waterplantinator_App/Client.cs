using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Waterplantinator_App
{
	class Client
	{

		public static string ip = "192.168.0.45";
		public static int port = 11000;
		private static Socket sender;

		private static SensorData data = new SensorData(0, 0, 0, 0, 0, "", "");

		/// <summary>
		/// Opens connection to server
		/// </summary>
		public static void OpenConnection()
		{
			IPAddress ipAddress = IPAddress.Parse(ip);
			IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

			sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			sender.Connect(remoteEP);
		}

		private static void Send(string message)
		{
			byte[] msg = Encoding.ASCII.GetBytes(message);
			sender.Send(msg);
		}


		/// <summary>
		/// Requests all sensor data from Server/Arduino
		/// </summary>
		/// <returns>Filled in SensorData class</returns>
		public static SensorData Receive()
		{
			byte[] bytes = new byte[1024];

			//TODO: change names to Arduino requests
			Send("<HUMIDITY>");
			int bytesRec = sender.Receive(bytes);
			data.Humidity = Convert.ToInt32(Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1]);

			Send("<LIGHT>");
			bytesRec = sender.Receive(bytes);
			data.Light = Convert.ToInt32(Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1]);

			Send("<TEMPERATURE>");
			bytesRec = sender.Receive(bytes);
			data.Temperature = Convert.ToInt32(Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1]);

			Send("<WATERTANK>");
			bytesRec = sender.Receive(bytes);
			data.Watertank = Convert.ToInt32(Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1]);

			Send("<SOILMOIST>");
			bytesRec = sender.Receive(bytes);
			data.Soilmoist = Convert.ToInt32(Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1]);
			data.Time = DateTime.Now.ToLongTimeString();
			data.Date = DateTime.Now.ToShortDateString();

			return data;
		}

		/// <summary>
		/// Requests Arduino to activate the pump
		/// </summary>
		/// <returns>Returned string from Aruduino</returns>
		public static string GiveWater()
		{
			byte[] bytes = new byte[1024];
			//TODO: change names to Arduino requests
			Send("<WATER>");
			int bytesRec = sender.Receive(bytes);
			return Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1];
		}

		/// <summary>
		/// Closes connection
		/// </summary>
		public static void CloseConnection()
		{
			sender.Shutdown(SocketShutdown.Both);
			sender.Close();
		}
	}
}
