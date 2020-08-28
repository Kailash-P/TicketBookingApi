using System.Collections.Generic;
using System.Linq;
using TicketBooking.DAL.Models;
using TicketBooking.Models;

namespace TicketBooking.BAL.DataTransfers
{
    public static class MultiplexDataTransfer
    {
		public static MultiplexModel TransformToViewModel(this Multiplex x)
		{
			return x != null ?
				new MultiplexModel
				{
					Id = x.Id,
					Name = x.Name,
					City = x.City.TransformToViewModel(),
					TotalSeats = x.TotalSeats
				} : null;
		}

		public static Multiplex TransformToDataModel(this MultiplexModel x)
		{
			return x != null ?
				new Multiplex
				{
					Id = x.Id,
					Name = x.Name,
					CityId = x.City.Id,
					TotalSeats = x.TotalSeats
				} : null;
		}

		public static IEnumerable<MultiplexModel> TransformToViewModelList(this IEnumerable<Multiplex> list)
		{
			if (list != null && list.Any())
			{
				return list.Select(x => x.TransformToViewModel()).ToList();
			}
			else
			{
				return new List<MultiplexModel>();
			}
		}

		public static IEnumerable<Multiplex> TransformToDataModelList(this IEnumerable<MultiplexModel> list)
		{
			if (list != null && list.Any())
			{
				return list.Select(x => x.TransformToDataModel()).ToList();
			}
			else
			{
				return new List<Multiplex>();
			}
		}
	}
}