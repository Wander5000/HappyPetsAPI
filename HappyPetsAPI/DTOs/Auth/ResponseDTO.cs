namespace HappyPetsAPI.DTOs.Auth
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public string? Token { get; set; }
    }
}
