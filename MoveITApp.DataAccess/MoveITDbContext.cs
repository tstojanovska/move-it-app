using Microsoft.EntityFrameworkCore;
using MoveITApp.Domain.Enums;
using MoveITApp.Domain.Models;

namespace MoveITApp.DataAccess
{
    public class MoveITDbContext : DbContext
    {
        public MoveITDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<DistanceRule> DistanceRules { get; set; }
        public DbSet<MovingObjectRule> MovingObjectRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //USER
            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.Password)
                .IsRequired();

            //PROPOSAL
            modelBuilder.Entity<Proposal>()
               .Property(x => x.CalculatedPrice)
               .IsRequired();
            modelBuilder.Entity<Proposal>()
               .Property(x => x.Distance)
               .IsRequired();
            modelBuilder.Entity<Proposal>()
               .Property(x => x.LivingAreaVolume)
               .IsRequired();

            //relation
            modelBuilder.Entity<Proposal>()
                .HasOne(x => x.User)
                .WithMany(x => x.Proposals)
                .HasForeignKey(x => x.UserId);

            //SEEDING INITIAL DATA
            modelBuilder.Entity<DistanceRule>()
                .HasData(
                new DistanceRule()
                {
                    Id = 1,
                    From = 1,
                    To = 50,
                    FixedPrice = 1000,
                    PricePerKm = 10
                },
                new DistanceRule()
                {
                    Id = 2,
                    From = 50,
                    To = 100,
                    FixedPrice = 5000,
                    PricePerKm = 8
                },
                new DistanceRule()
                {
                    Id = 3,
                    From = 100,
                    To = null,
                    FixedPrice = 10000,
                    PricePerKm = 7
                }
                );

            modelBuilder.Entity<MovingObjectRule>()
               .HasData(
               new MovingObjectRule()
               {
                   Id = 1,
                   MovingObjectType = MovingObjectType.Piano,
                   FixedPrice = 5000
               }
               );

        }
    }
}
