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
using FiveMinuteMeeting.Shared;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using FiveMinuteMeeting.Shared.Helpers;

namespace FiveMinuteMeeting.Droid
{
  [Activity(Label = "5 Minute Meeting", MainLauncher = true, Icon = "@drawable/ic_launcher")]
  public class LoginActivity : BaseActivity
  {
    protected override int LayoutResource
    {
	    get { return Resource.Layout.login; }
    }

		public override bool LoadTwilio
		{
			get
			{
				return false;
			}
		}

    ProgressBar progressBar;
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      var login = FindViewById<Button>(Resource.Id.login);
      AuthenticationHelper.PlatformParameters = new PlatformParameters(this);
      progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
      progressBar.Indeterminate = true;
      progressBar.Visibility = ViewStates.Invisible;

      login.Click += async (sender, args) =>
        {
          login.Enabled = false;
          progressBar.Visibility = ViewStates.Visible;

          var client = await AuthenticationHelper.GetCalendarClient();

          
          progressBar.Visibility = ViewStates.Invisible;
          login.Enabled = true;
          if (client == null)
          {
            Toast.MakeText(this, "Login failed", ToastLength.Long).Show();
            return;
          }

          StartActivity(typeof(MainActivity));
          Finish();
        };

      if(Settings.TenantId != "common")
      {
        StartActivity(typeof(MainActivity));
        Finish();
      }
    }


    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
      base.OnActivityResult(requestCode, resultCode, data);
      AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
    }
  }
}