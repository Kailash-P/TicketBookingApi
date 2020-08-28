using System.Collections.Generic;
using TicketBooking.Models;
using TicketBooking.Models.Common;

namespace TicketBooking.BAL.Interface
{
    public interface IMultiplexService
    {
        ApplicationResponse<MultiplexModel> Create(MultiplexModel multiplexModel);
        ApplicationResponse<MultiplexModel> Update(MultiplexModel multiplexModel);
        ApplicationResponse<bool> Delete(int id);
        ApplicationResponse<MultiplexModel> GetById(int id);
        ApplicationResponse<IEnumerable<MultiplexModel>> GetAll();
    }
}
