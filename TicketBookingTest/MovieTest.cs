using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TicketBooking.BAL.Implementation;
using TicketBooking.BAL.Interface;
using TicketBooking.DAL.Models;
using TicketBooking.DAL.Repositories.Implementation;
using TicketBooking.Models;
using Xunit;

namespace TicketBookingTest
{
    public class MovieTest
    {
        public IEnumerable<Movie> list;
        public Mock<IMovieRepository> mockRepo;
        public IMovieService service;
        public MovieTest()
        {
            list = new Movie[] { new Movie
            {
                Id = 1,
                Name = "Movie1",
                MultiplexId = 1,
                GenreId = 1,
                LanguageId = 1
            }, new Movie
            {
                Id = 2,
                Name = "Movie2",
                MultiplexId = 2,
                GenreId = 2,
                LanguageId = 2
            }};

            InitializeMockObject();
            service = new MovieService(mockRepo.Object);
            InitializeMockMovieRepo();
        }

        /// <summary>
        /// This method is to perform create Movie record mock test.
        /// </summary>
        [Fact]
        public void CreateMovieMockTest()
        {
            var obj = new MovieModel()
            {
                Name = "Movie1",
                Genre = "Drama",
                Language = "Hindi",
                Multiplex =
                            new MultiplexModel
                            {
                                Id = 1,
                                Name = "Multiplex1",
                                TotalSeats = 100,
                                City = new CityModel { Id = 1, Name = "City1" }
                            }
            };
            Assert.Equal(1, service.Create(obj).Data.Id);

            //empty Movie record flow test

            Assert.False(service.Create(new MovieModel() { Name = string.Empty }).State);
        }

        /// <summary>
        /// This method is to perform update Movie test.
        /// </summary>
        [Fact]
        public void UpdateMovieMockTest()
        {
            var obj = new MovieModel()
            {
                Id = 1,
                Name = "Movie3",
                Genre = "Drama",
                Language = "Hindi",
                Multiplex =
                            new MultiplexModel
                            {
                                Id = 1,
                                Name = "Multiplex1",
                                TotalSeats = 100,
                                City = new CityModel { Id = 1, Name = "City1" }
                            }
            };

            Assert.NotNull(service.Update(obj).Data);

            //invalid record record flow test

            Assert.False(service.Update(new MovieModel()
            {
                Id = 3,
                Name = "Movie4",
                Multiplex =
                            new MultiplexModel
                            {
                                Id = 2,
                                Name = "Multiplex2",
                                TotalSeats = 100,
                                City = new CityModel { Id = 2, Name = "City2" }
                            }
            }).State);

            //empty Movie record flow test

            Assert.False(service.Create(new MovieModel() { Name = string.Empty }).State);

            //ID not exists
            Assert.False(service.Update(new MovieModel() { Name = "Movie4" }).State);
        }

        /// <summary>
        /// This method is to perform delete Movie mock test.
        /// </summary>
        [Fact]
        public void DeleteMovieMockTest()
        {
            //Already existing description record flow test

            Assert.True(service.Delete(1).Data);

            //ID not exists

            Assert.False(service.Delete(3).State);
        }

        /// <summary>
        /// This method is to perform get Movie by id mock test.
        /// </summary>
        [Fact]
        public void GetMovieByIDTest()
        {
            //Already existing description record flow test

            Assert.NotNull(service.GetById(1).Data);

            //ID not exists
            Assert.False(service.GetById(3).State);
        }

        [Fact]
        public void GetAllMovieTest()
        {
            Assert.NotNull(service.GetAll().Data);
        }

        private void InitializeMockObject()
        {
            mockRepo = GetMovieRepo();
        }
        private void InitializeMockMovieRepo()
        {
            mockRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return list.Where(x => x.Id == id).SingleOrDefault();
                });
            mockRepo.Setup(x => x.Delete(It.IsAny<Movie>())).Returns(true);

            mockRepo.Setup(x => x.Update(It.IsAny<Movie>())).ReturnsAsync(
                (Movie target) =>
                {
                    return target;
                });
            mockRepo.Setup(x => x.Insert(It.IsAny<Movie>())).ReturnsAsync(
                (Movie target) =>
                {
                    DateTime utc = DateTime.UtcNow;
                    target.Id = 1;
                    return target;
                });
            mockRepo.Setup(x => x.GetAll()).ReturnsAsync(list);

            mockRepo.Setup(x => x.GetAllInclude(It.IsAny<Func<IQueryable<Movie>, IIncludableQueryable<Movie, object>>>(), It.IsAny<Func<IQueryable<Movie>, IOrderedQueryable<Movie>>>()))
              .ReturnsAsync((Func<IQueryable<Movie>, IIncludableQueryable<Movie, object>> include, Func<IQueryable<Movie>, IOrderedQueryable<Movie>> orderby) =>
              {
                  return list;
              });

            mockRepo.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<Movie, bool>>>(), It.IsAny<Expression<Func<Movie, object>>[]>())).ReturnsAsync(
               (Expression<Func<Movie, bool>> filter, Expression<Func<Movie, object>>[] includes) =>
               {
                   return list.AsQueryable().Where(filter).FirstOrDefault();
               });

            mockRepo.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<Movie, bool>>>(),
                                                                It.IsAny<Func<IQueryable<Movie>, IOrderedQueryable<Movie>>>(),
                                                                 It.IsAny<Func<IQueryable<Movie>, IIncludableQueryable<Movie, object>>>(),
                                                                 It.IsAny<bool>())).ReturnsAsync(
                (Expression<Func<Movie, bool>> filter, Func<IQueryable<Movie>, IOrderedQueryable<Movie>> orderBy,
                Func<IQueryable<Movie>, IIncludableQueryable<Movie, object>> include, bool disableTracking) =>
                {
                    return list.AsQueryable().Where(filter).FirstOrDefault();
                });
        }
        private Mock<IMovieRepository> GetMovieRepo()
        {
            return new Mock<IMovieRepository>();
        }
    }
}