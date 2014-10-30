using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiveMinuteMeeting.Shared.Helpers
{
    public static class ContactExtensions
    {
      public static string GetSortName(this IContact contact)
      {
        if (string.IsNullOrWhiteSpace(contact.Surname))
          return "?";

        return contact.Surname[0].ToString().ToUpper();
      }
    }
}
