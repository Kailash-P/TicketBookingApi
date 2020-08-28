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
    public class UserTest
    {
        public IEnumerable<User> list;
        public Mock<IUserRepository> mockRepo;
        public IUserService service;
        public UserTest()
        {
            list = new User[] { new User
            {
                Id = 1,
                Name = "User1"
            }, new User
            {
                Id = 2,
                Name = "User2"
            }};

            InitializeMockObject();
            service = new UserService(mockRepo.Object);
            InitializeMockUserRepo();
        }

        /// <summary>
        /// This method is to perform create User record mock test.
        /// </summary>
        [Fact]
        public void CreateUserMockTest()
        {
            var obj = new UserModel() { Name = "User1" };
            Assert.Equal(1, service.Create(obj).Data.Id);

            //empty User record flow test

            Assert.False(service.Create(new UserModel() { Name = string.Empty }).State);
        }

        /// <summary>
        /// This method is to perform update User test.
        /// </summary>
        [Fact]
        public void UpdateUserMockTest()
        {
            var obj = new UserModel() { Id = 1, Name = "User3" };

            Assert.NotNull(service.Update(obj).Data);

            //invalid record record flow test

            Assert.False(service.Update(new UserModel() { Id = 3, Name = "User4" }).State);

            //empty User record flow test

            Assert.False(service.Create(new UserModel() { Name = string.Empty }).State);

            //ID not exists
            Assert.False(service.Update(new UserModel() { Name = "User4" }).State);
        }

        /// <summary>
        /// This method is to perform delete User mock test.
        /// </summary>
        [Fact]
        public void DeleteUserMockTest()
        {
            //Already existing description record flow test

            Assert.True(service.Delete(1).Data);

            //ID not exists

            Assert.False(service.Delete(3).State);
        }

        /// <summary>
        /// This method is to perform get User by id mock test.
        /// </summary>
        [Fact]
        public void GetUserByIDTest()
        {
            //Already existing description record flow test

            Assert.NotNull(service.GetById(1).Data);

            //ID not exists
            Assert.False(service.GetById(3).State);
        }

        [Fact]
        public void GetAllUserTest()
        {
            Assert.NotNull(service.GetAll().Data);
        }

        private void InitializeMockObject()
        {
            mockRepo = GetUserRepo();
        }
        private void InitializeMockUserRepo()
        {
            mockRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return list.Where(x => x.Id == id).SingleOrDefault();
                });
            mockRepo.Setup(x => x.Delete(It.IsAny<User>())).Returns(true);

            mockRepo.Setup(x => x.Update(It.IsAny<User>())).ReturnsAsync(
                (User target) =>
                {
                    return target;
                });
            mockRepo.Setup(x => x.Insert(It.IsAny<User>())).ReturnsAsync(
                (User target) =>
                {
                    DateTime utc = DateTime.UtcNow;
                    target.Id = 1;
                    return target;
                });
            mockRepo.Setup(x => x.GetAll()).ReturnsAsync(list);

            mockRepo.Setup(x => x.GetAllInclude(It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(), It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
              .ReturnsAsync((Func<IQueryable<User>, IIncludableQueryable<User, object>> include, Func<IQueryable<User>, IOrderedQueryable<User>> orderby) => {
                  return list;
              });

            mockRepo.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<Expression<Func<User, object>>[]>())).ReturnsAsync(
               (Expression<Func<User, bool>> filter, Expression<Func<User, object>>[] includes) =>
               {
                   return list.AsQueryable().Where(filter).FirstOrDefault();
               });

            mockRepo.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(),
                                                                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                                                                 It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                                                                 It.IsAny<bool>())).ReturnsAsync(
                (Expression<Func<User, bool>> filter, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy,
                Func<IQueryable<User>, IIncludableQueryable<User, object>> include, bool disableTracking) =>
                {
                    return list.AsQueryable().Where(filter).FirstOrDefault();
                });
        }
        private Mock<IUserRepository> GetUserRepo()
        {
            return new Mock<IUserRepository>();
        }
    }
}
