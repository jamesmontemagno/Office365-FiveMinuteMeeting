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

namespace FiveMinuteMeeting.Shared
{
    public static class CalendarAPI
    {
      public static async Task AddEvent(DateTime start, string[] emails, string[] names, int length = 5)
      {
        var calendarEvent = new Event
        {
          Attendees = new List<Attendee>(),
          Body = new ItemBody{Content = length + " minute meeting for status report.", ContentType = BodyType.HTML},
          Start = new DateTimeOffset(start),
          End = new DateTimeOffset(start.AddMinutes(length)), 
          Subject = length + " Minute Meeting",
          Location = new Location { DisplayName = "Everywhere"}
        };

        for (int i = 0; i < emails.Length; i++)
        {
          if (string.IsNullOrWhiteSpace(emails[i]) || string.IsNullOrWhiteSpace(names[i]))
            continue;

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
            //var client = await AuthenticationHelper.GetCalendarClient();

            //await client.Me.Events.Take(1).ExecuteAsync();
            //await client.Me.Events.AddEventAsync(calendarEvent);
            var ce = new CalendarEvent
            {
              Attendees = calendarEvent.Attendees.Select(s => new CalendarAttendee
              {
                EmailAddress = new CalendarEmailAddress
                {
                  Address = s.EmailAddress.Address,
                  Name = s.EmailAddress.Name
                },
                Type = s.Type.ToString()
              }).ToList(),
              End = "2015-04-09T18:00:00Z",//calendarEvent.End.Value.ToString("u"),
              Start = "2015-04-09T1:00:00Z",//calendarEvent.Start.Value.ToString("u"),
              Location = calendarEvent.Location,
              Subject = calendarEvent.Subject,
              Body = new CalendarBody
              {
                Content = calendarEvent.Body.Content,
                ContentType = calendarEvent.Body.ContentType.ToString()
              }

            };
            var result = await AddCalendarEventRest(ce);
            var id = result.Id;
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
        var client = await AuthenticationHelper.GetCalendarClient();
     
        // Obtain first page of events
        var calResults = await (from i in client.Me.Calendar.Events
                                orderby i.Start     
                                select i).Take(100).ExecuteAsync();

        return calResults.CurrentPage;
       
      }


      public static async Task<CalendarEvent> AddCalendarEventRest(CalendarEvent calendarEvent)
      {

        var serviceInfo = await AuthenticationHelper.GetServiceInfo("Calendar");
        
        var requestUrl = String.Format(CultureInfo.InvariantCulture,
            "{0}/me/events",
            serviceInfo.ApiEndpoint);

        // string postData = JsonConvert.SerializeObject(calendarEvent);
        string postData = SerializationHelper.Serialize(calendarEvent);

        Func<HttpRequestMessage> requestCreator = () =>
        {
          HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
          request.Content = new StringContent(postData);
          request.Headers.Add("Accept", "application/json;odata=minimal");
          request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
          return request;
        };


        var client = await AuthenticationHelper.GetCalendarClient();
        var httpClient = new HttpClient();
        var response = await AuthenticationHelper.SendRequestAsync(serviceInfo.AccessToken,
          serviceInfo.ResourceId, httpClient, requestCreator);
        string responseString = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
          return null;
        }

        var c = SerializationHelper.Deserialize<CalendarEvent>(responseString);

        return c;
      }



     [DataContract]
      public class CalendarBody
      {

        [DataMember(Name = "ContentType")]
        public string ContentType { get; set; }

        [DataMember(Name = "Content")]
        public string Content { get; set; }
      }
      [DataContract]
      public class CalendarAttendee
      {
         [DataMember(Name = "EmailAddress")]
        public CalendarEmailAddress EmailAddress { get; set; }
         [DataMember(Name = "Type")]
        public string Type { get; set; }
      }
      [DataContract]
      public class CalendarEmailAddress
      {
         [DataMember(Name = "Address")]
        public string Address { get; set; }
         [DataMember(Name = "Name")]
        public string Name { get; set; }
      }

      [DataContract]
      public class CalendarEvent
      {
        //[DataMember(Name = "@odata.type")]
        //public string EntityType { get; set; }

        [DataMember(Name = "Subject")]
        public string Subject { get; set; }

        [DataMember(Name = "Start")]
        public string Start { get; set; }

        [DataMember(Name = "End")]
        public string End { get; set; }

        [DataMember(Name = "Location")]
        public Location Location { get; set; }

        [DataMember (Name = "Body")]
        public CalendarBody Body { get; set; }

        [DataMember(Name = "Attendees")]
        public IList<CalendarAttendee> Attendees { get; set; }

        public CalendarEvent()
        {
          //this.EntityType = "#Microsoft.OutlookServices.OData.Model.Event";
        }

        public string Id { get; set; } // Calendar Event ID

        public string Spid { get; set; } // Share Point ID
      }


     
    }
}
