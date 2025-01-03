using System.Text.Json;
using System.Text.Json.Nodes;

namespace acessgroup_csharpapp
{

    /// 
    /// Main Class
    /// 

    public class Program
    {
        static void CommandsList()
        {
            Console.WriteLine("\nBelow is list of commands where 'command' is the name of the command that should be entered. 'number' is an alternative number that we can enter instead of the command name. And 'description' is a description of what the command does.");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("command\t\tnumber\tdescription");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("check\t\t1\tCheck room avaibility");
            Console.WriteLine("hotelslist\t2\tList of hotels");
            Console.WriteLine("hotelroomtp\t3\tList of rooms types in hotel");
            Console.WriteLine("reslist\t\t4\tList of all Reservations");
            Console.WriteLine("help\t\t5\tShow list of commands");
            Console.WriteLine("\t\t0\tLeave blank or input 0 to exit application");
            Console.WriteLine("------------------------------------\n");
        }

        public static DateTime selectDate(int mode, DateTime? afterDate)
        {
            DateTime outDate = new DateTime(2015, 12, 08);
            string dateText = "Start";
            if (mode == 2)
            {
                dateText = "End";
            }
            Console.WriteLine("Plese input "+ dateText+" Date (format YYYYMMDD) including zeros:");
            bool date_selected = false;

            while (!date_selected)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out int value) && input.Length == 8)
                {
                    var date = input.Substring(0, 4) + "-" + input.Substring(4, 2) + "-" + input.Substring(6, 2) + " 00:00:00,001";
                    if (DateTime.TryParse(date, out outDate))
                    {
                        outDate = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                        if (afterDate != null)
                        {
                            if (outDate >= afterDate)
                            {
                                date_selected = true;
                            }
                            else
                            {
                                Console.Write("End date is before start date. ");
                            }
                        } else
                        {
                            date_selected = true;
                        }
                    }
                    else
                    {
                        Console.Write("Invalid Date. ");
                    }

                }
                else
                {
                    Console.Write("The input was not a number or less than 8 chars. ");
                }
            }
            
            return outDate;

        }

        /// 
        /// Main Program
        /// 

        public static void Main()
        {
            // Read Hotels JSON
            string hotelsFileName = "hotels.json";
            string hotelsJsonString = File.ReadAllText(hotelsFileName);
            var hotelsJsonObject = JsonNode.Parse(hotelsJsonString);
            HotelsList hotels = JsonSerializer.Deserialize<HotelsList>(hotelsJsonString)!;

            // Read Bookings JSON
            string bookingsFileName = "bookings.json";
            string bookingsJsonString = File.ReadAllText(bookingsFileName);
            var bookingsJsonObject = JsonNode.Parse(bookingsJsonString);
            ReservationsList reservations = JsonSerializer.Deserialize<ReservationsList>(bookingsJsonString)!;

            // Ask the user to choose an option.
            CommandsList();

            var lopp = true;
            while (lopp)
            {
                // Use a switch statement to do the math.
                switch (Console.ReadLine())
                {
                    case "1":
                    case "check":
                        Console.Clear();
                        Console.WriteLine("\n------------------------------------");
                        Console.WriteLine("--------CHECKING AVAILABILITY-------");
                        Console.WriteLine("------------------------------------");
                        Hotel selected_hotel = hotels.ChooseHotel();
                        RoomTypes room = selected_hotel.Chooseroomtype();
                        DateTime start = selectDate(1,null);
                        DateTime end = selectDate(2, start);
                        var occupancy = reservations.Occupancy(start,end, selected_hotel.id, room.code);
                        var countRooms = selected_hotel.CountRooms(room.code);
                        var availability = countRooms - occupancy;
                        Console.Clear();
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("Start Date: " + start.ToString());
                        Console.WriteLine("End Date: " + end.ToString());
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("Hotel: "+ selected_hotel.name);
                        Console.WriteLine("Room Type: " + room.code);
                        Console.WriteLine("All rooms of that type: "+ countRooms);
                        Console.WriteLine("Occupied rooms of that type: " + occupancy);
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("Available Rooms: " + availability);
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("\n");
                        Console.Write("Choode mode or type help for command list:");
                        break;
                    case "2":
                    case "hotelslist":
                        Console.Clear();
                        hotels.List();
                        Console.Write("Choode mode or type help for command list:");
                        break;
                    case "3":
                    case "hotelroomstypes":
                        Console.Clear();
                        Console.WriteLine("\n");
                        var properid = false;
                        while (!properid)
                        {
                            hotels.List();
                            Console.Write("Input Hotel Id: ");
                            var input2 = Console.ReadLine();
                            if (int.TryParse(input2, out int value2))
                            {
                                var myItem = hotels.data.Find(item => item.id == value2);
                                if (myItem != null)
                                {
                                    myItem.Roomtypes();
                                    properid = true;
                                }
                                else
                                {
                                    Console.Write("\nThere is no hotel with this id number. ");
                                }
                            }
                            else
                            {
                                Console.Write("\nThe input was not a number. ");
                            }
                        }
                        Console.Write("Choode mode or type help for command list:");
                        break;
                    case "4":
                    case "reslist":
                        Console.Clear();
                        reservations.List();
                        Console.Write("Choode mode or type help for command list:");
                        break;
                    case "5":
                    case "help":
                        Console.Clear();
                        CommandsList();
                        Console.Write("Choode mode or type help for command list:");
                        break;
                    case "0":
                    case "":
                        lopp = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Unrecognized command. Input help for command list. Leave blank to exit application");
                        break;
                }
            }

            // Wait for the user to respond before closing.
            Console.Write("Press any key to close this app...");
            Console.ReadKey();
        }
    }
}
