using CoreGraphics;
using FiveMinuteMeeting.Shared.ViewModels;
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FiveMinuteMeeting.iOS
{
	partial class SendEmailViewController : UIViewController
	{
    private SendEmailViewModel viewModel;
    public SendEmailViewModel ViewModel
    {
      get { return viewModel ?? (viewModel = new SendEmailViewModel()); }
    }
		public SendEmailViewController (IntPtr handle) : base (handle)
		{
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();


      TextFieldSubject.ShouldReturn += ShouldReturn;

      NSNotificationCenter.DefaultCenter.AddObserver
       (UIKeyboard.DidShowNotification, KeyBoardUpNotification);

      // Keyboard Down
      NSNotificationCenter.DefaultCenter.AddObserver
      (UIKeyboard.WillHideNotification, KeyBoardDownNotification);
    }

    private bool ShouldReturn(UITextField field)
    {
      field.ResignFirstResponder();
      return true;
    }



    async partial void ButtonSend_TouchUpInside(UIButton sender)
    {
      ViewModel.Subject = TextFieldSubject.Text;
      ViewModel.Body = TextViewBody.Text;
      viewModel.IsPriority = SwitchIsPriority.On;
      BigTed.BTProgressHUD.Show("Sending Email...");
      await ViewModel.SendEmail();
      BigTed.BTProgressHUD.Dismiss();
      
    }


    #region Keyboard

    private UIView activeview;             // Controller that activated the keyboard
    private float scrollamount;    // amount to scroll 
    private float bottom;           // bottom point
    private const float Offset = 68.0f; // extra offset
    private bool moveViewUp;           // which direction are we moving


    private void KeyBoardDownNotification(NSNotification notification)
    {
      if (moveViewUp) { ScrollTheView(false); }
    }

    private void ScrollTheView(bool move)
    {

      // scroll the view up or down
      UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
      UIView.SetAnimationDuration(0.3);

      CGRect frame = (CGRect)View.Frame;

      if (move)
      {
        frame.Y -= scrollamount;
      }
      else
      {
        frame.Y += scrollamount;
        scrollamount = 0;
      }

      View.Frame = frame;
      UIView.CommitAnimations();
    }

    private void KeyBoardUpNotification(NSNotification notification)
    {
      // get the keyboard size
      var r = (CGRect)UIKeyboard.FrameBeginFromNotification((NSNotification)notification);


      // Find what opened the keyboard
      foreach (UIView view in this.View.Subviews)
      {
        if (view.IsFirstResponder)
          activeview = view;
      }

      // Bottom of the controller = initial position + height + offset      
      bottom = ((float)activeview.Frame.Y + (float)activeview.Frame.Height + Offset);

      // Calculate how far we need to scroll
      scrollamount = ((float)r.Height - ((float)View.Frame.Size.Height - bottom));

      // Perform the scrolling
      if (scrollamount > 0)
      {
        moveViewUp = true;
        ScrollTheView(moveViewUp);
      }
      else
      {
        moveViewUp = false;
      }

    }
    #endregion
	}
}
