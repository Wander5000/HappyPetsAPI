using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HappyPetsAPI.Context;
using HappyPetsAPI.Models;
using HappyPetsAPI.DTOs.Rol;

namespace HappyPetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly HappyPetsDbContext _context;

        public RolesController(HappyPetsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async  Task<ActionResult<IEnumerable<RolDTO>>> ListarRoles()
        {
            return await _context.Roles
                .Select(r => new RolDTO
                {
                    IdRol = r.IdRol,
                    NombreRol = r.NombreRol,
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AccionesRolDTO>> CrearRol(AccionesRolDTO dto)
        {
            var rol = new Rol
            {
                NombreRol = dto.NombreRol,
            };
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarRoles), new { id = rol.IdRol }, dto);
        }
    }
}
