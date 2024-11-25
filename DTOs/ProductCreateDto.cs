using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_api.DTOs
{
    public class ProductCreateDto
    {
        public string Name {get; set;}
        public string Description {get; set;} = string.Empty;
    }
}