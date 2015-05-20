using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office365.OutlookServices;
using System.Threading.Tasks;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using FiveMinuteMeeting.Shared.Helpers;
using System.Runtime.Serialization;
using System.Net;
using FiveMinuteMeeting.Shared.Office.Models;

namespace FiveMinuteMeeting.Shared
{
  public static class CalendarAPI
  {

    public static async Task DeleteEvent(IEvent calendarEvent)
    {
      await calendarEvent.DeleteAsync();
    }
    public static async Task<IEnumerable<IEvent>> GetEventsAsync()
    {
      var client = await AuthenticationHelper.GetCalendarClient();

      // Obtain first page of events
      var calResults = await (from i in client.Me.Calendar.Events
                              orderby i.Start
                              select i).Take(100).ExecuteAsync();

      return calResults.CurrentPage;

    }

    public static async Task<bool> AddEvent(DateTime start, string[] emails, string[] names, int length = 5, string phone = "")
    {
      try
      {
        var calendarEvent = new CalendarEvent
        {
          Start = start.ToString("O"),
          End = start.AddMinutes(length).ToString("O"),
          Subject = length + " Minute Meeting",
          Body = new CalendarBody
          {
            Content = length + " minute meeting for status report",
            ContentType = "HTML"
          }

        };

        if (emails.Length > 0)
          calendarEvent.Attendees = new CalendarAttendee[emails.Length];
        else
          calendarEvent.Attendees = new List<CalendarAttendee>().ToArray();


        for (int i = 0; i < emails.Length; i++)
        {
          if (string.IsNullOrWhiteSpace(emails[i]) || string.IsNullOrWhiteSpace(names[i]))
            continue;

          calendarEvent.Attendees[i]=new CalendarAttendee
            {
              EmailAddress = new CalendarEmailAddress
              {
                Address = emails[i],
                Name = names[i]
              },
              Type = AttendeeType.Required.ToString()
            };
        }
        var result = await AddCalendarEvent(calendarEvent);

				if (!string.IsNullOrWhiteSpace(phone))
					TwilioHelper.ScheduleMeeting(phone, start, length); ;
      }
      catch (Exception ex)
      {
        var message = ex.ToString();
        return false;
      }

      return true;
    }


    public static async Task<CalendarEvent> AddCalendarEvent(CalendarEvent calendarEvent)
    {

      var serviceInfo = await AuthenticationHelper.GetServiceInfo("Calendar");

      var requestUrl = String.Format(CultureInfo.InvariantCulture,
          "{0}/me/events", serviceInfo.ApiEndpoint);

      string postData = SerializationHelper.Serialize(calendarEvent);

      postData = postData.Replace("null", string.Empty);

      Func<HttpRequestMessage> requestCreator = () =>
      {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
        request.Headers.Add("Accept", "application/json;odata.metadata=minimal");

        request.Content = new StringContent(postData, Encoding.UTF8, "application/json");

        return request;
      };


      var httpClient = new HttpClient();
      var response = await AuthenticationHelper.SendRequestAsync(serviceInfo.AccessToken,
        serviceInfo.ResourceId, httpClient, requestCreator);

      string responseString = await response.Content.ReadAsStringAsync();

      var c = SerializationHelper.Deserialize<CalendarEvent>(responseString);

      return c;
    }

  }
}
