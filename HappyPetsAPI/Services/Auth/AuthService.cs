using HappyPetsAPI.Context;
using HappyPetsAPI.DTOs.Auth;
using HappyPetsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HappyPetsAPI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly HappyPetsDbContext _context;

        public AuthService(IConfiguration config, HappyPetsDbContext context)
        {
            _config = config;
            _context = context;
        }

        private string GenerateToken(Usuario usuario)
        {
            var jwtKey = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
                throw new InvalidOperationException("La clave JWT no está configurada.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ResponseDTO> LoginAsync(LoginDTO request)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == request.Correo);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Correo u Contraseña Incorrectos"
                };
            }

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Ingresaste con Exito",
                Token = GenerateToken(usuario)
            };
        }

        public async Task<ResponseDTO> RegisterAsync(RegisterDTO request)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Correo == request.Correo))
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "El Correo ya esta en uso"
                };
            }
            var nuevoUsuario = new Usuario
            {
                NombreUsuario = request.NombreUsuario,
                TipoDocumento = request.TipoDocumento,
                NumeroDocumento = request.NumeroDocumento,
                Correo = request.Correo,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Direccion = request.Direccion,
                Rol = 1,
                Estado = true
            };
            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
            return new ResponseDTO
            {   
                IsSuccess = true,
                Message = "Usuario Registrado con Exito",
                Token = GenerateToken(nuevoUsuario)
            };
        }
    }
}
