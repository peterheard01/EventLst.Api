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
        public void Will_Load_Four_Models_When_4_Results()
        {
            IEventProvider meetupStubbedProvider = new DiskEventProvider("/Doubles/meetup_open_events_response_stub.json");
            eventsLoader = new EventsLoader(meetupStubbedProvider);

            PopulateCoOrds();

            var events = eventsLoader.Load();

            Assert.AreEqual(4, events.Count);
        }

        private void PopulateCoOrds()
        {
            eventsLoader.Lon = "1.1";
            eventsLoader.Lat = "1.2";
        }
    }
}
