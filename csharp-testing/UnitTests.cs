using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using acessgroup_csharpapp;
using System.Xml.Linq;

namespace csharp_testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCountRooms()
        {
            RoomTypes dummyroomtype = new RoomTypes { code = "SGL", description = "Single Room", amenities = ["WiFi", "TV"], features = ["Non-smoking"]};
            Rooms dummyroom1 = new Rooms { roomType = "SGL", roomId = 101 };
            Rooms dummyroom2 = new Rooms { roomType = "SGL", roomId = 101 };
            var dummyroomtypes = new List<RoomTypes> { dummyroomtype };
            var dummyrooms = new List<Rooms> { dummyroom1,dummyroom2 };

            Hotel dummyhotel = new Hotel { id = 1, name = "Dummy Hotel", roomTypes = dummyroomtypes, rooms = dummyrooms };
            var dummyhotels = new List<Hotel> { dummyhotel };
            HotelsList hotels = new HotelsList { data = dummyhotels };

            var countedRooms = dummyhotel.CountRooms("SGL");

            Assert.AreEqual(countedRooms, 2);
        }

    }
}
