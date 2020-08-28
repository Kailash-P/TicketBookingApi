using System.Collections.Generic;
using System.Net;
using TicketBooking.BAL.DataTransfers;
using TicketBooking.BAL.Interface;
using TicketBooking.DAL.Repositories.Implementation;
using TicketBooking.Models;
using TicketBooking.Models.Common;

namespace TicketBooking.BAL.Implementation
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// This method is used to create new User
        /// </summary>
        /// <param name="UserModel"></param>
        /// <returns></returns>
        public ApplicationResponse<UserModel> Create(UserModel userModel)
        {
            var response = new ApplicationResponse<UserModel>() { State = false, Data = null };
            if (string.IsNullOrEmpty(userModel?.Name))
            {
                response.Messages = new List<ApplicationError>()
                {
                        new ApplicationError { ErrorMessage = ErrorMessages.Name, ErrorCode = (int)HttpStatusCode.BadRequest }
                };
            }
            else
            {
                response.Data = this._userRepository.Insert(userModel.TransformToDataModel()).Result.TransformToViewModel();
                response.State = true;
            }
            return response;
        }

        /// <summary>
        /// Method to delete User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationResponse<bool> Delete(int id)
        {
            var response = new ApplicationResponse<bool>() { State = false, Data = false };
            var entity = _userRepository.GetFirstOrDefault(x => x.Id == id).Result;
            if (entity != null)
            {
                response.Data = this._userRepository.Delete(entity);
                response.State = true;
            }
            else
            {
                response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.Invalid, ErrorCode = (int)HttpStatusCode.NotFound } };
            }
            return response;
        }

        /// <summary>
        /// This method is used to update existing User
        /// </summary>
        /// <param name="UserModel"></param>
        /// <returns></returns>
        public ApplicationResponse<UserModel> Update(UserModel userModel)
        {
            var response = new ApplicationResponse<UserModel>() { State = false, Data = null };
            var entity = _userRepository.GetById(userModel.Id).Result;
            if (entity == null)
            {
                response.Messages = new List<ApplicationError>() {
                        new ApplicationError { ErrorMessage = ErrorMessages.Invalid, ErrorCode = (int)HttpStatusCode.NotFound }
                    };
            }
            else if (_userRepository.GetFirstOrDefault(_ => _.Id != userModel.Id && _.Name.Trim().ToLower() == userModel.Name.Trim().ToLower()).Result != null)
            {
                response.Messages = new List<ApplicationError>() {
                    new ApplicationError { ErrorMessage = ErrorMessages.AlreadyExists, ErrorCode = (int)HttpStatusCode.BadRequest }
                };
            }
            else
            {
                response.Data = this._userRepository.Update(userModel.TransformToDataModel()).Result.TransformToViewModel();
                response.State = true;
            }
            return response;
        }


        /// <summary>
        /// Method to get all User
        /// </summary>
        /// <returns></returns>
        public ApplicationResponse<IEnumerable<UserModel>> GetAll()
        {
            return new ApplicationResponse<IEnumerable<UserModel>>
            {
                Data = this._userRepository.GetAll().Result.TransformToViewModelList(),
                State = true
            };
        }

        /// <summary>
        /// Method to get User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationResponse<UserModel> GetById(int id)
        {
            var response = new ApplicationResponse<UserModel>() { Data = null, State = false };
            if (id <= 0)
            {
                response.Messages = new List<ApplicationError>() { new ApplicationError { ErrorMessage = ErrorMessages.ID, ErrorCode = (int)HttpStatusCode.BadRequest } };
            }
            else
            {
                response.Data = this._userRepository.GetById(id).Result?.TransformToViewModel();
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
