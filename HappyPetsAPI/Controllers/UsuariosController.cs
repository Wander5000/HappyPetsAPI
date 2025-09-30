using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HappyPetsAPI.Context;
using HappyPetsAPI.DTOs.Usuario;

namespace HappyPetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly HappyPetsDbContext _context;
        public UsuariosController(HappyPetsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> ListarUsuarios()
        {
            return await _context.Usuarios
                .Include(u => u.RolNavigation)
                .Select(u => new UsuarioDTO
                {
                    IdUsuario = u.IdUsuario,
                    NombreUsuario = u.NombreUsuario,
                    Correo = u.Correo,
                    TipoDocumento = u.TipoDocumento,
                    NumeroDocumento = u.NumeroDocumento,
                    Direccion = u.Direccion,
                    Rol = u.RolNavigation.NombreRol,
                    Estado = u.Estado
                }).ToListAsync();
        }
    }
}
