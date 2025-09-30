using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HappyPetsAPI.Context;
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
    }
}
