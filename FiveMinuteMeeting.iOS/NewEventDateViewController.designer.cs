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
	[Register ("NewEventDateViewController")]
	partial class NewEventDateViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem ButtonSave { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIDatePicker DatePickerDate { get; set; }

		[Action ("ButtonSave_Activated:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void ButtonSave_Activated (UIBarButtonItem sender);

		void ReleaseDesignerOutlets ()
		{
			if (ButtonSave != null) {
				ButtonSave.Dispose ();
				ButtonSave = null;
			}
			if (DatePickerDate != null) {
				DatePickerDate.Dispose ();
				DatePickerDate = null;
			}
		}
	}
}
