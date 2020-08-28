using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TicketBooking
{
    public class Program
    {
		/// <summary>
		/// Main Method for the Program.
		/// </summary>
		public static void Main()
		{
			CreateWebHostBuilder().Build().Run();
		}

		/// <summary>
		/// Method to build the Host.
		/// </summary>
		/// <param name="args">Arguments for the host builder.</param>
		/// <returns>web host.</returns>
		public static IWebHostBuilder CreateWebHostBuilder() =>
			 WebHost.CreateDefaultBuilder()
				 .UseKestrel()
				 .UseStartup<Startup>();
	}
}
