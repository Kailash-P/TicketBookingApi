using TicketBooking.DAL.Models;
using TicketBooking.DAL.Repositories.Interface;

namespace TicketBooking.DAL.Repositories.Implementation
{
    public class CityRepository : ApplicationRepository<City>, ICityRepository
    {
        public CityRepository(TicketContext dbContext) : base(dbContext)
        {

        }
    }

    public interface ICityRepository : IApplicationRepository<City>
    {

    }
}
