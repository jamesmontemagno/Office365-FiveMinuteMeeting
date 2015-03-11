using FiveMinuteMeeting.Shared.Helpers;
using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace FiveMinuteMeeting.Shared.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
      public ContactsViewModel()
      {
        Contacts = new ObservableCollection<IContact>();
        ContactsGrouped = new ObservableCollection<Grouping<string, IContact>>();
      }

      public ObservableCollection<IContact> Contacts { get; set; }
      public ObservableCollection<Grouping<string, IContact>> ContactsGrouped { get; set; }

      private bool isBusy;
      public bool IsBusy
      {
        get { return isBusy; }
        set { isBusy = value; OnPropertyChanged("IsBusy"); }
      }

      public async Task DeleteContact(IContact contact)
      {
        Contacts.Remove(contact);
        SortContacts();
        ContactsAPI.DeleteContact(contact);
      }

      public async Task GetContactsAsync()
      {
        if (IsBusy)
          return;

        IsBusy = true;

        try
        {
          Contacts.Clear();
          var contacts = await ContactsAPI.GetContacts();
          foreach (var contact in contacts)
            Contacts.Add(contact);

          SortContacts();

        }
        catch(Exception ex)
        {
          //add pop up here for error
        }
        finally
        {
          IsBusy = false;
        }
      }

      private void SortContacts()
      {

        var sorted = from contact in Contacts
                     orderby contact.Surname
                     group contact by contact.GetSortName() into contactGroup
                     select new Grouping<string, IContact>(contactGroup.Key, contactGroup);

        //create a new collection of groups
        ContactsGrouped = new ObservableCollection<Grouping<string, IContact>>(sorted);
      }


      public void AddContact(IContact contact)
      {
        Contacts.Add(contact);
        Contacts = new ObservableCollection<IContact>(Contacts.OrderBy(s => s.Surname));
        SortContacts();
      }

    }
}
