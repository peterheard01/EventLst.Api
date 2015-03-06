using System;
using System.Collections.Generic;

namespace EventLst.Core
{
    public class EventsDtoMapper : IDtoMapper<EventModel>
    {
        public List<EventModel> Map(dynamic json)
        {
            var retModels = new List<EventModel>();
            foreach (var dto in json.results)
            {
                var eventModel = new EventModel();
                eventModel.Name = dto.name;
                eventModel.DateAndTime = FromUnixTime(Convert.ToInt64(dto.time));
                eventModel.HtmlDescription = dto.description;

                if (dto.venue != null)
                {
                    eventModel.City = dto.venue.city;
                }
                retModels.Add(eventModel);
            }
            return retModels;
        }

        private DateTime FromUnixTime(long unixTimeMilliSeconds)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTimeMilliSeconds / 1000);
        }
    }
}
