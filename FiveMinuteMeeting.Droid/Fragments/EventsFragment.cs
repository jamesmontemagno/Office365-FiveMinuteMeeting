
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using com.refractored.fab;
using FiveMinuteMeeting.Droid.Adapters;
using FiveMinuteMeeting.Shared;
using FiveMinuteMeeting.Shared.ViewModels;

namespace FiveMinuteMeeting.Droid.Fragments
{
  public class EventsFragment : Fragment
  {
    private CalendarViewModel viewModel = App.CalendarViewModel;
    private SwipeRefreshLayout refresher;
    private ListView listView;
    public static EventsFragment NewInstance()
    {
      var f = new EventsFragment()
      {
      };
      var b = new Bundle();

      f.Arguments = b;
      return f;
    }
    public override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      RetainInstance = true;
      // Create your fragment here
    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var root = inflater.Inflate(Resource.Layout.fragment_events, container, false);

      refresher = root.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
      refresher.SetColorScheme(Resource.Color.blue);

      refresher.Refresh += async delegate
      {
        if (viewModel.IsBusy)
          return;


        await viewModel.GetEventsAsync();
        Activity.RunOnUiThread(() => { ((BaseAdapter)listView.Adapter).NotifyDataSetChanged(); });

      };

      viewModel.PropertyChanged += PropertyChanged;

      listView = root.FindViewById<ListView>(Resource.Id.list);

      listView.Adapter = new EventAdapter(Activity, viewModel);

      listView.ItemLongClick += ListViewItemLongClick;


      var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
      fab.AttachToListView(listView);
      fab.Click += (sender, args) =>
        {
          Activity.StartActivity(typeof(NewEventActivity));
        };

      return root;
    }

    async void ListViewItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
    {
      await viewModel.DeleteEvent(viewModel.Events[e.Position]);
      Activity.RunOnUiThread(() => { ((BaseAdapter)listView.Adapter).NotifyDataSetChanged(); });
    }



    public async override void OnResume()
    {
      base.OnResume();

     if (viewModel.IsBusy)
        return;

      if (viewModel.Events.Count == 0)
        viewModel.GetEventsAsync();
      else
        Activity.RunOnUiThread(() => { ((BaseAdapter)listView.Adapter).NotifyDataSetChanged(); });
    }

    void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      Activity.RunOnUiThread(() =>
      {
        switch (e.PropertyName)
        {
          case "IsBusy":
            {
              if (viewModel.IsBusy)
                AndroidHUD.AndHUD.Shared.Show(Activity, "Loading...");
              else
                AndroidHUD.AndHUD.Shared.Dismiss(Activity);

              refresher.Refreshing = viewModel.IsBusy;
              ((BaseAdapter)listView.Adapter).NotifyDataSetChanged();
            }
            break;
        }
      });
    }
  }
}