using Microsoft.Office365.OutlookServices;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Office365.Discovery;
using System.Linq;
using FiveMinuteMeeting.Shared.Helpers;


namespace FiveMinuteMeeting.Shared
{
    public class Client
    {

      public static string clientId = "1a080500-093f-422e-92fa-9138c66988d5";
      public static Uri returnUri = new Uri("http://localhost/e43de5a76e404eab0d9cb4527d18231f");
      const string graphResourceUri = "https://graph.windows.net/";
      public static string graphApiVersion = "2013-11-08";
      internal static Uri discoveryServiceEndpointUri = new Uri("https://api.office.com/discovery/v1.0/me/");
      internal static string discoveryServiceResourceId = "https://api.office.com/discovery/";



      public static string tenantAuthority = "https://login.windows.net/{0}";
      public static string Authority
      {
        get
        {
          return string.Format(tenantAuthority, Settings.TenantId);
        }
      }

      public static IAuthorizationParameters AuthorizationParams { get; set; }

      public static async Task<OutlookServicesClient> GetContactsClient()
      {
        return await GetOutlookClient("Contacts");
      }

      public static async Task<OutlookServicesClient> GetCalendarClient()
      {
        return await GetOutlookClient("Calendar");
      }

      public static async Task<OutlookServicesClient> GetMailClient()
      {
        return await GetOutlookClient("Mail");
      }


      private async static Task<AuthenticationResult> GetAccessToken(string resourceId)
      {
        

        var authContext = new AuthenticationContext(Authority, false);

        AuthenticationResult authResult = null;
        if(Settings.TenantId == "common")
        {
          authResult = await authContext.AcquireTokenAsync(resourceId, clientId, returnUri, AuthorizationParams);
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
            authResult = await authContext.AcquireTokenAsync(resourceId, clientId, returnUri, AuthorizationParams);
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

        
        discoveryClient = new DiscoveryClient(discoveryServiceEndpointUri,
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

        var authResult = await GetAccessToken(discoveryResult.ServiceResourceId);

        var outlookClient = new OutlookServicesClient(discoveryResult.ServiceEndpointUri,
            async () =>
            {
              return await Task.FromResult<string>(authResult.AccessToken);
            });

        return outlookClient;
      }


    }
}
