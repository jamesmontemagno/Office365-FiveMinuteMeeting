using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using FiveMinuteMeeting.Shared.Helpers;
using TwilioClient.Android;

namespace FiveMinuteMeeting.Droid
{
	public abstract class BaseActivity : AppCompatActivity, Twilio.IInitListener
  {
		public virtual bool LoadTwilio
		{
			get { return true; }
		}
    public static Activity CurrentActivity { get; private set; }
    public Toolbar Toolbar
    {
      get;
      set;
    }
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      SetContentView(LayoutResource);
      Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
      if (Toolbar != null)
      {
        SetSupportActionBar(Toolbar);
        SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        SupportActionBar.SetHomeButtonEnabled(true);

      }

			if (TwilioHelper.Device == null && LoadTwilio)
				Twilio.Initialize(this.ApplicationContext, this);
    }

    protected abstract int LayoutResource
    {
      get;
    }

    protected int ActionBarIcon
    {
      set { Toolbar.SetNavigationIcon(value); }
    }


		public void OnError(Java.Lang.Exception p0)
		{
		}

		public async void OnInitialized()
		{
			await TwilioHelper.Initialize();
			var intent = new Intent(ApplicationContext, typeof(CallActivity));
			var pendingIntent = PendingIntent.GetActivity(ApplicationContext, 0, intent, PendingIntentFlags.UpdateCurrent);

			TwilioHelper.Device.SetIncomingIntent(pendingIntent);
		}

		protected override void OnNewIntent(Intent intent)
		{
			base.OnNewIntent(intent);
			this.Intent = intent;
		}


		protected override void OnResume()
		{
			base.OnResume();

			CurrentActivity = this;

			var intent = this.Intent;
			var device = intent.GetParcelableExtra(Device.ExtraDevice) as Device;
			var connection = intent.GetParcelableExtra(Device.ExtraConnection).JavaCast<IConnection>();

			if (device != null && connection != null)
			{
				intent.RemoveExtra(Device.ExtraDevice);
				intent.RemoveExtra(Device.ExtraConnection);
				TwilioHelper.HandleIncomingConnection(device, connection);
			}
		}
  }
}