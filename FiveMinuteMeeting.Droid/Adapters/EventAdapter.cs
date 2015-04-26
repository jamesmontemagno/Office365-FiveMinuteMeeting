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
using Microsoft.Office365.OutlookServices;
using FiveMinuteMeeting.Shared.ViewModels;
using FiveMinuteMeeting.Shared;

namespace FiveMinuteMeeting.Droid.Adapters
{
  public class EventWrapper : Java.Lang.Object
  {
    public TextView Name { get; set; }
    public TextView Date { get; set; }
  }
  public class EventAdapter : BaseAdapter<IEvent>
  {
    private CalendarViewModel viewModel;
    private Activity context;
    public EventAdapter(Activity context, CalendarViewModel viewModel)
    {
      this.viewModel = viewModel;
      this.context = context;
    }
    public override View GetView(int position, View convertView, ViewGroup parent)
    {
      EventWrapper wrapper = null;
      var view = convertView;
      if (convertView == null)
      {
        view = context.LayoutInflater.Inflate(Resource.Layout.item_event, null);
        wrapper = new EventWrapper();
        wrapper.Name = view.FindViewById<TextView>(Resource.Id.name);
        wrapper.Date = view.FindViewById<TextView>(Resource.Id.date);
        view.Tag = wrapper;
      }
      else
      {
        wrapper = convertView.Tag as EventWrapper;
      }

      var theEvent = viewModel.Events[position];
      wrapper.Name.Text = theEvent.Subject;
      wrapper.Date.Text = theEvent.Start.Value.ToLocalTime().ToString("dd/MM/yy");
    
      return view;
    }

    public override IEvent this[int position]
    {
      get { return viewModel.Events[position]; }
    }

    public override int Count
    {
      get { return viewModel.Events.Count; }
    }

    public override long GetItemId(int position)
    {
      return position;
    }
  }
}