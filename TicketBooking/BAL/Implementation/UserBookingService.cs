using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using TicketBooking.BAL.DataTransfers;
using TicketBooking.BAL.Interface;
using TicketBooking.DAL.Repositories.Implementation;
using TicketBooking.Models;
using TicketBooking.Models.Common;

namespace TicketBooking.BAL.Implementation
{
    public class UserBookingService : IUserBookingService
    {
        public readonly IUserBookingRepository _userBookingRepository;

        public UserBookingService(IUserBookingRepository userBookingRepository)
        {
            _userBookingRepository = userBookingRepository;
        }

        /// <summary>
        /// This method is used to create new Multiplex
        /// </summary>
        /// <param name="UserBookingModel"></param>
        /// <returns></returns>
        public ApplicationResponse<UserBookingModel> Create(UserBookingModel userBookingModel)
        {
            var response = new ApplicationResponse<UserBookingModel>() { State = false, Data = null };
            if (userBookingModel?.Seats <= 0)
            {
                response.Messages = new List<ApplicationError>()
                {
                   new ApplicationError { ErrorMessage = ErrorMessages.MinimumBooking, ErrorCode = (int)HttpStatusCode.BadRequest }
                };
            }
            else if (userBookingModel?.Seats > 5)
            {
                response.Messages = new List<ApplicationError>()
                {
                   new ApplicationError { ErrorMessage = ErrorMessages.MaximumBooking, ErrorCode = (int)HttpStatusCode.BadRequest }
                };
            }
            else if (!ValidateTotalSeatBooking(userBookingModel.Movie.Id, userBookingModel.Seats))
            {
                response.Messages = new List<ApplicationError>()
                {
                   new ApplicationError { ErrorMessage = ErrorMessages.NoSeatsAvailable, ErrorCode = (int)HttpStatusCode.BadRequest }
                };
            }
            else
            {
                var result = this._userBookingRepository.Insert(userBookingModel.TransformToDataModel()).Result;
                response.Data = GetById(result.Id).Data;
                response.State = true;
            }
            return response;
        }

        /// <summary>
        /// Method to delete Multiplex by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationResponse<bool> Delete(int id)
        {
            var response = new ApplicationResponse<bool>() { State = false, Data = false };
            var entity = _userBookingRepository.GetFirstOrDefault(x => x.Id == id).Result;
            if (entity != null)
            {
                response.Data = this._userBookingRepository.Delete(entity);
                response.State = true;
            }
            else
            {
                response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.Invalid, ErrorCode = (int)HttpStatusCode.NotFound } };
            }
            return response;
        }

        /// <summary>
        /// Method to get all Multiplex
        /// </summary>
        /// <returns></returns>
        public ApplicationResponse<IEnumerable<UserBookingModel>> GetAll()
        {
            return new ApplicationResponse<IEnumerable<UserBookingModel>>
            {
                Data = this._userBookingRepository.GetAllInclude(source => source.Include(x => x.Movie).Include(x => x.User)).Result.TransformToViewModelList(),
                State = true
            };
        }

        /// <summary>
        /// Method to get Multiplex by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationResponse<UserBookingModel> GetById(int id)
        {
            var response = new ApplicationResponse<UserBookingModel>() { Data = null, State = false };
            if (id <= 0)
            {
                response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.ID } };
            }
            else
            {
                response.Data = this._userBookingRepository.GetFirstOrDefault(x => x.Id == id, include: source => source.Include(x => x.Movie).Include(x => x.User)).Result?.TransformToViewModel();
                if (response.Data == null)
                {
                    response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.Invalid, ErrorCode = (int)HttpStatusCode.NotFound } };
                }
                else
                    response.State = true;
            }
            return response;
        }

        public bool ValidateTotalSeatBooking(int movieId, int requestedSeats)
        {
            return _userBookingRepository.ValidateTotalSeatBooking(movieId, requestedSeats);
        }
    }
}
