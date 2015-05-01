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

namespace FiveMinuteMeeting.Droid
{
  [Activity(Label = "Send Email")]
  public class SendEmailActivity : BaseActivity
  {
    protected override int LayoutResource
    {
      get { return Resource.Layout.new_email; }
    }
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      var sendButton = FindViewById<Button>(Resource.Id.send);
      var subject = FindViewById<TextView>(Resource.Id.subject);
      var body = FindViewById<TextView>(Resource.Id.body);

      sendButton.Click += async (sender, args) =>
      {
        App.SendEmailViewModel.Subject = subject.Text;
        App.SendEmailViewModel.Body = body.Text;
        
        AndroidHUD.AndHUD.Shared.Show(this, "Sending Email...");
        await App.SendEmailViewModel.SendEmail();
        AndroidHUD.AndHUD.Shared.Dismiss(this);
        Finish();
      };

    }

    public override bool OnOptionsItemSelected(IMenuItem item)
    {
      if (item.ItemId == Android.Resource.Id.Home)
        Finish();

      return base.OnOptionsItemSelected(item);
    }

  }
}