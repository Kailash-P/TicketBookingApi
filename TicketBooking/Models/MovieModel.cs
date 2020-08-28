namespace TicketBooking.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public MultiplexModel Multiplex { get; set; }
    }
}
