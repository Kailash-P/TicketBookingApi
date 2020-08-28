using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TicketBooking.BAL.Interface;
using TicketBooking.Models;
using TicketBooking.Models.Common;

namespace TicketBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        /// <summary>
        /// Constuctor for User Controller.
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// This method is used to get all User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApplicationResponse<IEnumerable<UserModel>>> GetAll()
        {
            var response = this._userService.GetAll();
            if (response.State)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return new ContentResult
                {
                    StatusCode = response.Messages.FirstOrDefault().ErrorCode,
                    Content = response.Messages.FirstOrDefault().ErrorMessage,
                    ContentType = "text/plain",
                };
            }
        }

        /// <summary>
        /// This method is used to get Userv by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ApplicationResponse<UserModel>> GetById(int id)
        {
            var response = this._userService.GetById(id);
            if (response.State)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return new ContentResult
                {
                    StatusCode = response.Messages.FirstOrDefault().ErrorCode,
                    Content = response.Messages.FirstOrDefault().ErrorMessage,
                    ContentType = "text/plain",
                };
            }
        }

        /// <summary>
        /// This method is used to delete User by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<ApplicationResponse<bool>> Delete(int id)
        {
            var response = this._userService.Delete(id);
            if (response.State)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return new ContentResult
                {
                    StatusCode = response.Messages.FirstOrDefault().ErrorCode,
                    Content = response.Messages.FirstOrDefault().ErrorMessage,
                    ContentType = "text/plain",
                };
            }
        }

        /// <summary>
        /// This method is used to create new User
        /// </summary>
        /// <param name="UserModel"></param>
        [HttpPost]
        public ActionResult<ApplicationResponse<UserModel>> Create(UserModel userModel)
        {
            var response = this._userService.Create(userModel);
            if (response.State)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return new ContentResult
                {
                    StatusCode = response.Messages.FirstOrDefault().ErrorCode,
                    Content = response.Messages.FirstOrDefault().ErrorMessage,
                    ContentType = "text/plain",
                };
            }
        }

        /// <summary>
        /// This method is used to update existing User
        /// </summary>
        /// <param name="UserModel"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<ApplicationResponse<UserModel>> Update(UserModel userModel)
        {
            var response = this._userService.Update(userModel);
            if (response.State)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return new ContentResult
                {
                    StatusCode = response.Messages.FirstOrDefault().ErrorCode,
                    Content = response.Messages.FirstOrDefault().ErrorMessage,
                    ContentType = "text/plain",
                };
            }
        }
    }
}
