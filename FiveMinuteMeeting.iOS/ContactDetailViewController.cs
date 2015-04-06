using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using MessageUI;
using Microsoft.Office365.OutlookServices;
using FiveMinuteMeeting.Shared.ViewModels;
using CoreGraphics;
using CoreGraphics;
using FiveMinuteMeeting.Shared;
using SDWebImage;

namespace FiveMinuteMeeting.iOS
{
  partial class ContactDetailViewController : UIViewController
  {
    public ContactDetailViewController(IntPtr handle)
      : base(handle)
    {

    }

    public DetailsViewModel ViewModel
    {
      get;
      set;
    }

    public override void ViewDidLoad()
    {
       base.ViewDidLoad();

       NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;

       var save = new UIBarButtonItem(
         UIImage.FromBundle("save.png"),
         UIBarButtonItemStyle.Plain,
        async (sender, args) =>
       {
         ViewModel.FirstName = TextFirst.Text.Trim();
         ViewModel.LastName = TextLast.Text.Trim();
         ViewModel.Email = TextEmail.Text.Trim();
         ViewModel.Phone = TextEmail.Text.Trim();
         await ViewModel.SaveContact();
         DismissViewControllerAsync(true);
       });

       NavigationItem.RightBarButtonItem = save;

       TextEmail.ShouldReturn += ShouldReturn;
       TextFirst.ShouldReturn += ShouldReturn;
       TextPhone.ShouldReturn += ShouldReturn;
       TextLast.ShouldReturn += ShouldReturn;


       var color = new CGColor(17.0F / 255.0F, 113.0F / 255.0F, 197.0F / 255F);
       TextEmail.Layer.BorderColor = color;
       TextFirst.Layer.BorderColor = color;
       TextPhone.Layer.BorderColor = color;
       TextLast.Layer.BorderColor = color;


       ButtonCall.Clicked += (sender, args) => PlaceCall();
       ButtonEmail.Clicked += (sender, args) => SendEmail();

       NSNotificationCenter.DefaultCenter.AddObserver
        (UIKeyboard.DidShowNotification, KeyBoardUpNotification);

       // Keyboard Down
       NSNotificationCenter.DefaultCenter.AddObserver
       (UIKeyboard.WillHideNotification, KeyBoardDownNotification);

       double min = Math.Min((float)ImagePhoto.Frame.Width, (float)ImagePhoto.Frame.Height);
       ImagePhoto.Layer.CornerRadius = (float)(min / 2.0);
       ImagePhoto.Layer.MasksToBounds = false;
       ImagePhoto.Layer.BorderColor = new CGColor(1, 1, 1);
       ImagePhoto.Layer.BorderWidth = 3;
       ImagePhoto.ClipsToBounds = true;
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);
      if (ViewModel == null)
      {
        ViewModel = new DetailsViewModel();
      }
      else
      {
        this.Title = ViewModel.FirstName;
        TextEmail.Text = ViewModel.Email;
        TextFirst.Text = ViewModel.FirstName;
        TextLast.Text = ViewModel.LastName;
        TextPhone.Text = ViewModel.Phone;

        ImagePhoto.SetImage(
            url: new NSUrl(Gravatar.GetURL(ViewModel.Contact.EmailAddresses[0].Address, 172)),
            placeholder: UIImage.FromBundle("missing.png")
        );
      }
    }

    private bool ShouldReturn(UITextField field)
    {
      field.ResignFirstResponder();
      return true;
    }
   
    private void PlaceCall()
    {
      var alertPrompt = new UIAlertView("Dial Number?",
          "Do you want to call " + TextPhone.Text + "?",
          null, "No", "Yes");

      alertPrompt.Dismissed += (sender, e) =>
      {
        if ((int)e.ButtonIndex >= (int)alertPrompt.FirstOtherButtonIndex)
        {
          
          var url = new NSUrl("tel:" + TextPhone.Text);
          if (!UIApplication.SharedApplication.OpenUrl(url))
          {
            var av = new UIAlertView("Not supported",
              "Scheme 'tel:' is not supported on this device",
              null,
              "OK",
              null);
            av.Show();
          }
          else
          {
            UIApplication.SharedApplication.OpenUrl(url);
          }
        }
      };

      alertPrompt.Show();
    }


    private async void SendEmail()
    {
      var mailController = new MFMailComposeViewController();

      mailController.SetToRecipients(new string[] { TextEmail.Text });
      mailController.SetSubject("5 Minute Meeting");
      mailController.SetMessageBody("We are having a 5 minute stand up tomorrow at this time! Check your calendar.", false);

      mailController.Finished += (object s, MFComposeResultEventArgs args) =>
      {
        Console.WriteLine(args.Result.ToString());
        args.Controller.DismissViewController(true, (Action)null);
      };

      PresentViewControllerAsync(mailController, true);
     
    }

    public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
    {
      var vc = segue.DestinationViewController as NewEventDurationViewController;
      vc.ViewModel.FirstName = ViewModel.FirstName;
      vc.ViewModel.LastName = ViewModel.LastName;
      vc.ViewModel.Email = ViewModel.Email;

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
