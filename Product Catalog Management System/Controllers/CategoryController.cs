using Microsoft.AspNetCore.Mvc;
using Product_Catalog_Management_System.Models;
using Product_Catalog_Management_System.Repositories;
using Product_Catalog_Management_System.Services;

namespace Product_Catalog_Management_System.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepo;

        public CategoryController(IRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var categories = _categoryRepo.GetAll().ToList();
            return Ok(categories);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("tree")]
        public IActionResult GetTree()
        {
            var categories = _categoryRepo.GetAll();
            var tree = CategoryTreeBuilder.BuildTree(categories);
            return Ok(tree);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            if (category is null || string.IsNullOrWhiteSpace(category.Name))
                return BadRequest("Invalid.");

            var newCategory = category with
            {
                Id = Guid.NewGuid()
            };
            _categoryRepo.Add(newCategory);
            return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
        }
    }
}