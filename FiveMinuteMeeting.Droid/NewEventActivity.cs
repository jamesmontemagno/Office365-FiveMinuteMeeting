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
  [Activity(Label = "New Event")]
  public class NewEventActivity : BaseActivity
  {
    protected override int LayoutResource
    {
      get { return Resource.Layout.new_event; }
    }
    Spinner durationSpinner;
    DatePicker datePicker;
    TimePicker timePicker;
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      durationSpinner = FindViewById<Spinner>(Resource.Id.duration_spinner);

      var adapter = ArrayAdapter.CreateFromResource(
              this, Resource.Array.durations, Android.Resource.Layout.SimpleSpinnerItem);

      adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
      durationSpinner.Adapter = adapter;

      datePicker = FindViewById<DatePicker>(Resource.Id.date_picker);
      timePicker = FindViewById<TimePicker>(Resource.Id.time_picker);
      var saveEvent = FindViewById<Button>(Resource.Id.create_event);
      saveEvent.Click += async (sender, args) =>
        {
          App.NewEventViewModel.Date = new DateTime(datePicker.DateTime.Year, datePicker.DateTime.Month, datePicker.DateTime.Day,
            timePicker.CurrentHour.IntValue(), timePicker.CurrentMinute.IntValue(), 0);

          switch(durationSpinner.SelectedItemPosition)
          {
            case 0:
              App.NewEventViewModel.DurationMinutes = 5;
              break;
            case 1:
              App.NewEventViewModel.DurationMinutes = 10;
              break;
            case 2:
              App.NewEventViewModel.DurationMinutes = 15;
              break;
            case 3:
              App.NewEventViewModel.DurationMinutes = 30;
              break;
            case 4:
              App.NewEventViewModel.DurationMinutes = 45;
              break;
            case 5:
              App.NewEventViewModel.DurationMinutes = 60;
              break;
            default:
              App.NewEventViewModel.DurationMinutes = 5;
              break;
          }

          AndroidHUD.AndHUD.Shared.Show(this, "Adding Event...");
          await App.NewEventViewModel.AddEvent();
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