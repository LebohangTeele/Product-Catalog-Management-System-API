using Microsoft.AspNetCore.Mvc;
using Product_Catalog_Management_System.Models;
using Product_Catalog_Management_System.Repositories;
using Product_Catalog_Management_System.Services;
using Product_Catalog_Management_System.Extensions;

namespace Product_Catalog_Management_System.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _productRepo;
        private readonly ProductSearchEngine _searchEngine;

        public ProductController(IRepository<Product> productRepo, ProductSearchEngine searchEngine)
        {
            _productRepo = productRepo;
            _searchEngine = searchEngine;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="categoryId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] string? name, [FromQuery] Guid? categoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            IEnumerable<Product> products;

            if ((string.IsNullOrWhiteSpace(name) || name == "") && (!categoryId.HasValue || categoryId == Guid.Empty))
            {
                products = _productRepo.GetAll();
            }
            else
            {
                products = _searchEngine.Search(name ?? string.Empty, categoryId);
            }

            var pagedProducts = products
                .OrderBy(p => p)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(pagedProducts);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var product = _productRepo.GetById(id);
            if (product is null)
                return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {

            if (product is null ||
                product.Name is null or "" ||
                product.Price <= 0 ||
                product.Quantity < 0)
            {
                return BadRequest("Invalid");
            }
            var now = DateTime.UtcNow;
            var newProduct = product with
            {
                Id = Guid.NewGuid(),
                CreatedAt = now,
                UpdatedAt = now
            };
            _productRepo.Add(newProduct);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Product product)
        {
            var existing = _productRepo.GetById(id);
            if (existing is null)
                return NotFound();

            if (product is null ||
                product.Name is null or "" ||
                product.Price <= 0 ||
                product.Quantity < 0)
            {
                return BadRequest("Invalid product data.");
            }

            var updated = product with
            {
                Id = id,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = DateTime.UtcNow
            };
            _productRepo.Update(updated);
            return Ok(updated);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var existing = _productRepo.GetById(id);
            if (existing is null)
                return NotFound();
            _productRepo.Delete(id);
            return NoContent();
        }

    }
}