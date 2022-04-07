namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Book Model Class for Adding Book
    /// </summary>
    public class AddBookModel
    {
        
        public string BookName { get; set; }

      
        public string AuthorName { get; set; }

       
        public int Rating { get; set; }

        
        public int TotalRating { get; set; }

       
        public decimal DiscountPrice { get; set; }

        
        public decimal OriginalPrice { get; set; }

        
        public string Description { get; set; }

        
        public string BookImage { get; set; }

       
        public int BookCount { get; set; }
    }
}