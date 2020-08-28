using System.Collections.Generic;
using System.Linq;
using TicketBooking.DAL.Models;
using TicketBooking.Models;

namespace TicketBooking.BAL.DataTransfers
{
    public static class UserBookingDataTransfer
    {
		public static UserBookingModel TransformToViewModel(this UserBooking x)
		{
			return x != null ?
				new UserBookingModel
				{
					Id = x.Id,
					Movie = x.Movie.TransformToViewModel(),
					User = x.User.TransformToViewModel(),
					Seats = x.Seats
				} : null;
		}

		public static UserBooking TransformToDataModel(this UserBookingModel x)
		{
			return x != null ?
				new UserBooking
				{
					Id = x.Id,
					MovieId = x.Movie.Id,
					UserId = x.User.Id,
					Seats = x.Seats
				} : null;
		}

		public static IEnumerable<UserBookingModel> TransformToViewModelList(this IEnumerable<UserBooking> list)
		{
			if (list != null && list.Any())
			{
				return list.Select(x => x.TransformToViewModel()).ToList();
			}
			else
			{
				return new List<UserBookingModel>();
			}
		}

		public static IEnumerable<UserBooking> TransformToDataModelList(this IEnumerable<UserBookingModel> list)
		{
			if (list != null && list.Any())
			{
				return list.Select(x => x.TransformToDataModel()).ToList();
			}
			else
			{
				return new List<UserBooking>();
			}
		}
	}
}
