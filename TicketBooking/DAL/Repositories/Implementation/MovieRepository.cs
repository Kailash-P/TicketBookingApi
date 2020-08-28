using TicketBooking.DAL.Models;
using TicketBooking.DAL.Repositories.Interface;

namespace TicketBooking.DAL.Repositories.Implementation
{
    public class MovieRepository : ApplicationRepository<Movie>, IMovieRepository
    {
        public MovieRepository(TicketContext dbContext) : base(dbContext)
        {

        }
    }

    public interface IMovieRepository : IApplicationRepository<Movie>
    {

    }
}
