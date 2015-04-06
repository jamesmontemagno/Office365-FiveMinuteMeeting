using FiveMinuteMeeting.Shared.Office;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FiveMinuteMeeting.Shared.ViewModels
{
    public class SendEmailViewModel : BaseViewModel
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


      private string subject = string.Empty;
      public string Subject
      {
        get { return subject; }
        set { subject = value; OnPropertyChanged("Subject"); }
      }

      private string body = string.Empty;
      public string Body
      {
        get { return body; }
        set { body = value; OnPropertyChanged("Body"); }
      }

      private bool isPriority = false;
      public bool IsPriority
      {
        get { return isPriority; }
        set { isPriority = value; OnPropertyChanged("IsPriority"); }
      }

      public async Task SendEmail()
      {
        await MailAPI.SendEmail(email, firstName + " " + lastName, subject, body, isPriority);
      }
    }
}
