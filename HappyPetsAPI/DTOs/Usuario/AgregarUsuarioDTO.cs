using System.Collections;

namespace HappyPetsAPI.DTOs.Usuario
{
    public class AgregarUsuarioDTO
    {
        public string NombreUsuario { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string TipoDocumento { get; set; } = null!;

        public string NumeroDocumento { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public int Rol { get; set; }

        public bool Estado { get; set; }
    }
}
