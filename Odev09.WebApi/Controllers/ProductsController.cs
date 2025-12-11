using Microsoft.AspNetCore.Mvc;
using Odev09.WebApi.Models;

namespace Odev09.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<ProductDto> _products = new List<ProductDto>()
        {
            new ProductDto { Id = 1, Name = "Kalem", Description = "Mavi tükenmez kalem", Price = 20, Stock = 100 },
            new ProductDto { Id = 2, Name = "Defter", Description = "Noktalı defter", Price = 45, Stock = 50 }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound(new { Message = "Ürün bulunamadı." });

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_products.Any(p => p.Id == model.Id))
                return BadRequest(new { Message = "Bu ID zaten mevcut!" });

            _products.Add(model);
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = _products.FirstOrDefault(p => p.Id == id);

            if (existing == null)
                return NotFound(new { Message = "Güncellenecek ürün bulunamadı." });

            existing.Name = model.Name;
            existing.Description = model.Description;
            existing.Price = model.Price;
            existing.Stock = model.Stock;

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound(new { Message = "Silinecek ürün bulunamadı." });

            _products.Remove(product);
            return Ok(new { Message = "Ürün silindi." });
        }
    }
}