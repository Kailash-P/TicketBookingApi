using System.Collections.Generic;
using TicketBooking.DAL.Repositories.Interface;

namespace TicketBooking.DAL.Models
{
    public class City : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Multiplex> Multiplex { get; set; }
    }
}
