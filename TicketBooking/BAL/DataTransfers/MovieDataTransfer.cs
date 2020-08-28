using System.Collections.Generic;
using System.Linq;
using TicketBooking.DAL.Models;
using TicketBooking.Enums;
using TicketBooking.Models;

namespace TicketBooking.BAL.DataTransfers
{
    public static class MovieDataTransfer
    {
		public static MovieModel TransformToViewModel(this Movie x)
		{
			return x != null ?
				new MovieModel
				{
					Id = x.Id,
					Name = x.Name,
					Genre = EnumExtension.GetEnumDescription((TicketBookingEnums.Genre) x.GenreId),
					Language = EnumExtension.GetEnumDescription((TicketBookingEnums.Language)x.LanguageId),
					Multiplex = x.Multiplex.TransformToViewModel()
				} : null;
		}

		public static Movie TransformToDataModel(this MovieModel x)
		{
			return x != null ?
				new Movie
				{
					Id = x.Id,
					Name = x.Name,
					GenreId = (int)EnumExtension.GetValueFromDescription<TicketBookingEnums.Genre>(x.Genre),
					LanguageId = (int)EnumExtension.GetValueFromDescription<TicketBookingEnums.Language>(x.Language),
					MultiplexId  = x.Multiplex.Id
				} : null;
		}

		public static IEnumerable<MovieModel> TransformToViewModelList(this IEnumerable<Movie> list)
		{
			if (list != null && list.Any())
			{
				return list.Select(x => x.TransformToViewModel()).ToList();
			}
			else
			{
				return new List<MovieModel>();
			}
		}

		public static IEnumerable<Movie> TransformToDataModelList(this IEnumerable<MovieModel> list)
		{
			if (list != null && list.Any())
			{
				return list.Select(x => x.TransformToDataModel()).ToList();
			}
			else
			{
				return new List<Movie>();
			}
		}
	}
}
