namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    
    public class AddressModel
    {
    
        public string Address { get; set; }

      
        public string City { get; set; }

      
        public string State { get; set; }

      
        public int TypeId { get; set; }

    
        public int UserId { get; set; }

        public int AddressId { get; set; }
    }
}