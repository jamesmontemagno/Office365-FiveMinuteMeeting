using FiveMinuteMeeting.Shared.Helpers;
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FiveMinuteMeeting.iOS
{
	partial class CallViewController : UIViewController
	{
		public string Number { get; set; }
		public CallViewController (IntPtr handle) : base (handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TextFieldPhoneNumber.Text = Number;
			TwilioHelper.Initialize();
		}

		partial void ButtonCall_TouchUpInside(UIButton sender)
		{
			TextFieldPhoneNumber.ResignFirstResponder();
			TwilioHelper.PlaceCall(TextFieldPhoneNumber.Text.Trim());
		}

		partial void ButtonHangup_TouchUpInside(UIButton sender)
		{
			TextFieldPhoneNumber.ResignFirstResponder();
			TwilioHelper.Disconnect();
		}
	}
}
