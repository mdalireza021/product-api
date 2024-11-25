using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace product_api.DTOs
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Product name is Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "product name must be atleast 2 character long")]
        
        public string Name {get; set;}
        public string Description {get; set;} = string.Empty;
    }
}