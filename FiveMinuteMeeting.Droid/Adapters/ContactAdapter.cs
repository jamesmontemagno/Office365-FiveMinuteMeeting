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
  public class ContactWrapper : Java.Lang.Object
  {
    public TextView Name { get; set; }
    public TextView Phone { get; set; }
    public ImageView Photo { get; set; }
  }
  public class ContactAdapter : BaseAdapter<IContact>
  {
    private ContactsViewModel viewModel;
    private Activity context;
    public ContactAdapter(Activity context, ContactsViewModel viewModel)
    {
      this.viewModel = viewModel;
      this.context = context;
    }
    public override View GetView(int position, View convertView, ViewGroup parent)
    {
      ContactWrapper wrapper = null;
      var view = convertView;
      if (convertView == null)
      {
        view = context.LayoutInflater.Inflate(Resource.Layout.item_contact, null);
        wrapper = new ContactWrapper();
        wrapper.Name = view.FindViewById<TextView>(Resource.Id.name);
        wrapper.Phone = view.FindViewById<TextView>(Resource.Id.phone);
        wrapper.Photo = view.FindViewById<ImageView>(Resource.Id.photo);
        view.Tag = wrapper;
      }
      else
      {
        wrapper = convertView.Tag as ContactWrapper;
      }

      var contact = viewModel.Contacts[position];
      wrapper.Name.Text = contact.GivenName + " " + contact.Surname;
      wrapper.Phone.Text = contact.BusinessPhones.Count > 0 ? contact.BusinessPhones[0] : contact.MobilePhone1;
      Koush.UrlImageViewHelper.SetUrlDrawable(wrapper.Photo, Gravatar.GetURL(contact.EmailAddresses[0].Address, 88), Resource.Drawable.missing);

      return view;
    }

    public override IContact this[int position]
    {
      get { return viewModel.Contacts[position]; }
    }

    public override int Count
    {
      get { return viewModel.Contacts.Count; }
    }

    public override long GetItemId(int position)
    {
      return position;
    }
  }
}