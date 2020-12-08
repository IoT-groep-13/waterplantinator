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
		public static bool connection = false;
		private static Socket sender;


		public static void OpenConnection()
		{
			connection = true;
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


		public static SensorData Receive()
		{

			byte[] bytes = new byte[1024];

			Send("<HUMIDITY>");
			int bytesRec = sender.Receive(bytes);
			var humidity = Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1];

			Send("<LIGHT>");
			bytesRec = sender.Receive(bytes);
			var light = Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1];

			Send("<TEMPERATURE>");
			bytesRec = sender.Receive(bytes);
			var temp = Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1];

			Send("<WATERTANK>");
			bytesRec = sender.Receive(bytes);
			var watertank = Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1];

			Send("<SOILMOIST>");
			bytesRec = sender.Receive(bytes);
			var soilmoist = Encoding.ASCII.GetString(bytes, 0, bytesRec).Split('>')[1];

			return new SensorData(Convert.ToInt32(humidity), Convert.ToInt32(light), Convert.ToInt32(temp), Convert.ToInt32(watertank), Convert.ToInt32(soilmoist), DateTime.Now);
		}

		public static void CloseConnection()
		{
			connection = false;

			sender.Shutdown(SocketShutdown.Both);
			sender.Close();
		}
	}
}
