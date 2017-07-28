using Facebook;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace FacebookIntegration.Models
{
    public class UploadedVideo
    {
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }

        /// <summary>
        /// Function to view detail of Videos uploaded by User.
        /// </summary>
        /// <returns>Return list of Uploaded Video details.</returns>
        public List<UploadedVideo> GetUploadedVideos()
        {
            try
            {
                dynamic videos = AccountHandler._client.Get("/me/videos/uploaded");

                var uploadedVideos = new List<UploadedVideo>();

                foreach (var video in videos["data"])
                {
                    var userEvent = new UploadedVideo()
                    {
                        Description = (string)video["description"],
                        UploadDate = Convert.ToDateTime(video["updated_time"])
                    };

                    uploadedVideos.Add(userEvent);
                }

                return uploadedVideos;
            }
            catch
            {
                throw;
            }
        }
    }
}