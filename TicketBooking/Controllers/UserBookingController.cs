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
    public class UserBookingController : ControllerBase
    {
        private readonly IUserBookingService _userBookingService;


        /// <summary>
        /// Constuctor for User Booking Controller.
        /// </summary>
        /// <param name="userBookingService"></param>
        public UserBookingController(IUserBookingService userBookingService)
        {
            this._userBookingService = userBookingService;
        }

        /// <summary>
        /// This method is used to get all User Booking
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApplicationResponse<IEnumerable<UserBookingModel>>> GetAll()
        {
            var response = this._userBookingService.GetAll();
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
        /// This method is used to get User Bookingv by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ApplicationResponse<UserBookingModel>> GetById(int id)
        {
            var response = this._userBookingService.GetById(id);
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
        /// This method is used to delete User Booking by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<ApplicationResponse<bool>> Delete(int id)
        {
            var response = this._userBookingService.Delete(id);
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
        /// This method is used to create new User Booking
        /// </summary>
        /// <param name="UserBookingModel"></param>
        [HttpPost]
        public ActionResult<ApplicationResponse<UserBookingModel>> Create(UserBookingModel userBookingModel)
        {
            var response = this._userBookingService.Create(userBookingModel);
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
