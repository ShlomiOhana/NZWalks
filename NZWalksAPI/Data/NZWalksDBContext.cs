using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDBContext : DbContext
    {
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed difficulties data
            // Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("879a80ee-ebac-4d4a-855a-641fb5f48c8d"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("c88fb339-4ff0-45b7-befc-99daf87581ab"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("fcd7ddca-ea32-470f-bae5-c322fdc1490c"),
                    Name = "Hard"
                }
            };
            // Seed difficulties to DB
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed Regions data
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("4c3c5e98-22e8-47fe-8b97-8843a0bc91a5"),
                    Code = "AKL",
                    Name = "Auckland",
                    RegionImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fen.wikipedia.org%2Fwiki%2FAuckland&psig=AOvVaw2aBP2XnJ0K4ou_VGMhxGUx&ust=1720605944907000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCLCNxubamYcDFQAAAAAdAAAAABAE"
                },
                new Region()
                {
                    Id = Guid.Parse("75750007-efbd-46c2-8763-b193462e1390"),
                    Code = "NSN",
                    Name = "Nelson",
                    RegionImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.istockphoto.com%2Fphotos%2Fnelson-new-zealand&psig=AOvVaw0fJynlCYXwXyVVyR9zy6SD&ust=1720606022656000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCMj_3ojbmYcDFQAAAAAdAAAAABAE"
                },
                new Region()
                {
                    Id = Guid.Parse("7a2a6abe-62f6-42b2-950b-de1d6f461efa"),
                    Code = "STL",
                    Name = "Southland",
                    RegionImageUrl = null
                }
            };
            // Seed regions to DB
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
