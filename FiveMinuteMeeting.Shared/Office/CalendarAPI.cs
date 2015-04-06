using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office365.OutlookServices;
using System.Threading.Tasks;

namespace FiveMinuteMeeting.Shared
{
    public static class CalendarAPI
    {
      public static async Task AddEvent(DateTime start, string[] emails, string[] names, int length = 5)
      {
        var calendarEvent = new Event
        {
          Attendees = new List<Attendee>(),
          Body = new ItemBody{Content = length + " minute meeting for status report.", ContentType = BodyType.Text},
          Start = new DateTimeOffset(start),
          End = new DateTimeOffset(start.AddMinutes(length)), 
          Subject = length + " Minute Meeting",
          Location = new Location { DisplayName = "Everywhere"}
        };

        for (int i = 0; i < emails.Length; i++)
        {
          calendarEvent.Attendees.Add(new Attendee
            {
              EmailAddress = new EmailAddress
              {
                Address = emails[i],
                Name = names[i]
              },
              Type = AttendeeType.Required
            });
        }

          try
          {
            var client = await Client.GetCalendarClient();

            await client.Me.Events.AddEventAsync(calendarEvent, true);
            await client.Context.SaveChangesAsync();
          }
          catch (Exception ex)
          {
            var message = ex.ToString();
          }
      }

      public static async Task DeleteEvent(IEvent calendarEvent)
      {
        await calendarEvent.DeleteAsync();
      }

      public static async Task<IEnumerable<IEvent>> GetEventsAsync()
      {
        var client = await Client.GetCalendarClient();
     
        // Obtain first page of events
        var calResults = await (from i in client.Me.Calendar.Events
                                orderby i.Start     
                                select i).Take(100).ExecuteAsync();

        return calResults.CurrentPage;
       
      }

      


     
    }
}
