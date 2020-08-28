using System.Collections.Generic;
using TicketBooking.Models;
using TicketBooking.Models.Common;

namespace TicketBooking.BAL.Interface
{
    public interface IUserBookingService
    {
        ApplicationResponse<UserBookingModel> Create(UserBookingModel userBookingModel);
        ApplicationResponse<bool> Delete(int id);
        ApplicationResponse<UserBookingModel> GetById(int id);
        ApplicationResponse<IEnumerable<UserBookingModel>> GetAll();
    }
}
