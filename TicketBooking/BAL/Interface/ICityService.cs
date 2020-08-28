using System.Collections.Generic;
using TicketBooking.Models;
using TicketBooking.Models.Common;

namespace TicketBooking.BAL.Interface
{
    public interface ICityService
    {
        ApplicationResponse<CityModel> Create(CityModel cityModel);
        ApplicationResponse<CityModel> Update(CityModel cityModel);
        ApplicationResponse<bool> Delete(int id);
        ApplicationResponse<CityModel> GetById(int id);
        ApplicationResponse<IEnumerable<CityModel>> GetAll();
    }
}
