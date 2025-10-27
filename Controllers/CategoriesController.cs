using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.DTOs;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ECommerceDbContext context, ILogger<CategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            try
            {
                var categories = await _context.Categories
                    .Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        CreatedAt = c.CreatedAt,
                        ProductCount = c.Products.Count
                    })
                    .ToListAsync();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar categorias");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            try
            {
                var category = await _context.Categories
                    .Where(c => c.Id == id)
                    .Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        CreatedAt = c.CreatedAt,
                        ProductCount = c.Products.Count
                    })
                    .FirstOrDefaultAsync();

                if (category == null)
                {
                    return NotFound(new { message = $"Categoria com ID {id} não encontrada" });
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar categoria {CategoryId}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        #region Usar apenas sem o SQLite

        //// GET: api/categories/5/products
        //[HttpGet("{id}/products")]
        //public async Task<ActionResult<CategoryWithProductsDTO>> GetCategoryWithProducts(int id)
        //{
        //    try
        //    {
        //        var category = await _context.Categories
        //            .Include(c => c.Products)
        //            .Where(c => c.Id == id)
        //            .Select(c => new CategoryWithProductsDTO
        //            {
        //                Id = c.Id,
        //                Name = c.Name,
        //                Description = c.Description,
        //                CreatedAt = c.CreatedAt,
        //                Products = c.Products.Select(p => new ProductDTO
        //                {
        //                    Id = p.Id,
        //                    Name = p.Name,
        //                    Description = p.Description,
        //                    Price = p.Price,
        //                    Stock = p.Stock,
        //                    ImageUrl = p.ImageUrl,
        //                    CreatedAt = p.CreatedAt,
        //                    CategoryId = p.CategoryId,
        //                    CategoryName = c.Name
        //                }).ToList()
        //            })
        //            .FirstOrDefaultAsync();

        //        if (category == null)
        //        {
        //            return NotFound(new { message = $"Categoria com ID {id} não encontrada" });
        //        }

        //        return Ok(category);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao buscar categoria com produtos {CategoryId}", id);
        //        return StatusCode(500, "Erro interno do servidor");
        //    }
        //}

        #endregion

        // GET: api/categories/5/products
        [HttpGet("{id}/products")]
        public async Task<ActionResult<CategoryWithProductsDTO>> GetCategoryWithProducts(int id)
        {
            try
            {
                var category = await _context.Categories
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (category == null)
                    return NotFound(new { message = $"Categoria com ID {id} não encontrada" });

                var dto = new CategoryWithProductsDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    CreatedAt = category.CreatedAt,
                    Products = category.Products.Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Stock = p.Stock,
                        ImageUrl = p.ImageUrl,
                        CreatedAt = p.CreatedAt,
                        CategoryId = p.CategoryId,
                        CategoryName = category.Name
                    }).ToList()
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar categoria com produtos {CategoryId}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }


        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CreateCategoryDTO createCategoryDto)
        {
            try
            {
                // Verificar se já existe categoria com mesmo nome
                var categoryExists = await _context.Categories
                    .AnyAsync(c => c.Name.ToLower() == createCategoryDto.Name.ToLower());

                if (categoryExists)
                {
                    return BadRequest(new { message = "Já existe uma categoria com este nome" });
                }

                var category = new Category
                {
                    Name = createCategoryDto.Name,
                    Description = createCategoryDto.Description,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                var categoryDto = new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    CreatedAt = category.CreatedAt,
                    ProductCount = 0
                };

                return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, categoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar categoria");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        // PUT: api/categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDTO updateCategoryDto)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound(new { message = $"Categoria com ID {id} não encontrada" });
                }

                // Verificar se já existe outra categoria com o mesmo nome
                var categoryExists = await _context.Categories
                    .AnyAsync(c => c.Name.ToLower() == updateCategoryDto.Name.ToLower() && c.Id != id);

                if (categoryExists)
                {
                    return BadRequest(new { message = "Já existe outra categoria com este nome" });
                }

                category.Name = updateCategoryDto.Name;
                category.Description = updateCategoryDto.Description;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar categoria {CategoryId}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _context.Categories
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (category == null)
                {
                    return NotFound(new { message = $"Categoria com ID {id} não encontrada" });
                }

                // Verificar se a categoria tem produtos
                if (category.Products.Any())
                {
                    return BadRequest(new { message = "Não é possível excluir uma categoria que possui produtos" });
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar categoria {CategoryId}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
