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

namespace FiveMinuteMeeting.Droid.Helpers
{
  public static class MessageDialogs
  {

    public static void SendConfirmation(string message, string title, Action<bool> confirmationAction)
    {
      var builder = new AlertDialog.Builder(BaseActivity.CurrentActivity);
      builder
        .SetTitle(title ?? string.Empty)
        .SetMessage(message)
        .SetPositiveButton(Android.Resource.String.Ok, (args, which) =>
          {
            confirmationAction(true);
          })
        .SetNegativeButton(Android.Resource.String.Cancel, (args, which) =>
        {
          confirmationAction(false);
        });


      BaseActivity.CurrentActivity.RunOnUiThread(() =>
      {
        var alert = builder.Create();
        alert.Show();
      });
    }
  }
}