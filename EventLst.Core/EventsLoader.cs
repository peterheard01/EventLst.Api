using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

            _provider.Load(Lon,Lat);

            return null;
        }
    }
}