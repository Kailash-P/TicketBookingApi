using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBooking.Models.Common
{
	public interface IApplicationResponse<T>
	{
		/// <summary>
		/// Gets or sets the state of the response.
		/// </summary>
		public bool State { get; set; }

		/// <summary>
		/// Gets or sets the Data.
		/// </summary>
		public T Data { get; set; }

		/// <summary>
		/// Gets or sets the Message.
		/// </summary>
		public IEnumerable<ApplicationError> Messages { get; set; }
	}

	public class ApplicationResponse<T> : IApplicationResponse<T>
	{
		public ApplicationResponse()
		{
			State = false;
			Data = default;
			Messages = default;
		}

		public bool State { get; set; }

		/// <summary>
		/// Gets or sets the Data.
		/// </summary>
		public T Data { get; set; }

		/// <summary>
		/// Gets or sets the Message.
		/// </summary>
		public IEnumerable<ApplicationError> Messages { get; set; }
	}
}
