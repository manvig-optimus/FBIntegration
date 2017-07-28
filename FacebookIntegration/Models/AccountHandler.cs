using Facebook;
using System;
using System.Configuration;
using System.Text;

namespace FacebookIntegration.Models
{
    public class AccountHandler
    {
        public static FacebookClient _client = new FacebookClient();

        /// <summary>
        /// Function to get OAuth Login Uri of Facebook.
        /// </summary>
        /// <param name="absoulteUri">Contain abosulte Uri address.</param>
        /// <returns>Returns OAuth Login Uri of FaceBook.</returns>
        public Uri GetLoginUrl(string absoulteUri)
        {
            try
            {
                var facebookOptions = new StringBuilder("email,")
                    .Append("user_events,")
                    .Append("user_photos,")
                    .Append("user_videos,")
                    .Append("user_friends,")
                    .Append("user_relationship_details,")
                    .Append("user_tagged_places,")
                    .Append("user_work_history,")
                    .Append("user_birthday");                    

                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                string appKey = ConfigurationManager.AppSettings["AppSecretKey"].ToString();

                return _client.GetLoginUrl(new
                {
                    client_id = appId,
                    client_secret = appKey,
                    redirect_uri = absoulteUri,
                    response_type = "code",
                    scope = facebookOptions.ToString()
                });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Function to get Access Token after successful Authentication.
        /// </summary>
        /// <param name="absoluteUri">Contains Abosulte Uri of Facebook</param>
        /// <param name="code"></param>
        public void GetAccessToken(string absoluteUri, string code)
        {
            try
            {
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                string appKey = ConfigurationManager.AppSettings["AppSecretKey"].ToString();

                dynamic result = _client.Post("oauth/access_token", new
                {
                    client_id = appId,
                    client_secret = appKey,
                    redirect_uri = absoluteUri,
                    code = code
                });

                _client.AccessToken = result.access_token;
            }
            catch
            {
                throw;
            }
        }
    }
}