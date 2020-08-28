using System.Collections.Generic;
using System.Net;
using TicketBooking.BAL.DataTransfers;
using TicketBooking.BAL.Interface;
using TicketBooking.DAL.Repositories.Implementation;
using TicketBooking.Models;
using TicketBooking.Models.Common;

namespace TicketBooking.BAL.Implementation
{
    public class CityService : ICityService
    {
        public readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        /// <summary>
        /// This method is used to create new City
        /// </summary>
        /// <param name="CityModel"></param>
        /// <returns></returns>
        public ApplicationResponse<CityModel> Create(CityModel cityModel)
        {
            var response = new ApplicationResponse<CityModel>() { State = false, Data = null };
            if (string.IsNullOrEmpty(cityModel?.Name))
            {
                response.Messages = new List<ApplicationError>()
                {
                        new ApplicationError { ErrorMessage = ErrorMessages.Name, ErrorCode = (int)HttpStatusCode.BadRequest }
                };
            }
            else
            {
                response.Data = this._cityRepository.Insert(cityModel.TransformToDataModel()).Result.TransformToViewModel();
                response.State = true;
            }
            return response;
        }

        /// <summary>
        /// Method to delete City by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationResponse<bool> Delete(int id)
        {
            var response = new ApplicationResponse<bool>() { State = false, Data = false };
            var entity = _cityRepository.GetFirstOrDefault(x => x.Id == id).Result;
            if (entity != null)
            {
                response.Data = this._cityRepository.Delete(entity);
                response.State = true;
            }
            else
            {
                response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.Invalid, ErrorCode = (int)HttpStatusCode.NotFound } };
            }
            return response;
        }

        /// <summary>
        /// This method is used to update existing City
        /// </summary>
        /// <param name="CityModel"></param>
        /// <returns></returns>
        public ApplicationResponse<CityModel> Update(CityModel cityModel)
        {
            var response = new ApplicationResponse<CityModel>() { State = false, Data = null };
            var entity = _cityRepository.GetById(cityModel.Id).Result;
            if (entity == null)
            {
                response.Messages = new List<ApplicationError>() {
                        new ApplicationError { ErrorMessage = ErrorMessages.Invalid, ErrorCode = (int)HttpStatusCode.BadRequest }
                    };
            }
            else if (_cityRepository.GetFirstOrDefault(_ => _.Name.Trim().ToLower() == cityModel.Name.Trim().ToLower()).Result != null)
            {
                response.Messages = new List<ApplicationError>() {
                    new ApplicationError { ErrorMessage = ErrorMessages.AlreadyExists, ErrorCode = (int)HttpStatusCode.BadRequest }
                };
            }
            else
            {
                response.Data = this._cityRepository.Update(cityModel.TransformToDataModel()).Result.TransformToViewModel();
                response.State = true;
            }
            return response;
        }


        /// <summary>
        /// Method to get all City
        /// </summary>
        /// <returns></returns>
        public ApplicationResponse<IEnumerable<CityModel>> GetAll()
        {
            return new ApplicationResponse<IEnumerable<CityModel>>
            {
                Data = this._cityRepository.GetAll().Result.TransformToViewModelList(),
                State = true
            };
        }

        /// <summary>
        /// Method to get City by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationResponse<CityModel> GetById(int id)
        {
            var response = new ApplicationResponse<CityModel>() { Data = null, State = false };
            if (id <= 0)
            {
                response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.ID, ErrorCode = (int)HttpStatusCode.BadRequest } };
            }
            else
            {
                response.Data = this._cityRepository.GetById(id).Result?.TransformToViewModel();
                if (response.Data == null)
                {
                    response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.Invalid, ErrorCode = (int)HttpStatusCode.NotFound } };
                }
                else
                    response.State = true;
            }
            return response;
        }
    }
}

