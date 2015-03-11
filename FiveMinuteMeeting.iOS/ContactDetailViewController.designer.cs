// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace FiveMinuteMeeting.iOS
{
	[Foundation.Register("ContactDetailViewController")]
	partial class ContactDetailViewController
	{
		[Foundation.Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem ButtonAddEvent { get; set; }

		[Foundation.Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem ButtonCall { get; set; }

		[Foundation.Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem ButtonEmail { get; set; }

		[Foundation.Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView ImagePhoto { get; set; }

		[Foundation.Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextEmail { get; set; }

		[Foundation.Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextFirst { get; set; }

		[Foundation.Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextLast { get; set; }

		[Foundation.Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextPhone { get; set; }

		[Foundation.Action("OnEmailClicked:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnEmailClicked (UIBarButtonItem sender);

		[Foundation.Action("OnPhoneClicked:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnPhoneClicked (UIBarButtonItem sender);

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
