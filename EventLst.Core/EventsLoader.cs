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

        public DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public List<EventModel> Load()
        {
            if (Lon == null || Lat == null) throw new Exception("You are missing longitude or latitude param");

            var retModels = new List<EventModel>();

            var jsonAsString = _provider.Load(Lon,Lat);

            dynamic jsonAsDynamicObject = JObject.Parse(jsonAsString);

            foreach (var dto in jsonAsDynamicObject.results)
            {
                retModels.Add(new EventModel());

                //DateTime.

                //retModels.Add(new EventModel()
                //{
                //    Name = dto.name,
                //    DateAndTime = dto.created
                //    City = dto.venue.city

                //});
            }

            return retModels;
        }
    }
}