using System;
using NUnit.Framework;
using Moq;

namespace EventLst.Core.Tests
{
    [TestFixture]
    public class EventFacadeTests
    {
        public EventFacade _eventFacadeWithDiskStub;

        [SetUp]
        public void Setup()
        {
            _eventFacadeWithDiskStub = new EventFacade(new EventBuilder(new DiskEventProvider(@"Doubles\\meetup_open_events_response_stub.json"), new EventsDtoMapper()));
        }



        [Test]
        public void EventFacade_Will_Throw_Exception_If_Lon_Missing()
        {
            Assert.Throws<Exception>(() => _eventFacadeWithDiskStub.GetEventsInGeoArea(null, "1.2"), "You are missing longitude or latitude param");
        }

        [Test]
        public void EventFacade_Will_Throw_Exception_If_Lat_Missing()
        {
            Assert.Throws<Exception>(() => _eventFacadeWithDiskStub.GetEventsInGeoArea("1.1", null), "You are missing longitude or latitude param");
        }

        [Test]
        public void EventFacade_Will_Pass_Longitude_And_Latitude_To_Provider()
        {
            var provider = new Mock<IEventProvider>();
            provider.Setup(pro => pro.Load(It.IsAny<string>(), It.IsAny<string>())).Returns(@"{'results':[]}");

            var facade = new EventFacade(new EventBuilder(provider.Object, new EventsDtoMapper()));

            facade.GetEventsInGeoArea("1.1", "1.2");

            provider.Verify(spy => spy.Load("1.1", "1.2"), Times.Exactly(1));
        }

        [Test]
        public void EventFacade_Will_Load_4_Models_When_4_Results()
        {
            var events = _eventFacadeWithDiskStub.GetEventsInGeoArea("1.1", "1.2");

            Assert.AreEqual(4, events.Count);
        }

        [Test]
        public void EventFacade_Will_Map_Dto_Properties_To_Model_Properties()
        {
            var events = _eventFacadeWithDiskStub.GetEventsInGeoArea("1.1", "1.2");

            var firstEvent = events[0];

            Assert.AreEqual(firstEvent.Name, "The Oxford Spanish Language Exchange Meetup");
            Assert.AreEqual(firstEvent.DateAndTime, new DateTime(2015, 03, 04, 18, 30, 00));
            Assert.AreEqual(firstEvent.City, "Oxford");
            Assert.AreEqual(firstEvent.HtmlDescription, "<h1>I am the description</h1>");
        }


        [Test]
        public void Event_Facade_Will_Not_Map_Venue_Details_If_Missing()
        {
            var facade = new EventFacade(new EventBuilder(new DiskEventProvider(@"Doubles\\meetup_open_events_response_stub_missing_venue.json"), new EventsDtoMapper()));

            var events = facade.GetEventsInGeoArea("1.1", "1.2");

            var firstEvent = events[0];

            Assert.AreEqual(firstEvent.Name, "Rendez-vous avec Philippe Geluk!");
            Assert.AreEqual(firstEvent.DateAndTime, new DateTime(2015, 03, 06, 17, 15, 00));
            Assert.AreEqual(firstEvent.City, null);
            Assert.AreEqual(firstEvent.HtmlDescription, "<h1>I am the description</h1>");
        }

    }
}
