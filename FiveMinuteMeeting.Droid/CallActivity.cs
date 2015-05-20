using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FiveMinuteMeeting.Shared.Helpers;
using TwilioClient.Android;

namespace FiveMinuteMeeting.Droid
{
	[Activity(Label ="Call via Twilio")]
	public class CallActivity : BaseActivity
	{
		protected override int LayoutResource
		{
			get { return Resource.Layout.call_activity; }
		}
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			var number = Intent.GetStringExtra("phone");
			var call = FindViewById<Button>(Resource.Id.call);
			var phonenumber = FindViewById<EditText>(Resource.Id.phonenumber);
			call.Click += (sender, args) =>
			{
				call.Enabled = false;
				TwilioHelper.PlaceCall(phonenumber.Text.Trim());     
			};

			phonenumber.Text = number;
			var hangup = FindViewById<Button>(Resource.Id.hangup);
			hangup.Click += (sender, args) =>
			{
				TwilioHelper.Disconnect();
			};
		}


	}
}