using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FacebookIntegration.Models
{
    public class UserAlbum
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Function to get details of Uploaded Albums.
        /// </summary>
        /// <returns>Returns list of uploaded albums.</returns>
        public List<UserAlbum> GetAlbumDetails()
        {
            try
            {
                dynamic albums = AccountHandler._client.Get("/me/albums");

                var userAlbums = new List<UserAlbum>();

                foreach (var album in albums["data"])
                {
                    var userAlbum = new UserAlbum();

                    userAlbum.Name = (string)album["name"];
                    userAlbum.CreatedDate = Convert.ToDateTime(album["created_time"]);

                    userAlbums.Add(userAlbum);
                }

                return userAlbums;
            }
            catch
            {
                throw;
            }
        }
    }
}