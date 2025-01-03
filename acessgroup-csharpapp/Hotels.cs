using System;
namespace acessgroup_csharpapp
{
    public class HotelsList
    {
        /// 
        /// Hotels Class
        /// 

        public required List<Hotel> data { get; set; }
        public void List()
        {
            Console.WriteLine("\nLIST OF HOTELS");
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("Id \tName \t\t\tRoom Types");
            Console.WriteLine("-----------------------------------------------------------------");
            foreach (var hotel in this.data)
            {
                string rooms = "";
                foreach (var room in hotel.roomTypes)
                {
                    rooms = room.code + " " + rooms;
                }
                Console.WriteLine("{0}\t{1}\t{2}", hotel.id, hotel.name, rooms);
            }
            Console.WriteLine("-----------------------------------------------------------------\n");
        }

        public Hotel ChooseHotel()
        {
            bool hotel_selected = false;
            int hotel_id = 0;
            this.List();
            while (!hotel_selected)
            {
                Console.Write("Input Hotel Id: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int value))
                {
                    if (this.HotelExists(value))
                    {
                        hotel_id = value;
                        hotel_selected = true;
                    }
                    else
                    {
                        Console.Write("There is no hotel with this id number. ");
                    }
                }
                else
                {
                    Console.Write("The input was not a number. ");
                }
            }
            return this.data[hotel_id - 1];
        }


        private bool HotelExists(int id)
        {
            var myItem = this.data.Find(item => item.id == id);
            if (myItem != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Hotel
    {
        public required int id { get; set; }
        public required string name { get; set; }
        public required List<RoomTypes>? roomTypes { get; set; }
        public required List<Rooms>? rooms { get; set; }
        public void Roomtypes()
        {
            Console.WriteLine("\nLIST OF ROOMS IN HOTEL");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Code \tDescription \tAmenities \t\t\tFeatures");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------");
            foreach (var room in this.roomTypes)
            {
                var amenities = "";
                var features = "";
                foreach (var amenitie in room.amenities)
                {
                    amenities += amenitie + " ";
                }
                foreach (var feature in room.features)
                {
                    features += feature + " ";
                }

                int[] lengtharray = [room.description.Length, amenities.Length];
                string[] tabs = ["\t", "\t"];
                if (lengtharray[0] < 8)
                {
                    tabs[0] = tabs[0] + "\t";
                }
                if (lengtharray[1] < 8)
                {
                    tabs[1] = tabs[1] + "\t";
                }
                if (lengtharray[1] < 16)
                {
                    tabs[1] = tabs[1] + "\t";
                }
                if (lengtharray[1] < 24)
                {
                    tabs[1] = tabs[1] + "\t";
                }
                Console.WriteLine("{0}\t{1}" + tabs[0] + "{2}" + tabs[1] + "{3}", room.code, room.description, amenities, features);
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------------------\n");
        }
        public int CountRooms(string roomType)
        {
            var count = 0;
            foreach (var item in this.rooms)
            {
                if (item.roomType == roomType)
                {
                    count++;
                }
            }
            return count;
        }

        public RoomTypes Chooseroomtype()
        {
            RoomTypes roomType = null;
            bool roomtype_selected = false;
            this.Roomtypes();
            while (!roomtype_selected)
            {
                Console.Write("Input Room Code: ");
                var input = Console.ReadLine();
                roomType = this.roomTypes.Find(item => item.code == input);
                if (roomType != null)
                {
                    roomtype_selected = true;
                }
                else
                {
                    Console.Write("There is no Room with this code. ");
                }
            }
            return roomType;
        }

    }

    public class RoomTypes
    {
        public required string code { get; set; }
        public required string description { get; set; }
        public required string[]? amenities { get; set; }
        public required string[]? features { get; set; }
    }
    public class Rooms
    {

        public required string roomType { get; set; }
        public required int roomId { get; set; }
    }

}