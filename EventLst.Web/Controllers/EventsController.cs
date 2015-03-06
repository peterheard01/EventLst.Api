using System.Collections.Generic;
using System.Web.Http;
using EventLst.Core;
using EventLst.Providers;

namespace EventLst.Controllers
{
    public class EventsController : ApiController
    {
        public EventFacade _facade { get; set; }

        public EventsController()
        {
            _facade = new EventFacade(new EventBuilder(new MeetupProvider(),new EventsDtoMapper()));
        }

        public List<EventModel> Get(string lon, string lat)
        {
            return _facade.GetEventsInGeoArea(lon,lat);
        }
    }
}
