using Microsoft.Office365.OutlookServices;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Office365.Discovery;
using System.Linq;

//#if __ANDROID__
//using Android.Content;
//#elif __IOS__
//using MonoTouch.UIKit;
//#endif



namespace FiveMinuteMeeting.Shared
{
    public class Client
    {

      public static string clientId = "1a080500-093f-422e-92fa-9138c66988d5";
      public static string commonAuthority = "https://login.windows.net/common";
      public static Uri returnUri = new Uri("http://localhost/e43de5a76e404eab0d9cb4527d18231f");
      const string graphResourceUri = "https://graph.windows.net/";
      public static string graphApiVersion = "2013-11-08";
      internal static Uri discoveryServiceEndpointUri = new Uri("https://api.office.com/discovery/v1.0/me/");
      internal static string discoveryServiceResourceId = "https://api.office.com/discovery/";
      private static TokenCache tokenCache;

      static Client()
      {
        tokenCache = new TokenCache();
      }

      public static IAuthorizationParameters AuthorizationParams { get; set; }

      public static async Task<OutlookServicesClient> GetContactsClient()
      {
        return await GetClient("Contacts");
      }

      public static async Task<OutlookServicesClient> GetCalendarClient()
      {
        return await GetClient("Calendar");
      }

      public static async Task<OutlookServicesClient> GetMailClient()
      {
        return await GetClient("Mail");
      }

      private static AuthenticationContext GetAuthenticationContext()
      {
        var authContext = new AuthenticationContext(commonAuthority, false, tokenCache);
        if (authContext.TokenCache.ReadItems().Count() > 0)
          authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority, false, tokenCache);

        return authContext;
      }

      private static DiscoveryClient discoveryClient;
      private static DiscoveryClient DiscoveryClient
      {
        get
        {
          //if(discoveryClient != null)
          //  return discoveryClient;

          var authContext = GetAuthenticationContext();
          discoveryClient = new DiscoveryClient(discoveryServiceEndpointUri,
           async () =>
           {
             AuthenticationResult authResult;
             try
             {
               authResult = await authContext.AcquireTokenSilentAsync(discoveryServiceResourceId, clientId);
               return authResult.AccessToken;
             }
             catch(Exception ex)
             {
               //Unable to aquire silent, must login.
             }
             authResult = await authContext.AcquireTokenAsync(discoveryServiceResourceId, clientId, returnUri, AuthorizationParams);
             return authResult.AccessToken;
           });
           
            
           return discoveryClient;
        }
      }

      

      private static async Task<OutlookServicesClient> GetClient(string capability)
      {
        var dcr = await DiscoveryClient.DiscoverCapabilityAsync(capability);
        var authContext = GetAuthenticationContext();
        var outlookClient = new OutlookServicesClient(dcr.ServiceEndpointUri,
            async () =>
            {
             
              var authResult = await authContext.AcquireTokenSilentAsync(dcr.ServiceResourceId, clientId);

              return authResult.AccessToken;
            });

        return outlookClient;
      }

    }
}
