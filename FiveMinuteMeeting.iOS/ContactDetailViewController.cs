using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using MonoTouch.MessageUI;
using Microsoft.Office365.OutlookServices;

namespace FiveMinuteMeeting.iOS
{
  partial class ContactDetailViewController : UIViewController
  {
    public ContactDetailViewController(IntPtr handle)
      : base(handle)
    {

    }


    public override void ViewDidLoad()
    {
       base.ViewDidLoad();

       NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;

       var save = new UIBarButtonItem(
         UIImage.FromBundle("save.png"),
         UIBarButtonItemStyle.Plain,
         (sender, args) =>
       {
        
       });

       NavigationItem.RightBarButtonItem = save;

       TextEmail.ShouldReturn += ShouldReturn;
       TextFirst.ShouldReturn += ShouldReturn;
       TextPhone.ShouldReturn += ShouldReturn;
       TextLast.ShouldReturn += ShouldReturn;
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);
      if(Contact == null)
      {
        this.Title = "New Contact";
      }
      else
      {
        this.Title = Contact.GivenName;
      }
    }

    private bool ShouldReturn(UITextField field)
    {
      field.ResignFirstResponder();
      return true;
    }



    public IContact Contact { get; set; }

    private void PlaceCall()
    {
      var alertPrompt = new UIAlertView("Dial Number?",
          "Do you want to call " + string.Empty + "?",
          null, "No", "Yes");

      alertPrompt.Dismissed += (sender, e) =>
      {
        if (e.ButtonIndex >= alertPrompt.FirstOtherButtonIndex)
        {
          
          NSUrl url = new NSUrl("tel:" + string.Empty);
          UIApplication.SharedApplication.OpenUrl(url);
        }
      };

      alertPrompt.Show();
    }


    private async void SendEmail()
    {
      var mailController = new MFMailComposeViewController();

      mailController.SetToRecipients(new string[] { "john@doe.com" });
      mailController.SetSubject("mail test");
      mailController.SetMessageBody("this is a test", false);


      await PresentViewControllerAsync(mailController, true);
      mailController.DismissViewControllerAsync(true);
    }
  }
}
