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
    public class MultiplexController : ControllerBase
    {
        private readonly IMultiplexService _multiplexService;


        /// <summary>
        /// Constuctor for Multiplex Controller.
        /// </summary>
        /// <param name="userService"></param>
        public MultiplexController(IMultiplexService userService)
        {
            this._multiplexService = userService;
        }

        /// <summary>
        /// This method is used to get all Multiplex
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApplicationResponse<IEnumerable<MultiplexModel>>> GetAll()
        {
            var response = this._multiplexService.GetAll();
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
        /// This method is used to get Multiplexv by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ApplicationResponse<MultiplexModel>> GetById(int id)
        {
            var response = this._multiplexService.GetById(id);
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
        /// This method is used to delete Multiplex by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<ApplicationResponse<bool>> Delete(int id)
        {
            var response = this._multiplexService.Delete(id);
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
        /// This method is used to create new Multiplex
        /// </summary>
        /// <param name="MultiplexModel"></param>
        [HttpPost]
        public ActionResult<ApplicationResponse<MultiplexModel>> Create(MultiplexModel multiplexModel)
        {
            var response = this._multiplexService.Create(multiplexModel);
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
        /// This method is used to update existing Multiplex
        /// </summary>
        /// <param name="MultiplexModel"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<ApplicationResponse<MultiplexModel>> Update(MultiplexModel multiplexModel)
        {
            var response = this._multiplexService.Update(multiplexModel);
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