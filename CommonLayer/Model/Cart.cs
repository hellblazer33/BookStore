namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    ///  Cart Model
    /// </summary>
    public class Cart
    {
     
        public int CartId { get; set; }

        
        public int BookId { get; set; }

        
        public int Quantity { get; set; }
    }
}