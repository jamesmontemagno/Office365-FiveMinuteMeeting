
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

    #endregion


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