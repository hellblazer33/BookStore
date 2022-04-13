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
    public class OrdersController : ControllerBase
    {
        
        private readonly IOrderBL orderBL;

      
        public OrdersController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

       
        [HttpPost("Order")]
        public IActionResult AddOrders(OrderModel ordersModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "Id").Value);
                var orderData = this.orderBL.AddOrder(ordersModel, userId);
                if (orderData != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Placed Successfully", Response = orderData });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Enter Correct BookId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

      
        [HttpGet("Get")]
        public IActionResult GetAllOrders()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "Id").Value);
                var orderData = this.orderBL.GetAllOrder(userId);
                if (orderData != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Fetched Successfully", Response = orderData });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Please Login First" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, ex.Message });
            }
        }
    }
}