using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_api.Models
{
     public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}