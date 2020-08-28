using System.Linq;
using TicketBooking.DAL.Models;
using TicketBooking.DAL.Repositories.Interface;

namespace TicketBooking.DAL.Repositories.Implementation
{
    public class UserBookingRepository : ApplicationRepository<UserBooking>, IUserBookingRepository
    {
        private TicketContext _dbContext { get; set; }
        public UserBookingRepository(TicketContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ValidateTotalSeatBooking(int movieId, int requestedSeats)
        {
            var result = true;
            if(movieId > 0)
            {
                var bookedSeats = this._dbContext.UserBooking.Where(x => x.MovieId == movieId).GroupBy(g => g.Seats).Select(s => s.Key).FirstOrDefault();
                if (bookedSeats > 0 && (bookedSeats + requestedSeats > 100))
                    result = false;                
            }
            return result;
        }
    }

    public interface IUserBookingRepository : IApplicationRepository<UserBooking>
    {
        bool ValidateTotalSeatBooking(int movieId, int requestedSeats);
    }
}
