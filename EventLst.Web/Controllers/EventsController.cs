using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventLst.Core;

namespace EventLst.Controllers
{
    public class EventsController : ApiController
    {
        public EventFacade _facade { get; set; }

        public EventsController(EventFacade injectedFacade)
        {
            _facade = injectedFacade;
        }

        public List<EventModel> Get(string lon, string lat)
        {
            return _facade.GetEventsInGeoArea(lon,lat);
        }
    }
}
