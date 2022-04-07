namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Book Model Class for Update And Fetching Records
    /// </summary>
    public class BookModel
    {
       
        public int BookId { get; set; }

       
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