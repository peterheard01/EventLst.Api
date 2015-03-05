using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLst.Core.Tests
{
    public class DirectorBuilder
    {
        public EventBuilder eventBuilder;

        public DirectorBuilder()
        {
            eventBuilder = new EventBuilder(new DiskEventProvider("/Doubles/meetup_open_events_response_stub.json"), new EventsDtoMapper());
        }

        public ResultsDirector Build()
        {
            return new ResultsDirector(eventBuilder);
        }

        public DirectorBuilder WithProvider(IEventProvider provider)
        {
            eventBuilder = new EventBuilder(provider, new EventsDtoMapper());
            return this;
        }

        public DirectorBuilder WithLongitudeAndLatitude(string lon, string lat)
        {
            eventBuilder.InputModel.Lon = lon;
            eventBuilder.InputModel.Lat = lat;
            return this;
        }

    }
}
