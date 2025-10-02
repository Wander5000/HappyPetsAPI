using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HappyPetsAPI.Context;
using HappyPetsAPI.Models;
using HappyPetsAPI.DTOs.Estado;

namespace HappyPetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly HappyPetsDbContext _context;
        public EstadosController(HappyPetsDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoDTO>>> ListarEstados()
        {
            return await _context.Estados
                .Select(e => new EstadoDTO
                {
                    IdEstado = e.IdEstado,
                    NombreEstado = e.NombreEstado,
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AccionesEstadoDTO>> CrearCategoria(AccionesEstadoDTO dto)
        {
            var estado = new Estado
            {
                NombreEstado = dto.NombreEstado,
            };
            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarEstados), new { id = estado.IdEstado }, dto);
        }
    }
}
