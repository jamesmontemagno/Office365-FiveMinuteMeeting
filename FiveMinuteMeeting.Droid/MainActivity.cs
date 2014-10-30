using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FiveMinuteMeeting.Shared.ViewModels;
using FiveMinuteMeeting.Shared;
using Android.Support.V4.Widget;

namespace FiveMinuteMeeting.Droid
{
  [Activity(Label = "Five Minute Meeting", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : ListActivity
  {

    private ContactsViewModel viewModel = App.ContactsViewModel;
    private SwipeRefreshLayout refresher;

    protected async override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
      refresher.SetColorScheme(Resource.Color.blue,
                                Resource.Color.white,
                                Resource.Color.blue,
                                Resource.Color.white);
      refresher.Refresh += async delegate
      {
        if(viewModel.IsBusy)
          return;

        await viewModel.GetContactsAsync();
        RunOnUiThread(() => { ((BaseAdapter)ListAdapter).NotifyDataSetChanged(); });
      };

      viewModel.PropertyChanged += PropertyChanged;
      await Client.EnsureClientCreated(this);
      await viewModel.GetContactsAsync();
    }

    void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      RunOnUiThread(() =>
      {
        switch (e.PropertyName)
        {
          case "IsBusy":
            {
              refresher.Refreshing = viewModel.IsBusy;
            }
            break;
        }
      });
    }
  }
}

