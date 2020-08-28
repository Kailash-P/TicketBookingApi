using System.Collections.Generic;
using TicketBooking.Models;
using TicketBooking.Models.Common;

namespace TicketBooking.BAL.Interface
{
    public interface IUserService 
    {
        ApplicationResponse<UserModel> Create(UserModel userModel);
        ApplicationResponse<UserModel> Update(UserModel userModel);
        ApplicationResponse<bool> Delete(int id);
        ApplicationResponse<UserModel> GetById(int id);
        ApplicationResponse<IEnumerable<UserModel>> GetAll();
    }
}
