using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HappyPetsAPI.Context;
using HappyPetsAPI.Models;
using HappyPetsAPI.DTOs.Venta;

namespace HappyPetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly HappyPetsDbContext _context;

        public VentasController(HappyPetsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> ListarVentas()
        {
            return await _context.Ventas
                .Include(v => v.UsuarioNavigation)
                .Include(v => v.EstadoNavigation)
                .Select(v => new VentaDTO
                {
                    IdVenta = v.IdVenta,
                    Usuario = v.UsuarioNavigation.NombreUsuario,
                    Fecha = v.Fecha,
                    MetodoPago = v.MetodoPago,
                    Descuento = v.Descuento,
                    Total = v.Total,
                    Observaciones = v.Observaciones,
                    Estado = v.EstadoNavigation.NombreEstado
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AgregarVentaDTO>> CrearVenta(AgregarVentaDTO dto)
        {
            try
            {
                using var transaccion = await _context.Database.BeginTransactionAsync();

                var venta = new Venta
                {
                    Usuario = dto.Usuario,
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    MetodoPago = dto.MetodoPago,
                    Descuento = dto.Descuento,
                    Observaciones = dto.Observaciones,
                    Estado = dto.Estado
                };
                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                decimal total = 0;
                foreach (var detalleDto in dto.Detalles)
                {
                    var producto = await _context.Productos.FindAsync(detalleDto.Producto);
                    if (producto == null)
                    {
                        await transaccion.RollbackAsync();
                        return BadRequest($"Producto con ID {detalleDto.Producto} no encontrado.");
                    }
                    if (producto.Stock < detalleDto.Cantidad)
                    {
                        await transaccion.RollbackAsync();
                        return BadRequest($"Stock insuficiente para el producto {producto.NombreProducto}.");
                    }
                    if (detalleDto.Cantidad <= 0)
                    {
                        await transaccion.RollbackAsync();
                        return BadRequest("La cantidad debe ser mayor que cero.");
                    }

                            
                    var detalle = new DetallesVenta
                    {
                        Venta = venta.IdVenta,
                        Producto = detalleDto.Producto,
                        Cantidad = detalleDto.Cantidad,
                        PrecioUnidad= producto.PrecioUnidad,
                        Subtotal = producto.PrecioUnidad * detalleDto.Cantidad
                    };
                    total += detalle.Subtotal;
                    producto.Stock -= detalleDto.Cantidad;
                    _context.DetallesVenta.Add(detalle);
                }
                venta.Total = total - (total * dto.Descuento / 100);
                await _context.SaveChangesAsync();
                await transaccion.CommitAsync();
                return CreatedAtAction(nameof(CrearVenta), new { id = venta.IdVenta }, dto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la venta.");
            }
        }
    }
}
