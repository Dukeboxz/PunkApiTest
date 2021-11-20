using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PunkApi_Data.Models
{
    public class UserFavourites
    {
        public int UserFavouritesId { get; set; }
        public string UserID { get; set; }

        public List<Beer> Favourites { get; set; }

    }
}