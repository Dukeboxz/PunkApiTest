using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PunkApi_Data.Models;
using punkApi_ServerAPI.ControllerServices;
using punkApi_ServerAPI.ViewModels;
using System.Data.Entity;

namespace punkApi_ServerAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserFavourites : ControllerBase
    {

        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };

        [HttpGet]
        public ActionResult<ReturnViewModel> GetUserFavourites(string userID)
        {
            try
            {
                using(var context = new UserBeerContext())
                {
                    var ReturnView = new ReturnViewModel();

                    var userDetail = context.UserFavourites.Where(x=> x.UserID == userID).FirstOrDefault();
                    
                    
                    if(userDetail != null)
                    {
                        ReturnView.Message = "Success"; 
                        ReturnView.UserId = userDetail.UserID;
                        ReturnView.Beers = UserFavouritesService.GetUsersFavourites(context, userDetail.UserID);


                    }
                    else
                    {
                        UserFavouritesService.AddNewUser(context, userID, null);
                        ReturnView.Message = "Success";
                        ReturnView.UserId = userID;
                        ReturnView.Beers = new List<Beer>(); 

                    }

                    string json = JsonConvert.SerializeObject(ReturnView, new JsonSerializerSettings
                    {
                        ContractResolver = contractResolver,
                        Formatting = Formatting.Indented
                    });

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
            returnView.Beers = new List<Beer>();
            try
            {
                using (var context = new UserBeerContext())
                {
                    var existingUser = context.UserFavourites.Where(x=> x.UserID == userId).FirstOrDefault();

                  

                    if(existingUser != null)
                    {
                       
                        List<Beer> beers = UserFavouritesService.GetUsersFavourites(context, existingUser.UserID);

                        if(beers.Count() == 5)
                        {
                            returnView.Message = "TooMany";

                        }
                        else
                        {
                            if(!UserFavouritesService.AddBeerToUserFavs(context, existingUser.UserID, newBeer)){

                                throw new Exception("Failed to save favourite");
                            }
                            else
                            {
                                returnView.Message = "Success"; 
                            }
                        }
                        
                    }
                    else
                    {
                       UserFavouritesService.AddNewUser(context, userId, newBeer);
                        returnView.Message = "Success"; 
                    }
                   context.SaveChanges();

                }


            }catch (Exception)
            {
                returnView.Message = "Error"; 
                
               
            }

            string json = JsonConvert.SerializeObject(returnView);

            return Content(json);
        }

        [HttpPost("User")]
        public ActionResult CreateOrReturnUser(string userId)
        {
            ReturnViewModel returnView = new ReturnViewModel();
            try
            {
                using (var context = new UserBeerContext())
                {
                    var existingUser = context.UserFavourites.Where(s=> s.UserID == userId).FirstOrDefault();

                    if( existingUser != null)
                    {
                        returnView.Message = "Success";
                        returnView.UserId = existingUser.UserID; 
                        returnView.Beers = UserFavouritesService.GetUsersFavourites(context, existingUser.UserID);
                    }
                    else
                    {
                        PunkApi_Data.Models.UserFavourites newUser = UserFavouritesService.AddNewUser(context, userId);
                        returnView.Message = "Success";
                        returnView.UserId = newUser.UserID;
                        returnView.Beers = new List<Beer>(); 

                    }

                    
                   

                }
            }
            catch (Exception)
            {
                returnView.Message = "Error";
            }

            string json = JsonConvert.SerializeObject(returnView);

            return Content(json);
        }

        

       
    }
}
