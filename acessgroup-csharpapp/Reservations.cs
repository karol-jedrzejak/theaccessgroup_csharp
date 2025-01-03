namespace acessgroup_csharpapp
{
    /// 
    /// Reservations Class
    /// 

    public class ReservationsList
    {
        public required List<Reservation> data { get; set; }
        public void List()
        {
            Console.WriteLine("\nLIST OF RESERVATIONS");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Id \tHotel Id \tArrival \tDeparture \tRoom Type");
            Console.WriteLine("--------------------------------------------------------------------");
            foreach (var reservation in this.data)
            {
                Console.WriteLine("{0}\t{1}\t\t{2}\t{3}\t{4}", reservation.reservationId, reservation.hotelId, reservation.arrival, reservation.departure, reservation.roomType);
            }
            Console.WriteLine("--------------------------------------------------------------------\n");
        }
        public int Occupancy(DateTime start, DateTime end, int hotelId, string roomType)
        {
            var count = 0;
            foreach (var item in this.data)
            {
                var arrivalDate = DateTime.ParseExact(item.arrival + " 00:00:00,001", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                var departureDate = DateTime.ParseExact(item.departure + " 00:00:00,001", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                if ((item.hotelId == hotelId) && (item.roomType == roomType))
                {
                    if (start <= departureDate && end >= arrivalDate)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }

    public class Reservation
    {
        public required int reservationId { get; set; }
        public required int hotelId { get; set; }
        public required string arrival { get; set; }
        public required string departure { get; set; }
        public required string roomType { get; set; }
    }

}