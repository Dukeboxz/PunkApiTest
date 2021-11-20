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
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectsV13; Database=PunkApi;Trusted_Connection=True;MultipleActiveResultSets=true"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
           
           
        }

    }
}