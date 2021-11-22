using PunkApi_Data.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace punkApi_ServerAPI.ControllerServices
{
    public class UserFavouritesService
    {
        /// <summary>
        /// Adds to new user to database.  Assumes that validity of user name has been tested in UI
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// If the suer is not ergistered by has still logged in and add favourites this method will create the user and add favourite to user
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userid"></param>
        /// <param name="newBeer"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Gets users favourites from database through calling a stored procedure
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds beer to user favourites.  If beer is not in DB will also add new beer to DB
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <param name="newBeer"></param>
        /// <returns></returns>
        public static bool AddBeerToUserFavs(UserBeerContext context, string userId, Beer newBeer)
        {

            try
            {
                Beer beerExist = context.Beers.Where(x => x.ApiId == newBeer.ApiId).FirstOrDefault();
                UserFavourites user = context.UserFavourites.Where(x => x.UserID == userId).FirstOrDefault();

                if (beerExist == null)
                {

                    context.Beers.Add(newBeer);
                    context.SaveChanges();
                    UserBeers ub = new UserBeers();
                    ub.UserFavouritesId = user.UserFavouritesId;
                    ub.BeerId = newBeer.BeerId;
                    context.Add(ub);
                }
                else
                {
                    context.Beers.Add(newBeer);
                    UserBeers ub = new UserBeers();
                    ub.UserFavouritesId = user.UserFavouritesId;
                    ub.BeerId = beerExist.BeerId;
                    context.Add(ub);
                }

                context.SaveChanges();

                return true; 
            }
            catch (Exception)
            {
                return false; 
            }
           
        }



    }
}
