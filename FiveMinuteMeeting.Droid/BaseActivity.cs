using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

namespace FiveMinuteMeeting.Droid
{
  public abstract class BaseActivity : ActionBarActivity
  {
    public static Activity CurrentActivity { get; private set; }
    public Toolbar Toolbar
    {
      get;
      set;
    }
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      SetContentView(LayoutResource);
      Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
      if (Toolbar != null)
      {
        SetSupportActionBar(Toolbar);
        SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        SupportActionBar.SetHomeButtonEnabled(true);

      }
    }

    protected override void OnResume()
    {
      base.OnResume();
      CurrentActivity = this;
    }

    protected abstract int LayoutResource
    {
      get;
    }

    protected int ActionBarIcon
    {
      set { Toolbar.SetNavigationIcon(value); }
    }
  }
}