using FiveMinuteMeeting.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiveMinuteMeeting.Shared
{
    public static class App
    {
      private static ContactsViewModel contactsVM;
      public static ContactsViewModel ContactsViewModel
      {
        get
        {
          return contactsVM ?? (contactsVM = new ContactsViewModel());
        }
      }
    }
}
