using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLst.Core
{
    public class EventsDtoMapper
    {
        public List<EventModel> Map(dynamic json)
        {
            var retModels = new List<EventModel>();
            foreach (var dto in json.results)
            {
                retModels.Add(new EventModel()
                {
                    Name = dto.name,
                    DateAndTime = FromUnixTime(Convert.ToInt64(dto.time)),
                    City = dto.venue.city,
                    HtmlDescription = dto.description
                });
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
