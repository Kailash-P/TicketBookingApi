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
    public class MultiplexService : IMultiplexService
    {
        public readonly IMultiplexRepository _multiplexRepository;

        public MultiplexService(IMultiplexRepository multiplexRepository)
        {
            _multiplexRepository = multiplexRepository;
        }

        /// <summary>
        /// This method is used to create new Multiplex
        /// </summary>
        /// <param name="MultiplexModel"></param>
        /// <returns></returns>
        public ApplicationResponse<MultiplexModel> Create(MultiplexModel multiplexModel)
        {
            var response = new ApplicationResponse<MultiplexModel>() { State = false, Data = null };
            if (string.IsNullOrEmpty(multiplexModel?.Name))
            {
                response.Messages = new List<ApplicationError>()
                {
                        new ApplicationError { ErrorMessage = ErrorMessages.Name, ErrorCode = (int)HttpStatusCode.BadRequest }
                };
            }
            else
            {
                var result = this._multiplexRepository.Insert(multiplexModel.TransformToDataModel()).Result;
                response.Data =  GetById(result.Id).Data;
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
            var entity = _multiplexRepository.GetFirstOrDefault(x => x.Id == id).Result;
            if (entity != null)
            {
                response.Data = this._multiplexRepository.Delete(entity);
                response.State = true;
            }
            else
            {
                response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.Invalid, ErrorCode = (int)HttpStatusCode.NotFound } };
            }
            return response;
        }

        /// <summary>
        /// This method is used to update existing Multiplex
        /// </summary>
        /// <param name="MultiplexModel"></param>
        /// <returns></returns>
        public ApplicationResponse<MultiplexModel> Update(MultiplexModel multiplexModel)
        {
            var response = new ApplicationResponse<MultiplexModel>() { State = false, Data = null };
            var entity = _multiplexRepository.GetById(multiplexModel.Id).Result;
            if (entity == null)
            {
                response.Messages = new List<ApplicationError>() {
                        new ApplicationError { ErrorMessage = ErrorMessages.Invalid, ErrorCode = (int)HttpStatusCode.NotFound }
                    };
            }
            else if (_multiplexRepository.GetFirstOrDefault(_ => _.Id != multiplexModel.Id && _.Name.Trim().ToLower() == multiplexModel.Name.Trim().ToLower()).Result != null)
            {
                response.Messages = new List<ApplicationError>() {
                    new ApplicationError { ErrorMessage = ErrorMessages.AlreadyExists, ErrorCode = (int)HttpStatusCode.BadRequest }
                };
            }
            else
            {
                response.Data = this._multiplexRepository.Update(multiplexModel.TransformToDataModel()).Result.TransformToViewModel();
                response.State = true;
            }
            return response;
        }


        /// <summary>
        /// Method to get all Multiplex
        /// </summary>
        /// <returns></returns>
        public ApplicationResponse<IEnumerable<MultiplexModel>> GetAll()
        {
            return new ApplicationResponse<IEnumerable<MultiplexModel>>
            {
                Data = this._multiplexRepository.GetAllInclude(source => source.Include(x => x.City)).Result.TransformToViewModelList(),
                State = true
            };
        }

        /// <summary>
        /// Method to get Multiplex by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationResponse<MultiplexModel> GetById(int id)
        {
            var response = new ApplicationResponse<MultiplexModel>() { Data = null, State = false };
            if (id <= 0)
            {
                response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.ID, ErrorCode = (int)HttpStatusCode.BadRequest } };
            }
            else
            {
                response.Data = this._multiplexRepository.GetFirstOrDefault(x => x.Id == id, include: source => source.Include(x => x.City)).Result?.TransformToViewModel();
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