using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PunkApi_Data.Models
{
    public  class UserBeers
    {
        public int UserFavouritesId { get; set; }
        public UserFavourites? UserFavourites { get; set; }

        public int BeerId { get; set; }

        public Beer? Beer { get; set; }
    }
}
