using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HappyPetsAPI.Context;
using HappyPetsAPI.DTOs.Producto;

namespace HappyPetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly HappyPetsDbContext _context;

        public ProductosController(HappyPetsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> ListarProductos()
        {
            return await _context.Productos
                .Include(p => p.CategoriaProductoNavigation)
                .Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto,
                    CategoriaProducto = p.CategoriaProductoNavigation.NombreCategoria,
                    PrecioUnidad = p.PrecioUnidad,
                    Stock = p.Stock,
                    Estado = p.Estado
                })
                .ToListAsync();
        }
    }
}
