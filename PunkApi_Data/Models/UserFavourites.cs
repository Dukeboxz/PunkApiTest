using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PunkApi_Data.Models
{
    public class UserFavourites
    {
        public UserFavourites()
        {
            this.Favourites = new HashSet<Beer>(); 
        }
        public int UserFavouritesId { get; set; }
        public string UserID { get; set; }

        public ICollection<Beer> Favourites { get; set; }

    }
}