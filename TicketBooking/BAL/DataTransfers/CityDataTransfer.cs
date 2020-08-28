using System.Collections.Generic;
using System.Linq;
using TicketBooking.DAL.Models;
using TicketBooking.Models;

namespace TicketBooking.BAL.DataTransfers
{
    public static class CityDataTransfer
	{
		public static CityModel TransformToViewModel(this City x)
		{
			return x != null ?
				new CityModel
				{
					Id = x.Id,
					Name = x.Name
				} : null;
		}

		public static City TransformToDataModel(this CityModel x)
		{
			return x != null ?
				new City
				{
					Id = x.Id,
					Name = x.Name
				} : null;
		}

		public static IEnumerable<CityModel> TransformToViewModelList(this IEnumerable<City> list)
		{
			if (list != null && list.Any())
			{
				return list.Select(x => x.TransformToViewModel()).ToList();
			}
			else
			{
				return new List<CityModel>();
			}
		}

		public static IEnumerable<City> TransformToDataModelList(this IEnumerable<CityModel> list)
		{
			if (list != null && list.Any())
			{
				return list.Select(x => x.TransformToDataModel()).ToList();
			}
			else
			{
				return new List<City>();
			}
		}
	}
}
