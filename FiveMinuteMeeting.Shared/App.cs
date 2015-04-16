using FiveMinuteMeeting.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiveMinuteMeeting.Shared
{
    public static class App
    {

      private static SendEmailViewModel sendEmailViewModel;
      public static SendEmailViewModel SendEmailViewModel
      {
        get
        {
          return sendEmailViewModel ?? (sendEmailViewModel = new SendEmailViewModel());
        }
      }

      private static CalendarViewModel calendarViewModel;
      public static CalendarViewModel CalendarViewModel
      {
        get
        {
          return calendarViewModel ?? (calendarViewModel = new CalendarViewModel());
        }
      }

      private static ContactsViewModel contactsVM;
      public static ContactsViewModel ContactsViewModel
      {
        get
        {
          return contactsVM ?? (contactsVM = new ContactsViewModel());
        }
      }

      private static NewEventViewModel newEventVM;
      public static NewEventViewModel NewEventViewModel
      {
        get
        {
          return newEventVM ?? (newEventVM = new NewEventViewModel());
        }
      }
    }
}
