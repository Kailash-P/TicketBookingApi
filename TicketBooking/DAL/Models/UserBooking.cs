using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBooking.DAL.Repositories.Interface;

namespace TicketBooking.DAL.Models
{
    public class UserBooking : IEntity
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public int Seats { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}
