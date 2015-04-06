using FiveMinuteMeeting.Shared.ViewModels;
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FiveMinuteMeeting.iOS
{
	partial class NewEventDateViewController : UIViewController
	{
    public NewEventViewModel ViewModel
    {
      get;
      set;
    }
		public NewEventDateViewController (IntPtr handle) : base (handle)
		{
		}


    public static DateTime NSDateToDateTime(NSDate date)
    {
      DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
          new DateTime(2001, 1, 1, 0, 0, 0));
      return reference.AddSeconds(date.SecondsSinceReferenceDate);
    }

    async partial void ButtonSchedule_TouchUpInside(UIButton sender)
    {
      ViewModel.Date = NSDateToDateTime(DatePickerDate.Date);
      BigTed.BTProgressHUD.Show("Adding Event...");
      await ViewModel.AddEvent();
      BigTed.BTProgressHUD.Dismiss();
      NavigationController.PopToRootViewController(true);
    }
	}
}
