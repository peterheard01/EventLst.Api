using System.Collections.Generic;

namespace EventLst.Core
{
    public class EventFacade
    {
        private ResultsDirector _resultsDirector { get; set; }

        private EventBuilder _eventBuilder { get; set; }

        public EventFacade(EventBuilder injectedEventBuilder)
        {
            _eventBuilder = injectedEventBuilder;
            _resultsDirector = new ResultsDirector(_eventBuilder);
        }

        public List<EventModel> GetEventsInGeoArea(string lon, string lat)
        {
            _eventBuilder.InputModel.Lon = lon;
            _eventBuilder.InputModel.Lat = lat;
            _resultsDirector.Construct();
            return _eventBuilder.OutputModel;
        }
    }
}
