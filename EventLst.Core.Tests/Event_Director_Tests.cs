using System;
using NUnit.Framework;
using Moq;

namespace EventLst.Core.Tests
{

    //public class DirectorBuilder
    //{
    //    private Mock<IEventProvider> provider;
    //    private ResultsDirector eventsDirector;
    //    private EventBuilder eventBuilder;
    //}

    [TestFixture]
    public class EventLoaderTests
    {
        private Mock<IEventProvider> provider;
        private ResultsDirector eventsDirector;
        private EventBuilder eventBuilder;

        [SetUp]
        public void SetUp()
        {
            provider = new Mock<IEventProvider>();
            eventBuilder = new EventBuilder(provider.Object,new EventsDtoMapper());
            eventsDirector = new ResultsDirector(eventBuilder);
            provider.Setup(pro => pro.Load(It.IsAny<string>(), It.IsAny<string>())).Returns(@"{'results':[]}");
        }

        [Test]
        public void Will_Throw_Exception_If_Lon_Missing()
        {
            eventBuilder.InputModel.Lat = "1.1";

            Assert.Throws<Exception>(() => eventsDirector.Construct(), "You are missing longitude or latitude param");
        }

        [Test]
        public void Will_Throw_Exception_If_Lat_Missing()
        {
            eventBuilder.InputModel.Lon = "1.1";

            Assert.Throws<Exception>(() => eventsDirector.Construct(), "You are missing longitude or latitude param");
        }

        [Test]
        public void Will_Pass_Longitude_And_Latitude_To_Provider()
        {
            PopulateCoOrds();

            eventsDirector.Construct();

            provider.Verify(spy => spy.Load("1.1", "1.2"), Times.Exactly(1));
        }

        [Test]
        public void Will_Load_4_Models_When_4_Results()
        {
            IEventProvider meetupStubbedProvider = new DiskEventProvider("/Doubles/meetup_open_events_response_stub.json");
            eventBuilder = new EventBuilder(meetupStubbedProvider, new EventsDtoMapper());
            eventsDirector = new ResultsDirector(eventBuilder);

            PopulateCoOrds();

            eventsDirector.Construct();

            Assert.AreEqual(4, eventBuilder.OutputModel.Count);
        }

        [Test]
        public void Will_Map_Dto_Properties_To_Model_Properties()
        {
            IEventProvider meetupStubbedProvider = new DiskEventProvider("/Doubles/meetup_open_events_response_stub.json");
            eventBuilder = new EventBuilder(meetupStubbedProvider, new EventsDtoMapper());
            eventsDirector = new ResultsDirector(eventBuilder);

            PopulateCoOrds();

            eventsDirector.Construct();

            var firstEvent = eventBuilder.OutputModel[0];

            Assert.AreEqual(firstEvent.Name, "The Oxford Spanish Language Exchange Meetup");
            Assert.AreEqual(firstEvent.DateAndTime, new DateTime(2015,03,04,18,30,00));
            Assert.AreEqual(firstEvent.City, "Oxford");
            Assert.AreEqual(firstEvent.HtmlDescription, "<h1>I am the description</h1>");
        }

        private void PopulateCoOrds()
        {
            eventBuilder.InputModel.Lon = "1.1";
            eventBuilder.InputModel.Lat = "1.2";
        }
    }
}
