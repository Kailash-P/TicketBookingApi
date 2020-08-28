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
    public class CityController : ControllerBase
    {
        private readonly ICityService _userService;


        /// <summary>
        /// Constuctor for City Controller.
        /// </summary>
        /// <param name="userService"></param>
        public CityController(ICityService userService)
        {
            this._userService = userService;

        }

        /// <summary>
        /// This method is used to get all City
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApplicationResponse<IEnumerable<CityModel>>> GetAll()
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
        /// This method is used to get Cityv by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ApplicationResponse<CityModel>> GetById(int id)
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
        /// This method is used to delete City by id
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
        /// This method is used to create new City
        /// </summary>
        /// <param name="CityModel"></param>
        [HttpPost]
        public ActionResult<ApplicationResponse<CityModel>> Create(CityModel cityModel)
        {
            var response = this._userService.Create(cityModel);
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
        /// This method is used to update existing City
        /// </summary>
        /// <param name="CityModel"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<ApplicationResponse<CityModel>> Update(CityModel cityModel)
        {
            var response = this._userService.Update(cityModel);
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
