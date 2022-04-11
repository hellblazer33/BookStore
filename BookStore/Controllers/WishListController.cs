namespace BookStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    
    [Route("api/[controller]")]
    [Authorize(Roles = Role.User)]
    [ApiController]
    public class WishListController : ControllerBase
    {
        
        private readonly IWishListBL wishlistBL;

      
        public WishListController(IWishListBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        
        [HttpPost("Add")]
        public IActionResult AddInWishlist(int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.wishlistBL.AddInWishlist(bookId, userId);
                if (result.Equals("Book added in Wishlist"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

      
        [HttpDelete("Delete")]
        public IActionResult DeleteFromWishlist(int wishlistId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.wishlistBL.DeleteFromWishlist(userId, wishlistId))
                {
                    return this.Ok(new { Status = true, Message = "Deleted From Wishlist" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Some Error Occured" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpGet("{UserId}/ Get")]
        public IActionResult GetCart()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var wishData = this.wishlistBL.GetAllFromWishlist(userId);
                if (wishData != null)
                {
                    return this.Ok(new { success = true, message = "All Wishlist Data Fetched Successfully ", response = wishData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User Id is Wrong" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
    }
}