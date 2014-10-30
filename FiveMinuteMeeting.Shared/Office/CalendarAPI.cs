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
          Id = "10",
          BodyPreview = "Preview",
          Categories = new List<string>(),
          ChangeKey = string.Empty,
          HasAttachments = false,
          Importance = Microsoft.Office365.OutlookServices.Importance.High,
          IsAllDay = false,
          IsCancelled = false,
          ResponseRequested = false,
          ShowAs = FreeBusyStatus.Busy,
          Type = EventType.Occurrence,
          Attendees = new List<Attendee>
          {
            new Attendee
            {
              EmailAddress = new EmailAddress
              {
                Address = email,
                Name = name
              },
              Type = AttendeeType.Required
            }
          },
          Body = new ItemBody{Content = "5 minute meeting for status report.", ContentType = BodyType.Text},
          Start = new DateTimeOffset(start),
          End = new DateTimeOffset(start.AddMinutes(5)), 
          Subject = "5 Minute Meeting",
          Location = new Location { DisplayName = "Everywhere"}
        };
       
        try
        {
         // var info = await Client.CalendarInstance.Me.Calendars.GetById("Calendar").Events.Take(10).ExecuteAsync();
       
          Client.Instance.Me.Events.AddEventAsync(calendarEvent).ContinueWith((action) =>
            {
              var result = action.AsyncState;
            });
        }
        catch(Exception ex)
        {
          var message = ex.ToString();
        }
      }


     
    }
}
