using Microsoft.EntityFrameworkCore;
using TicketBooking.DAL.Models;

namespace TicketBooking.DAL
{
    public partial class TicketContext : DbContext
    {
		public TicketContext(DbContextOptions<TicketContext> options): base(options)
		{ }
		public virtual DbSet<User> User { get; set; }
		public virtual DbSet<City> City { get; set; }
		public virtual DbSet<Movie> Movie { get; set; }
		public virtual DbSet<Multiplex> Multiplex { get; set; }
		public virtual DbSet<UserBooking> UserBooking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<User>(entity =>
			{
				entity.ToTable("user","public");

				entity.Property(e => e.Id)
					.HasColumnName("id").UseIdentityAlwaysColumn();

				entity.Property(e => e.Name).HasColumnName("name");

				entity.Property(e => e.Password).HasColumnName("password");

				entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
			});

			modelBuilder.Entity<City>(entity =>
			{
				entity.ToTable("city", "public");

				entity.Property(e => e.Id)
					.HasColumnName("id").UseIdentityAlwaysColumn();

				entity.Property(e => e.Name).HasColumnName("name");
			});

			modelBuilder.Entity<Multiplex>(entity =>
			{
				entity.ToTable("multiplex", "public");

				entity.Property(e => e.Id)
					.HasColumnName("id").UseIdentityAlwaysColumn();

				entity.Property(e => e.Name).HasColumnName("name");

				entity.Property(e => e.CityId).HasColumnName("city_id");

				entity.Property(e => e.TotalSeats).HasColumnName("total_seats");

				entity.HasOne(d => d.City)
					.WithMany(p => p.Multiplex)
					.HasForeignKey(d => d.CityId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_multiplex_city_id");
			});

			modelBuilder.Entity<Movie>(entity =>
			{
				entity.ToTable("movie", "public");

				entity.Property(e => e.Id)
					.HasColumnName("id").UseIdentityAlwaysColumn();

				entity.Property(e => e.Name).HasColumnName("name");

				entity.Property(e => e.GenreId).HasColumnName("genre_id");

				entity.Property(e => e.LanguageId).HasColumnName("language_id");

				entity.Property(e => e.MultiplexId).HasColumnName("multiplex_id");

				entity.HasOne(d => d.Multiplex)
					.WithMany(p => p.Movies)
					.HasForeignKey(d => d.MultiplexId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_movie_multiplex_id");
			});

			modelBuilder.Entity<UserBooking>(entity =>
			{
				entity.ToTable("user_booking", "public");

				entity.Property(e => e.Id)
					.HasColumnName("id").UseIdentityAlwaysColumn();

				entity.Property(e => e.MovieId).HasColumnName("movie_id");

				entity.Property(e => e.UserId).HasColumnName("user_id");

				entity.Property(e => e.Seats).HasColumnName("seats");

				entity.HasOne(d => d.Movie)
					.WithMany(p => p.Bookings)
					.HasForeignKey(d => d.MovieId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_user_booking_movie_id");

				entity.HasOne(d => d.User)
					.WithMany(p => p.Bookings)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_user_booking_user_id");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
