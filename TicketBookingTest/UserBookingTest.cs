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
    public class UserBookingTest
    {
        public IEnumerable<UserBooking> list;
        public Mock<IUserBookingRepository> mockRepo;
        public IUserBookingService service;
        public UserBookingTest()
        {
            list = new UserBooking[] { new UserBooking
            {
                Id = 1,
                MovieId = 1,
                UserId = 1,
                Seats = 4
            }, new UserBooking
            {
                Id = 2,
                MovieId = 2,
                UserId = 2,
                Seats = 2
            }};

            InitializeMockObject();
            service = new UserBookingService(mockRepo.Object);
            InitializeMockUserBookingRepo();
        }

        /// <summary>
        /// This method is to perform create UserBooking record mock test.
        /// </summary>
        [Fact]
        public void CreateUserBookingMockTest()
        {
            var obj = new UserBookingModel() { 
                 Movie = new MovieModel {
                    Id = 1,
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
                },
                User = new UserModel
                {
                    Id = 1,
                    Name = "User1"
                },
                Seats = 3
            };
            Assert.Equal(1, service.Create(obj).Data.Id);

            //empty UserBooking seats record flow test

            Assert.False(service.Create(new UserBookingModel() { Seats = 0 }).State);

            //max UserBooking seats record flow test

            Assert.False(service.Create(new UserBookingModel() { Seats = 7 }).State);
        }

        /// <summary>
        /// This method is to perform delete UserBooking mock test.
        /// </summary>
        [Fact]
        public void DeleteUserBookingMockTest()
        {
            //Already existing description record flow test

            Assert.True(service.Delete(1).Data);

            //ID not exists

            Assert.False(service.Delete(3).State);
        }

        /// <summary>
        /// This method is to perform get UserBooking by id mock test.
        /// </summary>
        [Fact]
        public void GetUserBookingByIDTest()
        {
            //Already existing description record flow test

            Assert.NotNull(service.GetById(1).Data);

            //ID not exists
            Assert.False(service.GetById(3).State);
        }

        [Fact]
        public void GetAllUserBookingTest()
        {
            Assert.NotNull(service.GetAll().Data);
        }

        private void InitializeMockObject()
        {
            mockRepo = GetUserBookingRepo();
        }
        private void InitializeMockUserBookingRepo()
        {
            mockRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return list.Where(x => x.Id == id).SingleOrDefault();
                });
            mockRepo.Setup(x => x.Delete(It.IsAny<UserBooking>())).Returns(true);

            mockRepo.Setup(x => x.Update(It.IsAny<UserBooking>())).ReturnsAsync(
                (UserBooking target) =>
                {
                    return target;
                });
            mockRepo.Setup(x => x.Insert(It.IsAny<UserBooking>())).ReturnsAsync(
                (UserBooking target) =>
                {
                    DateTime utc = DateTime.UtcNow;
                    target.Id = 1;
                    return target;
                });
            mockRepo.Setup(x => x.GetAll()).ReturnsAsync(list);

            mockRepo.Setup(x => x.GetAllInclude(It.IsAny<Func<IQueryable<UserBooking>, IIncludableQueryable<UserBooking, object>>>(), It.IsAny<Func<IQueryable<UserBooking>, IOrderedQueryable<UserBooking>>>()))
              .ReturnsAsync((Func<IQueryable<UserBooking>, IIncludableQueryable<UserBooking, object>> include, Func<IQueryable<UserBooking>, IOrderedQueryable<UserBooking>> orderby) => {
                  return list;
              });

            mockRepo.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<UserBooking, bool>>>(), It.IsAny<Expression<Func<UserBooking, object>>[]>())).ReturnsAsync(
               (Expression<Func<UserBooking, bool>> filter, Expression<Func<UserBooking, object>>[] includes) =>
               {
                   return list.AsQueryable().Where(filter).FirstOrDefault();
               });

            mockRepo.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<UserBooking, bool>>>(),
                                                                It.IsAny<Func<IQueryable<UserBooking>, IOrderedQueryable<UserBooking>>>(),
                                                                 It.IsAny<Func<IQueryable<UserBooking>, IIncludableQueryable<UserBooking, object>>>(),
                                                                 It.IsAny<bool>())).ReturnsAsync(
                (Expression<Func<UserBooking, bool>> filter, Func<IQueryable<UserBooking>, IOrderedQueryable<UserBooking>> orderBy,
                Func<IQueryable<UserBooking>, IIncludableQueryable<UserBooking, object>> include, bool disableTracking) =>
                {
                    return list.AsQueryable().Where(filter).FirstOrDefault();
                });

            mockRepo.Setup(x => x.ValidateTotalSeatBooking(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
        }
        private Mock<IUserBookingRepository> GetUserBookingRepo()
        {
            return new Mock<IUserBookingRepository>();
        }
    }
}