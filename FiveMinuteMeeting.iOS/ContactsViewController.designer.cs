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
	[Register ("ContactsViewController")]
	partial class ContactsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem ButtonNewContact { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonNewContact != null) {
				ButtonNewContact.Dispose ();
				ButtonNewContact = null;
			}
		}
	}
}
