﻿using System;
using System.Configuration;
using System.Net;
using System.Text;
using EventLst.Core;
using EventLst.Core.Application;

namespace EventLst.Providers
{
    public class MeetupProvider : IEventProvider
    {
        public Config Config { get; set; }

        public MeetupProvider()
        {
            Config = new Config();
        }

        public dynamic Load(string lon, string lat)
        {

            string key = Config.Get("MeetupApiKey");

            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;

            return webClient.
                DownloadString(
                "https://api.meetup.com/2/open_events?sign=true&photo-host=public" +
                "&lon=" + lon + 
                "&lat=" + lat +
                "&page=50&key=" + key);
        }
    }
}
