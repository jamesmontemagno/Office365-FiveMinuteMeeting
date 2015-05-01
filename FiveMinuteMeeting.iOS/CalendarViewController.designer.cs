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
	[Register ("CalendarViewController")]
	partial class CalendarViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem ButtonNewEvent { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonNewEvent != null) {
				ButtonNewEvent.Dispose ();
				ButtonNewEvent = null;
			}
		}
	}
}
