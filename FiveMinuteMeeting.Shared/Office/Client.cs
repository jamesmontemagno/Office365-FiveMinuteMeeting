using Microsoft.Office365.OutlookServices;
using System.Threading.Tasks;
using Microsoft.Office365.OAuth;

#if __ANDROID__
using Android.Content;
#elif __IOS__
using MonoTouch.UIKit;
using System;
#endif



namespace FiveMinuteMeeting.Shared
{
    public class Client
    {

      const string ExchangeResourceId = "https://outlook.office365.com";
      const string ExchangeServiceRoot = "https://outlook.office365.com/ews/odata";

      public static OutlookServicesClient Instance;

#if __ANDROID__
      public static async Task<OutlookServicesClient> EnsureClientCreated(Context context)
#elif __IOS__
        public static async Task<OutlookServicesClient> EnsureClientCreated(UIViewController context)
      
#endif
      {
        Authenticator authenticator = new Authenticator(context);
        var authInfo = await authenticator.AuthenticateAsync(ExchangeResourceId);

        Instance = new OutlookServicesClient(new Uri(ExchangeServiceRoot), authInfo.GetAccessToken);
        return Instance;
      }
    }
}
