using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HappyPetsAPI.Context;
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
    }
}
