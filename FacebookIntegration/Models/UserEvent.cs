using Facebook;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace FacebookIntegration.Models
{
    public class UserEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }

        /// <summary>
        /// Function to get Facebook Events.
        /// </summary>
        /// <returns>Returns list of Events.</returns>
        public List<UserEvent> GetUserEvents()
        {
            try
            {
                dynamic events = AccountHandler._client.Get("/me/events");

                var userEvents = new List<UserEvent>();

                foreach (var eventInfo in events["data"])
                {
                    var userEvent = new UserEvent()
                    {
                        Name = (string)eventInfo["name"],
                        Description = (string)eventInfo["description"],
                        StartTime = Convert.ToDateTime(eventInfo["start_time"]),
                        EndTime = Convert.ToDateTime(eventInfo["end_time"]),
                        Status = (string)eventInfo["rsvp_status"]
                    };

                    userEvents.Add(userEvent);
                }

                return userEvents;
            }
            catch
            {
                throw;
            }
        }
    }
}