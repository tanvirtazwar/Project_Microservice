using Catalog.API.Models;
using Catalog.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogRepository _catalogRepository;

        public CatalogController(CatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _catalogRepository.GetAllAsync();

            return Ok(products);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var products = await _catalogRepository.GetByCategoryAsync(category);

            return Ok(products);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById (string id)
        {
            var product = await _catalogRepository.GetByIdAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody] Product newProduct)
        {
            await _catalogRepository.CreateAsync(newProduct);

            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product updatedProduct)
        {
            if (string.IsNullOrEmpty(updatedProduct.Id))
            {
                return BadRequest();
            }

            var book = await _catalogRepository.GetByIdAsync(updatedProduct.Id);

            if (book is null)
            {
                return NotFound();
            }

            await _catalogRepository.UpdateAsync(updatedProduct.Id, updatedProduct);

            return Ok(updatedProduct);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var deletedProduct = await _catalogRepository.GetByIdAsync(id);

            if (deletedProduct is null)
            {
                return NotFound();
            }

            await _catalogRepository.RemoveAsync(id);

            return Ok($"Product with Id: {id} deleted successfully");
        }
    }
}
