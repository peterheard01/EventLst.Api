using System.Net;
using EventLst.Core;

namespace EventLst.Providers
{
    public class MeetupProvider : IEventProvider
    {
        public dynamic Load(string lon, string lat)
        {
            return
                new WebClient().
                DownloadString(
                "https://api.meetup.com/2/open_events?sign=true&photo-host=public" +
                "&lon=" + lon + 
                "&lat=" + lat + 
                "&page=20&key=243f7745505b6e6d4c1b472860346554");
        }
    }
}
