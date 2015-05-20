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
	[Register ("ContactDetailViewController")]
	partial class ContactDetailViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem ButtonAddEvent { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem ButtonCall { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem ButtonEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView ImagePhoto { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextFirst { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextLast { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextPhone { get; set; }

		[Action ("OnEmailClicked:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnEmailClicked (UIBarButtonItem sender);

		void ReleaseDesignerOutlets ()
		{
			if (ButtonAddEvent != null) {
				ButtonAddEvent.Dispose ();
				ButtonAddEvent = null;
			}
			if (ButtonCall != null) {
				ButtonCall.Dispose ();
				ButtonCall = null;
			}
			if (ButtonEmail != null) {
				ButtonEmail.Dispose ();
				ButtonEmail = null;
			}
			if (ImagePhoto != null) {
				ImagePhoto.Dispose ();
				ImagePhoto = null;
			}
			if (TextEmail != null) {
				TextEmail.Dispose ();
				TextEmail = null;
			}
			if (TextFirst != null) {
				TextFirst.Dispose ();
				TextFirst = null;
			}
			if (TextLast != null) {
				TextLast.Dispose ();
				TextLast = null;
			}
			if (TextPhone != null) {
				TextPhone.Dispose ();
				TextPhone = null;
			}
		}
	}
}
