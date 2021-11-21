using PunkApi_Data.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;

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
                context.UserFavourites.Add(newUser); 
                
                if(newBeer != null)
                {
                    context.Beers.Add(newBeer);

                    context.SaveChanges();
                    UserBeers userBeers = new UserBeers();
                    userBeers.UserFavouritesId = newUser.UserFavouritesId;
                    userBeers.BeerId = newBeer.BeerId; 
                    context.UserBeers.Add(userBeers);



                }
                
                context.SaveChanges();





                return newUser; 
            }
            catch (Exception)
            {
                throw new Exception("Failed to add new user");
            }
        }

        public static List<Beer> GetUsersFavourites(UserBeerContext context, string userid)
        {
            try
            {
                var conn = context.Database.GetDbConnection();

                List<Beer> favs = conn.Query<Beer>("[dbo].[GetUserBeers]", new { UserId = userid }, commandType: System.Data.CommandType.StoredProcedure).ToList();


                return favs;
            }
            catch (Exception)
            {
                return null;
            }
        }



    }
}
