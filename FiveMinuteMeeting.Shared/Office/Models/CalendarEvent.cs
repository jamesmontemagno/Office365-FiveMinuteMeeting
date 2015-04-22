using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FiveMinuteMeeting.Shared.Office.Models
{
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

    [DataMember(Name = "Subject")]
    public string Subject { get; set; }

    [DataMember(Name = "Start")]
    public string Start { get; set; }

    [DataMember(Name = "End")]
    public string End { get; set; }

    [DataMember(Name = "Body")]
    public CalendarBody Body { get; set; }

    [DataMember(Name = "Attendees")]
    public IList<CalendarAttendee> Attendees { get; set; }
  }

}
