using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBooking.Models.Common
{
    public class ErrorMessages
    {
        public const string Name = "User Name is mandatory.";
        public const string MinimumBooking = "Atleast 1 seat must be selected.";
        public const string MaximumBooking = "Only 5 seats allowed per booking.";
        public const string NoSeatsAvailable = "No seats available.";
        public const string MovieName = "Movie Name is mandatory.";        
		public const string Invalid = "Id doesn't Exist";
		public const string ID = "Invalid Id";
		public const string AlreadyExists = "Data already exists";
    }
}
