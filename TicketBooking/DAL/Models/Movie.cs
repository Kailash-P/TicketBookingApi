using System.Collections;
using System.Collections.Generic;
using TicketBooking.DAL.Repositories.Interface;

namespace TicketBooking.DAL.Models
{
    public class Movie : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public int LanguageId { get; set; }
        public int MultiplexId { get; set; }

        public virtual Multiplex Multiplex { get; set; }

        public virtual ICollection<UserBooking> Bookings { get; set; }
    }
}
