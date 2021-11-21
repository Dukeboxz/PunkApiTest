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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectsV13; Database=PunkApiV2;Trusted_Connection=True;MultipleActiveResultSets=true"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Beer>()
                 .HasMany<UserFavourites>(f => f.UserFavourites)
                 .WithMany(c => c.Favourites);
                
           
        }

    }
}