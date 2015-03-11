using FiveMinuteMeeting.Shared.Helpers;
using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace FiveMinuteMeeting.Shared.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
      public CalendarViewModel()
      {
        Events = new ObservableCollection<IEvent>();
      }

      public ObservableCollection<IEvent> Events { get; set; }
     
      private bool isBusy;
      public bool IsBusy
      {
        get { return isBusy; }
        set { isBusy = value; OnPropertyChanged("IsBusy"); }
      }

      public async Task DeleteEvent(IEvent calendarEvent)
      {
        Events.Remove(calendarEvent);
        CalendarAPI.DeleteEvent(calendarEvent);
      }

      public async Task GetEventsAsync()
      {
        if (IsBusy)
          return;

        IsBusy = true;

        try
        {
          Events.Clear();
          var events = await CalendarAPI.GetEventsAsync();
          foreach (var e in events)
            Events.Add(e);

        }
        catch(Exception ex)
        {
          //add pop up here for error
        }
        finally
        {
          IsBusy = false;
        }
      }

    }
}
