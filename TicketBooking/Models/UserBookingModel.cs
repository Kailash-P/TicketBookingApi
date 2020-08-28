namespace TicketBooking.Models
{
    public class UserBookingModel
    {
        public int Id { get; set; }
        public MovieModel Movie { get; set; }
        public UserModel User { get; set; }
        public int Seats { get; set; }
    }
}
