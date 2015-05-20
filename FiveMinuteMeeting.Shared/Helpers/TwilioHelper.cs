#if !NETFX_CORE
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;

#if __ANDROID__
using TwilioClient.Android;
using FiveMinuteMeeting.Droid;
using Android.Content;
#elif __IOS__
using TwilioClient.iOS;
using Device = TwilioClient.iOS.TCDevice;
using IConnection = TwilioClient.iOS.TCConnection;
using Foundation;
#endif

namespace FiveMinuteMeeting.Shared.Helpers
{
    public static class TwilioHelper
    {
			public static Device Device { get; set; }
			public static IConnection Connection { get; set; }
			private const string TAG = "FiveMinuteMeeting";

			private const string BaseUrl = "http://your-website-here.azurewebsites.net/Client/";

			public static async Task ScheduleMeeting(string to, DateTime time, int length)
			{
				var information = "ScheduleMeeting?source={0}&target={1}&time={2}&length={3}";
				information = string.Format(information, Settings.TwilioNumber, to, time.Ticks, length);

				var client = new HttpClient();
				client.PostAsync(BaseUrl + information, new StringContent("Schedule Meeting"));
		

			}
			public static async Task Initialize()
			{
				try
				{
					var token =  await GetTokenAsync();

#if __ANDROID__
					Device = Twilio.CreateDevice(token, null);
#elif __IOS__
					Device = new Device(token, null);
#endif

				}
				catch (Exception ex)
				{
					Debug.WriteLine(TAG +" Error: " + ex.Message);
				}
			}

			public static async Task<string> GetTokenAsync()
			{
				var client = new HttpClient();

				var name = "motzandroid";
#if __IOS__
				name = "motzios";
#endif
				return await client.GetStringAsync(BaseUrl + "Token?clientName=" + name);
			}


			public static void HandleIncomingConnection(Device device, IConnection connection)
			{
				if (Connection != null)
					Connection.Disconnect();


				Connection = connection;
				Connection.Accept();
			}

			public static void PlaceCall(string number)
			{

#if __ANDROID__
				var parameters = new Dictionary<string, string>() {
						{ "Target", number },
						{ "Source", Settings.TwilioNumber }          
				};
#elif __IOS__
				var parameters = NSDictionary.FromObjectsAndKeys(
					 new object[] { Settings.TwilioNumber, number },
					 new object[] { "Source", "Target" }
			 );
#endif

				Connection = Device.Connect(parameters, null);
			}

			public static void Disconnect()
			{
				if (Connection == null)
					return;

				Connection.Disconnect();
			}

#if __IOS__

			private static void SetupDeviceEvents()
			{
				if (Device != null)
				{
					// When a new connection comes in, store it and use it to accept the incoming call.
					Device.ReceivedIncomingConnection += (sender, e) =>
					{
						Connection = e.Connection;
						Connection.Accept();
					};
				}
			}
#endif
    }
}

#endif
