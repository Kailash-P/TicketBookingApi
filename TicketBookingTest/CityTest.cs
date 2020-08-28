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
    public class CityTest
    {
        public IEnumerable<City> list;
        public Mock<ICityRepository> mockRepo;
        public ICityService service;
        public CityTest()
        {
            list = new City[] { new City
            {
                Id = 1,
                Name = "Chennai"
            }, new City
            {
                Id = 2,
                Name = "Hyderabad"
            }};

            InitializeMockObject();
            service = new CityService(mockRepo.Object);
            InitializeMockCityRepo();
        }

        /// <summary>
        /// This method is to perform create City record mock test.
        /// </summary>
        [Fact]
        public void CreateCityMockTest()
        {
            var obj = new CityModel() { Name = "Chennai" };
            Assert.Equal(1, service.Create(obj).Data.Id);

            //empty City record flow test

            Assert.False(service.Create(new CityModel() { Name = string.Empty }).State);
        }

        /// <summary>
        /// This method is to perform update City test.
        /// </summary>
        [Fact]
        public void UpdateCityMockTest()
        {
            var obj = new CityModel() { Id = 1, Name = "Madras" };

            Assert.NotNull(service.Update(obj).Data);

            //invalid record record flow test

            Assert.False(service.Update(new CityModel() { Id = 3, Name = "Delhi" }).State);

            //empty City record flow test

            Assert.False(service.Create(new CityModel() { Name = string.Empty }).State);

            //ID not exists
            Assert.False(service.Update(new CityModel() { Name = "Delhi" }).State);
        }

        /// <summary>
        /// This method is to perform delete City mock test.
        /// </summary>
        [Fact]
        public void DeleteCityMockTest()
        {
            //Already existing description record flow test

            Assert.True(service.Delete(1).Data);

            //ID not exists

            Assert.False(service.Delete(3).State);
        }

        /// <summary>
        /// This method is to perform get City by id mock test.
        /// </summary>
        [Fact]
        public void GetCityByIDTest()
        {
            //Already existing description record flow test

            Assert.NotNull(service.GetById(1).Data);

            //ID not exists
            Assert.False(service.GetById(3).State);
        }

        [Fact]
        public void GetAllCityTest()
        {
            Assert.NotNull(service.GetAll().Data);
        }

        private void InitializeMockObject()
        {
            mockRepo = GetCityRepo();
        }
        private void InitializeMockCityRepo()
        {
            mockRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return list.Where(x => x.Id == id).SingleOrDefault();
                });
            mockRepo.Setup(x => x.Delete(It.IsAny<City>())).Returns(true);

            mockRepo.Setup(x => x.Update(It.IsAny<City>())).ReturnsAsync(
                (City target) =>
                {
                    return target;
                });
            mockRepo.Setup(x => x.Insert(It.IsAny<City>())).ReturnsAsync(
                (City target) =>
                {
                    DateTime utc = DateTime.UtcNow;
                    target.Id = 1;
                    return target;
                });
            mockRepo.Setup(x => x.GetAll()).ReturnsAsync(list);

            mockRepo.Setup(x => x.GetAllInclude(It.IsAny<Func<IQueryable<City>, IIncludableQueryable<City, object>>>(), It.IsAny<Func<IQueryable<City>, IOrderedQueryable<City>>>()))
              .ReturnsAsync((Func<IQueryable<City>, IIncludableQueryable<City, object>> include, Func<IQueryable<City>, IOrderedQueryable<City>> orderby) => {
                  return list;
              });

            mockRepo.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<City, bool>>>(), It.IsAny<Expression<Func<City, object>>[]>())).ReturnsAsync(
               (Expression<Func<City, bool>> filter, Expression<Func<City, object>>[] includes) =>
               {
                   return list.AsQueryable().Where(filter).FirstOrDefault();
               });

            mockRepo.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<City, bool>>>(),
                                                                It.IsAny<Func<IQueryable<City>, IOrderedQueryable<City>>>(),
                                                                 It.IsAny<Func<IQueryable<City>, IIncludableQueryable<City, object>>>(),
                                                                 It.IsAny<bool>())).ReturnsAsync(
                (Expression<Func<City, bool>> filter, Func<IQueryable<City>, IOrderedQueryable<City>> orderBy,
                Func<IQueryable<City>, IIncludableQueryable<City, object>> include, bool disableTracking) =>
                {
                    return list.AsQueryable().Where(filter).FirstOrDefault();
                });
        }
        private Mock<ICityRepository> GetCityRepo()
        {
            return new Mock<ICityRepository>();
        }
    }
}