using TicketBooking.DAL.Models;
using TicketBooking.DAL.Repositories.Interface;

namespace TicketBooking.DAL.Repositories.Implementation
{
    public class UserRepository : ApplicationRepository<User>, IUserRepository
    {
        public UserRepository(TicketContext dbContext) : base(dbContext)
        {

        }
    }

    public interface IUserRepository : IApplicationRepository<User>
    {

    }
}
