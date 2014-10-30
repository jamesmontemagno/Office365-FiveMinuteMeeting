using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using FiveMinuteMeeting.Shared.ViewModels;
using FiveMinuteMeeting.Shared;
using System.Linq;

namespace FiveMinuteMeeting.iOS
{
	partial class ContactsViewController : UITableViewController
	{
		public ContactsViewController (IntPtr handle) : base (handle)
		{
		}

    private UIActivityIndicatorView activityIndicator;
    private ContactsViewModel viewModel = new ContactsViewModel();
    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
      NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
      this.RefreshControl = new UIRefreshControl();

      activityIndicator = new UIActivityIndicatorView(new System.Drawing.RectangleF(0,0,20,20));
      activityIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;
      activityIndicator.HidesWhenStopped = true;
      NavigationItem.LeftBarButtonItem = new UIBarButtonItem(activityIndicator);

      var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) =>
      {
        var storyboard = UIStoryboard.FromName("MainStoryboard", null);

        var vc = storyboard.InstantiateViewController("detail") as UIViewController;
        NavigationController.PushViewController(vc, true);
      });

      NavigationItem.RightBarButtonItem = addButton;;

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

      if (viewModel.IsBusy || viewModel.Contacts.Count > 0)
        return;

      await Client.EnsureClientCreated(this);
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

      public override int RowsInSection(UITableView tableview, int section)
      {
        return viewModel.ContactsGrouped[section].Count;
      }

      public override int NumberOfSections(UITableView tableView)
      {
        return viewModel.ContactsGrouped.Count;
      }

      public override string TitleForHeader(UITableView tableView, int section)
      {
        return viewModel.ContactsGrouped[section].Key;
      }

      public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
      {
        return UITableViewCellEditingStyle.Delete;
      }

      public async override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
      {
        var contact = viewModel.ContactsGrouped[indexPath.Section][indexPath.Row];
        //await viewModel.ExecuteDeleteExpenseCommand(expense);
        tableView.ReloadData();
      }

      public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
      {
        var cell = tableView.DequeueReusableCell(cellIdentifier);
        if (cell == null)
        {
          cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);
          cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
          cell.ImageView.Image = UIImage.FromBundle("missing.png");
        }

        var contact = viewModel.ContactsGrouped[indexPath.Section][indexPath.Row];
        cell.TextLabel.Text = contact.GivenName + " " + contact.Surname;
        cell.DetailTextLabel.Text = contact.MobilePhone1;

        return cell;
      }
    }

    public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
    {
      if (segue.Identifier == "showDetail")
      {
        var indexPath = TableView.IndexPathForSelectedRow;
        var contact = viewModel.ContactsGrouped[indexPath.Section][indexPath.Row];
       

        ((ContactDetailViewController)segue.DestinationViewController).Contact = contact;
      }
    }
	}
}
