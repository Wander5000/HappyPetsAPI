using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HappyPetsAPI.Context;
using HappyPetsAPI.Models;
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

        [HttpPost]
        public async Task<ActionResult<AgregarUsuarioDTO>> AgregarUsuario(AgregarUsuarioDTO dto)
        {
            var usuario = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                Correo = dto.Correo,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                TipoDocumento = dto.TipoDocumento,
                NumeroDocumento = dto.NumeroDocumento,
                Direccion = dto.Direccion,
                Rol = dto.Rol,
                Estado = dto.Estado
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarUsuarios), new { id = usuario.IdUsuario }, dto);
        }
    }
}
