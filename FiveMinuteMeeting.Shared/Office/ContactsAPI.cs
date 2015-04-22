using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office365.OutlookServices;
using System.Threading.Tasks;
using System.Net.Http;
using System.Globalization;

namespace FiveMinuteMeeting.Shared
{
  public static class ContactsAPI
  {
    
    public static async Task<IEnumerable<IContact>> GetContacts()
    {
      
      var client = await AuthenticationHelper.GetContactsClient();
      // Obtain first page of contacts
      var contactsResults = await (from i in client.Me.Contacts
                                   orderby i.Surname
                                   select i).Take(100).ExecuteAsync();

      return contactsResults.CurrentPage;
    }

    public static async Task UpdateContact(IContact contact)
    {
      await contact.UpdateAsync();
    }

    public static async Task DeleteContact(IContact contact)
    {
      await contact.DeleteAsync();
    }

    public static async Task<bool> AddContact(IContact contact)
    {
      try
      {
        var serviceInfo = await AuthenticationHelper.GetServiceInfo("Contacts");

        var requestUrl = String.Format(CultureInfo.InvariantCulture,
            "{0}/me/contacts", serviceInfo.ApiEndpoint);

        string postData = string.Format("{{\"GivenName\":\"{0}\",\"Surname\":\"{1}\",\"EmailAddresses\":[{{\"Address\":\"{2}\",\"Name\":\"{3}\"}}],\"BusinessPhones\":[\"{4}\"]}}",
          contact.GivenName, contact.Surname, contact.EmailAddresses[0].Address, contact.EmailAddresses[0].Name, contact.BusinessPhones[0]);

        Func<HttpRequestMessage> requestCreator = () =>
        {
          HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
          request.Headers.Add("Accept", "application/json;odata.metadata=minimal");

          request.Content = new StringContent(postData, Encoding.UTF8, "application/json");

          return request;
        };


        var httpClient = new HttpClient();
        var response = await AuthenticationHelper.SendRequestAsync(serviceInfo.AccessToken,
          serviceInfo.ResourceId, httpClient, requestCreator);

        //contact.FileAs = contact.Surname + ", " + contact.GivenName;
        //var client = await AuthenticationHelper.GetContactsClient();
        //await client.Me.Contacts.AddContactAsync(contact);
      }
      catch(Exception ex)
      {
        //add popup here for error
        return false;
      }
      return true;
    }

    
  }
}