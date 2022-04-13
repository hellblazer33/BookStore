namespace BookStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;

    /// <summary>
    ///  Book Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookBL bookBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public BookController(IBookBL bookBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.bookBL = bookBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }


        [Authorize(Roles = Role.Admin)]
        [HttpPost("post")]
        public IActionResult AddBook(AddBookModel book)
        {
            try
            {
                var bookDetail = this.bookBL.AddBook(book);
                if (bookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Added Sucessfully", Response = bookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Some Error Occured" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

       
        [Authorize(Roles = Role.Admin)]
        [HttpPut("Update")]
        public IActionResult UpdateBook(BookModel book)
        {
            try
            {
                var updatedBookDetail = this.bookBL.UpdateBook(book);
                if (updatedBookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Updated Sucessfully", Response = updatedBookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Some Error Occured" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("Delete")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                if (this.bookBL.DeleteBook(bookId))
                {
                    return this.Ok(new { Success = true, message = "Book Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }


        //[Authorize(Roles = Role.User)]
        [Authorize]
        [HttpGet("{bookId}/Get")]
        public IActionResult GetBookByBookId(int bookId)
        {
            try
            {
                var updatedBookDetail = this.bookBL.GetBookByBookId(bookId);
                if (updatedBookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Detail Fetched Sucessfully", Response = updatedBookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Correct Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        
        //[Authorize(Roles = Role.User)]
        //[Authorize]
        [HttpGet("redisGetAllBooks")]
        public async Task<IActionResult> GetAllBooksUsingRedisCache()
        {
            var cacheKey = "BookList";
            string serializedBookList;
            var BookList = new List<BookModel>();
            var redisBookList = await distributedCache.GetAsync(cacheKey);
            if (redisBookList != null)
            {
                serializedBookList = Encoding.UTF8.GetString(redisBookList);
                BookList = JsonConvert.DeserializeObject<List<BookModel>>(serializedBookList);
            }
            else
            {
                BookList = (List<BookModel>)bookBL.GetAllBooks();
                serializedBookList = JsonConvert.SerializeObject(BookList);
                redisBookList = Encoding.UTF8.GetBytes(serializedBookList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisBookList, options);
            }
            return Ok(BookList);
        }
    }
}