using System.Collections;

namespace HappyPetsAPI.DTOs.Usuario
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string TipoDocumento { get; set; } = null!;

        public string NumeroDocumento { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public int Rol { get; set; }

        public BitArray Estado { get; set; } = null!;
    }
}
