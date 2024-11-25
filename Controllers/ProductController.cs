using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using product_api.Models;
using product_api.DTOs;

namespace product_api.Controllers
{
    [ApiController]
    [Route("api/products/")]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new List<Product>()
        {
            new Product { Id = Guid.NewGuid(), Name = "Samsung Galaxy s24 ultra", Description = "short description", Price = 999, CreatedAt = DateTime.Now ,},

        };

        //get: /api/products => get all products
        [HttpGet]
        public IActionResult GetProducts([FromQuery] string searchValue = "")
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                var searchProducts = products.Where(p => p.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();

                return Ok(searchProducts);
            }
            return Ok(products);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductCreateDto productData)
        {
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = productData.Name,
                Description = productData.Description,
                CreatedAt = DateTime.UtcNow,
            };

            products.Add(newProduct);
            return Created($"/api/products/{newProduct.Id}", newProduct);

        }

        [HttpPut("{productId:guid}")]
        public IActionResult updateCategoryById(Guid productId, [FromBody] ProductUpdateDto productData)
        {

            if (productData == null)
            {
                return BadRequest("Product data is missing");
            }
            var tempProduct = products.FirstOrDefault(product => product.Id == productId);
            if (tempProduct == null)
            {
                return NotFound("product with this id does not exist");
            }
            if (!string.IsNullOrEmpty(productData.Name))
            {
                if (productData.Name.Length >= 2)
                {
                    tempProduct.Name = productData.Name;

                }
                else
                {
                    return BadRequest("product name must be atleast 2 characters long");

                }
            }

            if (!string.IsNullOrWhiteSpace(productData.Description))
            {
                tempProduct.Description = productData.Description;
            }
            return NoContent();
        }


        [HttpDelete("{productId:guid}")]
        public IActionResult DeleteCategoryById(Guid productId)
        {
            var tempProduct = products.FirstOrDefault(product => product.Id == productId);
            if (tempProduct == null)
            {
                return NotFound("product not found");
            }
            products.Remove(tempProduct);

            return NoContent();

        }
    }
}