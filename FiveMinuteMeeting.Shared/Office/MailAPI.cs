using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FiveMinuteMeeting.Shared.Office
{
    public static class MailAPI
    {

      public static async Task<bool> SendEmail(string email, string name, string subject, string body, bool isPriority = false)
      {
        try
        {
          var serviceInfo = await AuthenticationHelper.GetServiceInfo("Mail");

          var requestUrl = String.Format(CultureInfo.InvariantCulture,
              "{0}/me/sendmail", serviceInfo.ApiEndpoint);

          string postData = string.Format("{{\"Message\":{{\"Subject\":\"{0}\",\"Body\":{{\"ContentType\":\"Text\",\"Content\":\"{1}\"}},\"ToRecipients\":[{{\"EmailAddress\":{{\"Address\":\"{2}\"}}}}]}},\"SaveToSentItems\":\"false\"}}",
                                        subject, body, email);

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

        }
        catch (Exception ex)
        {
          //add popup here for error
          return false;
        }
        return true;
      }
    }
}
