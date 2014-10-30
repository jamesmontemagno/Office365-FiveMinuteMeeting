using FiveMinuteMeeting.Shared.Helpers;
using Microsoft.Office365.OutlookServices;
using Refractored.Xam.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace FiveMinuteMeeting.Shared.ViewModels
{
    public class ContactsViewModel : INotifyPropertyChanged
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
          var message = new Messages();
          message.ShowMessage("Unable to gather contacts.");
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

      public event PropertyChangedEventHandler PropertyChanged;
      private void OnPropertyChanged(string name)
      {
        if (PropertyChanged == null)
          return;

        PropertyChanged(this, new PropertyChangedEventArgs(name));
      }
    }
}
