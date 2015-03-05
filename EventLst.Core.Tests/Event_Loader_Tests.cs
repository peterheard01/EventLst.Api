using System;
using NUnit.Framework;
using Moq;

namespace EventLst.Core.Tests
{
    [TestFixture]
    public class EventLoaderTests
    {
        private Mock<IEventProvider> provider;
        private EventsLoader eventsLoader;

        [SetUp]
        public void SetUp()
        {
            provider = new Mock<IEventProvider>();
            provider.Setup(pro => pro.Load(It.IsAny<string>(), It.IsAny<string>())).Returns(@"{'results':[]}");
            eventsLoader = new EventsLoader(provider.Object);
        }

        [Test]
        public void Will_Throw_Exception_If_Lon_Missing()
        {
            eventsLoader.Lat = "1.1";

            Assert.Throws<Exception>(() => eventsLoader.Load(), "You are missing longitude or latitude param");
        }

        [Test]
        public void Will_Throw_Exception_If_Lat_Missing()
        {
            eventsLoader.Lon = "1.1";

            Assert.Throws<Exception>(() => eventsLoader.Load(), "You are missing longitude or latitude param");
        }

        [Test]
        public void Will_Pass_Longitude_And_Latitude_To_Provider()
        {
            PopulateCoOrds();

            eventsLoader.Load();

            provider.Verify(spy => spy.Load("1.1", "1.2"), Times.Exactly(1));
        }

        [Test]
        public void Will_Load_4_Models_When_4_Results()
        {
            IEventProvider meetupStubbedProvider = new DiskEventProvider("/Doubles/meetup_open_events_response_stub.json");
            eventsLoader = new EventsLoader(meetupStubbedProvider);

            PopulateCoOrds();

            var events = eventsLoader.Load();

            Assert.AreEqual(4, events.Count);
        }

        [Test]
        public void Will_Map_Dto_Properties_To_Model_Properties()
        {
            IEventProvider meetupStubbedProvider = new DiskEventProvider("/Doubles/meetup_open_events_response_stub.json");
            eventsLoader = new EventsLoader(meetupStubbedProvider);

            PopulateCoOrds();

            var firstEvent = eventsLoader.Load()[0];

            Assert.AreEqual(firstEvent.Name, "The Oxford Spanish Language Exchange Meetup");
            Assert.AreEqual(firstEvent.DateAndTime, new DateTime(2015,03,04,18,30,00));
            Assert.AreEqual(firstEvent.City, "Oxford");
            Assert.AreEqual(firstEvent.HtmlDescription, "<h1>I am the description</h1>");
        }

        private void PopulateCoOrds()
        {
            eventsLoader.Lon = "1.1";
            eventsLoader.Lat = "1.2";
        }
    }
}
