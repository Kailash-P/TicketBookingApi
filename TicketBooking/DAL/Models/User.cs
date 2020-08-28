using System.Collections.Generic;
using TicketBooking.DAL.Repositories.Interface;

namespace TicketBooking.DAL.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<UserBooking> Bookings { get; set; }
    }
}
