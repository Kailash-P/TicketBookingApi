using System.Collections.Generic;
using System.Linq;
using TicketBooking.DAL.Models;
using TicketBooking.Models;

namespace TicketBooking.BAL.DataTransfers
{
    public static class UserDataTransfer
    {
		public static UserModel TransformToViewModel(this User x)
		{
			return x != null ?
				new UserModel
				{
					Id = x.Id,
					Name = x.Name,
					Password = string.Empty, // empty - for security reasons during render
					IsAdmin = x.IsAdmin
				} : null;
		}

		public static User TransformToDataModel(this UserModel x)
		{
			return x != null ?
				new User
				{
					Id = x.Id,
					Name = x.Name,
					Password = x.Password,
					IsAdmin = x.IsAdmin
				} : null;
		}

		public static IEnumerable<UserModel> TransformToViewModelList(this IEnumerable<User> list)
		{
			if (list != null && list.Any())
			{
				return list.Select(x => x.TransformToViewModel()).ToList();
			}
			else
			{
				return new List<UserModel>();
			}
		}

		public static IEnumerable<User> TransformToDataModelList(this IEnumerable<UserModel> list)
		{
			if (list != null && list.Any())
			{
				return list.Select(x => x.TransformToDataModel()).ToList();
			}
			else
			{
				return new List<User>();
			}
		}
	}
}
