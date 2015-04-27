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
using Android.Content.PM;
using FiveMinuteMeeting.Shared.ViewModels;
using FiveMinuteMeeting.Shared;
using Android.Support.V7.App;


namespace FiveMinuteMeeting.Droid
{
  [Activity(Label = "New Contact", Icon="@drawable/ic_launcher", UiOptions = UiOptions.SplitActionBarWhenNarrow)]
  public class ContactDetailsActivity : BaseActivity
  {
    public static DetailsViewModel ViewModel { get; set; }
    ImageView photo;
    EditText email, phone, firstName, lastName;

    protected override int LayoutResource
    {
      get { return Resource.Layout.contact_details; }
    }
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      email = FindViewById<EditText>(Resource.Id.email);
      phone = FindViewById<EditText>(Resource.Id.phone);
      firstName = FindViewById<EditText>(Resource.Id.first_name);
      lastName = FindViewById<EditText>(Resource.Id.last_name);
      photo = FindViewById<ImageView>(Resource.Id.photo2);

      email.TextChanged += (sender, args) =>
        {
          Koush.UrlImageViewHelper.SetUrlDrawable(photo, Gravatar.GetURL(email.Text, 88), Resource.Drawable.missing);
        };

      if (ViewModel == null)
      {
        ViewModel = new DetailsViewModel();
      }
      else
      {
        SupportActionBar.Title = ViewModel.FirstName + " " + ViewModel.LastName;
        email.Text = ViewModel.Email;
        firstName.Text = ViewModel.FirstName;
        lastName.Text = ViewModel.LastName;
        phone.Text = ViewModel.Phone;
       
      }

      var bottomToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_bottom);

      bottomToolbar.InflateMenu(Resource.Menu.detail_bottom);
      bottomToolbar.MenuItemClick += (sender, args) =>
        {

          switch (args.Item.ItemId)
          {
            case Resource.Id.calendar:
              App.NewEventViewModel.Email = ViewModel.Email;
              App.NewEventViewModel.FirstName = ViewModel.FirstName;
              App.NewEventViewModel.LastName = ViewModel.LastName;
              StartActivity(typeof(NewEventActivity));
              //ViewModel.AddEvent(new[] { ViewModel.Email }, new[] { ViewModel.FirstName });
              break;
            case Resource.Id.phone:
              var uri = Android.Net.Uri.Parse("tel:" + ViewModel.Phone);
              var intent = new Intent(Intent.ActionView, uri);
              StartActivity(intent);
              break;
            case Resource.Id.email:
              App.SendEmailViewModel.Email = ViewModel.Email;
              App.SendEmailViewModel.FirstName = ViewModel.FirstName;
              App.SendEmailViewModel.LastName = ViewModel.LastName;

              StartActivity(typeof(SendEmailActivity));
              break;
          }
        };
      
    }

    protected override void OnResume()
    {
      base.OnResume();
      if(!string.IsNullOrWhiteSpace(ViewModel.Email))
        Koush.UrlImageViewHelper.SetUrlDrawable(photo, Gravatar.GetURL(ViewModel.Email, 88), Resource.Drawable.missing);

    }

    public override bool OnCreateOptionsMenu(IMenu menu)
    {
      if(ViewModel.Contact == null)
        MenuInflater.Inflate(Resource.Menu.detail, menu);
      return base.OnCreateOptionsMenu(menu);
    }


    public override bool OnOptionsItemSelected(IMenuItem item)
    {
	    switch (item.ItemId) {
        case Resource.Id.save:
          ViewModel.FirstName = firstName.Text.Trim();
          ViewModel.LastName = lastName.Text.Trim();
          ViewModel.Email = email.Text.Trim();
          ViewModel.Phone = phone.Text.Trim();

          AndroidHUD.AndHUD.Shared.Show(this, "Saving contact...", maskType: AndroidHUD.MaskType.Clear);
          ViewModel.SaveContact().ContinueWith((result) =>
          {
            AndroidHUD.AndHUD.Shared.Dismiss();
            if (result.Exception == null)
              Finish();
          });
          break;
        case Android.Resource.Id.Home:
        Finish();
        break;
	    }
	    return base.OnOptionsItemSelected(item);
    }


  }
}