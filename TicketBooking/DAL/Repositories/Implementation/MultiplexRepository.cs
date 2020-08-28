using TicketBooking.DAL.Models;
using TicketBooking.DAL.Repositories.Interface;

namespace TicketBooking.DAL.Repositories.Implementation
{
    public class MultiplexRepository : ApplicationRepository<Multiplex>, IMultiplexRepository
    {
        public MultiplexRepository(TicketContext dbContext) : base(dbContext)
        {

        }
    }

    public interface IMultiplexRepository : IApplicationRepository<Multiplex>
    {

    }
}
