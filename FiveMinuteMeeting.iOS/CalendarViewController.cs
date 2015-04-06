using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using FiveMinuteMeeting.Shared.ViewModels;

namespace FiveMinuteMeeting.iOS
{
	partial class CalendarViewController : UITableViewController
	{
		public CalendarViewController (IntPtr handle) : base (handle)
		{
		}
    private UIActivityIndicatorView activityIndicator;
    private CalendarViewModel viewModel = new CalendarViewModel();
    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      EdgesForExtendedLayout = UIRectEdge.None;
      ExtendedLayoutIncludesOpaqueBars = false;
      AutomaticallyAdjustsScrollViewInsets = false;


      NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
      this.RefreshControl = new UIRefreshControl();

      activityIndicator = new UIActivityIndicatorView(new CoreGraphics.CGRect(0, 0, 20, 20));
      activityIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;
      activityIndicator.HidesWhenStopped = true;
      NavigationItem.RightBarButtonItem = new UIBarButtonItem(activityIndicator);
      RefreshControl.ValueChanged += async (sender, args) =>
      {
        if (viewModel.IsBusy)
          return;

        await viewModel.GetEventsAsync();
        TableView.ReloadData();
      };

      viewModel.PropertyChanged += PropertyChanged;

      TableView.Source = new EventsSource(viewModel);
    }

    public async override void ViewDidAppear(bool animated)
    {
      base.ViewDidAppear(animated);

      if (viewModel.IsBusy)
        return;

      if (viewModel.Events.Count > 0)
      {
        TableView.ReloadData();
        return;
      }
  
      await viewModel.GetEventsAsync();
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
    public class EventsSource : UITableViewSource
    {
      private CalendarViewModel viewModel;
      private string cellIdentifier = "cal";
      public EventsSource(CalendarViewModel viewModel)
      {
        this.viewModel = viewModel;
      }

      public override nint RowsInSection(UITableView tableview, nint section)
      {
        return (nint)viewModel.Events.Count;
      }

      public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
      {
        return UITableViewCellEditingStyle.Delete;
      }

      public async override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
      {
        var calEvent = viewModel.Events[indexPath.Row];
        await viewModel.DeleteEvent(calEvent);
        tableView.ReloadData();
      }

      public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
      {
        var cell = tableView.DequeueReusableCell(cellIdentifier);
        if (cell == null)
        {
          cell = new UITableViewCell(UITableViewCellStyle.Value1, cellIdentifier);
        }

        var calEvent = viewModel.Events[indexPath.Row];
        cell.DetailTextLabel.Text = calEvent.Subject;
        if (calEvent.Start.HasValue)
          cell.TextLabel.Text = calEvent.Start.Value.ToLocalTime().ToString("dd/MM/yy");
        else
          cell.TextLabel.Text = string.Empty;

        return cell;
      }
    }
	}
}
