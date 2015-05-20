using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace TwilioBot.Web.Controllers
{
    public class ClientController : Controller
    {
        const string AccountSID = "AccoutnSID";
		const string AccountAuthToken = "AccountAuthToken";
		const string TWiMLSID = "TWiMLSID";

		// GET: Client/Token?ClientName=foo
		public ActionResult Token(string clientName = "motzandroid")
		{
			// Create a TwilioCapability object passing in your credentials.
			var capability = new TwilioCapability(AccountSID, AccountAuthToken);
 
			// Specify that this token allows receiving of incoming calls
			capability.AllowClientIncoming(clientName);
			capability.AllowClientOutgoing(TWiMLSID);

			// Return the token as text
			return Content(capability.GenerateToken());
		}


		public ActionResult CallAndroid()
		{
			var response = new TwilioResponse();
			response.Dial(new Client("motzandroid"));
			return new TwiMLResult(response);
		}


		public ActionResult CalliOS()
		{
			var response = new TwilioResponse();
			response.Dial(new Client("motzios"));
			return new TwiMLResult(response);
		}


		// /Client/InitiateCall?source=5551231234&target=5554561212
		public ActionResult InitiateCall(string source, string target)
		{
			var response = new TwilioResponse();
 
			// Add a <Dial> tag that specifies the callerId attribute
			response.Dial(target, new { callerId = source });
 
			return new TwiMLResult(response);
		}

		//GET: /Client/ScheduleMeeting?source=5551231234&target=5554561212&time=12345&length=5
		public ActionResult ScheduleMeeting(string source, string target, string time, string length)
		{
			var twilio = new TwilioRestClient(AccountSID, AccountAuthToken);

			var date = DateTime.Now;
			DateTime.TryParse(time, out date);

			var message = twilio.SendMessage(source, target,
				"Incoming " + length + " Minute Meeting on "
				+ date.ToShortDateString() + " " + date.ToShortTimeString() + "!",
				new string[] { "https://raw.githubusercontent.com/driftyco/ionicons/master/png/512/ios7-calendar.png" });

			return Content("Success");
		}
  }
}