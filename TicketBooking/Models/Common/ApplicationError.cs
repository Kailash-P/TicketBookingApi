using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBooking.Models.Common
{
	public class ApplicationError
	{
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
	}
}
