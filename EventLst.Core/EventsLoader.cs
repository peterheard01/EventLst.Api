using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace EventLst.Core
{
    public class EventsLoader
    {
        public string Lon { get; set; }
        public string Lat { get; set; }

        private IEventProvider _provider;

        public EventsLoader(IEventProvider injectedProvider)
        {
            _provider = injectedProvider;
        }

        public List<EventModel> Load()
        {
            if (Lon == null || Lat == null) throw new Exception("You are missing longitude or latitude param");

            var jsonDtoResult = LoadJson();

            return MapDtoObjectsToModels(jsonDtoResult);
        }

        private List<EventModel> MapDtoObjectsToModels(dynamic jsonDtoResult)
        {
            var retModels = new List<EventModel>();
            foreach (var dto in jsonDtoResult.results)
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

        private dynamic LoadJson()
        {
            var jsonAsString = _provider.Load(Lon, Lat);
            return JObject.Parse(jsonAsString);
        }

        private DateTime FromUnixTime(long unixTimeMilliSeconds)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTimeMilliSeconds / 1000);
        }
    }
}