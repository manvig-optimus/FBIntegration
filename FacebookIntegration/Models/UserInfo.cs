using Facebook;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace FacebookIntegration.Models
{
    public class UserInfo
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string InterestedIn { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public long FriendsCount { get; set; }

        private static dynamic _userInfo;

        /// <summary>
        /// Function to get details about User.
        /// </summary>
        /// <returns>Returns </returns>
        public UserInfo GetUserInfo()
        {
            try
            {
                string fields = "me?fields=first_name,middle_name,last_name,email,gender,interested_in,work,birthday";

                _userInfo = AccountHandler._client.Get(fields);

                var user = new UserInfo()
                {
                    FirstName = _userInfo.first_name ?? "",
                    MiddleName = _userInfo.middle_name ?? "",
                    LastName = _userInfo.last_name ?? "",
                    Email = _userInfo.email ?? "",
                    Gender = _userInfo.gender ?? "",
                    InterestedIn = _userInfo.interested_in == null ? "" : _userInfo.interested_in[0],
                    CompanyName = _userInfo.work == null ? "" : _userInfo.work[0].employer["name"],
                    Position = _userInfo.work == null ? "" : _userInfo.work[0].position["name"]
                };

                dynamic friends = AccountHandler._client.Get("/me/friends");

                user.FriendsCount = friends.summary["total_count"];

                return user;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Function to export data into Json format.
        /// </summary>
        /// <returns>Return file path if operation is successful.</returns>
        public string ExportData()
        {
            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(_userInfo, Newtonsoft.Json.Formatting.Indented);
                
                var fileName = new StringBuilder("FB");

                if (_userInfo.first_name != null)
                {
                    fileName.Append("_").Append(_userInfo.first_name);
                }

                if (_userInfo.middle_name != null)
                {
                    fileName.Append("_").Append(_userInfo.middle_name);
                }

                if (_userInfo.last_name != null)
                {
                    fileName.Append("_").Append(_userInfo.last_name);
                }

                fileName.Append(".txt");

                string filepath = AppDomain.CurrentDomain.BaseDirectory + fileName.ToString();

                System.IO.File.WriteAllText(filepath, json);

                return filepath;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}