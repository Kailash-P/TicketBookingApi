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
    public class MovieService : IMovieService
    {
        public readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// This method is used to create new Movie
        /// </summary>
        /// <param name="MovieModel"></param>
        /// <returns></returns>
        public ApplicationResponse<MovieModel> Create(MovieModel movieModel)
        {
            var response = new ApplicationResponse<MovieModel>() { State = false, Data = null };
            if (string.IsNullOrEmpty(movieModel?.Name))
            {
                response.Messages = new List<ApplicationError>()
                {
                        new ApplicationError { ErrorMessage = ErrorMessages.MovieName, ErrorCode = (int)HttpStatusCode.BadRequest }
                };
            }
            else
            {
                var result = this._movieRepository.Insert(movieModel.TransformToDataModel()).Result;
                response.Data = GetById(result.Id).Data;
                response.State = true;
            }
            return response;
        }

        /// <summary>
        /// Method to delete Movie by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationResponse<bool> Delete(int id)
        {
            var response = new ApplicationResponse<bool>() { State = false, Data = false };
            var entity = _movieRepository.GetFirstOrDefault(x => x.Id == id).Result;
            if (entity != null)
            {
                response.Data = this._movieRepository.Delete(entity);
                response.State = true;
            }
            else
            {
                response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.Invalid, ErrorCode = (int)HttpStatusCode.NotFound } };
            }
            return response;
        }

        /// <summary>
        /// This method is used to update existing Movie
        /// </summary>
        /// <param name="MovieModel"></param>
        /// <returns></returns>
        public ApplicationResponse<MovieModel> Update(MovieModel movieModel)
        {
            var response = new ApplicationResponse<MovieModel>() { State = false, Data = null };
            var entity = _movieRepository.GetById(movieModel.Id).Result;
            if (entity == null)
            {
                response.Messages = new List<ApplicationError>() {
                        new ApplicationError { ErrorMessage = ErrorMessages.Invalid, ErrorCode = (int)HttpStatusCode.NotFound }
                    };
            }
            else if (_movieRepository.GetFirstOrDefault(_ => _.Id != movieModel.Id && _.Name.Trim().ToLower() == movieModel.Name.Trim().ToLower()).Result != null)
            {
                response.Messages = new List<ApplicationError>() {
                    new ApplicationError { ErrorMessage = ErrorMessages.AlreadyExists, ErrorCode = (int)HttpStatusCode.BadRequest }
                };
            }
            else
            {
                response.Data = this._movieRepository.Update(movieModel.TransformToDataModel()).Result.TransformToViewModel();
                response.State = true;
            }
            return response;
        }


        /// <summary>
        /// Method to get all Movie
        /// </summary>
        /// <returns></returns>
        public ApplicationResponse<IEnumerable<MovieModel>> GetAll()
        {
            return new ApplicationResponse<IEnumerable<MovieModel>>
            {
                Data = this._movieRepository.GetAllInclude(source => source.Include(x => x.Multiplex).ThenInclude(x => x.City)).Result.TransformToViewModelList(),
                State = true
            };
        }

        /// <summary>
        /// Method to get Movie by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationResponse<MovieModel> GetById(int id)
        {
            var response = new ApplicationResponse<MovieModel>() { Data = null, State = false };
            if (id <= 0)
            {
                response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.ID, ErrorCode = (int)HttpStatusCode.NotFound } };
            }
            else
            {
                response.Data = this._movieRepository.GetFirstOrDefault(x => x.Id == id, include: source => source.Include(x => x.Multiplex).ThenInclude(x => x.City)).Result?.TransformToViewModel();
                if (response.Data == null)
                {
                    response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.Invalid, ErrorCode = (int)HttpStatusCode.BadRequest } };
                }
                else
                    response.State = true;
            }
            return response;
        }
    }
}
