
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using FiveMinuteMeeting.Droid.Adapters;
using FiveMinuteMeeting.Shared;
using FiveMinuteMeeting.Shared.ViewModels;

namespace FiveMinuteMeeting.Droid.Fragments
{
  public class ContactsFragment : Fragment
  {
    private ContactsViewModel viewModel = App.ContactsViewModel;
    private SwipeRefreshLayout refresher;
    private ListView listView;



    public static ContactsFragment NewInstance()
    {
      var f = new ContactsFragment()
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
      var root = inflater.Inflate(Resource.Layout.fragment_contacts, container, false);

      refresher = root.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
      refresher.SetColorScheme(Resource.Color.blue);

      refresher.Refresh += async delegate
      {
        if (viewModel.IsBusy)
          return;

        await viewModel.GetContactsAsync();
        Activity.RunOnUiThread(() => { ((BaseAdapter)listView.Adapter).NotifyDataSetChanged(); });

      };

      viewModel.PropertyChanged += PropertyChanged;

      listView = root.FindViewById<ListView>(Resource.Id.list);

      listView.Adapter = new ContactAdapter(Activity, viewModel);

      listView.ItemLongClick += ListViewItemLongClick;
      listView.ItemClick += ListViewItemClick;

      return root;
    }
    void ListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
    {
      var contact = viewModel.Contacts[e.Position];
      var vm = new DetailsViewModel(contact);
      ContactDetailsActivity.ViewModel = vm;
      var intent = new Intent(Activity, typeof(ContactDetailsActivity));
      StartActivity(intent);
    }

    async void ListViewItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
    {
      await viewModel.DeleteContact(viewModel.Contacts[e.Position]);
      Activity.RunOnUiThread(() => { ((BaseAdapter)listView.Adapter).NotifyDataSetChanged(); });
    }

    /*public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
    {
      inflater.Inflate(Resource.Menu.main, menu);
      base.OnCreateOptionsMenu(menu, inflater);
    }

 

    public override bool OnOptionsItemSelected(IMenuItem item)
    {
      switch (item.ItemId)
      {
        case Resource.Id.add:
          ContactDetailsActivity.ViewModel = null;
          var intent = new Intent(Activity, typeof(ContactDetailsActivity));
          StartActivity(intent);
          break;
      }
      return base.OnOptionsItemSelected(item);
    }*/


   
    public async override void OnResume()
    {
      base.OnResume();

      if (viewModel.IsBusy)
        return;

      if (viewModel.Contacts.Count == 0)
        viewModel.GetContactsAsync();
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