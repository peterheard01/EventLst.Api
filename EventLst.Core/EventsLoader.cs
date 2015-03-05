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
        private EventsDtoMapper _mapper;

        public EventsLoader(IEventProvider injectedProvider)
        {
            _provider = injectedProvider;
            _mapper = new EventsDtoMapper();
        }

        public List<EventModel> Load()
        {
            if (Lon == null || Lat == null) throw new Exception("You are missing longitude or latitude param");

            var json = LoadJson();

            return _mapper.Map(json);
        }

        private dynamic LoadJson()
        {
            var jsonAsString = _provider.Load(Lon, Lat);
            return JObject.Parse(jsonAsString);
        }


    }
}