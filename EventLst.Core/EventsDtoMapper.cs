using System;
using System.Collections.Generic;

namespace EventLst.Core
{
    public class EventsDtoMapper : IDtoMapper<EventModel>
    {
        public List<EventModel> Map(dynamic json)
        {
            var retModels = new List<EventModel>();
            
            MapResults(json, retModels);

            return retModels;
        }

        private void MapResults(dynamic json, List<EventModel> retModels)
        {
            var count = 0;
            foreach (var dto in json.results)
            {
                var eventModel = new EventModel();
                eventModel.Name = dto.name;
                eventModel.DateAndTime = FromUnixTime(Convert.ToInt64(dto.time));
                eventModel.HtmlDescription = dto.description;

                MapVenueDetails(dto, eventModel);

                retModels.Add(eventModel);

                count++;
            }
        }

        private static void MapVenueDetails(dynamic dto, EventModel eventModel)
        {
            if (dto.venue != null)
            {
                eventModel.City = dto.venue.city;
            }
        }

        private DateTime FromUnixTime(long unixTimeMilliSeconds)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTimeMilliSeconds / 1000);
        }
    }
}
