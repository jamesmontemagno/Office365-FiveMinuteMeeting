// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FiveMinuteMeeting.iOS
{
	[Register ("CallViewController")]
	partial class CallViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonCall { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonHangup { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextFieldPhoneNumber { get; set; }

		[Action ("ButtonCall_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void ButtonCall_TouchUpInside (UIButton sender);

		[Action ("ButtonHangup_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void ButtonHangup_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (ButtonCall != null) {
				ButtonCall.Dispose ();
				ButtonCall = null;
			}
			if (ButtonHangup != null) {
				ButtonHangup.Dispose ();
				ButtonHangup = null;
			}
			if (TextFieldPhoneNumber != null) {
				TextFieldPhoneNumber.Dispose ();
				TextFieldPhoneNumber = null;
			}
		}
	}
}
