namespace HappyPetsAPI.DTOs.Auth
{
    public class RegisterDTO
    {
        public string NombreUsuario { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string TipoDocumento { get; set; } = null!;

        public string NumeroDocumento { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public bool Estado { get; set; }
    }
}
