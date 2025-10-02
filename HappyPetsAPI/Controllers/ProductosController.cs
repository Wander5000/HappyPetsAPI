using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HappyPetsAPI.Context;
using HappyPetsAPI.Models;
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
                .Include(p => p.Imagenes)
                .Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto,
                    CategoriaProducto = p.CategoriaProductoNavigation.NombreCategoria,
                    PrecioUnidad = p.PrecioUnidad,
                    Stock = p.Stock,
                    Estado = p.Estado,
                    Imagenes = p.Imagenes.Select(i => new ImagenDTO
                    {
                        IdImagen = i.IdImagen,
                        Url = i.Url
                    }).ToList()
                })
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AccionesProductoDTO>> CrearProducto(AccionesProductoDTO dto)
        {
            var producto = new Producto
            {
                NombreProducto = dto.NombreProducto,
                CategoriaProducto = dto.CategoriaProducto,
                PrecioUnidad = dto.PrecioUnidad,
                Stock = dto.Stock,
                Estado = dto.Estado,
            };
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            if (dto.Imagenes != null && dto.Imagenes.Count > 0)
            {
                foreach (var imgDto in dto.Imagenes)
                {
                    var imagen = new Imagen
                    {
                        Url = imgDto.Url,
                        Producto = producto.IdProducto
                    };
                    _context.Imagenes.Add(imagen);
                }
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(ListarProductos), new { id = producto.IdProducto }, producto);
        }
    }
}
