using System;
using System.Collections.Generic;
using System.Text;

namespace Schema.Models
{
    public class ActivePromotion
    {
        // comma seperated 
        public string SKUIds { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }
    }
}
