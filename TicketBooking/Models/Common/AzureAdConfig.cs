using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBooking.Models.Common
{
    public class AzureAdConfig
    {
        /// <summary>
        /// Gets or sets the Instance.
        /// </summary>
        public string Instance { get; set; }

        /// <summary>
        /// Gets or sets the Domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the TenantId.
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Gets or sets the ClientId.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the Graph Resource.
        /// </summary>
        public string GraphResource { get; set; }

        /// <summary>
        /// Gets or sets the Graph Resource EndPoint.
        /// </summary>
        public string GraphResourceEndPoint { get; set; }

        /// <summary>
        /// Gets or sets the Client secret.
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
