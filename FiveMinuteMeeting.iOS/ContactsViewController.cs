using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using FiveMinuteMeeting.Shared.ViewModels;
using FiveMinuteMeeting.Shared;
using System.Linq;
using SDWebImage;
using CoreGraphics;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace FiveMinuteMeeting.iOS
{
	partial class ContactsViewController : UITableViewController
	{
		public ContactsViewController (IntPtr handle) : base (handle)
		{
		}

    private UIActivityIndicatorView activityIndicator;
    private ContactsViewModel viewModel = App.ContactsViewModel;
    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
      NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
      this.RefreshControl = new UIRefreshControl();

      activityIndicator = new UIActivityIndicatorView(new CoreGraphics.CGRect(0,0,20,20));
      activityIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;
      activityIndicator.HidesWhenStopped = true;
      NavigationItem.LeftBarButtonItem = new UIBarButtonItem(activityIndicator);

      var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) =>
      {
        var storyboard = UIStoryboard.FromName("MainStoryboard", null);

        var vc = storyboard.InstantiateViewController("detail") as UIViewController;
        NavigationController.PushViewController(vc, true);
      });

      NavigationItem.RightBarButtonItem = addButton;

      RefreshControl.ValueChanged += async (sender, args) =>
      {
        if (viewModel.IsBusy)
          return;

        await viewModel.GetContactsAsync();
        TableView.ReloadData();
      };

      viewModel.PropertyChanged += PropertyChanged;

      TableView.Source = new ContactsSource(viewModel, this);
    }

    public async override void ViewDidAppear(bool animated)
    {
      base.ViewDidAppear(animated);

      if (viewModel.IsBusy)
        return;
      
      if(viewModel.Contacts.Count > 0)
      {
        TableView.ReloadData();
        return;
      }
       
      Client.AuthorizationParams = new AuthorizationParameters(this);
      await viewModel.GetContactsAsync();
      TableView.ReloadData();
    }

    void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      InvokeOnMainThread(() =>
      {
        switch (e.PropertyName)
        {
          case "IsBusy":
            {
              if (viewModel.IsBusy)
              {
                RefreshControl.BeginRefreshing();
                activityIndicator.StartAnimating();
              }
              else
              {
                RefreshControl.EndRefreshing();
                activityIndicator.StopAnimating();
              }
            }
            break;
        }
      });
    }
    public class ContactsSource : UITableViewSource
    {
      private ContactsViewModel viewModel;
      private string cellIdentifier = "cell";
      private ContactsViewController controller;
      public ContactsSource(ContactsViewModel viewModel, ContactsViewController controller)
      {
        this.viewModel = viewModel;
        this.controller = controller;
      }

      public override string[] SectionIndexTitles(UITableView tableView)
      {
        return viewModel.ContactsGrouped.Select(s => s.Key).ToArray();
      }

      public override nint RowsInSection(UITableView tableview, nint section)
      {
        return (nint)viewModel.ContactsGrouped[(int)section].Count;
      }

      public override nint NumberOfSections(UITableView tableView)
      {
        return (nint)viewModel.ContactsGrouped.Count;
      }

      public override string TitleForHeader(UITableView tableView, nint section)
      {
        return viewModel.ContactsGrouped[(int)section].Key;
      }

      public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
      {
        return UITableViewCellEditingStyle.Delete;
      }

      public async override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
      {
        var contact = viewModel.ContactsGrouped[indexPath.Section][indexPath.Row];
        await viewModel.DeleteContact(contact);
        tableView.ReloadData();
      }

      public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
      {
        var cell = tableView.DequeueReusableCell(cellIdentifier);
        if (cell == null)
        {
          cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);
          cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
          cell.ImageView.Layer.CornerRadius = 21.0F;
          cell.ImageView.Layer.MasksToBounds = false;
          cell.ImageView.Layer.BorderColor = new CGColor(1, 1, 1);
          cell.ImageView.Layer.BorderWidth = 2;
          cell.ImageView.ClipsToBounds = true;
          //cell.ImageView.Image = UIImage.FromBundle("missing.png");
        }

        var contact = viewModel.ContactsGrouped[indexPath.Section][indexPath.Row];
        cell.TextLabel.Text = contact.GivenName + " " + contact.Surname;
        cell.DetailTextLabel.Text = contact.MobilePhone1;
        cell.ImageView.SetImage(
            url: new NSUrl(Gravatar.GetURL(contact.EmailAddresses[0].Address, 44)),
            placeholder: UIImage.FromBundle("missing.png")
        );
        return cell;
      }

      public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
      {
        var contact = viewModel.ContactsGrouped[indexPath.Section][indexPath.Row];
        var storyboard = UIStoryboard.FromName("MainStoryboard", null);

        var vc = storyboard.InstantiateViewController("detail") as ContactDetailViewController;
        vc.ViewModel = new DetailsViewModel(contact); ;
        controller.NavigationController.PushViewController(vc, true);

      }
    }

    /*public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
    {
      if (segue.Identifier == "showDetail")
      {
        var indexPath = TableView.IndexPathForSelectedRow;
        var contact = viewModel.ContactsGrouped[indexPath.Section][indexPath.Row];
       

        ((ContactDetailViewController)segue.DestinationViewController).Contact = contact;

      }
    }*/
	}
}
