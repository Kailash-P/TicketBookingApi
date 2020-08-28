using System.Collections;
using System.Collections.Generic;
using TicketBooking.DAL.Repositories.Interface;

namespace TicketBooking.DAL.Models
{
    public class Multiplex : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public int TotalSeats { get; set; }

        public virtual City City { get;set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
