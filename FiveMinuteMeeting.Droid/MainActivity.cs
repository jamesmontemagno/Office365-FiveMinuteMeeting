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
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.Support.V7.App;
using com.refractored;
using Android.Support.V4.View;
using FiveMinuteMeeting.Droid.Fragments;
using Android.Support.V4.App;

namespace FiveMinuteMeeting.Droid
{
  [Activity(Label = "5 Minute Meeting")]
  public class MainActivity : BaseActivity
  {

    protected override int LayoutResource
    {
      get { return Resource.Layout.main; }
    }

    private TabAdapter adapter;
    private ViewPager pager;
    private PagerSlidingTabStrip tabs;

    protected async override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      AuthenticationHelper.PlatformParameters = new PlatformParameters(this);
     

      adapter = new TabAdapter(this, SupportFragmentManager);
      pager = FindViewById<ViewPager>(Resource.Id.pager);
      tabs = FindViewById<PagerSlidingTabStrip>(Resource.Id.tabs);
      pager.Adapter = adapter;
      tabs.SetViewPager(pager);
      pager.OffscreenPageLimit = 2;


      SupportActionBar.SetDisplayHomeAsUpEnabled(false);
      SupportActionBar.SetHomeButtonEnabled(false);


    }


    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
      base.OnActivityResult(requestCode, resultCode, data);
      AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
    }
  }


  public class TabAdapter : FragmentStatePagerAdapter
  {
    private string[] Titles = new[] { "My Team", "My Events" };
    public TabAdapter(Context context, Android.Support.V4.App.FragmentManager fm)
      : base(fm)
    {

    }

    public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
    {
      return new Java.Lang.String(Titles[position]);
    }
    #region implemented abstract members of PagerAdapter
    public override int Count
    {
      get
      {
        return Titles.Length;
      }
    }
    #endregion
    #region implemented abstract members of FragmentPagerAdapter
    public override Android.Support.V4.App.Fragment GetItem(int position)
    {
      switch (position)
      {
        case 0:
          return ContactsFragment.NewInstance();
        case 1:
          return EventsFragment.NewInstance();
      }
      return null;
    }
    #endregion

    public override int GetItemPosition(Java.Lang.Object frag)
    {
      return PositionNone;
    }
  }
}

