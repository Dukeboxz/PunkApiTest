using PunkApi_Data.Models;

namespace punkApi_ServerAPI.ControllerServices
{
    public class UserFavouritesService
    {
        public static UserFavourites AddNewUser(UserBeerContext context, string userid)
        {
            try
            {
                PunkApi_Data.Models.UserFavourites newUser = new PunkApi_Data.Models.UserFavourites();
                newUser.UserID = userid;
                newUser.Favourites = new List<Beer>();
                

                context.UserFavourites.Add(newUser);
                context.SaveChanges();
                return newUser;
            }
            catch (Exception)
            {
                throw new Exception("Failed to add new user");
            }
        }

        public static UserFavourites AddNewUser(UserBeerContext context, string userid, Beer newBeer = null)
        {
            try
            {
                PunkApi_Data.Models.UserFavourites newUser = new PunkApi_Data.Models.UserFavourites();
                newUser.UserID = userid;
                newUser.Favourites = new List<Beer>();
                if(newBeer != null)
                {
                    newUser.Favourites.Add(newBeer);
                }

                context.UserFavourites.Add(newUser);

                return newUser; 
            }
            catch (Exception)
            {
                throw new Exception("Failed to add new user");
            }
        }

    }
}
