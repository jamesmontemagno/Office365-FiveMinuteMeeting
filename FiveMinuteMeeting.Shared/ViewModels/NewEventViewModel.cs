using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace FiveMinuteMeeting.Shared.ViewModels
{
    
    public class NewEventViewModel : BaseViewModel
    {


      private string firstName = string.Empty;
      public string FirstName
      {
        get { return firstName; }
        set { firstName = value; OnPropertyChanged("FirstName"); }
      }


      private string lastName = string.Empty;
      public string LastName
      {
        get { return lastName; }
        set { lastName = value; OnPropertyChanged("LastName"); }
      }

      private string email = string.Empty;
      public string Email
      {
        get { return email; }
        set { email = value; OnPropertyChanged("Email"); }
      }

			private string phone = string.Empty;
			public string Phone
			{
				get { return phone; }
				set { phone = value; OnPropertyChanged("Phone"); }
			}

      private DateTime date = DateTime.Now;
      public DateTime Date
      {
        get { return date; }
        set { date = value; OnPropertyChanged("Date"); }
      }

      private int durationMinutes = 5;
      public int DurationMinutes
      {
        get { return durationMinutes; }
        set { durationMinutes = value; OnPropertyChanged("DurationMinutes"); }
      }

      public async Task AddEvent()
      {
        await CalendarAPI.AddEvent(Date, new[] { Email }, new[] { FirstName + " " + LastName }, DurationMinutes, Phone);
      }
    }
}
