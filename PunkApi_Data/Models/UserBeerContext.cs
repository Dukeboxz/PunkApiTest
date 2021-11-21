using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace PunkApi_Data.Models
{
    public class UserBeerContext: DbContext
    {
      

        public DbSet<Beer> Beers { get; set; }
        public DbSet<UserFavourites> UserFavourites { get; set;}

        public DbSet<UserBeers> UserBeers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:stephenandsharon.database.windows.net,1433;Initial Catalog=Ert-Test;Persist Security Info=False;User ID=u04sjj;Password=R0ssC0unty4ever;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserBeers>().HasKey(sc => new { sc.UserFavouritesId, sc.BeerId });

            modelBuilder.Entity<UserBeers>()
                 .HasOne<Beer>(b => b.Beer)
                 .WithMany(u => u.UserBeers); 

            modelBuilder.Entity<UserBeers>()
                .HasOne<UserFavourites>(f=> f.UserFavourites)
                .WithMany(u=> u.UserBeers);
        }

    }
}