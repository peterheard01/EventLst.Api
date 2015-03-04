using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EventLst.Core.Tests
{
    public class DiskEventProvider : IEventProvider
    {
        private string _path;

        public DiskEventProvider(string pathArg)
        {
            _path = pathArg;
        }

        public dynamic Load(string lon, string lat)
        {
            return File.ReadAllText(@"Doubles\\meetup_open_events_response_stub.json");
        }
    }
}
