using Microsoft.Office365.OutlookServices;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Office365.Discovery;
using System.Linq;
using FiveMinuteMeeting.Shared.Helpers;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;
using System.Globalization;


namespace FiveMinuteMeeting.Shared
{

    public class ServiceInfo
    {
      public string AccessToken { get; set; }
      public string ResourceId { get; set; }
      public string ApiEndpoint { get; set; }
    }

    public static class AuthenticationHelper
    {

      public static string clientId = "1a080500-093f-422e-92fa-9138c66988d5";
      public static Uri returnUri = new Uri("http://localhost/e43de5a76e404eab0d9cb4527d18231f");
      const string graphResourceUri = "https://graph.windows.net/";
      public static string graphApiVersion = "2013-11-08";
      internal static Uri discoveryServiceEndpointUri = new Uri("https://api.office.com/discovery/v1.0/me/");
      internal static string discoveryServiceResourceId = "https://api.office.com/discovery/";



      public static string tenantAuthority = "https://login.microsoftonline.com/{0}";
      public static string Authority
      {
        get
        {
          return string.Format(tenantAuthority, Settings.TenantId);
        }
      }

      public static IPlatformParameters PlatformParameters { get; set; }

      private static OutlookServicesClient contactsClient, calendarClient, mailClient;
      public static async Task<OutlookServicesClient> GetContactsClient()
      {
        if (contactsClient != null)
          return contactsClient;
        contactsClient =  await GetOutlookClient("Contacts");
        return contactsClient;
      }

      public static async Task<OutlookServicesClient> GetCalendarClient()
      {
        if (calendarClient != null)
          return calendarClient;
        calendarClient = await GetOutlookClient("Calendar");
        return calendarClient;
      }

      public static async Task<OutlookServicesClient> GetMailClient()
      {
        if (mailClient != null)
          return mailClient;
        mailClient = await GetOutlookClient("Mail");
        return mailClient;
      }


      private async static Task<AuthenticationResult> GetAccessToken(string resourceId)
      {
        

        var authContext = new AuthenticationContext(Authority);

        AuthenticationResult authResult = null;
        if(Settings.TenantId == "common")
        {
          authResult = await authContext.AcquireTokenAsync(resourceId, clientId, returnUri, PlatformParameters);
        }
        else
        {

          try
          {
            authResult = await authContext.AcquireTokenSilentAsync(resourceId, clientId);
          }
          catch(AdalSilentTokenAcquisitionException ex)
          {
            //failed
          }

          if(authResult == null)
          {
            authResult = await authContext.AcquireTokenAsync(resourceId, clientId, returnUri, PlatformParameters);
          }

        }

  
        
        
        Settings.TenantId = authResult.TenantId;

        return authResult;
      }


      private static DiscoveryClient discoveryClient;
      private async static Task<DiscoveryClient> GetDiscoveryClient()
      {

          if(discoveryClient != null)
            return discoveryClient;

        
        discoveryClient = new DiscoveryClient(
          async () =>
          {
            var authResult = await GetAccessToken(discoveryServiceResourceId);
            return authResult.AccessToken; 
          });
           
            
          return discoveryClient;
        
      }

      private static async Task<OutlookServicesClient> GetOutlookClient(string capability)
      {
        var discoveryClient = await GetDiscoveryClient();

        var discoveryResult = await discoveryClient.DiscoverCapabilityAsync(capability);


        var outlookClient = new OutlookServicesClient(discoveryResult.ServiceEndpointUri,
            async () =>
            {
              var authResult = await GetAccessToken(discoveryResult.ServiceResourceId);

              return authResult.AccessToken;
            });

        return outlookClient;
      }


     

      public static async Task<ServiceInfo> GetServiceInfo(string capability)
      {
        var discoveryClient = await GetDiscoveryClient();

        var discoveryResult = await discoveryClient.DiscoverCapabilityAsync(capability);
        var authResult = await GetAccessToken(discoveryResult.ServiceResourceId);

        return new ServiceInfo
          {
            AccessToken = authResult.AccessToken,
            ResourceId = discoveryResult.ServiceResourceId,
            ApiEndpoint = "https://outlook.office365.com/api/v1.0"
          };
      }


     
      /// <summary>
      /// Send an HTTP request, with authorization. If the request fails due to an unauthorized exception,
      ///     this method will try to renew the access token in serviceInfo and try again.
      /// </summary>
      public static async Task<HttpResponseMessage> SendRequestAsync(string accessToken, string resourceId, HttpClient client, Func<HttpRequestMessage> requestCreator)
      {
        using (var request = requestCreator.Invoke())
        {
          request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
          request.Headers.Add("X-ClientService-ClientTag", "Office 365 API Tools 1.1.0612" );
          var response = await client.SendAsync(request);

          // Check if the server responded with "Unauthorized". If so, it might be a real authorization issue, or 
          //     it might be due to an expired access token. To be sure, renew the token and try one more time:
          if (response.StatusCode == HttpStatusCode.Unauthorized)
          {
            string authority = string.Format(CultureInfo.InvariantCulture, tenantAuthority, Settings.TenantId);
            var authContext = new AuthenticationContext(authority);
            var token = await AuthenticationHelper.GetAccessToken(resourceId);

            // Create and send a new request:
            using (HttpRequestMessage retryRequest = requestCreator.Invoke())
            {
              retryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
              retryRequest.Headers.Add("X-ClientService-ClientTag", "Office 365 API Tools 1.1.0612");
              response = await client.SendAsync(retryRequest);
            }
          }

          // Return either the original response, or the response from the second attempt:
          return response;
        }
      }
    }
}
