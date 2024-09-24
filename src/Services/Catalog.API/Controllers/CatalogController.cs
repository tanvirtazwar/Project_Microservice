using Catalog.API.Models;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository catalogRepository;

        public CatalogController(ICatalogRepository catalogRepository)
        {
            this.catalogRepository = catalogRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 10)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await catalogRepository.GetAllAsync();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCategory(string? category)
        {
            try
            {
                var products = await catalogRepository.GetByCategoryAsync(category!);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById (string? id)
        {
            try
            {
                var product = await catalogRepository.GetByIdAsync(id!);

                if (product is null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody] Product newProduct)
        {
            try
            {
                newProduct.Id = ObjectId.GenerateNewId().ToString();
                await catalogRepository.CreateAsync(newProduct);

                return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product updatedProduct)
        {
            try
            {
                var book = await catalogRepository.GetByIdAsync(updatedProduct.Id!);

                if (book is null)
                {
                    return NotFound();
                }

                await catalogRepository.UpdateAsync(updatedProduct.Id!, updatedProduct);

                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                var deletedProduct = await catalogRepository.GetByIdAsync(id);

                if (deletedProduct is null)
                {
                    return NotFound();
                }

                await catalogRepository.RemoveAsync(id);

                return Ok($"Product with Id: {id} deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
