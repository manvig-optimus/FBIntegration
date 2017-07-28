using Facebook;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace FacebookIntegration.Models
{
    public class UserTaggedPlace
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public DateTime TaggedTime { get; set; }

        public List<UserTaggedPlace> GetTaggedPlaces()
        {
            try
            {
                dynamic places = AccountHandler._client.Get("/me/tagged_places");

                var taggedPlaces = new List<UserTaggedPlace>();

                foreach (var placeInfo in places["data"])
                {
                    var taggedPlace = new UserTaggedPlace()
                    {
                        Name = placeInfo.place["name"],
                        Street = placeInfo.place["location"].street,
                        TaggedTime = Convert.ToDateTime(placeInfo["created_time"])
                    };

                    taggedPlaces.Add(taggedPlace);
                }

                return taggedPlaces;
            }
            catch
            {
                throw;
            }
        }
    }
}