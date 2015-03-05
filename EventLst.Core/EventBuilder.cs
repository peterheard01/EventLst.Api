using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EventLst.Core
{
    public class EventBuilder : IBuilder<LocationSearchModel,List<EventModel>>
    {
        public LocationSearchModel InputModel { get; set; }

        public List<EventModel> OutputModel { get; set; }

        private IEventProvider _provider;

        private IDtoMapper<EventModel> _mapper;

        private dynamic _json;

        public EventBuilder(IEventProvider injectedProvider, IDtoMapper<EventModel> injectedMapper)
        {
            InputModel = new LocationSearchModel();
            _provider = injectedProvider;
            _mapper = injectedMapper;
        }

        public void Load()
        {
            if (InputModel.Lon == null || InputModel.Lat == null) throw new Exception("You are missing longitude or latitude param");

            var jsonString = _provider.Load(InputModel.Lon, InputModel.Lat);

            _json = JObject.Parse(jsonString);   

        }

        public void Map()
        {
            OutputModel = _mapper.Map(_json);
        }
    }
}
