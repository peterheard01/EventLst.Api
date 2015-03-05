using System;
using NUnit.Framework;
using Moq;

namespace EventLst.Core.Tests
{
    [TestFixture]
    public class EventLoaderTests
    {
        [Test]
        public void Will_Throw_Exception_If_Lon_Missing()
        {
            var eventDirector = new DirectorBuilder().WithLongitudeAndLatitude(null, "1.1").Build();

            Assert.Throws<Exception>(() => eventDirector.Construct(), "You are missing longitude or latitude param");
        }

        [Test]
        public void Will_Throw_Exception_If_Lat_Missing()
        {
            var eventDirector = new DirectorBuilder().WithLongitudeAndLatitude("1.1", null).Build();

            Assert.Throws<Exception>(() => eventDirector.Construct(), "You are missing longitude or latitude param");
        }

        [Test]
        public void Will_Pass_Longitude_And_Latitude_To_Provider()
        {
            var provider = new Mock<IEventProvider>();
            provider.Setup(pro => pro.Load(It.IsAny<string>(), It.IsAny<string>())).Returns(@"{'results':[]}");

            var eventDirector = new DirectorBuilder().WithProvider(provider.Object).WithLongitudeAndLatitude("1.1", "1.2").Build();

            eventDirector.Construct();

            provider.Verify(spy => spy.Load("1.1", "1.2"), Times.Exactly(1));
        }

        [Test]
        public void Will_Load_4_Models_When_4_Results()
        {
            var builder = new DirectorBuilder();
            var eventDirector = builder.WithLongitudeAndLatitude("1.1", "1.2").Build();

            eventDirector.Construct();

            Assert.AreEqual(4, builder.eventBuilder.OutputModel.Count);
        }

        [Test]
        public void Will_Map_Dto_Properties_To_Model_Properties()
        {
            var builder = new DirectorBuilder();
            var eventDirector = builder.WithLongitudeAndLatitude("1.1", "1.2").Build();

            eventDirector.Construct();

            var firstEvent = builder.eventBuilder.OutputModel[0];

            Assert.AreEqual(firstEvent.Name, "The Oxford Spanish Language Exchange Meetup");
            Assert.AreEqual(firstEvent.DateAndTime, new DateTime(2015, 03, 04, 18, 30, 00));
            Assert.AreEqual(firstEvent.City, "Oxford");
            Assert.AreEqual(firstEvent.HtmlDescription, "<h1>I am the description</h1>");
        }

    }
}
