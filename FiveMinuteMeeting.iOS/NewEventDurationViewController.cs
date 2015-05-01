using FiveMinuteMeeting.Shared.ViewModels;
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FiveMinuteMeeting.iOS
{
	partial class NewEventDurationViewController : UIViewController
	{
    private NewEventViewModel viewModel;
    public NewEventViewModel ViewModel
    {
      get { return viewModel ?? (viewModel = new NewEventViewModel()); }
    }

		public NewEventDurationViewController (IntPtr handle) : base (handle)
		{
		}

    public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
    {
      var vc = segue.DestinationViewController as NewEventDateViewController;
      vc.ViewModel = ViewModel;

      switch(segue.Identifier)
      {
        case "five":
          vc.ViewModel.DurationMinutes = 5;
          break;
        case "ten":
          vc.ViewModel.DurationMinutes = 10;
          break;
        case "fifteen":
          vc.ViewModel.DurationMinutes = 15;
          break;
        case "thirty":
          vc.ViewModel.DurationMinutes = 30;
          break;
        case "sixty":
          vc.ViewModel.DurationMinutes = 60;
          break;
      }

    }
	}
}
