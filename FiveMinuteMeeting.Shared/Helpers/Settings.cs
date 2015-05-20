
// Helpers/Settings.cs
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;

namespace FiveMinuteMeeting.Shared.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

    #region Setting Constants

    private const string TenantKey = "tenant_key";
    private static readonly string TenantDefault = "common";


		private const string LoggedInUserKey = "logged_in_user_key";
		private static readonly string LoggedInUserDefault = string.Empty;

		private const string TwilioNumberKey = "twilio_number_key";
#if __ANDROID__
		private static readonly string TwilioNumberDefault = "12014686561";
#else
		private static readonly string TwilioNumberDefault = "14088316689";
#endif

		#endregion
		public static string TwilioNumber
		{
			get
			{
				return AppSettings.GetValueOrDefault(TwilioNumberKey, TwilioNumberDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(TwilioNumberKey, value);
			}
		}

    public static string TenantId
    {
      get
      {
        return AppSettings.GetValueOrDefault(TenantKey, TenantDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue(TenantKey, string.IsNullOrWhiteSpace(value) ? TenantDefault : value);
      }
    }


    public static string LoggedInUser
    {
      get
      {
        return AppSettings.GetValueOrDefault(LoggedInUserKey, LoggedInUserDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue(LoggedInUserKey, value);
      }
    }

  }
}