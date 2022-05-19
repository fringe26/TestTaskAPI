using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DatabaseContext :DbContext
    {
        public DatabaseContext (DbContextOptions options) :base(options) //options will come from EX startup
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                    Id = 2,
                    Name = "Bahamas",
                    ShortName = "BS"
                },
                new Country
                {
                    Id = 3,
                    Name = "Russia",
                    ShortName = "RUS"
                }
                );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id=1,
                    Name="Hayat",
                    Adress="Negril",
                    CountryId = 1,
                    Rating = 4.3
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Quba Palace",
                    Adress = "Negril",
                    CountryId = 2,
                    Rating = 4.7
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Casino",
                    Adress = "Moscow",
                    CountryId = 3,
                    Rating = 4.9
                }
                );

        }
        
    }
}
