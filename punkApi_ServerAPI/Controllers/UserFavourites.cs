﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PunkApi_Data.Models;
using punkApi_ServerAPI.ControllerServices;
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
                        ReturnObj.Beers = userDetail.Favourites.ToList();


                    }
                    else
                    {
                        UserFavouritesService.AddNewUser(context, userID, null);
                        ReturnObj.Message = "NoneFound";
                        ReturnObj.UserId = userID;
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
            returnView.Beers = new List<Beer>();
            try
            {
                using (var context = new UserBeerContext())
                {
                    var existingUser = context.UserFavourites.Where(x=> x.UserID == userId).FirstOrDefault();

                    if(existingUser != null)
                    {
                        if(existingUser.Favourites is null)
                        {
                            existingUser.Favourites = new List<Beer>();
                            existingUser.Favourites.Add(newBeer);
                            
                            context.UserFavourites.Update(existingUser);
                        }
                        else
                        {
                            if (existingUser.Favourites.Count == 5)
                            {
                                returnView.Message = "TooMany";
                            }
                            else
                            {
                                existingUser.Favourites.Add(newBeer);
                                context.UserFavourites.Update(existingUser);
                            }
                        }
                        
                    }
                    else
                    {
                       UserFavouritesService.AddNewUser(context, userId, newBeer); 
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

        [HttpPost("NewUser")]
        public ActionResult CreateNewUser(string userId)
        {
            ReturnViewModel returnView = new ReturnViewModel();
            try
            {
                using (var context = new UserBeerContext())
                {
                    PunkApi_Data.Models.UserFavourites newUser = UserFavouritesService.AddNewUser(context, userId);
                    returnView.Message = "Success"; 
                    returnView.UserId= newUser.UserID;
                    returnView.Beers = newUser.Favourites.ToList();

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