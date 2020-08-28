namespace TicketBooking.Models
{
    public class MultiplexModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CityModel City { get; set; }
        public int TotalSeats { get; set; }
    }
}
