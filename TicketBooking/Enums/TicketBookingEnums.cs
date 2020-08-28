using System.ComponentModel;

namespace TicketBooking.Enums
{
    public class TicketBookingEnums
    {
        public enum Language
        {
            [Description("Hindi")]
            Hindi = 1,
            [Description("Kannada")]
            Kannada = 2,
            [Description("English")]
            English = 3
        }

        public enum Genre
        {
            [Description("Action")]
            Action = 1,
            [Description("Drama")]
            Drama = 2,
            [Description("Comedy")]
            Comedy = 3
        }
    }
}
