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
	[Register ("SendEmailViewController")]
	partial class SendEmailViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonSend { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch SwitchIsPriority { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextFieldSubject { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView TextViewBody { get; set; }

		[Action ("ButtonSend_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void ButtonSend_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (ButtonSend != null) {
				ButtonSend.Dispose ();
				ButtonSend = null;
			}
			if (SwitchIsPriority != null) {
				SwitchIsPriority.Dispose ();
				SwitchIsPriority = null;
			}
			if (TextFieldSubject != null) {
				TextFieldSubject.Dispose ();
				TextFieldSubject = null;
			}
			if (TextViewBody != null) {
				TextViewBody.Dispose ();
				TextViewBody = null;
			}
		}
	}
}
