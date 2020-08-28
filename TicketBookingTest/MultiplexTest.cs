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
    public class MultiplexTest
    {
        public IEnumerable<Multiplex> list;
        public Mock<IMultiplexRepository> mockRepo;
        public IMultiplexService service;
        public MultiplexTest()
        {
            list = new Multiplex[] { new Multiplex
            {
                Id = 1,
                Name = "Multiplex1",
                CityId = 1,
                TotalSeats = 100
            }, new Multiplex
            {
                Id = 2,
                Name = "Multiplex2",
                CityId = 2,
                TotalSeats = 100
            }};

            InitializeMockObject();
            service = new MultiplexService(mockRepo.Object);
            InitializeMockMultiplexRepo();
        }

        /// <summary>
        /// This method is to perform create Multiplex record mock test.
        /// </summary>
        [Fact]
        public void CreateMultiplexMockTest()
        {
            var obj = new MultiplexModel() { Name = "Multiplex1", City = new CityModel { Id = 1, Name = "City1" } };
            Assert.Equal(1, service.Create(obj).Data.Id);

            //empty Multiplex record flow test

            Assert.False(service.Create(new MultiplexModel() { Name = string.Empty }).State);
        }

        /// <summary>
        /// This method is to perform update Multiplex test.
        /// </summary>
        [Fact]
        public void UpdateMultiplexMockTest()
        {
            var obj = new MultiplexModel() { Id = 1, Name = "Multiplex3", City = new CityModel { Id = 3, Name = "City3" } };

            Assert.NotNull(service.Update(obj).Data);

            //invalid record record flow test

            Assert.False(service.Update(new MultiplexModel() { Id = 3, Name = "Multiplex4", City = new CityModel { Id = 3, Name = "City3" } }).State);

            //empty Multiplex record flow test

            Assert.False(service.Create(new MultiplexModel() { Name = string.Empty }).State);

            //ID not exists
            Assert.False(service.Update(new MultiplexModel() { Name = "Multiplex4" }).State);
        }

        /// <summary>
        /// This method is to perform delete Multiplex mock test.
        /// </summary>
        [Fact]
        public void DeleteMultiplexMockTest()
        {
            //Already existing description record flow test

            Assert.True(service.Delete(1).Data);

            //ID not exists

            Assert.False(service.Delete(3).State);
        }

        /// <summary>
        /// This method is to perform get Multiplex by id mock test.
        /// </summary>
        [Fact]
        public void GetMultiplexByIDTest()
        {
            //Already existing description record flow test

            Assert.NotNull(service.GetById(1).Data);

            //ID not exists
            Assert.False(service.GetById(3).State);
        }

        [Fact]
        public void GetAllMultiplexTest()
        {
            Assert.NotNull(service.GetAll().Data);
        }

        private void InitializeMockObject()
        {
            mockRepo = GetMultiplexRepo();
        }
        private void InitializeMockMultiplexRepo()
        {
            mockRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return list.Where(x => x.Id == id).SingleOrDefault();
                });
            mockRepo.Setup(x => x.Delete(It.IsAny<Multiplex>())).Returns(true);

            mockRepo.Setup(x => x.Update(It.IsAny<Multiplex>())).ReturnsAsync(
                (Multiplex target) =>
                {
                    return target;
                });
            mockRepo.Setup(x => x.Insert(It.IsAny<Multiplex>())).ReturnsAsync(
                (Multiplex target) =>
                {
                    DateTime utc = DateTime.UtcNow;
                    target.Id = 1;
                    return target;
                });
            mockRepo.Setup(x => x.GetAll()).ReturnsAsync(list);

            mockRepo.Setup(x => x.GetAllInclude(It.IsAny<Func<IQueryable<Multiplex>, IIncludableQueryable<Multiplex, object>>>(), It.IsAny<Func<IQueryable<Multiplex>, IOrderedQueryable<Multiplex>>>()))
              .ReturnsAsync((Func<IQueryable<Multiplex>, IIncludableQueryable<Multiplex, object>> include, Func<IQueryable<Multiplex>, IOrderedQueryable<Multiplex>> orderby) => {
                  return list;
              });

            mockRepo.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<Multiplex, bool>>>(), It.IsAny<Expression<Func<Multiplex, object>>[]>())).ReturnsAsync(
               (Expression<Func<Multiplex, bool>> filter, Expression<Func<Multiplex, object>>[] includes) =>
               {
                   return list.AsQueryable().Where(filter).FirstOrDefault();
               });

            mockRepo.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<Multiplex, bool>>>(),
                                                                It.IsAny<Func<IQueryable<Multiplex>, IOrderedQueryable<Multiplex>>>(),
                                                                 It.IsAny<Func<IQueryable<Multiplex>, IIncludableQueryable<Multiplex, object>>>(),
                                                                 It.IsAny<bool>())).ReturnsAsync(
                (Expression<Func<Multiplex, bool>> filter, Func<IQueryable<Multiplex>, IOrderedQueryable<Multiplex>> orderBy,
                Func<IQueryable<Multiplex>, IIncludableQueryable<Multiplex, object>> include, bool disableTracking) =>
                {
                    return list.AsQueryable().Where(filter).FirstOrDefault();
                });
        }
        private Mock<IMultiplexRepository> GetMultiplexRepo()
        {
            return new Mock<IMultiplexRepository>();
        }
    }
}
