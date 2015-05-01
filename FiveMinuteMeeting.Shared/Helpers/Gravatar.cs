using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;


using System.Globalization;


namespace FiveMinuteMeeting.Shared
{
	
	 /// <summary>
    /// Gravatar interaction.
    /// </summary>
    public class Gravatar
    {
      const string HttpUrl = "http://www.gravatar.com/avatar.php?gravatar_id=";
      const string HttpsUrl = "https://secure.gravatar.com/avatar.php?gravatar_id=";
      private const string defaultImage = @"http%3a%2f%2fd1iqk4d73cu9hh.cloudfront.net%2fcomponents%2fimg%2fuser-icon.png";
   
      /// <summary>
      /// Gets the Gravatar URL.
      /// </summary>
      /// <param name="author">The author.</param>
      /// <param name="secure">Use HTTPS?</param>
      /// <param name="size">The Gravatar size.</param>
      /// <param name="rating">The Gravatar rating.</param>
      /// <returns>A gravatar URL.</returns>
      public static string GetURL(string email,int size = 64, bool secure = true, string rating = "x")
      {
		email = GetMD5(email);
        var url = (secure) ? HttpsUrl : HttpUrl;
        return string.Format("{0}{1}&s={2}&r={3}", url, email, size.ToString(CultureInfo.InvariantCulture), rating);
      }

      /// <summary>
      /// Gets the MD5 of the given string.
      /// </summary>
      /// <param name="input">The input.</param>
      /// <returns>The MD5 hash.</returns>
      static string GetMD5(string input)
      {

        var bytes = Encoding.UTF8.GetBytes(input);
        var data = MD5Core.GetHash(bytes);
        var builder = new StringBuilder();

        for (int i = 0; i < data.Length; i++) builder.Append(data[i].ToString("x2"));
        return builder.ToString();
      }
    }
  
}


