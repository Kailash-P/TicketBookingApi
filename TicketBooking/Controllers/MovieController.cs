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
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;


        /// <summary>
        /// Constuctor for Movie Controller.
        /// </summary>
        /// <param name="movieService"></param>
        public MovieController(IMovieService movieService)
        {
            this._movieService = movieService;
        }

        /// <summary>
        /// This method is used to get all Movie
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApplicationResponse<IEnumerable<MovieModel>>> GetAll()
        {
            var response = this._movieService.GetAll();
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
        /// This method is used to get Moviev by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ApplicationResponse<MovieModel>> GetById(int id)
        {
            var response = this._movieService.GetById(id);
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
        /// This method is used to delete Movie by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<ApplicationResponse<bool>> Delete(int id)
        {   
            var response = this._movieService.Delete(id);
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
        /// This method is used to create new Movie
        /// </summary>
        /// <param name="MovieModel"></param>
        [HttpPost]
        public ActionResult<ApplicationResponse<MovieModel>> Create(MovieModel movieModel)
        {
            var response = this._movieService.Create(movieModel);
            if(response.State)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return StatusCode(400);
            }
        }

        /// <summary>
        /// This method is used to update existing Movie
        /// </summary>
        /// <param name="MovieModel"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<ApplicationResponse<MovieModel>> Update(MovieModel movieModel)
        {
            var response = this._movieService.Update(movieModel);
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

