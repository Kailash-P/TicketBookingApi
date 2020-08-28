using System.Collections.Generic;
using TicketBooking.Models;
using TicketBooking.Models.Common;

namespace TicketBooking.BAL.Interface
{
    public interface IMovieService
    {
        ApplicationResponse<MovieModel> Create(MovieModel movieModel);
        ApplicationResponse<MovieModel> Update(MovieModel movieModel);
        ApplicationResponse<bool> Delete(int id);
        ApplicationResponse<MovieModel> GetById(int id);
        ApplicationResponse<IEnumerable<MovieModel>> GetAll();
    }
}
