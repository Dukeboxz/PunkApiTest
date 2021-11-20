using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PunkApi_Data.Models;
using punkApi_ServerAPI.ViewModels;

namespace punkApi_ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFavourites : ControllerBase
    {

        [HttpGet]
        public ActionResult<ReturnViewModel> GetUserFavourites(string userID)
        {
            try
            {
                using(var context = new UserBeerContext())
                {
                    var ReturnObj = new ReturnViewModel();

                    var userDetail = context.UserFavourites.Where(x=> x.UserID == userID).FirstOrDefault();
                    
                    if(userDetail != null)
                    {
                        ReturnObj.Message = "Success"; 
                        ReturnObj.UserId = userDetail.UserID;
                        ReturnObj.Beers = userDetail.Favourites;


                    }
                    else
                    {

                        ReturnObj.Message = "NoneFound";
                        ReturnObj.UserId = userDetail.UserID;
                        ReturnObj.Beers = new List<Beer>(); 

                    }

                    string json = JsonConvert.SerializeObject(ReturnObj);

                    return Content(json);



                }

            }
            catch (Exception)
            {
                return BadRequest(); 
            }
        }

        [HttpPost]
        public ActionResult<ReturnViewModel> AddFavourite(string userId, Beer newBeer)
        {
            ReturnViewModel returnView = new ReturnViewModel();
            returnView.UserId = userId; 
            try
            {
                using (var context = new UserBeerContext())
                {
                    var existingUser = context.UserFavourites.Where(x=> x.UserID == userId).FirstOrDefault();

                    if(existingUser != null)
                    {

                    }
                    else
                    {
                        AddNewUser(context, userId, newBeer); 
                    }
                }


            }catch (Exception)
            {
                returnView.Message = "Error"; 
                returnView.Beers = new List<Beer>();
               
            }

            string json = JsonConvert.SerializeObject(returnView);

            return Content(json);
        }

        public void AddNewUser(UserBeerContext context, string userid, Beer newBeer)
        {
            try
            {
                PunkApi_Data.Models.UserFavourites newUser = new PunkApi_Data.Models.UserFavourites();
                newUser.UserID = userid;
                newUser.Favourites = new List<Beer>() { newBeer };

                context.UserFavourites.Add(newUser);
            }
            catch (Exception)
            {
                throw new Exception("Failed to add new user");
            }
        }
    }
}
