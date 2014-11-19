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
using com.refractored.monodroidtoolkit.imageloader;
using FiveMinuteMeeting.Shared;

namespace FiveMinuteMeeting.Droid
{
  [Activity(Label = "New Contact", Icon="@drawable/ic_launcher", UiOptions = UiOptions.SplitActionBarWhenNarrow)]
  public class DetailActivity : Activity
  {
    public static DetailsViewModel ViewModel { get; set; }
    private ImageLoader loader;
    ImageView photo;
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      SetContentView(Resource.Layout.Detail);


      ActionBar.SetDisplayHomeAsUpEnabled(true);
      ActionBar.SetDisplayShowHomeEnabled(true);
      loader = new ImageLoader(this);

      var email = FindViewById<EditText>(Resource.Id.email);
      var phone = FindViewById<EditText>(Resource.Id.phone);
      var firstName = FindViewById<EditText>(Resource.Id.first_name);
      var lastName = FindViewById<EditText>(Resource.Id.last_name);
      photo = FindViewById<ImageView>(Resource.Id.photo2);

      if (ViewModel == null)
      {
        ViewModel = new DetailsViewModel();
      }
      else
      {
        ActionBar.Title = ViewModel.FirstName + " " + ViewModel.LastName;
        email.Text = "134" + ViewModel.Email;
        firstName.Text = ViewModel.FirstName;
        lastName.Text = ViewModel.LastName;
        phone.Text = ViewModel.Phone;
       
        //loader.DisplayImage(Gravatar.GetURL(ViewModel.Email, 88), photo, Resource.Drawable.missing);
      }
      
    }

    protected override void OnResume()
    {
      base.OnResume();
      if(!string.IsNullOrWhiteSpace(ViewModel.Email))
        Koush.UrlImageViewHelper.SetUrlDrawable(photo, Gravatar.GetURL(ViewModel.Email, 88), Resource.Drawable.missing);

    }


    public override bool OnCreateOptionsMenu(IMenu menu)
    {
      MenuInflater.Inflate(Resource.Menu.detail, menu);
      return base.OnCreateOptionsMenu(menu);
    }


    public override bool OnOptionsItemSelected(IMenuItem item)
    {
	    switch (item.ItemId) {
	    case Resource.Id.calendar:
          ViewModel.AddEvent(ViewModel.Email, ViewModel.FirstName);
		    break;
	    case Resource.Id.save:
        ViewModel.SaveContact();
		    break;
      case Resource.Id.phone:
          var uri = Android.Net.Uri.Parse ("tel:" + ViewModel.Phone);
          var intent = new Intent (Intent.ActionView, uri); 
          StartActivity (intent);  
        break;
      case Resource.Id.email:
        var email = new Intent(Android.Content.Intent.ActionSend);
          email.PutExtra (Android.Content.Intent.ExtraEmail, 
          new string[]{ViewModel.Email} );

          email.PutExtra (Android.Content.Intent.ExtraSubject,
            "5 Minute Meeting");

          email.PutExtra (Android.Content.Intent.ExtraText, 
          "We are having a 5 minute stand up tomorrow at this time! Check your calendar.");
          email.SetType("message/rfc822");
          StartActivity(email);
        break;
        case Android.Resource.Id.Home:
        Finish();
        break;
	    }
	    return base.OnOptionsItemSelected(item);
    }


  }
}