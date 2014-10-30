using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office365.OutlookServices;
using System.Threading.Tasks;
using Microsoft.Office365.OAuth;

namespace FiveMinuteMeeting.Shared
{
    public static class CalendarAPI
    {
      public static async Task AddEvent(DateTime start, string email, string name)
      {
        var calendarEvent = new Event
        {
          Attendees = new List<Attendee>
          {
            new Attendee
            {
              EmailAddress = new EmailAddress
              {
                Address = email,
                Name = name
              }
            }
          },
          Body = new ItemBody{Content = "5 minute meeting for status report.", ContentType = BodyType.Text},
          Start = start,
          End = start.AddMinutes(5),
          Type = EventType.SingleInstance
        };
        await Client.Instance.Me.Calendar.CalendarView.AddEventAsync(calendarEvent);
      }
    }
}
