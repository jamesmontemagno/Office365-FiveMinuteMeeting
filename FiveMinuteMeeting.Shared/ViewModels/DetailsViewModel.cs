using FiveMinuteMeeting.Shared.Office;
using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace FiveMinuteMeeting.Shared.ViewModels
{
  public class DetailsViewModel : BaseViewModel
  {

    public IContact Contact { get; set; }

    
    public DetailsViewModel(IContact contact = null)
    {
      Contact = contact;
      if (contact == null)
        return;
      
      FirstName = Contact.GivenName;
      LastName = Contact.Surname;
      Phone = Contact.BusinessPhones.Count > 0 ? Contact.BusinessPhones[0] : string.Empty;
      Email = Contact.EmailAddresses.Count > 0 ? Contact.EmailAddresses[0].Address : string.Empty; 
    }


    private string title = "New Contact";
    public string Title
    {
      get { return title; }
      set { title = value; OnPropertyChanged("Title"); }
    }

    private string firstName = string.Empty;
    public string FirstName
    {
      get { return firstName; }
      set { firstName = value; OnPropertyChanged("FirstName"); }
    }


    private string lastName = string.Empty;
    public string LastName
    {
      get { return lastName; }
      set { lastName = value; OnPropertyChanged("LastName"); }
    }

    private string email = string.Empty;
    public string Email
    {
      get { return email; }
      set { email = value; OnPropertyChanged("Email"); }
    }


    private string phone = string.Empty;
    public string Phone
    {
      get { return phone; }
      set { phone = value; OnPropertyChanged("Phone"); }
    }

    public async Task AddEvent(string[] email, string[] name)
    {
      await CalendarAPI.AddEvent(DateTime.Now.AddDays(1), email, name);
      //add message here
    }

    public async Task SendEmail()
    {
      await MailAPI.SendEmail(Email, FirstName + " " + LastName,
        "Meeting tomorrow",
        "Don't forget about our meeting tomorrow.");

      //Add Popup here
    }


    public async Task SaveContact()
    {
        var contact = Contact;
        bool newContact = (Contact == null);
        //add new contact
        if(newContact)
        {
          contact = new Contact();
        }

        contact.GivenName = FirstName;
        contact.Surname = LastName;
        if (contact.EmailAddresses.Count == 0)
        {
          contact.EmailAddresses.Add(new EmailAddress
            {
              Address = Email,
              Name = FirstName  + " " + LastName
            });
        }
        else
        {
          contact.EmailAddresses[0].Address = Email;
          contact.EmailAddresses[0].Name = FirstName + " " + LastName;
        }

        if(contact.BusinessPhones.Count == 0)
        {
          contact.BusinessPhones.Add(Phone);
        }
        else
        {
          contact.BusinessPhones[0] = Phone;
        }

        if (newContact)
        {
          await ContactsAPI.AddContact(contact);
          App.ContactsViewModel.AddContact(contact);
          Contact = contact;
        }
        else
        {
          await ContactsAPI.UpdateContact(contact);
        }
    }

  }
}
