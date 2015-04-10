using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FiveMinuteMeeting.Shared.Office
{
    public static class MailAPI
    {

      public static async Task SendEmail(string email, string name, string subject, string body, bool isPriority = false)
      {
        var message = new Message
        {
          Body = new ItemBody { Content = body, ContentType = BodyType.Text },
          Importance = isPriority ? Importance.High : Importance.Normal,
          Subject = subject,
          ToRecipients = new List<Recipient>
          {
            new Recipient
            {
              EmailAddress = new EmailAddress{Address = email, Name = name }
            }
          }
        };
        var client = await AuthenticationHelper.GetMailClient();

        await client.Me.SendMailAsync(message, true);
      }
    }
}
